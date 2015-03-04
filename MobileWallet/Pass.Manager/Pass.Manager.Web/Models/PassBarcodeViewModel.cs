﻿using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassBarcodeViewModelValidator))]
    public class PassBarcodeViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Barcode"; } }
        public override int EntityId
        {
            get { return PassContentTemplateId; }
        }

        public int PassContentTemplateId { get; set; }

        public string AltText { get; set; }
        public PassBarcodeFormat? Format { get; set; }
        public string Message { get; set; }
        public string MessageEncoding { get; set; }
    }
}