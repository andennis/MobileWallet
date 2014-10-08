using System.Web.Mvc;

namespace Common.Web.FileUpload
{
    public static class FileUploadExtension
    {
        public static FileUploadBuilder FileUpload(this HtmlHelper html)
        {
            return new FileUploadBuilder();
        }
    }
}
