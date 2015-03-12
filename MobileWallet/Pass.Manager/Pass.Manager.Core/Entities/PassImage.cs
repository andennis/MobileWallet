using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassImage : EntityVersionable
    {
        public int PassImageId { get; set; }
        public string Name { get; set; }
        public PassImageType ImageType { get; set; }
        public int? FileStorageId { get; set; }
        public int? FileStorage2xId { get; set; }

        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }

    }
}
