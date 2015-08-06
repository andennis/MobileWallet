using System;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassContentFieldViewModelValidator))]
    public class PassContentFieldViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Content Field"; } }
        public override int EntityId
        {
            get { return PassContentFieldId.HasValue ? PassContentFieldId.Value : 0; }
        }

        public int PassContentId { get; set; }
        public int PassProjectFieldId { get; set; }
        public string FieldName { get; set; }
        public PassContentFieldKind FieldKind { get; set; }
        public int? PassContentFieldId { get; set; }
        public string FieldLabel { get; set; }
        public string FieldValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}