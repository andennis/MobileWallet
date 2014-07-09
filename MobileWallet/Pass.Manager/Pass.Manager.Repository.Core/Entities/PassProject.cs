using Common.Repository;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassProject : EntityVersionable
    {
        public int PassProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PassTemplateId { get; set; }
        public int PassSiteId { get; set; }
        public PassSite PassSite { get; set; }
    }
}
