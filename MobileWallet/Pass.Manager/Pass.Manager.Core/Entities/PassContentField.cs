using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContentField : EntityVersionable
    {
        public int PassContentFieldId { get; set; }
        public int PassContentId { get; set; }
        public int PassFieldId { get; set; }
        public PassContentFieldKind FieldKind { get; set; }
        public string AttributedValue { get; set; }
        public string ChangeMessage { get; set; }
        public string Label { get; set; }
        public TextAlignment TextAlignment { get; set; }
        public PassField PassField { get; set; }
        public PassContent PassContent { get; set; }
    }
}
