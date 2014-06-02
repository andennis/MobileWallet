using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PromoSitePass.Startup))]
namespace PromoSitePass
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
