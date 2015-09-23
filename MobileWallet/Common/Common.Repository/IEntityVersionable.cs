using System;

namespace Common.Repository
{
    public interface IEntityVersionable
    {
        byte[] Version { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}