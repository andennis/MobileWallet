using System;
using System.Collections.Generic;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContent : EntityVersionable
    {
        public int PassContentId { get; set; }
        public string SerialNumber { get; set; }
        public string AuthToken { get; set; }
        public DateTime? ExpDate { get; set; }
        public bool IsVoided { get; set; }
        public EntityStatus Status { get; set; }

        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }

        public ICollection<PassContentField> Fields { get; set; }

        public int? ContainerPassId { get; set; }
    }
}
