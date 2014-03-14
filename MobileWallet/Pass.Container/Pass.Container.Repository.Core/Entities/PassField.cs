using System.Collections.Generic;
using Common.Repository;

namespace Pass.Container.Repository.Core.Entities
{
    public sealed class PassField : EntityVersionable
    {
        public int PassFieldId { get; set; }
        public int PassTemplateId { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultLabel { get; set; }
        public PassFieldFormat Format { get; set; }
        public PassTemplate Template { get; set; }
        public ICollection<PassFieldValue> FieldValues { get; set; }
    }
}
