﻿using System.Collections.Generic;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassProject : EntityVersionable
    {
        public int PassProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PassSiteId { get; set; }
        public PassSite PassSite { get; set; }

        public int PassCertificateId { get; set; }
        public PassCertificate PassCertificate { get; set; }

        public ICollection<PassContentTemplate> PassContentTemplates { get; set; }
        public ICollection<PassProjectField> PassFields { get; set; }
    }
}
