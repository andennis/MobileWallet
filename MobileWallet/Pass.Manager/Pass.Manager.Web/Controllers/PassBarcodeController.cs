using System;
using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassBarcodeController : BaseEntityController<PassBarcodeViewModel, PassBarcode, IPassBarcodeService, PassBarcodeFilter>
    {
        public PassBarcodeController(IPassBarcodeService barcodeService)
            : base(barcodeService)
        {
        }

        [AjaxOnly]
        public ActionResult TabBarcode(int id)
        {
            var barcode = GetViewModel(id) ?? new PassBarcodeViewModel();
            return PartialView("~/Views/PassContentTemplate/Tabs/_Barcode.cshtml", barcode);
        }

        [HttpPost]
        public ActionResult CreateBarcode(PassBarcodeViewModel model)
        {
            return base.Create(model);
        }

        [HttpPost]
        public ActionResult EditBarcode(PassBarcodeViewModel model)
        {
            return base.Edit(model);
        }
	}
}