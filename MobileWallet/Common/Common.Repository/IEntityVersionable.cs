using System;

namespace Common.Repository
{
    public interface IEntityVersionable
    {
        int Version { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}