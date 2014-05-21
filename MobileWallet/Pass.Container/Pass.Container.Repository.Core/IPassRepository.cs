using System;
using System.Collections.Generic;
using Common.Repository;

namespace Pass.Container.Repository.Core
{
    public interface IPassRepository : IRepository<Entities.Pass>
    {
        IList<string> GetSerialNumbersOfChangedPassesApple(string deviceId, string passTypeId, DateTime? updateTag);
    }
}