using Common.Repository;

namespace Pass.Container.Repository.Core.Entities
{
    public sealed class PassFieldValue : EntityVersionable
    {
        public int PassFieldValueId { get; set; }
        public int PassFieldId { get; set; }
        public int PassId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public Pass Pass { get; set; }
        public PassField PassField { get; set; }
    }
}
