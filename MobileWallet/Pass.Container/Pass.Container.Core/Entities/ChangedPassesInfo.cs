using System;
using System.Collections.Generic;

namespace Pass.Container.Core.Entities
{
    public class ChangedPassesInfo
    {
        public IList<string> SerialNumbers { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
