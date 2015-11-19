using System.Collections.Generic;
using System.Web;

namespace Common.Web.Tests.Fake
{
    public class FakeHttpSession : HttpSessionStateBase
    {
        private readonly Dictionary<string, object> _session = new Dictionary<string, object>();

        public override object this[string name]
        {
            get
            {
                if (!_session.ContainsKey(name))
                    return null;

                return _session[name];
            }
            set { _session[name] = value; }
        }
    }
}
