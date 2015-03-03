using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pass.Manager.Web.Models;
using Pass.Manager.Web.Models.GeneralPassTemplate;

namespace PromoSitePass.Controllers
{
    public class PassDesignerController : Controller
    {
        //
        // GET: /PassDesigner/
        public ActionResult EmptyCardDesigner()
        {
            var passModel = new PassTemplateViewModel
                            {
                                PassStyle = PassStyle.Coupon,
                                BackgroundColor = "#004460",
                                LabelTextColor = "#000000",
                                ValueTextColor = "#ffffff",
                                LogoText = "",
                                LocationDetails = new LocationDetails()
                                {
                                    Locations = new List<GeneralLocation>()
                                                  {
                                                      new GeneralLocation()
                                                  }
                                },
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
                                                   HeaderFields = new List<GeneralField>()
                                                                  {
                                                                      new GeneralField(),
                                                                      new GeneralField(),
                                                                      new GeneralField()
                                                                  },
                                                   PrimaryFields = new List<GeneralField>()
                                                                   {
                                                                       new GeneralField(),
                                                                       new GeneralField()
                                                                   },
                                                   AuxiliaryFields = new List<GeneralField>(5)
                                                                     {
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   SecondaryFields = new List<GeneralField>()
                                                                     {
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   BackFields = new List<GeneralField>()
                                                                {
                                                                    new GeneralField()
                                                                },
                                                   TransitType = Transit.Generic
                                               }
                            };
            return View("_PassDesigner", passModel);
        }

        public ActionResult TransitStrategyCard()
        {
            var passModel = new PassTemplateViewModel
                            {
                                PassStyle = PassStyle.BoardingPass,
                                BackgroundColor = "#FD7C16",
                                LabelTextColor = "#004151",
                                ValueTextColor = "#ffffff",
                                LogoText = "",
                                LocationDetails = new LocationDetails()
                                {
                                    Locations = new List<GeneralLocation>()
                                                  {
                                                      new GeneralLocation()
                                                  }
                                },
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
                                                   HeaderFields = new List<GeneralField>()
                                                                  {
                                                                      new GeneralField(),
                                                                      new GeneralField(),
                                                                      new GeneralField()
                                                                  },
                                                   PrimaryFields = new List<GeneralField>()
                                                                   {
                                                                       new GeneralField
                                                                       {
                                                                           IsMarkedField = true,
                                                                           Label = "СТ. ОТПРАВЛЕНИЯ",
                                                                           Value = "МИНСК",
                                                                           IsDynamicValue = true
                                                                       },
                                                                       new GeneralField
                                                                       {
                                                                           IsMarkedField = true,
                                                                           Label = "СТ. НАЗНАЧЕНИЯ",
                                                                           Value = "МОСКВА",
                                                                           IsDynamicValue = true
                                                                       }
                                                                   },
                                                   AuxiliaryFields = new List<GeneralField>(5)
                                                                     {
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "ОТПРАВЛЕНИЕ",
                                                                             Value = "05/03/15 19:02",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "ПРИБЫТИЕ",
                                                                             Value = "06/03/15 05:09",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   SecondaryFields = new List<GeneralField>()
                                                                     {
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "ПАССАЖИР",
                                                                             Value = "ИВАНОВ ИВАН",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "№ ВАГОНА",
                                                                             Value = "3",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "МЕСТО",
                                                                             Value = "10",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField()
                                                                         },
                                                   BackFields = new List<GeneralField>()
                                                                {
                                                                    new GeneralField()
                                                                },
                                                   TransitType = Transit.Train
                                               }
                            };


            return View("_PassDesigner", passModel);
        }

        public ActionResult CouponsStrategyCard()
        {
            var passModel = new PassTemplateViewModel
                            {
                                OrganizationName = "Passlight",
                                PassDescription = "Купон для сети магазинов Clever",
                                PassStyle = PassStyle.Coupon,
                                BackgroundColor = "#004460",
                                LabelTextColor = "#000000",
                                ValueTextColor = "#ffffff",
                                LogoText = "",
                                LocationDetails = new LocationDetails()
                                {
                                    Locations = new List<GeneralLocation>()
                                                  {
                                                      new GeneralLocation()
                                                  }
                                },
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
                                                   HeaderFields = new List<GeneralField>()
                                                                  {
                                                                      new GeneralField(),
                                                                      new GeneralField(),
                                                                      new GeneralField()
                                                                  },
                                                   PrimaryFields = new List<GeneralField>()
                                                                   {
                                                                       new GeneralField
                                                                       {
                                                                           IsMarkedField = true,
                                                                           Label = "НА КАЖДУЮ ВТОРУЮ ВЕЩЬ",
                                                                           Value = "-40%",
                                                                           IsDynamicValue = true
                                                                       },
                                                                       new GeneralField()
                                                                   },
                                                   AuxiliaryFields = new List<GeneralField>(5)
                                                                     {
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "НОМЕР КЛИЕНТА",
                                                                             Value = "02387",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "ПЕРИОД ДЕЙСТВИЯ",
                                                                             Value = "01.09 - 31.09.2014",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   SecondaryFields = new List<GeneralField>()
                                                                     {
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   BackFields = new List<GeneralField>()
                                                                {
                                                                    new GeneralField()
                                                                },
                                                   TransitType = Transit.Generic
                                               }
                            };
            return View("_PassDesigner", passModel);
        }

        public ActionResult EventTicketStrategyCard()
        {
            var passModel = new PassTemplateViewModel
                            {
                                PassStyle = PassStyle.Coupon,
                                BackgroundColor = "#004460",
                                LabelTextColor = "#000000",
                                ValueTextColor = "#ffffff",
                                LogoText = "",
                                LocationDetails = new LocationDetails()
                                {
                                    Locations = new List<GeneralLocation>()
                                                  {
                                                      new GeneralLocation()
                                                  }
                                },
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
                                                   HeaderFields = new List<GeneralField>()
                                                                  {
                                                                      new GeneralField(),
                                                                      new GeneralField(),
                                                                      new GeneralField()
                                                                  },
                                                   PrimaryFields = new List<GeneralField>()
                                                                   {
                                                                       new GeneralField(),
                                                                       new GeneralField()
                                                                   },
                                                   AuxiliaryFields = new List<GeneralField>(5)
                                                                     {
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   SecondaryFields = new List<GeneralField>()
                                                                     {
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   BackFields = new List<GeneralField>()
                                                                {
                                                                    new GeneralField()
                                                                },
                                                   TransitType = Transit.Generic
                                               }
                            };
            return View("_PassDesigner", passModel);
        }

        public ActionResult MembershipStrategyCard()
        {
            var passModel = new PassTemplateViewModel
                            {
                                OrganizationName = "Passlight",
                                PassDescription = "Купон для сети магазинов Clever",
                                PassStyle = PassStyle.Generic,
                                BackgroundColor = "#004460",
                                LabelTextColor = "#000000",
                                ValueTextColor = "#ffffff",
                                LogoText = "",
                                LocationDetails = new LocationDetails()
                                {
                                    Locations = new List<GeneralLocation>()
                                                  {
                                                      new GeneralLocation()
                                                  }
                                },
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
                                                   HeaderFields = new List<GeneralField>()
                                                                  {
                                                                      new GeneralField
                                                                      {
                                                                          IsMarkedField = true,
                                                                          Label = "БАЛЛЫ",
                                                                          Value = "300",
                                                                          IsDynamicValue = true
                                                                      },
                                                                      new GeneralField(),
                                                                      new GeneralField()
                                                                  },
                                                   PrimaryFields = new List<GeneralField>()
                                                                   {
                                                                       new GeneralField
                                                                       {
                                                                           IsMarkedField = true,
                                                                           Label = "Ковганко Дмитрий",
                                                                           Value = "ИМЯ",
                                                                           IsDynamicValue = true
                                                                       },
                                                                       new GeneralField()
                                                                   },
                                                   AuxiliaryFields = new List<GeneralField>(5)
                                                                     {
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   SecondaryFields = new List<GeneralField>()
                                                                     {
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "НОМЕР КЛИЕНТА",
                                                                             Value = "00123",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField
                                                                         {
                                                                             IsMarkedField = true,
                                                                             Label = "ТИП КАРТЫ",
                                                                             Value = "ЗОЛОТАЯ",
                                                                             IsDynamicValue = true
                                                                         },
                                                                         new GeneralField(),
                                                                         new GeneralField()
                                                                     },
                                                   BackFields = new List<GeneralField>()
                                                                {
                                                                    new GeneralField()
                                                                },
                                                   TransitType = Transit.Generic
                                               }
                            };
            return View("_PassDesigner", passModel);
        }
    }
}