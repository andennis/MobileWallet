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
        private string _name;
        private readonly FileUploadEventBuilder _fileUploadEventBuilder;
        private readonly IDictionary<string, object> _events;

        public FileUploadBuilder()
        {
            _events = new Dictionary<string, object>();
            _fileUploadEventBuilder = new FileUploadEventBuilder(_events);
        }

        public FileUploadBuilder Name(string name)
        {
            _name = name;
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
           string test = @"<form method='post'action='/kendo-ui/upload/submit'>
                <div class='demo-section k-header'>
                    <input name='files' id='files' type='file' />
                    <input type='submit' value='Submit' class='k-button' />
                </div>
            </form>";

            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml = test + @"$(document).ready(function () {
                $('#" + _name + @"').kendoUpload();});";

            return scriptTag.ToString();
        }
    }
}
