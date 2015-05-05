using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Moq;

namespace Common.Web.Tests
{
    [TestFixture]
    public class UrlHelperExtensionsTests
    {
        private class MyClassController : Controller
        {
            public ActionResult Action1()
            {
                return Content(string.Empty);
            }

        }
        private class MyClass : Controller
        {

        }


        [Test]
        public void ActionTest()
        {
            throw new NotImplementedException();
            /*
            var urlHelper = GetUrlHelper();
            string url = urlHelper.Action<MyClassController>(x => x.Action1());
            Assert.AreEqual("MyClass/Action1", url);
            */
        }

        /*
        private UrlHelper GetUrlHelper(string fileName = "/", string url = "http://localhost", string queryString = "")
        {
            // Use routes from actual app
            var routeCollection = new RouteCollection();
            //MvcApplication.RegisterRoutes(routeCollection);

            //Make a request context
            var request = new HttpRequest(fileName, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new FakeHttpContext();// new HttpContext(request, response);
            var httpContextBase = new HttpContextWrapper(httpContext);

            // Make the UrlHelper with empty route data
            var requestContext = new RequestContext(httpContextBase, new RouteData());
            return new UrlHelper(requestContext, routeCollection);
        }
        */
    }
}
