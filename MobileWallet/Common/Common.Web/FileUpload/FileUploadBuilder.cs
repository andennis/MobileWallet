using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Common.Web.FileUpload
{
    public class FileUploadBuilder : IHtmlString
    {
        private FileUploadConfig _fileUploadConfig;
        private readonly FileUploadEventBuilder _fileUploadEventBuilder;
        private readonly IDictionary<string, object> _events;

        public FileUploadBuilder()
        {
            _fileUploadConfig = new FileUploadConfig();
            _events = new Dictionary<string, object>();
            _fileUploadEventBuilder = new FileUploadEventBuilder(_events);
        }

        public FileUploadBuilder Name(string name)
        {
            _fileUploadConfig.Name = name;
            return this;
        }

        public FileUploadBuilder SaveAction(string saveUrl)
        {
            _fileUploadConfig.SaveAction = saveUrl;
            return this;
        }

         public FileUploadBuilder RemoveAction(string removeUrl)
        {
            _fileUploadConfig.RemoveAction = removeUrl;
            return this;
        }

         public FileUploadBuilder AutoUpload(bool autoUpload)
        {
            _fileUploadConfig.AutoUpload = autoUpload;
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
            mainTag.GenerateId(_fileUploadConfig.Name);
            sb.AppendLine(mainTag.ToString());
            sb.AppendLine(GetInitializationScript());

            return sb.ToString();
        }

        private string GetInitializationScript()
        {
            var sb = new StringBuilder();
            var inputTag = new TagBuilder("input");
            inputTag.GenerateId("files");
            inputTag.Attributes.Add("name", _fileUploadConfig.Name);
            inputTag.Attributes.Add("type", "file");
            sb.AppendLine(inputTag.ToString());
           
            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml =  @"$(document).ready(function () {
                $('#" + _fileUploadConfig.Name + @"').kendoUpload({ 
                    async: {
                            saveUrl: '"+ _fileUploadConfig.SaveAction + @"',
                            removeUrl: '" + _fileUploadConfig.RemoveAction + @"',
                            autoUpload: true
                    }
                  });});";

            sb.AppendLine(scriptTag.ToString());

            return sb.ToString();
        }
    }
}
