
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassBeacon : EntityVersionable
    {
        public int PassBeaconId { get; set; }
        public int? Major { get; set; }
        public int? Minor { get; set; }
        public string ProximityUuid { get; set; }
        public string RelevantText { get; set; }

        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }
    }
}
