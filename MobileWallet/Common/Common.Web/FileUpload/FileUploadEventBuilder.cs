using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Web.FileUpload
{
    public class FileUploadEventBuilder
    {
         private readonly IDictionary<string, object> _events;

         public FileUploadEventBuilder(IDictionary<string, object> events)
        {
            _events = events;
        }

        public FileUploadEventBuilder Upload(string handler)
        {
            _events.Add("upload", handler);
            return this;
        }
        public FileUploadEventBuilder Success(string handler)
        {
            _events.Add("success", handler);
            return this;
        }
        public FileUploadEventBuilder Select(string handler)
        {
            _events.Add("select", handler);
            return this;
        }
        public FileUploadEventBuilder Error(string handler)
        {
            _events.Add("error", handler);
            return this;
        }

        public FileUploadEventBuilder Complete(string handler)
        {
            _events.Add("complete", handler);
            return this;
        }
        public FileUploadEventBuilder Cancel(string handler)
        {
            _events.Add("cancel", handler);
            return this;
        }
        public FileUploadEventBuilder Remove(string handler)
        {
            _events.Add("remove", handler);
            return this;
        }
        public FileUploadEventBuilder Progress(string handler)
        {
            _events.Add("progress", handler);
            return this;
        }
    }
}
