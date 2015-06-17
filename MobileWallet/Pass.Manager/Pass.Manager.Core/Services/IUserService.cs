using Common.BL;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Services
{
    public interface IUserService : IBaseService<User, SearchFilterBase>
    {
        bool IsAuthenticated(string userName, string password);
        void ChangePassword(User user);
        User Get(string userName);
    }
}
