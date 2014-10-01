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

        public PassSiteUser GetUser(int passSiteId, int userId)
        {
            var rep = _unitOfWork.GetRepository<PassSiteUser>();
            return rep.Query()
                .Filter(x => x.PassSiteId == passSiteId && x.UserId == userId)
                .Include(x => x.User)
                .Get().First();
        }

        public void UpdateUser(PassSiteUser siteUser)
        {
            var rep = _unitOfWork.GetRepository<PassSiteUser>();
            rep.Update(siteUser);
            _unitOfWork.Save();
        }

        public void RemoveUser(int passSiteId, int userId)
        {
            var rep = _unitOfWork.GetRepository<PassSiteUser>();
            PassSiteUser siteUser = rep.Find(passSiteId, userId);
            rep.Delete(siteUser);
            _unitOfWork.Save();
        }
    }
}
