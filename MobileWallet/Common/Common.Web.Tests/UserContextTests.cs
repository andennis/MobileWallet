using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Moq;
using NUnit.Framework;

namespace Common.Web.Tests
{
    [TestFixture]
    public class UserContextTests
    {
        private const string UserName = "User1";
        private const int UserId = 123;
        private const string ContextId = "111";

        private class UserData
        {
            public string P1 { get; set; } 
        }

        [Test]
        public void UserIsAuthenticatedTest()
        {
            var httpContext = new Mock<HttpContextBase>();
            var identity = new FormsIdentity(new FormsAuthenticationTicket(2, UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, ContextId));
            var principal = new GenericPrincipal(identity, null);
            httpContext.Setup(x => x.User).Returns(principal);

            var provider = new Mock<IUserContextProvider>();
            provider.Setup(x => x.GetCustomProperty<int?>("UserId")).Returns(UserId);
            provider.Setup(x => x.GetCustomProperty<string>("UserName")).Returns(UserName);
            provider.Setup(x => x.ContextId).Returns(ContextId);

            var context = new UserContext<UserData>(provider.Object, httpContext.Object);
            Assert.True(context.IsAuthenticated);
        }

        [Test]
        public void UserIsNotAuthenticatedSessionExpiredTest()
        {
            var httpContext = new Mock<HttpContextBase>();
            var identity = new FormsIdentity(new FormsAuthenticationTicket(2, UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, ContextId));
            var principal = new GenericPrincipal(identity, null);
            httpContext.Setup(x => x.User).Returns(principal);

            var provider = new Mock<IUserContextProvider>();
            provider.Setup(x => x.GetCustomProperty<int?>("UserId")).Returns((int?)null);
            provider.Setup(x => x.GetCustomProperty<string>("UserName")).Returns((string)null);
            provider.Setup(x => x.ContextId).Returns((string)null);

            var context = new UserContext<UserData>(provider.Object, httpContext.Object);
            Assert.False(context.IsAuthenticated);
        }

        [Test]
        public void UserIsNotAuthenticatedAuthTicketExpiredTest()
        {
            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.IsAuthenticated).Returns(false);
            var principal = new GenericPrincipal(identity.Object, null);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.User).Returns(principal);

            var provider = new Mock<IUserContextProvider>();
            provider.Setup(x => x.GetCustomProperty<int?>("UserId")).Returns(UserId);
            provider.Setup(x => x.GetCustomProperty<string>("UserName")).Returns(UserName);
            provider.Setup(x => x.ContextId).Returns(ContextId);

            var context = new UserContext<UserData>(provider.Object, httpContext.Object);
            Assert.False(context.IsAuthenticated);
        }

        [Test]
        public void UserIsNotAuthenticatedWrongSessionIdTest()
        {
            var httpContext = new Mock<HttpContextBase>();
            var identity = new FormsIdentity(new FormsAuthenticationTicket(2, UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, ContextId));
            var principal = new GenericPrincipal(identity, null);
            httpContext.Setup(x => x.User).Returns(principal);

            var provider = new Mock<IUserContextProvider>();
            provider.Setup(x => x.GetCustomProperty<int?>("UserId")).Returns(UserId);
            provider.Setup(x => x.GetCustomProperty<string>("UserName")).Returns(UserName);
            provider.Setup(x => x.ContextId).Returns(ContextId+"_new");

            var context = new UserContext<UserData>(provider.Object, httpContext.Object);
            Assert.False(context.IsAuthenticated);
        }

        [Test]
        public void UserContextStandardPropertiesTest()
        {
            var provider = new Mock<IUserContextProvider>();
            provider.Setup(x => x.GetCustomProperty<string>("UserName")).Returns(UserName);
            provider.Setup(x => x.GetCustomProperty<int?>("UserId")).Returns(UserId);
            var context = new UserContext<UserData>(provider.Object, null);
            Assert.AreEqual(UserName, context.UserName);
            Assert.AreEqual(UserId, context.UserId);
        }

        [Test]
        public void UserContextCustomPropertiesTest()
        {
            var provider = new Mock<IUserContextProvider>();
            provider.Setup(x => x.GetCustomProperty<UserData>("UserData")).Returns(new UserData() { P1 = "val1" });
            var context = new UserContext<UserData>(provider.Object, null);
            Assert.NotNull(context.UserData);
            Assert.AreEqual("val1", context.UserData.P1);
        }

        [Test]
        public void UserContextClearTest()
        {
            throw new NotImplementedException();
        }
    }
}
