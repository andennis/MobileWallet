using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Lower_Level_Keys;
using Pass.Container.Core.Entities.Templates.NativePassTemplatess.ApplePassTemplate.Lower_Level_Keys;
using BarcodeType = Pass.Container.Core.Entities.Templates.GeneralPassTemplate.BarcodeType;
using Beacon = Pass.Container.Core.Entities.Templates.GeneralPassTemplate.Beacon;
using Location = Pass.Container.Core.Entities.Templates.GeneralPassTemplate.Location;

namespace Pass.Container.BL.NativePassTemplateGenerators
{
    public class ApplePassTemplateGenerator : INativePassTemplateGenerator
    {
        private readonly IPassTemplateConfig _ptConfig;
        private const string ApplePassTemplateFolderName = "ApplePassTemplate";
        private const string ApplePassTemplateFileName = "template.json";
        private static List<string> _applePassTemplateFiles;

        public ApplePassTemplateGenerator(IPassTemplateConfig config)
        {
            _ptConfig = config;
            _applePassTemplateFiles = new List<string> { 
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
        }

        public ClientType ClientType
        {
            get { return ClientType.Apple; }
        }

        public bool Generate(GeneralPassTemplate passTemplate, string storageItemPath)
        {
            //Create Apple pass template
            ApplePassTemplate applePassTemplate = CreateApplePassTemplate(passTemplate);
            string applePassTemplateJson = applePassTemplate.ObjectToJson();

            //Save Apple pass template into file storage
            string applePassTemplateFolderPath = Path.Combine(storageItemPath, ApplePassTemplateFolderName);
            if (!Directory.Exists(applePassTemplateFolderPath))
                Directory.CreateDirectory(applePassTemplateFolderPath);
            string applePassTemplateFilePath = Path.Combine(applePassTemplateFolderPath, ApplePassTemplateFileName);
            if (File.Exists(applePassTemplateFilePath))
                File.Delete(applePassTemplateFilePath);
            File.WriteAllText(applePassTemplateFilePath, applePassTemplateJson);

            //Copy Apple pass template files into Apple pass template folder
            IEnumerable<string> files = Directory.EnumerateFiles(Path.Combine(storageItemPath, _ptConfig.PassTemplateFolderName));
            foreach (var file in files)
            {
                if (!_applePassTemplateFiles.Any(applePassTemplateFile => file.Contains(applePassTemplateFile)))
                    continue;

                string filePath = file.Replace(_ptConfig.PassTemplateFolderName, ApplePassTemplateFolderName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.Copy(file, filePath);
            }

            return true;
        }

        private ApplePassTemplate CreateApplePassTemplate(GeneralPassTemplate passTemplate)
        {
            var applePassTemplate = new ApplePassTemplate();
            //Standard Keys
            applePassTemplate.Description = passTemplate.PassDescription;
            applePassTemplate.FormatVersion = 1;
            applePassTemplate.OrganizationName = passTemplate.OrganizationName;
            applePassTemplate.PassTypeIdentifier = passTemplate.PassCertificate;
            applePassTemplate.TeamIdentifier = passTemplate.TeamIdentifier;

            //Visual Appearance Keys
            applePassTemplate.BackgroundColor = new ApplePassTemplate.RgbColor(passTemplate.BackgroundColor);
            applePassTemplate.ForegroundColor = new ApplePassTemplate.RgbColor(passTemplate.ValueTextColor);
            applePassTemplate.LabelColor = new ApplePassTemplate.RgbColor(passTemplate.LabelTextColor);
            if (!string.IsNullOrEmpty(passTemplate.LogoText))
                applePassTemplate.LogoText = passTemplate.LogoText;
            if (passTemplate.SuppressStripShine != null)
                applePassTemplate.SuppressStripShine = passTemplate.SuppressStripShine;
            if (passTemplate.GroupingIdentifier != null)
                applePassTemplate.GroupingIdentifier = passTemplate.GroupingIdentifier;
            //1.Barcode Dictionary
            if (passTemplate.BarcodeDetails != null && passTemplate.BarcodeDetails.BarcodeType != BarcodeType.DoNotDisplay)
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


            //Associated App Keys
            //Companion App Keys
            if (passTemplate.IntegrationDetails != null && passTemplate.IntegrationDetails.AppOptions != null)
            {
                applePassTemplate.AppLaunchUrl = passTemplate.IntegrationDetails.AppOptions.AppLaunchUrl;
                applePassTemplate.AssociatedStoreIdentifiers = passTemplate.IntegrationDetails.AppOptions.AppIdentifier;
                applePassTemplate.UserInfo = passTemplate.IntegrationDetails.AppOptions.CustomJsonData;
            }

            //Expiration Keys
            if (passTemplate.DistributionDetails != null)
            {
                if (passTemplate.DistributionDetails.ExpirationDate != null)
                    applePassTemplate.ExpirationDate = passTemplate.DistributionDetails.ExpirationDate;
                applePassTemplate.Voided = passTemplate.DistributionDetails.AllPassesAsExpired;
            }

            //Relevance Keys
            //1.Beacon Dictionary
            if (passTemplate.BeaconDetails != null)
            {
                var beacons = new List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Lower_Level_Keys.Beacon>();
                foreach (Beacon beacon in passTemplate.BeaconDetails.Beacons)
                {
                    var appleBeacon = new Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Lower_Level_Keys.Beacon { ProximityUuid = beacon.ProximityUuid };
                    if (beacon.Major != null)
                        appleBeacon.Major = beacon.Major;
                    if (beacon.Minor != null)
                        appleBeacon.Minor = beacon.Minor;
                    if (beacon.RelevantText != null)
                        appleBeacon.RelevantText = beacon.RelevantText;
                    beacons.Add(appleBeacon);
                }
                applePassTemplate.Beacons = beacons;
            }
            //2.Location Dictionary
            if (passTemplate.LocationDetails != null)
            {
                var locations = new List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Lower_Level_Keys.Location>();
                foreach (Location location in passTemplate.LocationDetails.Locations)
                {
                    var appleLocation = new Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Lower_Level_Keys.Location() { Latitude = location.Latitude, Longitude = location.Longitude };
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
            if (passTemplate.FieldDetails.AuxiliaryFields != null)
            {
                List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field> auxiliaryFields = 
                    passTemplate.FieldDetails.AuxiliaryFields.Select(GetAppleField).ToList();
                passStructure.AuxiliaryFields = auxiliaryFields;
            }
            if (passTemplate.FieldDetails.BackFields != null)
            {
                List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field> backFields =
                    passTemplate.FieldDetails.BackFields.Select(GetAppleField).ToList();
                passStructure.BackFields = backFields;
            }
            if (passTemplate.FieldDetails.HeaderFields != null)
            {
                List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field> headerFields =
                    passTemplate.FieldDetails.HeaderFields.Select(GetAppleField).ToList();
                passStructure.HeaderFields = headerFields;
            }
            if (passTemplate.FieldDetails.PrimaryFields != null)
            {
                List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field> primaryFields =
                    passTemplate.FieldDetails.PrimaryFields.Select(GetAppleField).ToList();
                passStructure.PrimaryFields = primaryFields;
            }
            if (passTemplate.FieldDetails.SecondaryFields != null)
            {
                List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field> secondaryFields =
                    passTemplate.FieldDetails.SecondaryFields.Select(GetAppleField).ToList();
                passStructure.SecondaryFields = secondaryFields;
            }
            return passStructure;
        }

        private Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field GetAppleField(Field templatefield)
        {
            var field = new Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field();
            //TODO need validation AttributedValue
            if (!string.IsNullOrEmpty(templatefield.AttributedValue))
                field.AttributedValue = templatefield.AttributedValue;
            //TODO need validation ChangeMessage
            if (!string.IsNullOrEmpty(templatefield.ChangeMessage))
                field.ChangeMessage = templatefield.ChangeMessage;
            if (templatefield.DataDetectorTypes != null)
                field.DataDetectorTypes = GetAppleDataDetectorTypes(templatefield.DataDetectorTypes);
            field.Key = templatefield.Key;
            if (!string.IsNullOrEmpty(templatefield.Label))
                field.Label = templatefield.Label;
            field.Value = templatefield.Value;
            field.TextAlignment = GetAppleTextAlligment(templatefield.TextAlignment);

            if (templatefield.Type == Field.DataType.Number)
                field.NumberStyle = GetAppleNumberStyle(templatefield.NumberStyle);
            if (templatefield.Type == Field.DataType.Currency)
                field.CurrencyCode = templatefield.CurrencyCode;
            if (templatefield.Type == Field.DataType.Date)
            {
                field.DateStyle = GetAppleDateStyle(templatefield.DateStyle);
                field.IsRelative = templatefield.IsRelative;
            }
            if (templatefield.Type == Field.DataType.DateTime)
            {
                field.TimeStyle = GetAppleDateStyle(templatefield.TimeStyle);
                field.IsRelative = templatefield.IsRelative;
            }
            return field;
        }

        # region Enum converters
        private PassStructure.Transit GetAppleTransit(Transit transit)
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
        private Core.Entities.Templates.NativePassTemplatess.ApplePassTemplate.Lower_Level_Keys.BarcodeType GetAppleBarcodeType(BarcodeType barcodeType)
        {
            switch (barcodeType)
            {
                case BarcodeType.Pdf417Code:
                    return Core.Entities.Templates.NativePassTemplatess.ApplePassTemplate.Lower_Level_Keys.BarcodeType.Pdf417Code;
                case BarcodeType.AztecCode:
                    return Core.Entities.Templates.NativePassTemplatess.ApplePassTemplate.Lower_Level_Keys.BarcodeType.AztecCode;
                case BarcodeType.QrCode:
                    return Core.Entities.Templates.NativePassTemplatess.ApplePassTemplate.Lower_Level_Keys.BarcodeType.QrCode;
            }
            return Core.Entities.Templates.NativePassTemplatess.ApplePassTemplate.Lower_Level_Keys.BarcodeType.Pdf417Code;
        }

        private Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.TextAlignmentType GetAppleTextAlligment(Field.TextAlignmentType textAlignmentType)
        {
            switch (textAlignmentType)
            {
                case Field.TextAlignmentType.Center:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.TextAlignmentType.PkTextAlignmentCenter;
                case Field.TextAlignmentType.Left:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.TextAlignmentType.PkTextAlignmentLeft;
                case Field.TextAlignmentType.Right:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.TextAlignmentType.PkTextAlignmentRight;
                case Field.TextAlignmentType.Natural:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.TextAlignmentType.PkTextAlignmentNatural;
            }
            return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.TextAlignmentType.PkTextAlignmentLeft;
        }

        private Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.NumberStyleType GetAppleNumberStyle(Field.NumberStyleType numberStyleType)
        {
            switch (numberStyleType)
            {
                case Field.NumberStyleType.Decimal:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.NumberStyleType.PkNumberStyleDecimal;
                case Field.NumberStyleType.Percent:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.NumberStyleType.PkNumberStylePercent;
                case Field.NumberStyleType.Scientific:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.NumberStyleType.PkNumberStyleScientific;
                case Field.NumberStyleType.SpellOut:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.NumberStyleType.PkNumberStyleSpellOut;
            }
            return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.NumberStyleType.PkNumberStyleDecimal;
        }

        private Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType GetAppleDateStyle(Field.DateStyleType dateStyleType)
        {
            switch (dateStyleType)
            {
                case Field.DateStyleType.Full:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType.PkDateStyleFull;
                case Field.DateStyleType.Long:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType.PkDateStyleLong;
                case Field.DateStyleType.Medium:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType.PkDateStyleMedium;
                case Field.DateStyleType.None:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType.PkDateStyleNone;
                case Field.DateStyleType.Short:
                    return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType.PkDateStyleShort;
            }
            return Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DateStyleType.PkDateStyleNone;
        }

        private List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DataDetector> GetAppleDataDetectorTypes(IEnumerable<Field.DataDetector> dataDetectors)
        {
            var appleDataDetectors =
                new List<Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DataDetector>();
            foreach (var dataDetector in dataDetectors)
            {
                switch (dataDetector)
                {
                    case Field.DataDetector.Address:
                        appleDataDetectors.Add(Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DataDetector.PkDataDetectorTypeAddress);
                        break;
                    case Field.DataDetector.CalendarEvent:
                        appleDataDetectors.Add(Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DataDetector.PkDataDetectorTypeCalendarEvent);
                        break;
                    case Field.DataDetector.Link:
                        appleDataDetectors.Add(Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DataDetector.PkDataDetectorTypeLink);
                        break;
                    case Field.DataDetector.PhoneNumber:
                        appleDataDetectors.Add(Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys.Field.DataDetector.PkDataDetectorTypePhoneNumber);
                        break;
                }
            }
            return appleDataDetectors;
        }
        #endregion
    }
}
