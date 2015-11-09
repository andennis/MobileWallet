using System.Web;
using System.Web.Security;

namespace Common.Web
{
    public class UserContext<TUserData> where TUserData : class, new()
    {
        private readonly IUserContextProvider _contextProvider;
        private readonly HttpContextBase _httpContext;

        public UserContext(IUserContextProvider contextProvider, HttpContextBase httpContext)
        {
            _contextProvider = contextProvider;
            _httpContext = httpContext;
        }

        public bool IsAuthenticated
        {
            get
            {
                if (!_httpContext.User.Identity.IsAuthenticated)
                    return false;

                var identity = _httpContext.User.Identity as FormsIdentity;
                if (identity == null)
                    return false;

                return UserId.HasValue
                       && UserName == identity.Name
                       && _contextProvider.ContextId == identity.Ticket.UserData;
            }
        }
        public string UserName
        {
            get
            {
                return _contextProvider.GetCustomProperty<string>("UserName");
            }
            set
            {
                _contextProvider.SetCustomProperty("UserName", value);
            }
        }
        public int? UserId
        {
            get
            {
                return _contextProvider.GetCustomProperty<int?>("UserId");
            }
            set
            {
                _contextProvider.SetCustomProperty("UserId", value);
            }
        }
        public TUserData UserData
        {
            get
            {
                var userData = _contextProvider.GetCustomProperty<TUserData>("UserData");
                if (userData == null)
                {
                    userData = new TUserData();
                    _contextProvider.SetCustomProperty("UserData", userData);
                }
                return userData;
            }
            set
            {
                _contextProvider.SetCustomProperty("UserData", value);
            }
        }

        public void Clear()
        {
            _contextProvider.Clear();
        }

    }
}