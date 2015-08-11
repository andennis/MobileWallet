using System;

namespace Pass.Manager.Core.Entities
{
    public class PassContentFieldView
    {
        public int PassContentId { get; set; }
        public int PassProjectFieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldKindIds { get; set; }
        public int? PassContentFieldId { get; set; }
        public string FieldLabel { get; set; }
        public string FieldValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
