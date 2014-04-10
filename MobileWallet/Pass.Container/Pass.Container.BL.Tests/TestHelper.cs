using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Common.Extensions;
using FileStorage.BL;
using FileStorage.BL.Tests;
using FileStorage.Core;
using FileStorage.Repository.Core;
using FileStorage.Repository.EF;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.EF;

namespace Pass.Container.BL.Tests
{
    public static class TestHelper
    {
        private const string AppleIconFileName = "icon.png";
        public static IPassContainerConfig PassContainerConfig { get { return new PassContainerConfig(); } }
        public static IPassContainerUnitOfWork PassContainerUnitOfWork { get { return new PassContainerUnitOfWork(new PassContainerConfig()); } }
        public static IFileStorageUnitOfWork FileStorageUnitOfWork { get { return new FileStorageUnitOfWork(new FileStorageConfig()); } }

        public static GeneralPassTemplate PreparePassTemplateSource(string testPassTemplateDir, string passTemplateFileName)
        {
            //Prepare pass template source
            GeneralPassTemplate generalTemplate = GetPassTemplateObject();
            //generalTemplate.FieldDetails.AuxiliaryFields.Add(new GeneralField
            //        {
            //            Key = "TestDynamicField",
            //            Value = "TestDynamicFieldValue",
            //            Type = GeneralField.DataType.Text,
            //            IsDynamicValue = true
            //        });

            string path = Path.Combine(testPassTemplateDir, passTemplateFileName);
            if (File.Exists(path))
                File.Delete(path);

            generalTemplate.SaveToXml(path);
            File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassFiles", "Apple", AppleIconFileName), Path.Combine(testPassTemplateDir, AppleIconFileName));
            return generalTemplate;
        }

        public static GeneralPassTemplate GetPassTemplateObject()
        {
            var template = new GeneralPassTemplate
            {
                //Standart keys
                TemplateName = "Template name",
                TemplateDescription = "Template description",
                OrganizationName = "Organization name",
                PassStyle = PassStyle.Coupon,
                PassDescription = "Pass description",
                PassSerialNumberType = PassSerialNumberType.AutoGgenerated,
                PassTypeIdentifier = "pass.com.passlight.dev.test",
                TeamIdentifier = "YHQB764QFA",
                
                //Visual Appearance Keys
                BackgroundColor = Color.Blue,
                LabelTextColor = Color.Red,
                ValueTextColor = Color.CadetBlue,
                //Others Keys
                LogoText = "Logo text",




                FieldDetails = null



                //Integration Details
                //IntegrationDetails = new IntegrationDetails
                //{
                //   CallbackNotifications = new CallbackNotifications
                //    {
                //        PassIssued = "passIssued@mail.com",
                //        PassRegistered = "passRegistered@mail.com",
                //        PassUnregistered = "passUnregistered@mail.com",
                //        PassUpdated = "passUpdated@mail.com"
                //    }
                //},
                ////Location Details
                //LocationDetails = new LocationDetails
                //{
                //    Locations = new List<GeneralLocation>
                //                {
                //                    new GeneralLocation
                //                        {
                //                            Latitude = 53.883980386469425,
                //                            Longitude = 27.59497405000002,
                //                            RelevantText = "Relevant text"
                //                        }
                //                }
                //},
                ////Barcode Details
                //BarcodeDetails = new BarcodeDetails
                //{
                //    AlternativeText = AlternativeText.DisplayTheBarcodeContent,
                //    BarcodeType = GeneralBarcodeType.AztecCode,
                //    EncodedMessage = EncodedMessage.EncodeThePassUniqueId,
                //    EncodingFormat = "Encoding.UTF8",
                //    TextToDisplay = "Text to display",
                //    TextToEncode = "Text to encode"

                //},
                ////Field Details
                //FieldDetails = new FieldDetails
                //{
                //    AuxiliaryFields = new List<GeneralField>
                //                {
                //                    new GeneralField
                //                        {
                //                            AttributedValue = "AttributedValue",
                //                            ChangeMessage = "ChangeMessage",
                //                            CurrencyCode = "CurrencyCode",
                //                            DataDetectorTypes = new List<GeneralField.DataDetector>
                //                                {
                //                                    GeneralField.DataDetector.Address,
                //                                    GeneralField.DataDetector.Link
                //                                },
                //                                DateStyle = GeneralField.DateStyleType.Long,
                //                                IsDynamicValue = true,
                //                                IsRelative = true,
                //                                Key = "Key1",
                //                                Label = "label",
                //                                Value = "value",
                //                                Type = GeneralField.DataType.Text
                //                        },
                //                        new GeneralField
                //                        {
                //                            AttributedValue = "AttributedValue",
                //                            ChangeMessage = "ChangeMessage",
                //                            CurrencyCode = "CurrencyCode",
                //                            DataDetectorTypes = new List<GeneralField.DataDetector>
                //                                {
                //                                    GeneralField.DataDetector.Address,
                //                                    GeneralField.DataDetector.Link
                //                                },
                //                                DateStyle = GeneralField.DateStyleType.Long,
                //                                IsDynamicValue = true,
                //                                IsRelative = true,
                //                                Key = "Key2",
                //                                Label = "label",
                //                                Value = "value",
                //                                Type = GeneralField.DataType.Text
                //                        }
                //                },
                //    BackFields = new List<GeneralField>
                //                    {
                //                        new GeneralField
                //                        {
                //                            AttributedValue = "AttributedValue",
                //                            ChangeMessage = "ChangeMessage",
                //                            CurrencyCode = "CurrencyCode",
                //                            DataDetectorTypes = new List<GeneralField.DataDetector>
                //                                {
                //                                    GeneralField.DataDetector.Address,
                //                                    GeneralField.DataDetector.Link
                //                                },
                //                                DateStyle = GeneralField.DateStyleType.Long,
                //                                IgnoresTimeZone = true,
                //                                IsDynamicValue = true,
                //                                IsRelative = true,
                //                                Key = "Key3",
                //                                Label = "label",
                //                                Value = "value",
                //                                Type = GeneralField.DataType.Text
                //                        }
                //                    },
                //    HeaderFields = new List<GeneralField>
                //                        {
                //                            new GeneralField
                //                        {
                //                            AttributedValue = "AttributedValue",
                //                            ChangeMessage = "ChangeMessage",
                //                            CurrencyCode = "CurrencyCode",
                //                            DataDetectorTypes = new List<GeneralField.DataDetector>
                //                                {
                //                                    GeneralField.DataDetector.Address,
                //                                    GeneralField.DataDetector.Link
                //                                },
                //                                DateStyle = GeneralField.DateStyleType.Long,
                //                                IgnoresTimeZone = true,
                //                                IsDynamicValue = true,
                //                                IsRelative = true,
                //                                Key = "Key4",
                //                                Label = "label",
                //                                Value = "value",
                //                                Type = GeneralField.DataType.Text
                //                        }
                //                        },
                //    PrimaryFields = new List<GeneralField>
                //                            {
                //                                new GeneralField
                //                        {
                //                            AttributedValue = "AttributedValue",
                //                            ChangeMessage = "ChangeMessage",
                //                            CurrencyCode = "CurrencyCode",
                //                            DataDetectorTypes = new List<GeneralField.DataDetector>
                //                                {
                //                                    GeneralField.DataDetector.Address,
                //                                    GeneralField.DataDetector.Link
                //                                },
                //                                DateStyle = GeneralField.DateStyleType.Long,
                //                                IgnoresTimeZone = true,
                //                                IsDynamicValue = true,
                //                                IsRelative = true,
                //                                Key = "Key5",
                //                                Label = "label",
                //                                Value = "value",
                //                                Type = GeneralField.DataType.Text
                //                        }
                //                            },
                //    SecondaryFields = new List<GeneralField>
                //                                {
                //                                    new GeneralField{
                //                            AttributedValue = "AttributedValue",
                //                            ChangeMessage = "ChangeMessage",
                //                            CurrencyCode = "CurrencyCode",
                //                            DataDetectorTypes = new List<GeneralField.DataDetector>
                //                                {
                //                                    GeneralField.DataDetector.Address,
                //                                    GeneralField.DataDetector.Link
                //                                },
                //                                DateStyle = GeneralField.DateStyleType.Long,
                //                                IgnoresTimeZone = true,
                //                                IsDynamicValue = true,
                //                                IsRelative = true,
                //                                Key = "Key6",
                //                                Label = "label",
                //                                Value = "value",
                //                                Type = GeneralField.DataType.Text
                //                                    }
                //                                }
                //}
            };
            return template;
        }

        public static void ClearFileStorage(IFileStorageConfig fsConfig)
        {
            FsTestHelper.ClearFileStorage(fsConfig);
        }
    }
}
