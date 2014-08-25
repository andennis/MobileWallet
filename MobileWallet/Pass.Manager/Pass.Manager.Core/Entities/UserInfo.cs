using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Manager.Core.Entities
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
