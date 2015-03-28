﻿using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Repositories;
using Common.Repository.EF;

namespace Pass.Manager.Repository.EF
{
    public class PassSiteRepository : PassManagerRepository<PassSite>, IPassSiteRepository
    {
        public PassSiteRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }
    }
}
