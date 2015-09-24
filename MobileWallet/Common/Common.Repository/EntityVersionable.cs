using System;

namespace Common.Repository
{
    public abstract class EntityVersionable : IEntityVersionable
    {
        public int Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
