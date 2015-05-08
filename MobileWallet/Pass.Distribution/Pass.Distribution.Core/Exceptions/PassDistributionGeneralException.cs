using System;

namespace Pass.Distribution.Core.Exceptions
{
    public class PassDistributionGeneralException : Exception
    {
        public PassDistributionGeneralException(string message)
            : base(message)
        {
        }
        public PassDistributionGeneralException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
