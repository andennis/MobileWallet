using System.Collections.Generic;
using System.Linq;
using Common.BL;
using Common.Repository;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassSiteUserService : BaseService<PassSiteUser, PassSiteUserFilter, IPassManagerUnitOfWork>, IPassSiteUserService
    {
        public PassSiteUserService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }

        public override PassSiteUser Get(int entityId)
        {
            return _repository.Query().Filter(x => x.PassSiteUserId == entityId).Include(x => x.User).Get().First();
        }

        public IEnumerable<User> GetUnassignedUsers(int passSiteId)
        {
            return _unitOfWork.PassSiteUserRepository.GetUnassignedUsers(passSiteId);
        }

        public override SearchResult<PassSiteUser> Search(SearchContext searchContext, PassSiteUserFilter searchFilter = null)
        {
            int totalCount;
            IEnumerable<PassSiteUser> data = _repository.Query()
                .Filter(x => x.PassSiteId == searchFilter.PassSiteId)
                .Include(x => x.User)
                .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);

            return new SearchResult<PassSiteUser>()
                    {
                        Data = data,
                        TotalCount = totalCount
                    };
        }
    }
}
