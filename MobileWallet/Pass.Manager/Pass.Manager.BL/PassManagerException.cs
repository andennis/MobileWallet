using System;

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
