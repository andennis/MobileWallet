using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core
{
    public interface IUserService : IBaseService<User, SearchFilterBase>
    {
        void ChangePassword(User user);
        User Get(string userName);
    }
}
