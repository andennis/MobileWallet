using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassSiteUser : EntityVersionable
    {
        public int PassSiteId { get; set; }
        public int UserId { get; set; }
        public PassSiteUserState UserState { get; set; }
        public EntityStatus Status { get; set; }
        public PassSite PassSite { get; set; }
        public User User { get; set; }
    }
}
