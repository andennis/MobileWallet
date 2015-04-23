using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassLocation : EntityVersionable
    {
        public int PassLocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Altitude { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string RelevantText { get; set; }

        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }
    }
}
