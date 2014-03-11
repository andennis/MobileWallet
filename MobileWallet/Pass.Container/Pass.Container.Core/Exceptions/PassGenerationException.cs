using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
