
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassBeacon : EntityVersionable
    {
        public int PassBeaconId { get; set; }
        public int PassContentId { get; set; }
        public ushort? Major { get; set; }
        public ushort? Minor { get; set; }
        public string ProximityUuid { get; set; }
        public string RelevantText { get; set; }

        public PassContent PassContent { get; set; }
    }
}
