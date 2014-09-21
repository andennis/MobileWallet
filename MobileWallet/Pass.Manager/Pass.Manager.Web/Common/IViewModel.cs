using System.Security.Cryptography.X509Certificates;

namespace Pass.Manager.Web.Common
{
    public interface IViewModel
    {
        int EntityId { get; }
        bool IsNew { get; }
        string DisplayName { get; }
        string RedirectUrl { get; set; }
    }
}