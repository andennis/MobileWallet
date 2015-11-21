using System;
using System.Web.Routing;
using NUnit.Framework;

namespace Common.Web.Tests
{
    [TestFixture]
    public class RouteDataExtensionsTests
    {
        [Test]
        public void RouteValueTest()
        {
            var routeData = new RouteData();
            Assert.DoesNotThrow(() => routeData.GetRouteValue<string>("Prm1"));
            Assert.Null(routeData.GetRouteValue<string>("Prm1"));

            routeData.SetRouteValue("Prm1", "Val1");
            Assert.True(routeData.Values.ContainsKey("Prm1"));
            Assert.AreEqual("Val1", routeData.Values["Prm1"]);
            Assert.AreEqual("Val1", routeData.GetRouteValue<string>("Prm1"));
            Assert.Throws<InvalidCastException>(() => routeData.GetRouteValue<int>("Prm1"));

            routeData.Values["Prm1"] = null;
            Assert.DoesNotThrow(() => routeData.GetRouteValue<string>("Prm1"));
        }

        [Test]
        public void AreaTest()
        {
            var routeData = new RouteData();
            Assert.Null(routeData.GetArea());

            routeData.SetArea("Area1");
            Assert.True(routeData.DataTokens.ContainsKey("area"));
            Assert.AreEqual("Area1", routeData.DataTokens["area"]);
            Assert.AreEqual("Area1", routeData.GetArea());
        }

        [Test]
        public void ControllerTest()
        {
            var routeData = new RouteData();
            Assert.Throws<InvalidOperationException>(() => routeData.GetController());

            routeData.SetController("Controller1");
            Assert.True(routeData.Values.ContainsKey("controller"));
            Assert.AreEqual("Controller1", routeData.Values["controller"]);
            Assert.AreEqual("Controller1", routeData.GetController());
        }

        [Test]
        public void ActionTest()
        {
            var routeData = new RouteData();
            Assert.Throws<InvalidOperationException>(() => routeData.GetAction());

            routeData.SetAction("Action1");
            Assert.True(routeData.Values.ContainsKey("action"));
            Assert.AreEqual("Action1", routeData.Values["action"]);
            Assert.AreEqual("Action1", routeData.GetAction());
        }

    }
}

