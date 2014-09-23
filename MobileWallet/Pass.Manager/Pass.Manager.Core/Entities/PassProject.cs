using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassProject : EntityVersionable, IEntityWithId
    {
        public int EntityId { get { return PassProjectId; } }

        public int PassProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PassTemplateId { get; set; }
        public int PassSiteId { get; set; }
        public PassProjectType ProjectType { get; set; }
        public PassSite PassSite { get; set; }
    }
}
