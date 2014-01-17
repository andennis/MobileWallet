using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class Pass
    {
        public int PassId { get; set; }
        public string AuthToken { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public PassStatus Status { get; set;}
        public DateTime UpdatedDate { get; set; }
        public ICollection<PassFieldValue> PassFieldValues { get; set; }
    }
}
