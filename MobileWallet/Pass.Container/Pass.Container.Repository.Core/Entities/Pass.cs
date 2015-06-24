using System;
using System.Collections.Generic;
using Common.Repository;

namespace Pass.Container.Repository.Core.Entities
{
    public class Pass : EntityVersionable
    {
        public int PassId { get; set; }
        public string AuthToken { get; set; }
        public string SerialNumber { get; set; }
        //public string PassTypeId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public EntityStatus Status { get; set;}

        public int PassTemplateId { get; set; }
        public PassTemplate Template { get; set; }

        public ICollection<PassFieldValue> FieldValues { get; set; }
        public ICollection<Registration> PassRegistrations { get; set; }
        public ICollection<PassNative> NativePasses { get; set; }
    }
}
