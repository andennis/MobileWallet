using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core
{
    public interface IUserService : IBaseService<User>
    {
        void ChangePassword(User user);
        User Get(string userName);
    }
}
