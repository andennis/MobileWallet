using System;
using System.Collections.Generic;
using Common.Repository;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public abstract class Pass : EntityVersionable
    {
        public int PassId { get; set; }
        public string AuthToken { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public PassStatus Status { get; set;}
        public PassTemplate Template { get; set; }
        public ICollection<PassFieldValue> FieldValues { get; set; }
    }
}
