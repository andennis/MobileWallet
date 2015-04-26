using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContentField : EntityVersionable
    {
        public int PassContentFieldId { get; set; }
        
        public int PassProjectFieldId { get; set; }
        public PassProjectField PassProjectField { get; set; }

        public int PassContentId { get; set; }
        public PassContent PassContent { get; set; }

        public string FieldLabel { get; set; }
        public string FieldValue { get; set; }
    }
}
