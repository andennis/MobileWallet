using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Notification.Repository.Core.Enums
{
   public enum PushStatus
    {
        None = 0,
        Pending = 1,
        InProcess = 2,
        Processed = 3,
        NotProcessed = 4,
        Error = 5
    }
}
