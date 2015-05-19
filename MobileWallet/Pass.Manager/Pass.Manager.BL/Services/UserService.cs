using System.Linq;
using Common.BL;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class UserService : PassManagerServiceBase<User, SearchFilterBase>, IUserService
    {
        public UserService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool IsAuthenticated(string userName, string password)
        {
            User user = Get(userName);
            if (user != null)
                return (user.Password == Crypto.CalculateHash(userName.ToLower(), password));

            return false;
        }

        public void ChangePassword(User user)
        {
            user.Password = Crypto.CalculateHash(user.UserName.ToLower(), user.Password);
            base.Update(user);
        }

        public User Get(string userName)
        {
            User user = _repository.Query()
                .Filter(x => x.UserName == userName)
                .Get().FirstOrDefault();
           
            return user;
        }
    }
}
