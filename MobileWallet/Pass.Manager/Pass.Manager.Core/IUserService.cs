using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core
{
    public interface IUserService : IBaseService<User>
    {
        User Get(string userName);
    }
}
