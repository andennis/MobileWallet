using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Web.Mvc;
using Common.Web.Grid;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using AutoMapper;

namespace Pass.Manager.Web.Common
{
    public class BaseEntityController<TEntityModelView, TEntity> : Controller 
        where TEntityModelView : class 
        where TEntity : class, IEntityWithID
    {
        private readonly IBaseService<TEntity> _service;

        public BaseEntityController(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [AjaxOnly]
        public virtual ActionResult GridSearch(GridDataRequest request)
        {
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request), x => true);
            IEnumerable<TEntityModelView> resultView = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityModelView>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
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