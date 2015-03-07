using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassContentTemplateFieldViewModelValidator))]
    public class PassContentTemplateFieldViewModel : BaseViewModel
    {
        public int PassContentTemplateFieldId { get; set; }
        public PassContentFieldKind FieldKind { get; set; }
        public string AttributedValue { get; set; }
        public string ChangeMessage { get; set; }
        public string Label { get; set; }
        public TextAlignment? TextAlignment { get; set; }

        public int PassProjectFieldId { get; set; }
        public int PassContentTemplateId { get; set; }

        public IEnumerable<SelectListItem> PassProjectFields { get; set; }
    }
}