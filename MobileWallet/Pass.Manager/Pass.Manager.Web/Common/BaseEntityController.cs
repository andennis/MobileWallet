using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.BL;
using Common.Repository;
using Common.Web.Controls.Grid;
using AutoMapper;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseEntityController<TEntityViewModel, TEntity, TService, TSearchFilter> : BaseEntityController<TEntityViewModel, TEntity, TEntity, TService, TSearchFilter>
        where TEntityViewModel : class, IViewModel, new()
        where TEntity : class, new()
        where TService : IBaseService<TEntity, TSearchFilter>
        where TSearchFilter : SearchFilterBase
    {
        protected BaseEntityController(TService service)
            :base(service)
        {
        }
    }

    [Authorize]
    public abstract class BaseEntityController<TEntityViewModel, TEntity, TEntityView, TService, TSearchFilter> : BaseController
        where TEntityViewModel : class, IViewModel, new() 
        where TEntity : class, new()
        where TEntityView : class
        where TService : IBaseService<TEntity, TSearchFilter>
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

        protected ActionResult CreateView(object model)
        {
            if (Request.IsAjaxRequest())
                return PartialView("_Create", model);

            return View("Create", model);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            TEntityViewModel model = GetViewModel(id);
            SetDefaultReturnUrl(model);
            PrepareModelToEditView(model);
            return EditView(model);
        }

        protected virtual TEntityViewModel GetViewModel(int entityId)
        {
            TEntity entity = _service.Get(entityId);
            return Mapper.Map<TEntity, TEntityViewModel>(entity);
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
        public virtual ActionResult GridSearch(GridDataRequest request, TSearchFilter searchFilter)
        {
            request.sortColumn = Request.QueryString["order[0][column]"] != null ? Request.QueryString["columns[" + Request.QueryString["order[0][column]"] + "][data]"] : null;
            request.sortDirection = Request.QueryString["order[0][dir]"];
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request), searchFilter);
            IEnumerable<TEntityViewModel> resultView = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual ActionResult GridSearchView(GridDataRequest request, TSearchFilter searchFilter)
        {
            request.sortColumn = Request.QueryString["order[0][column]"] != null ? Request.QueryString["columns[" + Request.QueryString["order[0][column]"] + "][data]"] : null;
            request.sortDirection = Request.QueryString["order[0][dir]"];
            SearchResult<TEntityView> result = _service.SearchView<TEntityView>(GridRequestToSearchContext(request), searchFilter);
            IEnumerable<TEntityViewModel> resultView = Mapper.Map<IEnumerable<TEntityView>, IEnumerable<TEntityViewModel>>(result.Data);
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

        protected void SetFormAttributes(object htmlAttributes)
        {
            ViewBag.HtmlFormAttributes = htmlAttributes;
        }

        protected virtual void SetDefaultReturnUrl(TEntityViewModel model)
        {
            if (model.RedirectUrl == null && Request.UrlReferrer != null)
                model.RedirectUrl = Request.UrlReferrer.ToString();
        }

        protected virtual ActionResult RedirectTo(TEntityViewModel model)
        {
            if (model.RedirectUrl == null)
                return RedirectToAction("Index");

            return Redirect(model.RedirectUrl);
        }

    }
}