using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;

namespace Pass.Manager.Core
{
    public interface IPassManagerConfig : IDbConfig
    {
        string SecurityKey { get; }
    }
}
