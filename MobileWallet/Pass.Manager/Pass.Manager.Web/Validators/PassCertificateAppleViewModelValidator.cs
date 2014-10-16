using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassCertificateAppleViewModelValidator : AbstractValidator<PassCertificateAppleViewModel>
    {
        public PassCertificateAppleViewModelValidator()
        {
            RuleFor(x => x.PassTypeId).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
        }
    }
}