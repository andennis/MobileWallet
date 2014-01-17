using System.ComponentModel.DataAnnotations;

namespace Pass.Processing.Web.Models
{
    public class DevicePushToken
    {
        [Required]
        public string PushToken { get; set; }
    }
}