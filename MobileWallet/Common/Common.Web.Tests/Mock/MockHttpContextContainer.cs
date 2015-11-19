using System.Collections.Specialized;
using System.Web;
using Moq;

namespace Common.Web.Tests.Mock
{
    public class MockHttpContextContainer
    {
        public Mock<HttpContextBase> Context { get; set; }
        public Mock<HttpRequestBase> Request { get; set; }
        public Mock<HttpResponseBase> Response { get; set; }
        public Mock<HttpSessionStateBase> SessionState { get; set; }
        public Mock<HttpServerUtilityBase> ServerUtility { get; set; }


        private NameValueCollection _formData;
        public NameValueCollection FormData
        {
            get
            {
                if (_formData == null)
                {
                    _formData = new NameValueCollection();
                    this.Request.Expect(r => r.Form).Returns(FormData);
                }
                return _formData;
            }
        }

        public MockHttpContextContainer()
        {
            Context = new Mock<HttpContextBase>();
            Request = new Mock<HttpRequestBase>();
            Response = new Mock<HttpResponseBase>();
            SessionState = new Mock<HttpSessionStateBase>();
            ServerUtility = new Mock<HttpServerUtilityBase>();

            Context.Setup(c => c.Request).Returns(Request.Object);
            Context.Setup(c => c.Response).Returns(Response.Object);
            Context.Setup(c => c.Session).Returns(SessionState.Object);
            Context.Setup(c => c.Server).Returns(ServerUtility.Object);
        }

        public HttpContextBase HttpContext
        {
            get { return Context.Object; }
        }

    }
}
