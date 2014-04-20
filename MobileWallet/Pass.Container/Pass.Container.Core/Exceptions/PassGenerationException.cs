using System;

namespace Pass.Container.Core.Exceptions
{
    public class PassGenerationException : Exception
    {
        public PassGenerationException(string message)
            : base(message)
        {
        }
        public PassGenerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
