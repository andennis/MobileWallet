
using System;

namespace Common.Repository
{
    public interface IDbSession : IDisposable
    {
        object DbContext { get; }
    }
}
