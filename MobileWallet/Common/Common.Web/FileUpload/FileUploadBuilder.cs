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

        private readonly UploadAsyncSettings _asyncSettings = new UploadAsyncSettings();
        private readonly UploadAsyncSettingsBuilder _asyncSettingsBuilder;
        private readonly FileUploadEventBuilder _fileUploadEventBuilder;
        private readonly IDictionary<string, object> _events = new Dictionary<string, object>();

        public FileUploadBuilder()
        {
            _fileUploadEventBuilder = new FileUploadEventBuilder(_events);
            _asyncSettingsBuilder = new UploadAsyncSettingsBuilder(_asyncSettings);
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
            configurator(_asyncSettingsBuilder);
            return this;
        }

        public FileUploadBuilder Events(Action<FileUploadEventBuilder> clientEventsAction)
        {
            clientEventsAction(_fileUploadEventBuilder);
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
            inputTag.GenerateId("files");
            inputTag.Attributes.Add("name", _name);
            inputTag.Attributes.Add("type", "file");
            sb.AppendLine(inputTag.ToString());

            var settings = new
            {
                enabled = _enable,
                multiple = _multiple,
                showFileList = _showFileList
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
