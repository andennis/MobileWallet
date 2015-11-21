using System.Web.Mvc;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Controllers
{
    public class HomeController : BaseController
    {
        [FormAuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}