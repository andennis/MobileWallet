using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.BL;
using Common.Web;
using Common.Web.Controls.Grid;
using AutoMapper;
using Common.Web.Navigation;

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

    [FormAuthenticationFilter]
    public abstract class BaseEntityController<TEntityViewModel, TEntity, TEntityView, TService, TSearchFilter> : BaseController
        where TEntityViewModel : class, IViewModel, new() 
        where TEntity : class, new()
        where TEntityView : class
        where TService : IBaseService<TEntity, TSearchFilter>
        where TSearchFilter : SearchFilterBase
    {
        protected readonly TService _service;
        protected ActionHistory _actionHistory;

        protected BaseEntityController(TService service)
        {
            _service = service;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actionHistory = new ActionHistory(this.ControllerContext);
            base.OnActionExecuting(filterContext);
        }

        public virtual ActionResult Index()
        {
            _actionHistory.ResetHistory();
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
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request), searchFilter);
            IEnumerable<TEntityViewModel> resultView = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual ActionResult GridSearchView(GridDataRequest request, TSearchFilter searchFilter)
        {
            SearchResult<TEntityView> result = _service.SearchView<TEntityView>(GridRequestToSearchContext(request), searchFilter);
            IEnumerable<TEntityViewModel> resultView = Mapper.Map<IEnumerable<TEntityView>, IEnumerable<TEntityViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual ActionResult GetItems(TSearchFilter searchFilter)
        {
            SearchResult<TEntity> result = _service.Search(new SearchContext(), searchFilter);
            IEnumerable<EntityItem> items = Mapper.Map<IEnumerable<TEntity>, IEnumerable<EntityItem>>(result.Data).OrderBy(x => x.Name);
            return Json(new SelectListTyped<EntityItem, int, string>(items, v => v.ID, t => t.Name), JsonRequestBehavior.AllowGet);
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
            if (model.RedirectUrl != null)
                return;

            ActionHistoryItem ahi = _actionHistory.GoToCurrentAction();
            if (ahi != null && ahi.PreviousUrl != null)
                model.RedirectUrl = ahi.PreviousUrl;
            else if (Request.UrlReferrer != null)
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