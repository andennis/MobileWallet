using System;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContentView
    {
        public int PassContentId { get; set; }
        public string SerialNumber { get; set; }
        public string AuthToken { get; set; }
        public DateTime? ExpDate { get; set; }
        public bool IsValid { get; set; }
        public EntityStatus Status { get; set; }

        public int PassContentTemplateId { get; set; }
        public string PassContentTemplateName { get; set; }
        public int? ContainerPassId { get; set; }
        public int PassProjectId { get; set; }

    }
}
