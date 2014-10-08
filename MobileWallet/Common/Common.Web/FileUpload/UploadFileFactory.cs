
namespace Common.Web.FileUpload
{
    public class UploadFileFactory
    {
        public UploadFileBuilder Add()
        {
            return new UploadFileBuilder();
        }
    }
}
