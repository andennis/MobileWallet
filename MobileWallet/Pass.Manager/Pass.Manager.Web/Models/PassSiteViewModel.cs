using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Models
{
    public class PassSiteViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Site"; } }
        public int PassSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}