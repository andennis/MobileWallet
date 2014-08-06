using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pass.Manager.Web.Startup))]
namespace Pass.Manager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
