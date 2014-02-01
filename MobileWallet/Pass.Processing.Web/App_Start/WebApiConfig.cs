using System.Web.Http;

namespace Pass.Processing.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
             config.MapHttpAttributeRoutes();
        }
    }
}
