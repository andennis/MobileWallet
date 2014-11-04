using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContentTemplateField : EntityVersionable
    {
        public int PassContentTemplateFieldId { get; set; }
        public PassContentFieldKind FieldKind { get; set; }
        public string AttributedValue { get; set; }
        public string ChangeMessage { get; set; }
        public string Label { get; set; }
        public TextAlignment? TextAlignment { get; set; }

        public int PassProjectFieldId { get; set; }
        public PassProjectField PassProjectField { get; set; }

        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }
    }
}
