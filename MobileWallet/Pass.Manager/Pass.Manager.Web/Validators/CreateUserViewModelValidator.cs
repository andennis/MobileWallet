using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class CreateUserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public CreateUserViewModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("'User Name' should not be empty'");
            RuleFor(x => x.Password).NotEmpty().WithMessage("'Password' should not be empty'");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Must((model, confirmPassword) => confirmPassword == model.Password)
                                                       .WithMessage("'Confirm Password' must be the same as 'Password'");
        }
    }
}