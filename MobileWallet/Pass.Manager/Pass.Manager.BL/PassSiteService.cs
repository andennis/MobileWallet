using System.Collections;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pass.Manager.BL
{
    public class PassSiteService : BaseService<PassSite>, IPassSiteService
    {
        public PassSiteService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }

        public SearchResult<PassSiteUser> GetUsers(SearchContext searchContext, int passSiteId)
        {
            var rep = _unitOfWork.GetRepository<PassSiteUser>();
            IEnumerable<PassSiteUser> data = rep.Query().Filter(x => x.PassSiteId == passSiteId).Include(x => x.User).Get();

            return new SearchResult<PassSiteUser>()
            {
                Data = data,
                TotalCount = data.Count()
            };

        }
        
    }
}
