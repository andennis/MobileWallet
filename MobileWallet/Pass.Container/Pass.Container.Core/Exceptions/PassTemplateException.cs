using System;

namespace Pass.Container.Core.Exceptions
{
    public class PassTemplateException : Exception
    {
        public PassTemplateException(string message)
            : base(message)
        {
        }

        public PassTemplateException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
