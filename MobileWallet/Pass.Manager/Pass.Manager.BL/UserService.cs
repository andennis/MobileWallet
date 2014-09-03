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
        private readonly IPassManagerUnitOfWork _pmUnitOfWork;

        public UserService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _pmUnitOfWork = unitOfWork;
        }
       
        public override int Create(User user)
        {
            user.Password = Crypto.CalculateHash(user.UserName, user.Password);
            base.Create(user);
            _pmUnitOfWork.Save();

            return user.UserId;
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
