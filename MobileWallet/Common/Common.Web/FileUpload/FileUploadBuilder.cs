using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web.FileUpload
{
    public class FileUploadBuilder : IHtmlString
    {
        private string _name;
        private bool _enable = true;
        private bool _multiple;
        private bool _showFileList;
        private string _templateId;

        private UploadAsyncSettings _asyncSettings;
        private readonly FileUploadEventBuilder _fileUploadEventBuilder;
        private readonly UploadFileFactory _uploadFileFactory = new UploadFileFactory();
        private readonly IDictionary<string, object> _events = new Dictionary<string, object>();

        public FileUploadBuilder()
        {
            _fileUploadEventBuilder = new FileUploadEventBuilder(_events);
        }

        public FileUploadBuilder Name(string name)
        {
            _name = name;
            return this;
        }
        public FileUploadBuilder Enable(bool value)
        {
            _enable = value;
            return this;
        }
        public FileUploadBuilder Multiple(bool value)
        {
            _multiple = value;
            return this;
        }
        public FileUploadBuilder ShowFileList(bool value)
        {
            _showFileList = value;
            return this;
        }

        public FileUploadBuilder Async(Action<UploadAsyncSettingsBuilder> configurator)
        {
            if (_asyncSettings == null)
                _asyncSettings = new UploadAsyncSettings();

            configurator(new UploadAsyncSettingsBuilder(_asyncSettings));
            return this;
        }

        public FileUploadBuilder Events(Action<FileUploadEventBuilder> clientEventsAction)
        {
            clientEventsAction(_fileUploadEventBuilder);
            return this;
        }

        public FileUploadBuilder TemplateId(string templateId)
        {
            _templateId = templateId;
            return this;
        }

        public FileUploadBuilder Files(Action<UploadFileFactory> configurator)
        {
            configurator(_uploadFileFactory);
            return this;
        }

        public string ToHtmlString()
        {
            return RenderFileUpload();
        }

        private string RenderFileUpload()
        {
            var sb = new StringBuilder();
            var mainTag = new TagBuilder("div");
            mainTag.GenerateId(_name);
            sb.AppendLine(mainTag.ToString());
            sb.AppendLine(GetInitializationScript());

            return sb.ToString();
        }

        private string GetInitializationScript()
        {
            var sb = new StringBuilder();
            var inputTag = new TagBuilder("input");
            inputTag.GenerateId(_name);
            //inputTag.Attributes.Add("name", _name);
            inputTag.Attributes.Add("type", "file");
            sb.AppendLine(inputTag.ToString());

            var settings = new
            {
                enabled = _enable,
                multiple = _multiple,
                showFileList = _showFileList,
                //template = kendo.template($('#fileTemplate').html())
                async = (_asyncSettings != null)
                        ? new
                            {
                                autoUpload = _asyncSettings.AutoUpload,
                                batch = _asyncSettings.Batch,
                                removeField = _asyncSettings.RemoveField,
                                saveField = _asyncSettings.SaveField,
                                saveUrl = _asyncSettings.Save.Url,
                                removeUrl = _asyncSettings.Remove.Url
                            }
                        : null
            };

            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml =  @"$(document).ready(function () {
                $('#" + _name + @"').kendoUpload("+settings.ObjectToJson()+@");
            });";

            sb.AppendLine(scriptTag.ToString());

            return sb.ToString();
        }
    }
}
