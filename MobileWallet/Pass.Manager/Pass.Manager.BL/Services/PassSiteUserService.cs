using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            IEnumerable<PassSiteUser> data;
            if (searchContext.SortBy != null)
            {
                //For grid sorting
                var param = Expression.Parameter(typeof(PassSiteUser), "p");
                var prop = Expression.Property(param, searchContext.SortBy);
                var exp = Expression.Lambda(prop, param);
                var paramExpr = Expression.Parameter(typeof(IQueryable<PassSiteUser>));
                MethodInfo orderByMethodInfo = typeof(Queryable).GetMethods().First(method => method.Name == "OrderBy" && method.GetParameters().Count() == 2).MakeGenericMethod(typeof(PassSiteUser), prop.Type);
                MethodInfo orderByDescMethodInfo = typeof(Queryable).GetMethods().First(method => method.Name == "OrderByDescending" && method.GetParameters().Count() == 2).MakeGenericMethod(typeof(PassSiteUser), prop.Type);
                var orderByExpr = Expression.Call(searchContext.SortDirection == "asc" ? orderByMethodInfo : orderByDescMethodInfo, paramExpr, exp);
                var expr = Expression.Lambda<Func<IQueryable<PassSiteUser>, IOrderedQueryable<PassSiteUser>>>(orderByExpr, paramExpr).Compile();

                data = _repository.Query()
                .Filter(x => x.PassSiteId == searchFilter.PassSiteId)
                .Include(x => x.User)
                .OrderBy(expr)
                .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);
            }
            else
            {
                data = _repository.Query()
               .Filter(x => x.PassSiteId == searchFilter.PassSiteId)
               .Include(x => x.User)
               .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);

            }

            return new SearchResult<PassSiteUser>()
                    {
                        Data = data,
                        TotalCount = totalCount
                    };
        }
    }
}
