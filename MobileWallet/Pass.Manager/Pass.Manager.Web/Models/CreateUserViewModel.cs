using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Models
{
    public class CreateUserViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Create user"; } }
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}