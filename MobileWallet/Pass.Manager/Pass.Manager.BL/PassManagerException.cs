using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Manager.BL
{
    public class PassManagerException : Exception
    {
        public PassManagerException(string message)
            : base(message)
        {
        }
        public PassManagerException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
