using System.ComponentModel.DataAnnotations;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Models
{
    public class LoginViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Login"; } }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        //public bool RememberMe { get; set; }
    }
}
