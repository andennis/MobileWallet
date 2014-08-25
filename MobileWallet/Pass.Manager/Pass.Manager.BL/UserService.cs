using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.Core;
using Pass.Manager.Repository.Core.Entities;

namespace Pass.Manager.BL
{
    public class UserService: IUserService
    {
        private readonly IPassManagerUnitOfWork _pmUnitOfWork;
        private readonly IRepository<User> _repUser;
        private readonly IPassManagerConfig _config;

        private const string SecurityVector = "142b1a)v(b#Oc&Mq";

        public UserService(IPassManagerConfig config, IPassManagerUnitOfWork pmUnitOfWork)
        {
            _pmUnitOfWork = pmUnitOfWork;
            _repUser = _pmUnitOfWork.GetRepository<User>();
            _config = config;
        }
        public int Create(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException("userInfo");
            if (userInfo.UserName == null)
                throw new PassManagerException("User name should not be null");

            var user = new User()
                       {
                           UserName = userInfo.UserName,
                           FirstName = userInfo.FirstName,
                           LastName = userInfo.LastName
                       };
            string psw = userInfo.Password.ConvertToUnsecureString();
            user.Password = Crypto.EncryptString(psw, _config.SecurityKey, SecurityVector);
            _repUser.Insert(user);
            _pmUnitOfWork.Save();

            return user.UserId;
        }

        public UserInfo Read(int userId)
        {
            User user = _repUser.Find(userId);
            if (user == null)
                throw new PassManagerException(string.Format("Usaer ID:{0} not found", userId));

            return ConvertTo(user);
        }

        public UserInfo Read(string userName)
        {
            User user = _repUser.Query()
                .Filter(x => x.UserName == userName)
                .Get().FirstOrDefault();
            if (user == null)
                throw new PassManagerException(string.Format("Usaer name:{0} not found", userName));

            return ConvertTo(user);
        }

        public void Update(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException("userInfo");

            User user = _repUser.Find(userInfo.UserId);
            if (user == null)
                throw new PassManagerException(string.Format("User ID:{0} not found", userInfo.UserId));

            user.UserName = userInfo.UserName;
            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            string psw = userInfo.Password.ConvertToUnsecureString();
            user.Password = Crypto.EncryptString(psw, _config.SecurityKey, SecurityVector);

            _repUser.Update(user);
            _pmUnitOfWork.Save();
        }

        public void Delete(int userId)
        {
            User user = _repUser.Find(userId);
            if (user == null)
                throw new PassManagerException(string.Format("Usaer ID:{0} not found", userId));

            _repUser.Delete(user);
            _pmUnitOfWork.Save();
        }

        private UserInfo ConvertTo(User user)
        {
            var userInfo = new UserInfo()
                           {
                               UserId = user.UserId,
                               UserName = user.UserName,
                               FirstName = user.FirstName,
                               LastName = user.LastName
                           };
            string psw = Crypto.DecryptString(user.Password, _config.SecurityKey, SecurityVector);
            userInfo.Password = psw.ConvertToSecureString();
            return userInfo;
        }
    }
}
