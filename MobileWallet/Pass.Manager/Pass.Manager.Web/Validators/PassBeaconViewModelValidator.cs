﻿using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassBeaconViewModelValidator : AbstractValidator<PassBeaconViewModel>
    {
        public PassBeaconViewModelValidator()
        {
            RuleFor(x => x.ProximityUuid).NotEmpty();
            RuleFor(x => x.Major).GreaterThanOrEqualTo(ushort.MinValue).LessThan(ushort.MaxValue).When(x => x.Major != null);
            RuleFor(x => x.Minor).GreaterThanOrEqualTo(ushort.MinValue).LessThan(ushort.MaxValue).When(x => x.Minor != null);
        }
    }
}