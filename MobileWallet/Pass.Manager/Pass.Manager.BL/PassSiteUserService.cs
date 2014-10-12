using System.Collections.Generic;
using System.Linq;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.BL
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
            IEnumerable<PassSiteUser> data = _repository.Query()
                .Filter(x => x.PassSiteId == searchFilter.PassSiteId)
                .Include(x => x.User)
                .Get();

            return new SearchResult<PassSiteUser>()
                    {
                        Data = data,
                        TotalCount = data.Count()
                    };
        }
    }
}
