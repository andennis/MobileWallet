using System;
using System.Collections.Generic;
using Common.Repository;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.Core
{
    public interface IPassRepository : IRepository<Entities.Pass>
    {
        IList<ChangedPass> GetChangedPassesApple(string deviceId, string passTypeId, DateTime? updateTag);
        Entities.Pass GetPassApple(string serialNumber, string passTypeId);
    }

}