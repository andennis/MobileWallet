using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using Common.Repository.EF;
using Pass.Container.Core;
using Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerUnitOfWork : UnitOfWork, IPassContainerUnitOfWork
    {
        private readonly IDbSession _dbSession;

        public PassContainerUnitOfWork(IDbSession dbSession)
            :base(dbSession)
        {
            _dbSession = dbSession;
            AddRepository(new Repository<PassApple>((DbContext)_dbSession.DbContext));
        }
    }
}
