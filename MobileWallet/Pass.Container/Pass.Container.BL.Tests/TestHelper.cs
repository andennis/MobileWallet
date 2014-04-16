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
                                   BackgroundColor = Color.Lavender,
                                   LabelTextColor = Color.Teal,
                                   ValueTextColor = Color.SlateGray,
                                   //Others Keys
                                   LogoText = "Passlight",

                                   //Integration Details
                                   IntegrationDetails = new IntegrationDetails
                                                            {
                                                                CallbackNotifications = new CallbackNotifications
                                                                                            {
                                                                                                PassIssued = "passIssued@mail.com",
                                                                                                PassRegistered = "passRegistered@mail.com",
                                                                                                PassUnregistered = "passUnregistered@mail.com",
                                                                                                PassUpdated = "passUpdated@mail.com"
                                                                                            }
                                                            },
                                   //Location Details
                                   LocationDetails = new LocationDetails
                                                         {
                                                             Locations = new List<GeneralLocation>
                                                                             {
                                                                                 new GeneralLocation
                                                                                     {
                                                                                         Latitude = 53.883980386469425,
                                                                                         Longitude = 27.59497405000002,
                                                                                         RelevantText = "Relevant text"
                                                                                     }
                                                                             }
                                                         },
                                   //Barcode Details
                                   BarcodeDetails = new BarcodeDetails
                                                        {
                                                            AlternativeText = AlternativeText.DisplayTheBarcodeContent,
                                                            BarcodeType = GeneralBarcodeType.Pdf417Code,
                                                            EncodedMessage = EncodedMessage.EncodeThePassUniqueId,
                                                            EncodingFormat = "UTF-8",
                                                            TextToDisplay = "Some text",
                                                            TextToEncode = "Text to encode"
                                                        },
                                   //Distribution details
                                   DistributionDetails = new DistributionDetails
                                                             {
                                                                 ExpirationDate = DateTime.Now.AddMinutes(5).ToString(),
                                                                 AllPassesAsExpired = false
                                                             },
                                   //Field Details
                                   FieldDetails = new FieldDetails
                                                      {
                                                          AuxiliaryFields = new List<GeneralField>
                                                                                {
                                                                                    new GeneralField
                                                                                        {
                                                                                            ChangeMessage = "Gate changed to %@.",
                                                                                            Key = "Key1",
                                                                                            Label = "Field",
                                                                                            Value = "AuxiliaryField",
                                                                                            TextAlignment = GeneralField.TextAlignmentType.Right,
                                                                                            Type = GeneralField.DataType.Text
                                                                                        },
                                                                                    new GeneralField
                                                                                        {
                                                                                            Key = "Key2",
                                                                                            Label = "Created date",
                                                                                            Value = DateTime.Now.ToString(),
                                                                                            DateStyle = GeneralField.DateStyleType.Short,
                                                                                            IsRelative = false,
                                                                                            Type = GeneralField.DataType.Date
                                                                                        },
                                                                                    new GeneralField
                                                                                        {
                                                                                            //These keys are optional if the field’s value is a number; otherwise they are not allowed.
                                                                                            CurrencyCode = "USD",
                                                                                            Key = "Key3",
                                                                                            Label = "Balance",
                                                                                            Value = "10",
                                                                                            //Type = GeneralField.DataType.Text
                                                                                            Type = GeneralField.DataType.Currency
                                                                                        }
                                                                                },
                                                          BackFields = new List<GeneralField>
                                                                           {
                                                                              new GeneralField
                                                                                        {
                                                                                            AttributedValue = "<a href = \"http://example.com/customers/123\"> Edit my profile </a>",
                                                                                            ChangeMessage = "Gate changed to %@.",
                                                                                            Key = "Key4",
                                                                                            Label = "Field",
                                                                                            Value = "BackField",
                                                                                            TextAlignment = GeneralField.TextAlignmentType.Right,
                                                                                            Type = GeneralField.DataType.Text
                                                                                        },
                                                                                    new GeneralField
                                                                                        {
                                                                                            Key = "Key5",
                                                                                            Label = "Created date",
                                                                                            Value = DateTime.Now.ToString(),
                                                                                            DateStyle = GeneralField.DateStyleType.Short,
                                                                                            IsRelative = false,
                                                                                            Type = GeneralField.DataType.Date
                                                                                        },
                                                                                    new GeneralField
                                                                                        {
                                                                                            //These keys are optional if the field’s value is a number; otherwise they are not allowed.
                                                                                            CurrencyCode = "USD",
                                                                                            Key = "Key6",
                                                                                            Label = "Balance",
                                                                                            Value = "10",
                                                                                            //Type = GeneralField.DataType.Text
                                                                                            Type = GeneralField.DataType.Currency
                                                                                        }
                                                                           },
                                                          HeaderFields = new List<GeneralField>
                                                                             {
                                                                                    new GeneralField
                                                                                        {
                                                                                            ChangeMessage = "Gate changed to %@.",
                                                                                            Key = "Key7",
                                                                                            Label = "Field",
                                                                                            Value = "HeaderField",
                                                                                            TextAlignment = GeneralField.TextAlignmentType.Right,
                                                                                            Type = GeneralField.DataType.Text
                                                                                        }
                                                                             },
                                                          PrimaryFields = new List<GeneralField>
                                                                              {
                                                                                  new GeneralField
                                                                                        {
                                                                                            ChangeMessage = "Gate changed to %@.",
                                                                                            Key = "Key8",
                                                                                            Label = "Field",
                                                                                            Value = "PrimaryField",
                                                                                            TextAlignment = GeneralField.TextAlignmentType.Right,
                                                                                            Type = GeneralField.DataType.Text
                                                                                        }
                                                                              },
                                                          SecondaryFields = new List<GeneralField>
                                                                                {
                                                                                  new GeneralField
                                                                                        {
                                                                                            ChangeMessage = "Gate changed to %@.",
                                                                                            Key = "Key9",
                                                                                            Label = "Field",
                                                                                            Value = "SecondaryField",
                                                                                            TextAlignment = GeneralField.TextAlignmentType.Right,
                                                                                            Type = GeneralField.DataType.Text
                                                                                        }
                                                                                }
                                                      }
                               };
            return template;
        }

        public static void ClearFileStorage(IFileStorageConfig fsConfig)
        {
            FsTestHelper.ClearFileStorage(fsConfig);
        }
    }
}
