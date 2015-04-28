using System;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassContentService : PassManagerServiceBase<PassContent, PassContentFilter>, IPassContentService
    {
        public PassContentService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Create(PassContent entity)
        {
            entity.SerialNumber = Guid.NewGuid().ToString();
            entity.AuthToken = Guid.NewGuid().ToString();
            base.Create(entity);
        }
    }
}
