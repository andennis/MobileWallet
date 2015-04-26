namespace Pass.Manager.Core.Entities
{
    public class PassContentFieldView
    {
        public int PassContentFieldId { get; set; }
        public int PassProjectFieldId { get; set; }
        public int PassContentId { get; set; }
        public string FieldLabel { get; set; }
        public string FieldValue { get; set; }
        public string FieldName { get; set; }
        public PassContentFieldKind? FieldKind { get; set; }
    }
}
