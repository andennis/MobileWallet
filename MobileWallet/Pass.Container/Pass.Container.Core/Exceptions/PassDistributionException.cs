using System;

namespace Pass.Container.Core.Exceptions
{
    public class PassDistributionException : Exception
    {
        public PassDistributionException(string message)
            : base(message)
        {
        }

        public PassDistributionException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
