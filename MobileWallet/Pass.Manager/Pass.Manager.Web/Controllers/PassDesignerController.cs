using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using Pass.Manager.Web.Models.GeneralPassTemplate;


namespace Pass.Manager.Web.Controllers
{
    public class PassDesignerController : Controller
    {
        //
        // GET: /PassDesigner/
        public ActionResult EmptyCardDesigner()
        {
            var passModel = new GeneralPassTemplate
            {
                PassStyle = PassStyle.Coupon,
                BackgroundColor = Color.FromArgb(0, 68, 96),
                LabelTextColor = Color.FromArgb(0, 0, 0),
                ValueTextColor = Color.FromArgb(255, 255, 255),
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
                    TransitType = Transit.Generic
                }
            };
            return View("_PassDesigner", passModel);
        }

        public ActionResult TransitStrategyCard()
        {
            var passModel = new GeneralPassTemplate
            {
                PassStyle = PassStyle.BoardingPass,
                BackgroundColor = Color.FromArgb(0, 68, 96),
                LabelTextColor = Color.FromArgb(0, 0, 0),
                ValueTextColor = Color.FromArgb(255, 255, 255),
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
                    TransitType = Transit.Generic
                }
            };


            return View("_PassDesigner", passModel);
        }

        public ActionResult CouponsStrategyCard()
        {
            var passModel = new GeneralPassTemplate
            {
                OrganizationName = "Clever",
                PassDescription = "Купон для сети магазинов Clever",
                PassStyle = PassStyle.Coupon,
                BackgroundColor = Color.FromArgb(0, 68, 96),
                LabelTextColor = Color.FromArgb(0, 0, 0),
                ValueTextColor = Color.FromArgb(255, 255, 255),
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
                            Label = "-40%",
                            Value = "на каждую вторую вещь",
                            IsDynamicValue = true
                        },
                        new GeneralField()
                    },
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
                    TransitType = Transit.Generic
                }
            };
            return View("_PassDesigner", passModel);
        }

        public ActionResult EventTicketStrategyCard()
        {
            var passModel = new GeneralPassTemplate
            {
                PassStyle = PassStyle.Coupon,
                BackgroundColor = Color.FromArgb(0, 68, 96),
                LabelTextColor = Color.FromArgb(0, 0, 0),
                ValueTextColor = Color.FromArgb(255, 255, 255),
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
                    TransitType = Transit.Generic
                }
            };
            return View("_PassDesigner", passModel);
        }

        public ActionResult MembershipStrategyCard()
        {
            var passModel = new GeneralPassTemplate
            {
                OrganizationName = "Clever",
                PassDescription = "Купон для сети магазинов Clever",
                PassStyle = PassStyle.Generic,
                BackgroundColor = Color.FromArgb(255, 215, 0),
                LabelTextColor = Color.FromArgb(0, 96, 8),
                ValueTextColor = Color.FromArgb(255, 255, 255),
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
                    TransitType = Transit.Generic
                }
            };
            return View("_PassDesigner", passModel);
        }
	}
}