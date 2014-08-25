using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core
{
    public interface IUserService
    {
        int Create(UserInfo userInfo);
        UserInfo Read(int userId);
        UserInfo Read(string userName);
        void Update(UserInfo userInfo);
        void Delete(int userId);
    }
}
