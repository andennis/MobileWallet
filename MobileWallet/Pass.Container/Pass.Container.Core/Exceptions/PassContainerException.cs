using System;

namespace Pass.Container.Core.Exceptions
{
    public class PassContainerException : Exception
    {
        public PassContainerException(string message)
            : base(message)
        {
        }

        public PassContainerException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
