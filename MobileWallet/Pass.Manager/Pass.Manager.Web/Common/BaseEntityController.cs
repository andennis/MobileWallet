using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Web.Grid;
using Pass.Manager.Core;
using AutoMapper;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Web.Common
{
    [Authorize]
    public abstract class BaseEntityController<TEntityModelView, TEntity, TService, TSearchFilter> : BaseController
        where TEntityModelView : class, IViewModel, new() 
        where TEntity : class
        where TService : class, IBaseService<TEntity, TSearchFilter>
        where TSearchFilter : SearchFilterBase
    {
        protected readonly TService _service;

        protected BaseEntityController(TService service)
        {
            _service = service;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            var model = new TEntityModelView();
            SetDefaultReturnUrl(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntityModelView model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = Mapper.Map<TEntityModelView, TEntity>(model);
                _service.Create(entity);
                return RedirectTo(model);
            }

            return View(new TEntityModelView());
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            TEntity entity = _service.Get(id);
            TEntityModelView model = Mapper.Map<TEntity, TEntityModelView>(entity);
            SetDefaultReturnUrl(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEntityModelView model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = _service.Get(model.EntityId);
                entity = Mapper.Map<TEntityModelView, TEntity>(model, entity);
                _service.Update(entity);
                return RedirectTo(model);
            }

            return View(model);
        }

        [AjaxOnly]
        public virtual ActionResult GridSearch(GridDataRequest request, TSearchFilter searchFilter = null)
        {
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request), searchFilter);
            IEnumerable<TEntityModelView> resultView = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityModelView>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            _service.Delete(id);
            return JsonEx();
        }

        protected SearchContext GridRequestToSearchContext(GridDataRequest request)
        {
            return new SearchContext()
                   {
                       PageIndex = request.start,
                       PageSize = request.length
                   };
        }

    }
}