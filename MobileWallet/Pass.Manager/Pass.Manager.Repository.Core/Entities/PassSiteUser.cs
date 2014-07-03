using Common.Repository;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassSiteUser : EntityVersionable
    {
        public int ClientSiteId { get; set; }
        public int UserId { get; set; }
        public PassSiteUserState UserState { get; set; }
        public PassSite ClientSite { get; set; }
        public User User { get; set; }
    }
}
