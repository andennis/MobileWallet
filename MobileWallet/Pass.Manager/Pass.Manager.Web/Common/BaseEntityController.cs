using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.BL;
using Common.Web.Grid;
using AutoMapper;

namespace Pass.Manager.Web.Common
{
    [Authorize]
    public abstract class BaseEntityController<TEntityViewModel, TEntity, TService, TSearchFilter> : BaseController
        where TEntityViewModel : class, IViewModel, new() 
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
            return Create(x => { });
        }

        protected ActionResult Create(Action<TEntityViewModel> prepareModelAction)
        {
            var model = new TEntityViewModel();
            SetDefaultReturnUrl(model);

            if (prepareModelAction != null)
                prepareModelAction(model);

            PrepareModelToCreateView(model);
            return CreateView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntityViewModel model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = Mapper.Map<TEntityViewModel, TEntity>(model);
                _service.Create(entity);

                if (Request.IsAjaxRequest())
                    return JsonEx();

                return RedirectTo(model);
            }

            PrepareModelToCreateView(model);
            return CreateView(model);
        }

        private ActionResult CreateView(object model)
        {
            if (Request.IsAjaxRequest())
                return PartialView("_Create", model);

            return View("Create", model);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            TEntity entity = _service.Get(id);
            TEntityViewModel model = Mapper.Map<TEntity, TEntityViewModel>(entity);
            SetDefaultReturnUrl(model);
            PrepareModelToEditView(model);
            return EditView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEntityViewModel model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = _service.Get(model.EntityId);
                entity = Mapper.Map<TEntityViewModel, TEntity>(model, entity);
                _service.Update(entity);

                if (Request.IsAjaxRequest())
                    return JsonEx();
                return RedirectTo(model);
            }

            PrepareModelToEditView(model);
            return EditView(model);
        }

        private ActionResult EditView(object model)
        {
            if (Request.IsAjaxRequest())
                return PartialView("_Edit", model);

            return View(model);
        }

        [AjaxOnly]
        public virtual ActionResult Get(int id)
        {
            TEntity entity = _service.Get(id);
            TEntityViewModel model = Mapper.Map<TEntity, TEntityViewModel>(entity);
            return JsonEx(model, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual ActionResult GridSearch(GridDataRequest request, TSearchFilter searchFilter = null)
        {
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request), searchFilter);
            IEnumerable<TEntityViewModel> resultView = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            _service.Delete(id);
            return JsonEx();
        }

        protected virtual void PrepareModelToCreateView(TEntityViewModel model)
        {
        }
        protected virtual void PrepareModelToEditView(TEntityViewModel model)
        {
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