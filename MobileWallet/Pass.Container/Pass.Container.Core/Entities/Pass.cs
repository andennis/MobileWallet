﻿using System;
using System.Collections.Generic;
using Common.Repository;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class Pass : EntityVersionable
    {
        public int PassId { get; set; }
        public string AuthToken { get; set; }
        public string SerialNumber { get; set; }
        public string PassTypeIdentifier { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public PassStatus Status { get; set;}
        public int PassTemplateId { get; set; }
        public PassTemplate Template { get; set; }
        public ICollection<PassFieldValue> FieldValues { get; set; }
        public ICollection<Registration> PassRegistrations { get; set; }
    }
}
