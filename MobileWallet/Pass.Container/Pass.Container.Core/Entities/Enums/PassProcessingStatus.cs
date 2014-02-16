using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities.Enums
{
    public enum PassProcessingStatus
    {
        Succeed = 1,
        AlreadyDone = 2,
        Unauthorized = 3,
        NotFound = 4,
        NoContent = 5,
        Error = 6
    }
}
