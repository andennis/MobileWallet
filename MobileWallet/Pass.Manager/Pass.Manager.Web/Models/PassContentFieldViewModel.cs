using System;
using System.Collections.Generic;
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

        public IEnumerable<PassContentFieldKind> FieldKinds { get; set; }
        public string FieldKindsAsString
        {
            get
            {
                if (FieldKinds == null)
                    return null;

                return string.Join(", ", FieldKinds);
            }
        }

        public int? PassContentFieldId { get; set; }
        public string FieldLabel { get; set; }
        public string FieldValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}