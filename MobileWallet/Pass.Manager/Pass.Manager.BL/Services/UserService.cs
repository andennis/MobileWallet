﻿using System.Linq;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class UserService : BaseService<User, SearchFilterBase, IPassManagerUnitOfWork>, IUserService
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