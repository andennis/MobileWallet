using System;
using System.Linq;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.BL
{
    public class UserService: BaseService<User>, IUserService
    {
        public UserService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
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
