namespace Common.Web.Controls.FileUpload
{
    public class UploadFileFactory
    {
        public UploadFileBuilder Add()
        {
            return new UploadFileBuilder();
        }
    }
}
