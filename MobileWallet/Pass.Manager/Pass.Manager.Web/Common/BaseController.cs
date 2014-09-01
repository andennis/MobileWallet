using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Web.Mvc;
using Common.Web.Grid;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Web.Common
{
    public class BaseController<TEntity> : Controller where TEntity : class, IEntityWithID
    {
        private readonly IBaseService<TEntity> _service;

        public BaseController(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [AjaxOnly]
        public virtual ActionResult GridSearch(GridDataRequest request)
        {
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request), x => true);
            return Json(GridDataResponse.Create(request, result.Data, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        private SearchContext GridRequestToSearchContext(GridDataRequest request)
        {
            return new SearchContext()
                   {
                       PageIndex = request.iDisplayStart,
                       PageSize = request.iDisplayLength
                   };
        }
    }
}