using Common.Repository;

namespace Pass.CertificateStorage.Repository.Core.Entities
{
    public class Certificate : EntityVersionable
    {
        public int CertificateId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int FileId { get; set; }
        public EntityStatus Status { get; set; }
    }
}
