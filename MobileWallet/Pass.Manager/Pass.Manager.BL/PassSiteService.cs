using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.Core;
using Pass.Manager.Repository.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Manager.BL
{
    public class PassSiteService : BaseService<PassSite>, IPassSiteService
    {
        public PassSiteService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork.GetRepository<PassSite>())
        { 
        }
    }
}
