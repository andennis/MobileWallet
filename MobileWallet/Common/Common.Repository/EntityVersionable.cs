using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Repository
{
    public abstract class EntityVersionable : IEntityVersionable
    {
        [Timestamp]
        public byte[] Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
