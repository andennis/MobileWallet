using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Common.Extensions;
using FileStorage.Core;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;
using Pass.Manager.Web.Models.GeneralPassTemplate;


namespace Pass.Manager.Web.Controllers
{
    public class PassDesignerController : BaseController
    {
        private readonly IPassProjectService _passProjectService;
        private readonly IFileStorageService _fileStorageService;

        public PassDesignerController(IPassProjectService passProjectService, IFileStorageService fileStorageService)
        {
            _passProjectService = passProjectService;
            _fileStorageService = fileStorageService;
        }

        //public ActionResult Edit(int passProjectId)
        //{
        //    PassProject prj = _passProjectService.Get(passProjectId);

        //    PassTemplateViewModel model;
        //    if (prj.PassContentId.HasValue)
        //    {
        //        string path = _fileStorageService.GetStorageItemPath(prj.PassContentId.Value) + "\\XMLData.xml";
        //        model = path.LoadFromXml<PassTemplateViewModel>();
        //    }
        //    else
        //    {
        //        model = GetInitialModel(prj.ProjectType, passProjectId);
        //    }

        //    return View("_PassDesigner", model);
        //}

        public ActionResult Edit(int passProjectId)
        {
            PassProject prj = _passProjectService.Get(passProjectId);

            PassTemplateViewModel model;
            if (prj.PassContentId.HasValue)
            {
                string path = _fileStorageService.GetStorageItemPath(prj.PassContentId.Value) + "\\XMLData.xml";
                model = path.LoadFromXml<PassTemplateViewModel>();
                model.TemplatePath = Path.GetDirectoryName(path);
            }
            else
            {
                model = GetInitialModel(prj.ProjectType, passProjectId);
            }

            return View("_PassDesigner", model);
        }

        [HttpPost]
        public ActionResult Edit(PassTemplateViewModel model)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                PassProject prj = _passProjectService.Get(model.PassProjectId);
                string path;
                if (prj.PassContentId.HasValue)
                {
                    path = _fileStorageService.GetStorageItemPath(prj.PassContentId.Value);
                }
                else
                {
                    prj.PassContentId = _fileStorageService.CreateStorageFolder(out path);
                    _passProjectService.Update(prj);
                }

                model.SaveToXml(path + "\\XMLData.xml");
                return RedirectToAction("Edit", "PassProject", new { id = model.PassProjectId });
            }
            return View("_PassDesigner", model);
        }

        public void TestForm(PassTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                //model.Logo.SaveAs("D:\\logo.png");
                Console.WriteLine("");
            }
        }

        //[HttpPost]
        //public ActionResult Edit()
        //{
        //    var model = new PassTemplateViewModel();
        //    if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        var jsonData = System.Web.HttpContext.Current.Request.Files["jsonData"];
        //        var logo = System.Web.HttpContext.Current.Request.Files["logo"];
        //        var strip = System.Web.HttpContext.Current.Request.Files["strip"];
        //        var background = System.Web.HttpContext.Current.Request.Files["background"];
        //        var thumbnail = System.Web.HttpContext.Current.Request.Files["thumbnail"];
        //        if (jsonData != null)
        //        {
        //            var json = new StreamReader(jsonData.InputStream).ReadToEnd();
        //            model = json.JsonToObject<PassTemplateViewModel>();
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            PassProject prj = _passProjectService.Get(model.PassProjectId);
        //            string path;
        //            if (prj.PassContentId.HasValue)
        //            {
        //                path = _fileStorageService.GetStorageItemPath(prj.PassContentId.Value);
        //            }
        //            else
        //            {
        //                prj.PassContentId = _fileStorageService.CreateStorageFolder(out path);
        //                _passProjectService.Update(prj);
        //            }

        //            model.SaveToXml(path + "\\XMLData.xml");
        //            if (logo != null)
        //            {
        //                logo.SaveAs(path + "\\Images\\logo.png");
        //            }
        //            if (strip != null)
        //            {
        //                strip.SaveAs(path + "\\strip.png");
        //            }
        //            if (background != null)
        //            {
        //                background.SaveAs(path + "\\background.png");
        //            }
        //            if (thumbnail != null)
        //            {
        //                thumbnail.SaveAs(path + "\\thumbnail.png");
        //            }
        //            return RedirectToAction("Edit", "PassProject", new { id = model.PassProjectId });
        //        }
        //    }
        //    return View("_PassDesigner", model);
        //}

        public ActionResult GetImage(string path, string imageName)
        {
            return File(path + imageName, "image/png");
        }

        private PassTemplateViewModel GetInitialModel(PassProjectType projectType, int passProjectId)
        {
            return new PassTemplateViewModel
            {
                PassProjectId = passProjectId,
                PassStyle = Mapper.Map<PassProjectType, PassStyle>(projectType),

                //BackgroundColor = Color.FromArgb(0, 68, 96),
                //LabelTextColor = Color.FromArgb(0, 0, 0),
                //ValueTextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = "#004460",
                LabelTextColor = "#000000",
                ValueTextColor = "#ffffff",
                //LogoText = "Clever",
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
        }

        public ActionResult EmptyCardDesigner()
        {
            var passModel = new PassTemplateViewModel
            {
                PassStyle = PassStyle.Coupon,
                //BackgroundColor = Color.FromArgb(0, 68, 96),
                //LabelTextColor = Color.FromArgb(0, 0, 0),
                //ValueTextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = "#004460",
                LabelTextColor = "#000000",
                ValueTextColor = "#ffffff",
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
            var passModel = new PassTemplateViewModel
            {
                PassStyle = PassStyle.BoardingPass,
                //BackgroundColor = Color.FromArgb(0, 68, 96),
                //LabelTextColor = Color.FromArgb(0, 0, 0),
                //ValueTextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = "#004460",
                LabelTextColor = "#000000",
                ValueTextColor = "#ffffff",
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
            var passModel = new PassTemplateViewModel
            {
                OrganizationName = "Clever",
                PassDescription = "Купон для сети магазинов Clever",
                PassStyle = PassStyle.Coupon,
                //BackgroundColor = Color.FromArgb(0, 68, 96),
                //LabelTextColor = Color.FromArgb(0, 0, 0),
                //ValueTextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = "#004460",
                LabelTextColor = "#000000",
                ValueTextColor = "#ffffff",
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
            var passModel = new PassTemplateViewModel
            {
                PassStyle = PassStyle.Coupon,
                //BackgroundColor = Color.FromArgb(0, 68, 96),
                //LabelTextColor = Color.FromArgb(0, 0, 0),
                //ValueTextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = "#004460",
                LabelTextColor = "#000000",
                ValueTextColor = "#ffffff",
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
            var passModel = new PassTemplateViewModel
            {
                OrganizationName = "Clever",
                PassDescription = "Купон для сети магазинов Clever",
                PassStyle = PassStyle.Generic,
                //BackgroundColor = Color.FromArgb(255, 215, 0),
                //LabelTextColor = Color.FromArgb(0, 96, 8),
                //ValueTextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = "#004460",
                LabelTextColor = "#000000",
                ValueTextColor = "#ffffff",
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