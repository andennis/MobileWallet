using System.Web.Mvc;
using Common.Web;

namespace Pass.Manager.Web.Common
{
    public abstract class PmWebViewPage<TModel> : WebViewPage<TModel>
    {
        public UserContext<UserContextData> AuthUserContext { get; set; }

        public override void InitHelpers()
        {
            AuthUserContext = DependencyResolver.Current.GetService(typeof(UserContext<UserContextData>)) as UserContext<UserContextData>;
            base.InitHelpers();
        }
    }

}