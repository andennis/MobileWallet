using System;
using System.Linq;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.Core;

namespace Pass.Manager.BL
{
    public class UserService: BaseService<User>, IUserService
    {
        public UserService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
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
