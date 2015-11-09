using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
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

        public string FieldKindIds { get; set; }
        //public IEnumerable<PassContentFieldKind> FieldKinds { get; set; }
        public string FieldKindsAsString
        {
            get
            {
                if (FieldKindIds == null)
                    return null;

                IEnumerable<PassContentFieldKind> fieldKinds = FieldKindIds.ConvertToInts().Select(x => (PassContentFieldKind)x);
                return string.Join(", ", fieldKinds);
            }
        }

        public int? PassContentFieldId { get; set; }
        public string FieldLabel { get; set; }
        public string FieldValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}