﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;
using Common.Utils;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.FieldDictionaryKeys;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.LowerLevelKeys;

namespace Pass.Container.BL.PassTemplateGenerators
{
    public class ApplePassTemplateGenerator : IPassTemplateGenerator
    {
        /*
        private readonly static List<string> _applePassTemplateFiles = new List<string> 
                                                                    { 
                                                                        "logo.png", 
                                                                        "icon.png", 
                                                                        "strip.png", 
                                                                        "background.png", 
                                                                        "thumbnail.png",
                                                                        "footer.png",
                                                                        "logo@2x.png", 
                                                                        "icon@2x.png", 
                                                                        "strip@2x.png", 
                                                                        "background@2x.png", 
                                                                        "thumbnail@2x.png",
                                                                        "footer@2x.png"
                                                                    };
        */

        #region IPassTemplateGenerator

        public ClientType ClientType
        {
            get { return ClientType.Apple; }
        }

        public void Generate(GeneralPassTemplate generalTemplate, IEnumerable<string> imageFiles, string dstTemplateFilesPath)
        {
            if (generalTemplate == null)
                throw new ArgumentNullException("generalTemplate");
            if (dstTemplateFilesPath == null)
                throw new ArgumentNullException("dstTemplateFilesPath");

            //Build Apple pass template
            ApplePassTemplate applePassTemplate = CreateApplePassTemplate(generalTemplate);
            string passContent = applePassTemplate.ObjectToJson();
            string passContentFile = Path.Combine(dstTemplateFilesPath, ApplePass.PassTemplateFileName);
            File.WriteAllText(passContentFile, passContent);

            //Build manifest for images
            if (imageFiles != null && imageFiles.Any())
            {
                string manifest = BuildManifest(imageFiles);
                string manifestFilePath = Path.Combine(dstTemplateFilesPath, ApplePass.ManifestTemplateFileName);
                File.WriteAllText(manifestFilePath, manifest);

                //Copy image files
                string dstImageFilesPath = Path.Combine(dstTemplateFilesPath, ApplePass.TemplateImageFolder);
                FileHelper.CopyFilesToDirectory(imageFiles, dstImageFilesPath, true);
            }
        }

        private string BuildManifest(IEnumerable<string> files)
        {
            var dictManifest = new Dictionary<string, string>();
            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                byte[] fileData = File.ReadAllBytes(filePath);
                string fileHash = Crypto.CalculateHash(fileData);
                dictManifest.Add(fileName, fileHash);
            }
            return dictManifest.ObjectToJson();
        }

        #endregion

        #region Create ApplePassTemplate
        private ApplePassTemplate CreateApplePassTemplate(GeneralPassTemplate passTemplate)
        {
            var applePassTemplate = new ApplePassTemplate();

            //Standard Keys
            applePassTemplate.Description = passTemplate.PassDescription;
            applePassTemplate.FormatVersion = 1;
            applePassTemplate.OrganizationName = passTemplate.OrganizationName;
            applePassTemplate.PassTypeIdentifier = passTemplate.PassTypeIdentifier;
            applePassTemplate.TeamIdentifier = passTemplate.TeamIdentifier;
            applePassTemplate.SerialNumber = ApplePass.FieldSerialNumber;
            applePassTemplate.WebServiceUrl = ApplePass.FieldWebServiceUrl;
            applePassTemplate.AuthenticationToken = ApplePass.FieldAuthToken;
            
            //Visual Appearance Keys
            if (passTemplate.BackgroundColor.HasValue)
                applePassTemplate.BackgroundColor = "rgb(" + passTemplate.BackgroundColor.Value.R + ", " + passTemplate.BackgroundColor.Value.G + ", " + passTemplate.BackgroundColor.Value.B + ")";

            if (passTemplate.ValueTextColor.HasValue)
                applePassTemplate.ForegroundColor = "rgb(" + passTemplate.ValueTextColor.Value.R + ", " + passTemplate.ValueTextColor.Value.G + ", " + passTemplate.ValueTextColor.Value.B + ")";

            if (passTemplate.LabelTextColor.HasValue)
                applePassTemplate.LabelColor = "rgb(" + passTemplate.LabelTextColor.Value.R + ", " + passTemplate.LabelTextColor.Value.G + ", " + passTemplate.LabelTextColor.Value.B + ")";

            if (!string.IsNullOrEmpty(passTemplate.LogoText))
                applePassTemplate.LogoText = passTemplate.LogoText;
            if (passTemplate.SuppressStripShine != null)
                applePassTemplate.SuppressStripShine = passTemplate.SuppressStripShine;
            if (passTemplate.GroupingIdentifier != null)
                applePassTemplate.GroupingIdentifier = passTemplate.GroupingIdentifier;

            //1.Barcode Dictionary
            if (passTemplate.BarcodeDetails != null && passTemplate.BarcodeDetails.BarcodeType != GeneralBarcodeType.DoNotDisplay)
            {
                var barcode = new Barcode
                                  {
                                      Format = GetAppleBarcodeType(passTemplate.BarcodeDetails.BarcodeType),
                                      Message = passTemplate.BarcodeDetails.TextToEncode,
                                      MessageEncoding = passTemplate.BarcodeDetails.EncodingFormat
                                  };
                if (!string.IsNullOrEmpty(passTemplate.BarcodeDetails.TextToDisplay))
                    barcode.AltText = passTemplate.BarcodeDetails.TextToDisplay;
                applePassTemplate.Barcode = barcode;
            }

            /*
            //Associated App Keys
            //Companion App Keys
            if (passTemplate.IntegrationDetails != null && passTemplate.IntegrationDetails.AppOptions != null)
            {
                applePassTemplate.AppLaunchUrl = passTemplate.IntegrationDetails.AppOptions.AppLaunchUrl;
                applePassTemplate.AssociatedStoreIdentifiers = passTemplate.IntegrationDetails.AppOptions.AppIdentifier;
                applePassTemplate.UserInfo = passTemplate.IntegrationDetails.AppOptions.CustomJsonData;
            }
            */

            /*
            //Expiration Keys
            if (passTemplate.DistributionDetails != null)
            {
                if (passTemplate.DistributionDetails.ExpirationDate.HasValue)
                    applePassTemplate.ExpirationDate = passTemplate.DistributionDetails.ExpirationDate.Value.ToString(@"yyyy-MM-ddTHH\:mmzzz");

                if (passTemplate.DistributionDetails.AllPassesAsExpired != null)
                    applePassTemplate.Voided = passTemplate.DistributionDetails.AllPassesAsExpired;
            }
            */

            //Relevance Keys
            //1.Beacon Dictionary
            if (passTemplate.BeaconDetails != null)
            {
                var beacons = new List<Beacon>();
                foreach (GeneralBeacon beacon in passTemplate.BeaconDetails.Beacons)
                {
                    var appleBeacon = new Beacon { ProximityUuid = beacon.ProximityUuid };
                    if (beacon.Major.HasValue)
                        appleBeacon.Major = Convert.ToInt16(beacon.Major.Value);

                    if (beacon.Minor.HasValue)
                        appleBeacon.Minor = Convert.ToInt16(beacon.Minor.Value);

                    appleBeacon.RelevantText = beacon.RelevantText;

                    beacons.Add(appleBeacon);
                }
                applePassTemplate.Beacons = beacons;
            }
            //2.Location Dictionary
            if (passTemplate.LocationDetails != null)
            {
                var locations = new List<Location>();
                foreach (GeneralLocation location in passTemplate.LocationDetails.Locations)
                {
                    var appleLocation = new Location() { Latitude = location.Latitude, Longitude = location.Longitude };
                    if (location.Altitude != null)
                        appleLocation.Altitude = location.Altitude;
                    if (!string.IsNullOrEmpty(location.RelevantText))
                        appleLocation.RelevantText = location.RelevantText;
                    locations.Add(appleLocation);
                }
                applePassTemplate.Locations = locations;
                if (passTemplate.LocationDetails.MaxDistance != null)
                    applePassTemplate.MaxDistance = passTemplate.LocationDetails.MaxDistance;
            }

            //Style Keys
            switch (passTemplate.PassStyle)
            {
                case PassStyle.BoardingPass:
                    PassStructure passStructure = GetPassStructure(passTemplate);
                    passStructure.TransitType = GetAppleTransit(passTemplate.FieldDetails.TransitType);
                    applePassTemplate.BoardingPass = passStructure;
                    break;
                case PassStyle.Coupon:
                    applePassTemplate.Coupon = GetPassStructure(passTemplate);
                    break;
                case PassStyle.EventTicket:
                    applePassTemplate.EventTicket = GetPassStructure(passTemplate);
                    break;
                case PassStyle.Generic:
                    applePassTemplate.Generic = GetPassStructure(passTemplate);
                    break;
                case PassStyle.StoreCard:
                    applePassTemplate.StoreCard = GetPassStructure(passTemplate);
                    break;
            }

            return applePassTemplate;
        }
        private PassStructure GetPassStructure(GeneralPassTemplate passTemplate)
        {
            var passStructure = new PassStructure();

            if (passTemplate.FieldDetails == null)
                return passStructure;

            if (passTemplate.FieldDetails.AuxiliaryFields != null && passTemplate.FieldDetails.AuxiliaryFields.Any())
            {
                List<Field> auxiliaryFields = passTemplate.FieldDetails.AuxiliaryFields.Select(GetAppleField).ToList();
                passStructure.AuxiliaryFields = auxiliaryFields;
            }

            if (passTemplate.FieldDetails.BackFields != null && passTemplate.FieldDetails.BackFields.Any())
            {
                List<Field> backFields = passTemplate.FieldDetails.BackFields.Select(GetAppleField).ToList();
                passStructure.BackFields = backFields;
            }

            if (passTemplate.FieldDetails.HeaderFields != null && passTemplate.FieldDetails.HeaderFields.Any())
            {
                List<Field> headerFields = passTemplate.FieldDetails.HeaderFields.Select(GetAppleField).ToList();
                passStructure.HeaderFields = headerFields;
            }

            if (passTemplate.FieldDetails.PrimaryFields != null && passTemplate.FieldDetails.PrimaryFields.Any())
            {
                List<Field> primaryFields = passTemplate.FieldDetails.PrimaryFields.Select(GetAppleField).ToList();
                passStructure.PrimaryFields = primaryFields;
            }

            if (passTemplate.FieldDetails.SecondaryFields != null && passTemplate.FieldDetails.SecondaryFields.Any())
            {
                List<Field> secondaryFields = passTemplate.FieldDetails.SecondaryFields.Select(GetAppleField).ToList();
                passStructure.SecondaryFields = secondaryFields;
            }

            return passStructure;
        }
        private Field GetAppleField(GeneralField templatefield)
        {
            var field = new Field();
            if (!string.IsNullOrEmpty(templatefield.ChangeMessage))
                if (templatefield.ChangeMessage.Contains("%@"))
                {
                    field.ChangeMessage = templatefield.ChangeMessage;
                }
                else
                {
                    //TODO log 
                }

            //Data detectors are applied only to back fields
            if (templatefield.DataDetectorTypes != null && templatefield.DataDetectorTypes.Count > 0)
                field.DataDetectorTypes = GetAppleDataDetectorTypes(templatefield.DataDetectorTypes);

            field.Key = templatefield.Key;
            field.Label = (templatefield.IsDynamicLabel == true)
                ? string.Format(ApplePass.FieldLabelFormat, templatefield.Key) //"LB" + templatefield.Key + "$$";
                : templatefield.Label;

            field.Value = (templatefield.IsDynamicValue == true)
                ? string.Format(ApplePass.FieldValueFormat, templatefield.Key) //"VL$$" + templatefield.Key + "$$";
                : templatefield.Value;

            //This key is not allowed for primary fields
            if (templatefield.TextAlignment != null)
                field.TextAlignment = GetAppleTextAlligment(templatefield.TextAlignment);

            if (!string.IsNullOrEmpty(templatefield.AttributedValue))
            {
                if (templatefield.AttributedValue.Contains("href"))
                {
                    field.AttributedValue = templatefield.AttributedValue;
                }
                else
                {
                    //Only the <a> tag and its href attribute are supported
                    //TODO log 
                }
            }

            switch (templatefield.FieldType)
            {
                case GeneralField.DataType.Number:
                    field.NumberStyle = GetAppleNumberStyle(templatefield.NumberStyle);
                    break;

                case GeneralField.DataType.Date:
                    field.DateStyle = GetAppleDateStyle(templatefield.DateStyle);
                    field.IsRelative = templatefield.IsRelative;
                    field.Value = DateTime.Parse(templatefield.Value).ToString(@"yyyy-MM-ddTHH\:mmzzz");
                    break;

                case GeneralField.DataType.DateTime:
                    field.TimeStyle = GetAppleDateStyle(templatefield.TimeStyle);
                    field.IsRelative = templatefield.IsRelative;
                    field.Value = DateTime.Parse(templatefield.Value).ToString(@"yyyy-MM-ddTHH\:mmzzz");
                    break;

                case GeneralField.DataType.Currency:
                    field.CurrencyCode = templatefield.CurrencyCode;
                    field.Value = Int32.Parse(templatefield.Value);
                    break;
            }

            /*
            if (templatefield.FieldType == GeneralField.DataType.Number)
                field.NumberStyle = GetAppleNumberStyle(templatefield.NumberStyle);

            if (templatefield.FieldType == GeneralField.DataType.Date)
            {
                field.DateStyle = GetAppleDateStyle(templatefield.DateStyle);
                field.IsRelative = templatefield.IsRelative;
                field.Value = DateTime.Parse(templatefield.Value).ToString(@"yyyy-MM-ddTHH\:mmzzz");
            }
            if (templatefield.FieldType == GeneralField.DataType.DateTime)
            {
                field.TimeStyle = GetAppleDateStyle(templatefield.TimeStyle);
                field.IsRelative = templatefield.IsRelative;
                field.Value = DateTime.Parse(templatefield.Value).ToString(@"yyyy-MM-ddTHH\:mmzzz");
            }
            if (templatefield.FieldType == GeneralField.DataType.Currency)
            {
                field.CurrencyCode = templatefield.CurrencyCode;
                field.Value = Int32.Parse(templatefield.Value);
            }
            */
            return field;
        }
        #endregion

        #region Enum converters
        private PassStructure.Transit GetAppleTransit(Transit? transit)
        {
            switch (transit)
            {
                case Transit.Air:
                    return PassStructure.Transit.PkTransitTypeAir;
                case Transit.Boat:
                    return PassStructure.Transit.PkTransitTypeBoat;
                case Transit.Bus:
                    return PassStructure.Transit.PkTransitTypeBus;
                case Transit.Generic:
                    return PassStructure.Transit.PkTransitTypeGeneric;
                case Transit.Train:
                    return PassStructure.Transit.PkTransitTypeTrain;
            }
            return PassStructure.Transit.PkTransitTypeAir;
        }
        private BarcodeType GetAppleBarcodeType(GeneralBarcodeType? barcodeType)
        {
            switch (barcodeType)
            {
                case GeneralBarcodeType.Pdf417Code:
                    return BarcodeType.Pdf417Code;
                case GeneralBarcodeType.AztecCode:
                    return BarcodeType.AztecCode;
                case GeneralBarcodeType.QrCode:
                    return BarcodeType.QrCode;
            }
            return BarcodeType.Pdf417Code;
        }
        private Field.TextAlignmentType GetAppleTextAlligment(GeneralField.TextAlignmentType? textAlignmentType)
        {
            switch (textAlignmentType)
            {
                case GeneralField.TextAlignmentType.Center:
                    return Field.TextAlignmentType.PkTextAlignmentCenter;
                case GeneralField.TextAlignmentType.Left:
                    return Field.TextAlignmentType.PkTextAlignmentLeft;
                case GeneralField.TextAlignmentType.Right:
                    return Field.TextAlignmentType.PkTextAlignmentRight;
                case GeneralField.TextAlignmentType.Natural:
                    return Field.TextAlignmentType.PkTextAlignmentNatural;
            }
            return Field.TextAlignmentType.PkTextAlignmentLeft;
        }
        private Field.NumberStyleType GetAppleNumberStyle(GeneralField.NumberStyleType? numberStyleType)
        {
            switch (numberStyleType)
            {
                case GeneralField.NumberStyleType.Decimal:
                    return Field.NumberStyleType.PkNumberStyleDecimal;
                case GeneralField.NumberStyleType.Percent:
                    return Field.NumberStyleType.PkNumberStylePercent;
                case GeneralField.NumberStyleType.Scientific:
                    return Field.NumberStyleType.PkNumberStyleScientific;
                case GeneralField.NumberStyleType.SpellOut:
                    return Field.NumberStyleType.PkNumberStyleSpellOut;
            }
            return Field.NumberStyleType.PkNumberStyleDecimal;
        }
        private Field.DateStyleType GetAppleDateStyle(GeneralField.DateStyleType? dateStyleType)
        {
            switch (dateStyleType)
            {
                case GeneralField.DateStyleType.Full:
                    return Field.DateStyleType.PkDateStyleFull;
                case GeneralField.DateStyleType.Long:
                    return Field.DateStyleType.PkDateStyleLong;
                case GeneralField.DateStyleType.Medium:
                    return Field.DateStyleType.PkDateStyleMedium;
                case GeneralField.DateStyleType.None:
                    return Field.DateStyleType.PkDateStyleNone;
                case GeneralField.DateStyleType.Short:
                    return Field.DateStyleType.PkDateStyleShort;
            }
            return Field.DateStyleType.PkDateStyleNone;
        }
        private List<Field.DataDetector> GetAppleDataDetectorTypes(IEnumerable<GeneralField.DataDetector> dataDetectors)
        {
            var appleDataDetectors =
                new List<Field.DataDetector>();
            foreach (var dataDetector in dataDetectors)
            {
                switch (dataDetector)
                {
                    case GeneralField.DataDetector.Address:
                        appleDataDetectors.Add(Field.DataDetector.PkDataDetectorTypeAddress);
                        break;
                    case GeneralField.DataDetector.CalendarEvent:
                        appleDataDetectors.Add(Field.DataDetector.PkDataDetectorTypeCalendarEvent);
                        break;
                    case GeneralField.DataDetector.Link:
                        appleDataDetectors.Add(Field.DataDetector.PkDataDetectorTypeLink);
                        break;
                    case GeneralField.DataDetector.PhoneNumber:
                        appleDataDetectors.Add(Field.DataDetector.PkDataDetectorTypePhoneNumber);
                        break;
                }
            }
            return appleDataDetectors;
        }
        #endregion
    }
}
