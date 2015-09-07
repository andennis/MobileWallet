using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassProjectField : EntityVersionable
    {
        public int PassProjectFieldId { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultLabel { get; set; }
        public string Description { get; set; }

        public int PassProjectId { get; set; }
        public PassProject PassProject { get; set; }
    }
}
