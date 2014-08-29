using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(CreateUserViewModel))]
    public class CreateUserViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Create user"; } }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}