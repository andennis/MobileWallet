using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core
{
    public interface IUserService
    {
        int Create(User userInfo);
        User Get(int userId);
        User Get(string userName);
        void Update(User userInfo);
        void Delete(int userId);
    }
}
