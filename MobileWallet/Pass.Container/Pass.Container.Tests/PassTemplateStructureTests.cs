using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using NUnit.Framework;
using Pass.Container.Core.Entities.Templates.PassTemplate;


namespace Pass.Container.Tests
{
    [TestFixture]
    public class PassTemplateStructureTests
    {
        [Test]
        public void PassTemplateXmlSerializationTest()
        {
            PassTemplate generalTemplate = GetPassTemplateObject();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template.xml");
            if (File.Exists(path))
                File.Delete(path);
            generalTemplate.SaveToXml(path);
            Assert.IsTrue(File.Exists(path));

            var template = new PassTemplate();
            template = template.LoadFromXml(path);
            Assert.AreEqual(generalTemplate.TemplateName, template.TemplateName);
        }

        [Test]
        public void PassTemplateJsonSerializationTest()
        {
            PassTemplate generalTemplate = GetPassTemplateObject();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template.json");
            if (File.Exists(path))
                File.Delete(path);
            string json = generalTemplate.ObjectToJson();
            File.WriteAllText(path, json);
            Assert.IsTrue(File.Exists(path));

            string jsonResult = File.ReadAllText(path);
            PassTemplate template = jsonResult.JsonToObject<PassTemplate>();
            Assert.AreEqual(generalTemplate.TemplateName, template.TemplateName);
        }

        private PassTemplate GetPassTemplateObject()
        {
            var template = new PassTemplate
                {
                    //Standart keys
                    TemplateName = "Template name",
                    TemplateDescription = "Template description",
                    OrganizationName = "Organization name",
                    PassType = PassStyle.Coupon,
                    PassDescription = "Pass description",
                    PassSerialNumberType = PassSerialNumberType.AutoGgenerated,
                    PassCertificate = "pass.test.coupon",
                    TeamIdentifier = "TeamIdentifier",
                    //Visual Appearance Keys
                    BackgroundColor = Color.Blue,
                    LabelTextColor = Color.Red,
                    ValueTextColor = Color.CadetBlue,
                    SuppressStripShine = false,
                    //Others Keys
                    GroupingIdentifier = "Grouping identifier",
                    PassTimezone = null,
                    LogoText = "Logo text",
                    //Integration Details
                    IntegrationDetails = new IntegrationDetails
                        {
                            AppOptions = new AppOptions
                                {
                                    AppIdentifier = new List<int> { 1, 2 },
                                    AppLaunchUrl = "http://test.passtemplate.com",
                                    CustomJsonData = "Custom json data"
                                },
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
                            Locations = new List<Location>
                                {
                                    new Location
                                        {
                                            Altitude = 1,
                                            Latitude = 2,
                                            Longitude = 3,
                                            RelevantText = "Relevant text"
                                        },
                                        new Location
                                        {
                                            Altitude = 1,
                                            Latitude = 2,
                                            Longitude = 3,
                                            RelevantText = "Relevant text"
                                        }
                                },
                            MaxDistance = 10
                        },
                    //Beacon Details
                    BeaconDetails = new BeaconDetails
                        {
                            Beacons = new List<Beacon>
                               {
                                   new Beacon
                                       {
                                           Name = "Beacon name",
                                           Major = 1,
                                           Minor = 2,
                                           ProximityUuid = "ProximityUuid",
                                           RelevantText = "Relevant text"
                                       },
                                       new Beacon
                                       {
                                           Name = "Beacon name",
                                           Major = 1,
                                           Minor = 2,
                                           ProximityUuid = "ProximityUuid",
                                           RelevantText = "Relevant text"
                                       }
                               }
                        },
                    //Distribution Details
                    DistributionDetails = new DistributionDetails
                        {
                            AllPassesAsExpired = true,
                            DateRestriction = DateTime.Now,
                            ExpirationDate = DateTime.Now,
                            LimitPassPerUser = 15000,
                            PassLinkType = PassLinkType.Public,
                            PasswordToIssue = "PasswordToIssue",
                            PasswordToUpdate = "PasswordToUpdate",
                            QuantityRestriction = 100
                        },
                    //Barcode Details
                    BarcodeDetails = new BarcodeDetails
                        {
                            AlternativeText = AlternativeText.DisplayTheBarcodeContent,
                            BarcodeType = BarcodeType.AztecCode,
                            EncodedMessage = EncodedMessage.EncodeThePassUniqueId,
                            EncodingFormat = "Encoding.UTF8",
                            TextToDisplay = "Text to display",
                            TextToEncode = "Text to encode"

                        },
                    //Field Details
                    FieldDetails = new FieldDetails
                        {
                            AuxiliaryFields = new List<Field>
                                {
                                    new Field
                                        {
                                            AttributedValue = "AttributedValue",
                                            ChangeMessage = "ChangeMessage",
                                            CurrencyCode = "CurrencyCode",
                                            DataDetectorTypes = new List<Field.DataDetector>
                                                {
                                                    Field.DataDetector.Address,
                                                    Field.DataDetector.Link
                                                },
                                                DateStyle = Field.DateStyleType.Long,
                                                DefaultValue = "DefaultValue",
                                                IgnoresTimeZone = true,
                                                IsDynamic = true,
                                                IsRelative = true,
                                                Key = "Key",
                                                Label = "label",
                                                Value = "value",
                                                Type = Field.DataType.Text
                                        },
                                        new Field
                                        {
                                            AttributedValue = "AttributedValue",
                                            ChangeMessage = "ChangeMessage",
                                            CurrencyCode = "CurrencyCode",
                                            DataDetectorTypes = new List<Field.DataDetector>
                                                {
                                                    Field.DataDetector.Address,
                                                    Field.DataDetector.Link
                                                },
                                                DateStyle = Field.DateStyleType.Long,
                                                DefaultValue = "DefaultValue",
                                                IgnoresTimeZone = true,
                                                IsDynamic = true,
                                                IsRelative = true,
                                                Key = "Key",
                                                Label = "label",
                                                Value = "value",
                                                Type = Field.DataType.Text
                                        }
                                },
                            BackFields = new List<Field>
                                    {
                                        new Field
                                        {
                                            AttributedValue = "AttributedValue",
                                            ChangeMessage = "ChangeMessage",
                                            CurrencyCode = "CurrencyCode",
                                            DataDetectorTypes = new List<Field.DataDetector>
                                                {
                                                    Field.DataDetector.Address,
                                                    Field.DataDetector.Link
                                                },
                                                DateStyle = Field.DateStyleType.Long,
                                                DefaultValue = "DefaultValue",
                                                IgnoresTimeZone = true,
                                                IsDynamic = true,
                                                IsRelative = true,
                                                Key = "Key",
                                                Label = "label",
                                                Value = "value",
                                                Type = Field.DataType.Text
                                        }
                                    },
                            HeaderFields = new List<Field>
                                        {
                                            new Field
                                        {
                                            AttributedValue = "AttributedValue",
                                            ChangeMessage = "ChangeMessage",
                                            CurrencyCode = "CurrencyCode",
                                            DataDetectorTypes = new List<Field.DataDetector>
                                                {
                                                    Field.DataDetector.Address,
                                                    Field.DataDetector.Link
                                                },
                                                DateStyle = Field.DateStyleType.Long,
                                                DefaultValue = "DefaultValue",
                                                IgnoresTimeZone = true,
                                                IsDynamic = true,
                                                IsRelative = true,
                                                Key = "Key",
                                                Label = "label",
                                                Value = "value",
                                                Type = Field.DataType.Text
                                        }
                                        },
                            PrimaryFields = new List<Field>
                                            {
                                                new Field
                                        {
                                            AttributedValue = "AttributedValue",
                                            ChangeMessage = "ChangeMessage",
                                            CurrencyCode = "CurrencyCode",
                                            DataDetectorTypes = new List<Field.DataDetector>
                                                {
                                                    Field.DataDetector.Address,
                                                    Field.DataDetector.Link
                                                },
                                                DateStyle = Field.DateStyleType.Long,
                                                DefaultValue = "DefaultValue",
                                                IgnoresTimeZone = true,
                                                IsDynamic = true,
                                                IsRelative = true,
                                                Key = "Key",
                                                Label = "label",
                                                Value = "value",
                                                Type = Field.DataType.Text
                                        }
                                            },
                            SecondaryFields = new List<Field>
                                                {
                                                    new Field{
                                            AttributedValue = "AttributedValue",
                                            ChangeMessage = "ChangeMessage",
                                            CurrencyCode = "CurrencyCode",
                                            DataDetectorTypes = new List<Field.DataDetector>
                                                {
                                                    Field.DataDetector.Address,
                                                    Field.DataDetector.Link
                                                },
                                                DateStyle = Field.DateStyleType.Long,
                                                DefaultValue = "DefaultValue",
                                                IgnoresTimeZone = true,
                                                IsDynamic = true,
                                                IsRelative = true,
                                                Key = "Key",
                                                Label = "label",
                                                Value = "value",
                                                Type = Field.DataType.Text
                                                    }
                                                },
                            TransitType = Transit.Air
                        }
                };
            return template;
        }
    }
}
