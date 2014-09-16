﻿using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Web.Grid;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using AutoMapper;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseEntityController<TEntityModelView, TEntity> : Controller
        where TEntityModelView : class, IViewModel, new() 
        where TEntity : class, IEntityWithID
    {
        private readonly IBaseService<TEntity> _service;

        protected BaseEntityController(IBaseService<TEntity> service)
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
            return View(new TEntityModelView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntityModelView model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = Mapper.Map<TEntityModelView, TEntity>(model);
                _service.Create(entity);
                return RedirectToAction("Index");
            }

            return View(new TEntityModelView());
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            TEntity entity = _service.Get(id);
            TEntityModelView model = Mapper.Map<TEntity, TEntityModelView>(entity);
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
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [AjaxOnly]
        public virtual ActionResult GridSearch(GridDataRequest request)
        {
            SearchResult<TEntity> result = _service.Search(GridRequestToSearchContext(request));
            IEnumerable<TEntityModelView> resultView = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityModelView>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        private SearchContext GridRequestToSearchContext(GridDataRequest request)
        {
            return new SearchContext()
                   {
                       PageIndex = request.start,
                       PageSize = request.length
                   };
        }
    }
}