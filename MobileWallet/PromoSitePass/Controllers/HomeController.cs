using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PromoSitePass.Models.GeneralPassTemplate;

namespace PromoSitePass.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public void GiveMeJson(GeneralPassTemplate passModel)
        {
            Console.WriteLine(passModel);
        }

        public ActionResult CreateCard(GeneralPassTemplate passModel)
        {

            return View(passModel);
        }

        public ActionResult EmptyCardDesigner()
        {
            var passModel = new GeneralPassTemplate
            {
                PassStyle = PassStyle.Coupon,
                BackgroundColor = Color.LightGray,
                LogoText = "Clever",
                LocationDetails = new LocationDetails(),
                DistributionDetails = new DistributionDetails()
                {
                    QuantityRestriction = 1000
                },
                BarcodeDetails = new BarcodeDetails()
                {
                    BarcodeType = GeneralBarcodeType.QrCode
                },
                FieldDetails = new FieldDetails()
                {
                    AuxiliaryFields = new List<GeneralField>(5)
                    {
                        new GeneralField(),
                        new GeneralField(),
                        new GeneralField(),
                        new GeneralField(),
                        new GeneralField()
                    }
                }
            };

            ViewData["divHeaderLabelPass1"] = "Баланс";
            ViewData["divHeaderValuePass1"] = "2 100 000";
            ViewData["divPrimaryValuePass"] = "-20%";
            ViewData["divPrimaryLabelPass"] = "Cкидка на весь товар";
            ViewData["divAuxiliaryLabelPass1"] = "Номер клиента";
            ViewData["divAuxiliaryValuePass1"] = "02387";
            ViewData["divAuxiliaryLabelPass2"] = "Период действия";
            ViewData["divAuxiliaryValuePass2"] = "01.03 - 31.05.2014";
            ViewData["divPassBodyColor"] = "#bdc3c7";
            return View("CreateCard", passModel);
        }

        public ActionResult CouponsStrategyCard()
        {
            var passModel = new GeneralPassTemplate
            {
                OrganizationName = "Clever",
                PassDescription = "Купон для сети магазинов Clever",
                PassStyle = PassStyle.Coupon,
                BackgroundColor = Color.PaleTurquoise,
                LabelTextColor = Color.Navy,
                ValueTextColor = Color.FromArgb(235, 171, 12),
                LogoText = "Clever",
                LocationDetails = new LocationDetails(),
                DistributionDetails = new DistributionDetails
                                      {
                    QuantityRestriction = 1000
                },
                BarcodeDetails = new BarcodeDetails()
                {
                    BarcodeType = GeneralBarcodeType.QrCode,
                    EncodedMessage = EncodedMessage.EncodeTheSameMessageOnEachPass,
                    AlternativeText = AlternativeText.DisplayTheSameMessageOnEachPass,
                    TextToDisplay = "http://www.passlight.com"
                },
                FieldDetails = new FieldDetails
                {
                    AuxiliaryFields = new List<GeneralField>(5)
                    {
                        new GeneralField
                        {
                            IsMarkedField = true,
                            Label = "Номер клиента",
                            Value = "02387",
                            IsDynamicValue = true
                        },
                        new GeneralField
                        {
                            IsMarkedField = true,
                            Label = "Период действия",
                            Value = "01.03 - 31.05.2014",
                            IsDynamicValue = true
                        },
                        new GeneralField(),
                        new GeneralField(),
                        new GeneralField()
                    }
                }
            };

            var divPassBodyColor = String.Format("#" + passModel.BackgroundColor.R.ToString("X2")
                                    + passModel.BackgroundColor.G.ToString("X2") +
                                    passModel.BackgroundColor.B.ToString("X2"));
            var labelTextColor = String.Format("#" + passModel.LabelTextColor.R.ToString("X2")
                                    + passModel.LabelTextColor.G.ToString("X2") +
                                    passModel.LabelTextColor.B.ToString("X2"));
            var valueTextColor = String.Format("#" + passModel.ValueTextColor.R.ToString("X2")
                                    + passModel.ValueTextColor.G.ToString("X2") +
                                    passModel.ValueTextColor.B.ToString("X2"));
            ViewData["divPassBodyColor"] = divPassBodyColor;
            ViewData["labelTextColor"] = labelTextColor;
            ViewData["valueTextColor"] = valueTextColor;


            ViewData["divLogoTextPass"] = "rtyrtryr";
            ViewData["divHeaderLabelPass"] = "Баланс";
            ViewData["divHeaderValuePass"] = "2 100 000";
            ViewData["divPrimaryValuePass"] = "-20%";
            ViewData["divPrimaryLabelPass"] = "Cкидка на весь товар";
            //ViewData["divAuxiliaryLabelPass1"] = "Номер клиента";
            //ViewData["divAuxiliaryValuePass1"] = "02387";
            //ViewData["divAuxiliaryLabelPass2"] = "Период действия";
            //ViewData["divAuxiliaryValuePass2"] = "01.03 - 31.05.2014";
            return View("CreateCard", passModel);
        }

        public ActionResult CouponsStrategy()
        {
            return View();
        }

        public ActionResult FrontContentTab()
        {
            return PartialView();
        }
    }
}