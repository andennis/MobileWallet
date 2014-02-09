
using Common.Repository;

namespace Pass.Container.Core.Entities
{
    public sealed class PassField : EntityVersionable
    {
        public int PassFieldId { get; set; }
        public int PassTemplateId { get; set; }
        public string Name { get; set; }
        public PassTemplate Template { get; set; }
    }
}
