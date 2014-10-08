
namespace Common.Web.FileUpload
{
    public class UploadAsyncSettings
    {
        public Navigatable Save { get; set; }
        public string SaveField { get; set; }
        public Navigatable Remove { get; set; }
        public string RemoveField { get; set; }
        public bool? AutoUpload { get; set; }
        public bool? Batch { get; set; }
        public bool? WithCredentials { get; set; }
    }
}
