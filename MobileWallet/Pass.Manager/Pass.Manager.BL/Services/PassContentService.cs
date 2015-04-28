using System;
using System.Linq;
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

        public override PassContent Get(int entityId)
        {
            return _repository.Query()
                .Include(x => x.PassContentTemplate)
                .Filter(x => x.PassContentId == entityId)
                .Get()
                .FirstOrDefault();
        }

        public PassContent GetDetails(int entityId)
        {
            return _repository.Query()
                .Include(x => x.PassContentTemplate)
                .Include(x => x.Fields.Select(x2 => x2.PassProjectField))
                .Filter(x => x.PassContentId == entityId)
                .Get()
                .FirstOrDefault();
        }
    }
}
