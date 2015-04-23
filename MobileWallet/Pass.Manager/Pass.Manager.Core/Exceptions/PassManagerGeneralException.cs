using System;

namespace Pass.Manager.Core.Exceptions
{
    public class PassManagerGeneralException : Exception
    {
        public PassManagerGeneralException(string message)
            : base(message)
        {
        }
        public PassManagerGeneralException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
