using System.Collections.Generic;
using System.Web;

namespace Common.Web
{
    public class UserContextSessionProvider : IUserContextProvider
    {
        private const string UserContext = "UserContext";
        private IDictionary<string, object> _userContext;

        private IDictionary<string, object> UserContextProperties
        {
            get
            {
                /*
                if (_userContext != null)
                    return _userContext;
                */

                _userContext = HttpContext.Current.Session[UserContext] as IDictionary<string, object>;
                if (_userContext == null)
                {
                    _userContext = new Dictionary<string, object>();
                    HttpContext.Current.Session[UserContext] = _userContext;
                }
                return _userContext;
            }
        }

        public string ContextId { get { return HttpContext.Current.Session.SessionID; } }

        public T GetCustomProperty<T>(string propName)
        {
            object val;
            if (UserContextProperties.TryGetValue(propName, out val))
                return (T)val;

            return default(T);
        }

        public void SetCustomProperty<T>(string propName, T propValue)
        {
            UserContextProperties[propName] = propValue;
        }

        public void Clear()
        {
            if (HttpContext.Current.Session[UserContext] != null)
            {
                _userContext = null;
                HttpContext.Current.Session.Remove(UserContext);
            }
        }
    }
}
