using System;
using System.Collections.Generic;
using System.Drawing;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;

namespace Pass.Container.BL.Tests
{
    public static class TestHelper
    {
        public static IPassContainerConfig PassContainerConfig { get { return new PassContainerConfig(); } }

        //private class TestPassContainerConfig : IPassContainerConfig
        //{
        //    public string PassTemplateFolderName
        //    {
        //        get { return "PassTemplateFolderName"; }
        //    }
        //    public string PassTemplateFileName
        //    {
        //        get { return "PassTemplateFileName"; }
        //    }
        //    public string ConnectionString
        //    {
        //        get { return "MobileWalletConnection"; }
        //    }
        //}

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
                                                Key = "Key1",
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
                                                Key = "Key2",
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
                                                Key = "Key3",
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
                                                Key = "Key4",
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
                                                Key = "Key5",
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
                                                Key = "Key6",
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
