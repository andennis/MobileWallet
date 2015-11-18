using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Web.Navigation;
using Moq;
using NUnit.Framework;

namespace Common.Web.Tests
{
    [TestFixture]
    public class ActionHistoryTests
    {
        private const string ActionHistoryKey = "ActionHistory";
        private ControllerContext _controllerContext;
        private MockHttpContextContainer _httpContextContainer;

        [SetUp]
        public void InitEachTest()
        {
            _httpContextContainer = new MockHttpContextContainer();
            _controllerContext = new ControllerContext(_httpContextContainer.HttpContext, new RouteData(), new Mock<ControllerBase>().Object);
        }

        [Test]
        public void GoToCurrentAction_FirstAction_Test()
        {
            var fakeSession = new FakeHttpSession();
            _httpContextContainer.Context.Setup(x => x.Session).Returns(fakeSession);
            _httpContextContainer.Request.Setup(x => x.UrlReferrer).Returns(new Uri("http://localhost/home"));

            _controllerContext.RouteData.Values.Add("area", "Area1");
            _controllerContext.RouteData.Values.Add("controller", "controller1");
            _controllerContext.RouteData.Values.Add("action", "action1");

            var actionHistory = new ActionHistory(_controllerContext);
            ActionHistoryItem ahi = actionHistory.GoToCurrentAction();
            Assert.NotNull(ahi);
            Assert.AreEqual("Area1_controller1_action1", ahi.Name);
            Assert.AreEqual("http://localhost/home", ahi.PreviousUrl);
            var historyItems = fakeSession[ActionHistoryKey] as IList<ActionHistoryItem>;
            Assert.NotNull(historyItems);
            Assert.AreEqual(1, historyItems.Count);

            //Do the same second time
            ahi = actionHistory.GoToCurrentAction();
            Assert.NotNull(ahi);
            Assert.AreEqual("Area1_controller1_action1", ahi.Name);
            Assert.AreEqual("http://localhost/home", ahi.PreviousUrl);
            historyItems = fakeSession[ActionHistoryKey] as IList<ActionHistoryItem>;
            Assert.NotNull(historyItems);
            Assert.AreEqual(1, historyItems.Count);
        }

        [Test]
        public void GoToCurrentAction_NextAction_Test()
        {
            var historyItems = new List<ActionHistoryItem>() { new ActionHistoryItem() { Name = "User_Edit", PreviousUrl = "http://localhost/user" } };
            _httpContextContainer.SessionState.Setup(x => x[ActionHistoryKey]).Returns(historyItems);
            _httpContextContainer.Request.Setup(x => x.UrlReferrer).Returns(new Uri("http://localhost/user/edit"));

            _controllerContext.RouteData.Values.Add("controller", "Profile");
            _controllerContext.RouteData.Values.Add("action", "Edit");

            var actionHistory = new ActionHistory(_controllerContext);
            ActionHistoryItem ahi = actionHistory.GoToCurrentAction();
            Assert.NotNull(ahi);
            Assert.AreEqual("Profile_Edit", ahi.Name);
            Assert.AreEqual("http://localhost/user/edit", ahi.PreviousUrl);
            Assert.AreEqual(2, historyItems.Count);
        }

        [Test]
        public void GoToCurrentAction_PreviousAction_Test()
        {
            var historyItems = new List<ActionHistoryItem>()
            {
                new ActionHistoryItem() { Name = "User_Edit", PreviousUrl = "http://localhost/user" },
                new ActionHistoryItem() { Name = "Profile_Edit", PreviousUrl = "http://localhost/user/edit" }
            };
            _httpContextContainer.SessionState.Setup(x => x[ActionHistoryKey]).Returns(historyItems);
            _httpContextContainer.Request.Setup(x => x.UrlReferrer).Returns(new Uri("http://localhost/profile/edit"));

            _controllerContext.RouteData.Values.Add("controller", "User");
            _controllerContext.RouteData.Values.Add("action", "Edit");

            var actionHistory = new ActionHistory(_controllerContext);
            ActionHistoryItem ahi = actionHistory.GoToCurrentAction();
            Assert.NotNull(ahi);
            Assert.AreEqual("User_Edit", ahi.Name);
            Assert.AreEqual("http://localhost/user", ahi.PreviousUrl);
            Assert.AreEqual(1, historyItems.Count);
        }

        [Test]
        public void CurrentActionTest()
        {
            var historyItems = new List<ActionHistoryItem>()
            {
                new ActionHistoryItem() { Name = "User_Edit", PreviousUrl = "http://localhost/user" },
                new ActionHistoryItem() { Name = "Profile_Edit", PreviousUrl = "http://localhost/user/edit" }
            };
            _httpContextContainer.SessionState.Setup(x => x[ActionHistoryKey]).Returns(historyItems);

            var actionHistory = new ActionHistory(_controllerContext);
            ActionHistoryItem ahi = actionHistory.CurrentAction;
            Assert.NotNull(ahi);
            Assert.AreEqual("Profile_Edit", ahi.Name);
            Assert.AreEqual("http://localhost/user/edit", ahi.PreviousUrl);

            _httpContextContainer.SessionState.Verify(x => x[ActionHistoryKey], Times.Once());
        }

        [Test]
        public void ResetHistoryTest()
        {
            var fakeSession = new FakeHttpSession();
            fakeSession[ActionHistoryKey] = new List<ActionHistoryItem>
            {
                new ActionHistoryItem(){Name = "Profile_Edit", PreviousUrl = "http://localhost/user/edit"}
            };
            _httpContextContainer.Context.Setup(x => x.Session).Returns(fakeSession);

            var actionHistory = new ActionHistory(_controllerContext);
            ActionHistoryItem ahi = actionHistory.CurrentAction;
            Assert.NotNull(ahi);
            actionHistory.ResetHistory();
            ahi = actionHistory.CurrentAction;
            Assert.Null(ahi);
        }

    }
}
