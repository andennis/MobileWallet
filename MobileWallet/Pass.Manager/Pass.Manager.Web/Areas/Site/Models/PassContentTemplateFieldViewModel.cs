using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassContentTemplateFieldViewModelValidator))]
    public class PassContentTemplateFieldViewModel : BaseViewModel
    {
        public override string DisplayName { get { return Resources.Resources.PassContentTemplateField; } }
        public override int EntityId
        {
            get { return PassContentTemplateFieldId; }
        }

        public int PassContentTemplateFieldId { get; set; }
        public PassContentFieldKind FieldKind { get; set; }
        public string AttributedValue { get; set; }
        public string ChangeMessage { get; set; }
        public TextAlignment? TextAlignment { get; set; }

        private string _name;
        public string Name
        {
            get { return IsStatic ? PassContentTemplateField.StaticFieldPrefix + PassContentTemplateFieldId : _name; }
            set { _name = value; }
        }
        public string Label { get; set; }
        public string Value { get; set; }

        public string DefaultLabel { get { return Label; } }
        public string DefaultValue { get { return Value; } }

        public bool IsStatic { get { return !PassProjectFieldId.HasValue; } }

        public int? PassProjectFieldId { get; set; }
        public int PassContentTemplateId { get; set; }
        
        public IEnumerable<SelectListItem> PassProjectFields { get; set; }
    }
}