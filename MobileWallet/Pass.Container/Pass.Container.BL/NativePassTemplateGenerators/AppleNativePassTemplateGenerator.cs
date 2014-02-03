using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.Lower_Level_Keys;
using Pass.Container.Core.Entities.Templates.NativePassTemplatess.Lower_Level_Keys;
using Pass.Container.Core.Entities.Templates.PassTemplate;
using BarcodeType = Pass.Container.Core.Entities.Templates.PassTemplate.BarcodeType;
using Beacon = Pass.Container.Core.Entities.Templates.PassTemplate.Beacon;
using Location = Pass.Container.Core.Entities.Templates.PassTemplate.Location;

namespace Pass.Container.BL.NativePassTemplateGenerators
{
    public class AppleNativePassTemplateGenerator : INativePassTemplateGenerator
    {
        public PassTemplateType PassTemplateType
        {
            get { return PassTemplateType.AppleTemplate; }
        }

        public bool Generate(PassTemplate passTemplate, string storageItemPath)
        {
            throw new NotImplementedException();
        }

        private ApplePassTemplate CreateApplePassTemplate(PassTemplate passTemplate)
        {
            var applePassTemplate = new ApplePassTemplate();
            //Standard Keys
            applePassTemplate.Description = passTemplate.PassDescription;
            applePassTemplate.FormatVersion = 1;
            applePassTemplate.OrganizationName = passTemplate.OrganizationName;
            applePassTemplate.PassTypeIdentifier = GetPassTypeIdentifier(passTemplate.PassType, passTemplate.PassCertificate);
            applePassTemplate.SerialNumber = GetPassSerialNumber(passTemplate.PassSerialNumberType);
            applePassTemplate.TeamIdentifier = GetPassTeamIdentifier();

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
                                      Format = GetBarcodeType(passTemplate.BarcodeDetails.BarcodeType),
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
                var beacons = new List<Core.Entities.Templates.NativePassTemplates.Lower_Level_Keys.Beacon>();
                foreach (Beacon beacon in passTemplate.BeaconDetails.Beacons)
                {
                    var appleBeacon = new Core.Entities.Templates.NativePassTemplates.Lower_Level_Keys.Beacon { ProximityUuid = beacon.ProximityUuid };
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
                var locations = new List<Core.Entities.Templates.NativePassTemplates.Lower_Level_Keys.Location>();
                foreach (Location location in passTemplate.LocationDetails.Locations)
                {
                    var appleLocation = new Core.Entities.Templates.NativePassTemplates.Lower_Level_Keys.Location() { Latitude = location.Latitude, Longitude = location.Longitude };
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
            switch (passTemplate.PassType)
            {
                case PassType.BoardingPass:
                    applePassTemplate.BoardingPass = GetPassStructure(passTemplate, PassType.BoardingPass);
                    break;
                case PassType.Coupon:
                    applePassTemplate.Coupon = GetPassStructure(passTemplate, PassType.Coupon);
                    break;
                case PassType.EventTicket:
                    applePassTemplate.EventTicket = GetPassStructure(passTemplate, PassType.EventTicket);
                    break;
                case PassType.Generic:
                    applePassTemplate.Generic = GetPassStructure(passTemplate, PassType.Generic);
                    break;
                case PassType.StoreCard:
                    applePassTemplate.StoreCard = GetPassStructure(passTemplate, PassType.StoreCard);
                    break;
            }

            //Web Service Keys
            applePassTemplate.AuthenticationToken = GetAuthenticationToken();
            applePassTemplate.WebServiceUrl = GetWebServiceUrl();

            return applePassTemplate;
        }

        private PassStructure GetPassStructure(PassTemplate passTemplate, PassType passType)
        {
            var passStructure = new PassStructure();
            if (passTemplate.FieldDetails.AuxiliaryFields != null)
            {
                var auxiliaryFields = new List<Field>();
                foreach (var auxiliaryField in passTemplate.FieldDetails.AuxiliaryFields)
                {
                    var field = new Field();
                    //TODO need validation AttributedValue
                    if (!string.IsNullOrEmpty(auxiliaryField.AttributedValue))
                        field.AttributedValue = auxiliaryField.AttributedValue;
                    //TODO need validation ChangeMessage
                    if (!string.IsNullOrEmpty(auxiliaryField.ChangeMessage))
                        field.ChangeMessage = auxiliaryField.ChangeMessage;
                    if (auxiliaryField.DataDetectorTypes != null)
                        field.DataDetectorTypes = auxiliaryField.DataDetectorTypes;
                }
                
            }
            return null;
        }

        private string GetWebServiceUrl()
        {
            throw new NotImplementedException();//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private string GetAuthenticationToken()
        {
            throw new NotImplementedException();//!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private string GetPassTypeIdentifier(PassType passType, string passCertificate)
        {
            throw new NotImplementedException();
        }

        private string GetPassSerialNumber(PassSerialNumberType passSerialNumberType)
        {
            throw new NotImplementedException();//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private string GetPassTeamIdentifier()
        {
            throw new NotImplementedException();
        }

        private Core.Entities.Templates.NativePassTemplatess.Lower_Level_Keys.BarcodeType GetBarcodeType(BarcodeType barcodeType)
        {
            switch (barcodeType)
            {
                case BarcodeType.Pdf417Code:
                    return Core.Entities.Templates.NativePassTemplatess.Lower_Level_Keys.BarcodeType.Pdf417Code;
                case BarcodeType.AztecCode:
                    return Core.Entities.Templates.NativePassTemplatess.Lower_Level_Keys.BarcodeType.AztecCode;
                case BarcodeType.QrCode:
                    return Core.Entities.Templates.NativePassTemplatess.Lower_Level_Keys.BarcodeType.QrCode;
            }
            return Core.Entities.Templates.NativePassTemplatess.Lower_Level_Keys.BarcodeType.Pdf417Code;
        }
    }
}
