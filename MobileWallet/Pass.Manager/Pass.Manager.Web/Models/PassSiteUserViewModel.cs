using Pass.Manager.Core.Entities;

namespace Pass.Manager.Web.Models
{
    public class PassSiteUserViewModel : UserViewModel
    {
        public int PassSiteId { get; set; }
        public PassSiteUserState UserState { get; set; }
    }
}