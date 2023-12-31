﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.LowerLevelKeys;

namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate
{
    public class ApplePassTemplate
    {
        //Information that is required for all passes
        #region Standard Keys 

        [JsonProperty(PropertyName = "description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "formatVersion", Required = Required.Always)]
        public int FormatVersion { get; set; }

        [JsonProperty(PropertyName = "organizationName", Required = Required.Always)]
        public string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "passTypeIdentifier", Required = Required.Always)]
        public string PassTypeIdentifier { get; set; }

        [JsonProperty(PropertyName = "serialNumber", Required = Required.Always)]
        public string SerialNumber { get; set; }

        [JsonProperty(PropertyName = "teamIdentifier", Required = Required.Always)]
        public string TeamIdentifier { get; set; }

        #endregion

        //Information about an app that is associated with a pass
        #region Associated App Keys

        [JsonProperty(PropertyName = "appLaunchURL", NullValueHandling = NullValueHandling.Ignore)]
        public string AppLaunchUrl { get; set; }

        [JsonProperty(PropertyName = "associatedStore-Identifiers", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> AssociatedStoreIdentifiers { get; set; }

        #endregion

        //Custom information about a pass provided for a companion app to use
        #region Companion App Keys

        [JsonProperty(PropertyName = "userInfo", NullValueHandling = NullValueHandling.Ignore)]
        public object UserInfo { get; set; }

        #endregion

        //Information about when a pass expires and whether it is still valid
        #region Expiration Keys
        [JsonProperty(PropertyName = "expirationDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ExpirationDate { get; set; }

        [JsonProperty(PropertyName = "voided", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Voided { get; set; }
        #endregion

        //Information about where and when a pass is relevant
        #region Relevance Keys

        [JsonProperty(PropertyName = "beacons", NullValueHandling = NullValueHandling.Ignore)]
        public List<Beacon> Beacons { get; set; }

        [JsonProperty(PropertyName = "locations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Location> Locations { get; set; }

        [JsonProperty(PropertyName = "maxDistance", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxDistance { get; set; }

        [JsonProperty(PropertyName = "relevantDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? RelevantDate { get; set; }

        #endregion

        //Specifies the pass style
        #region Style Keys
        
        [JsonProperty(PropertyName = "boardingPass", NullValueHandling = NullValueHandling.Ignore)]
        public PassStructure BoardingPass { get; set; }

        [JsonProperty(PropertyName = "coupon", NullValueHandling = NullValueHandling.Ignore)]
        public PassStructure Coupon { get; set; }

        [JsonProperty(PropertyName = "eventTicket", NullValueHandling = NullValueHandling.Ignore)]
        public PassStructure EventTicket { get; set; }

        [JsonProperty(PropertyName = "generic", NullValueHandling = NullValueHandling.Ignore)]
        public PassStructure Generic { get; set; }

        [JsonProperty(PropertyName = "storeCard", NullValueHandling = NullValueHandling.Ignore)]
        public PassStructure StoreCard { get; set; }

        #endregion

        //Visual styling and appearance of the pass
        #region Visual Appearance Keys

        [JsonProperty(PropertyName = "barcode", NullValueHandling = NullValueHandling.Ignore)]
        public Barcode Barcode { get; set; }

        //Example: "backgroundColor" : "rgb(206, 140, 53)"
        [JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "foregroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public string ForegroundColor { get; set; }

        //WARNING! Optional for event tickets and boarding passes; otherwise not allowed
        [JsonProperty(PropertyName = "groupingIdentifier", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupingIdentifier { get; set; }

        [JsonProperty(PropertyName = "labelColor", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelColor { get; set; }

        [JsonProperty(PropertyName = "logoText", NullValueHandling = NullValueHandling.Ignore)]
        public string LogoText { get; set; }

        //The default value prior to iOS 7.0 is false.
        //In iOS 7.0, a shine effect is never applied, and this key is deprecated.
        [JsonProperty(PropertyName = "suppressStripShine", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SuppressStripShine { get; set; }

        #endregion

        //Information used to update passes using the web service
        #region Web Service Keys

        [JsonProperty(PropertyName = "authenticationToken", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthenticationToken { get; set; }

        [JsonProperty(PropertyName = "webServiceURL", NullValueHandling = NullValueHandling.Ignore)]
        public string WebServiceUrl { get; set; }

        #endregion

        //Set default value
        public ApplePassTemplate()
        {
            this.FormatVersion = 1;
        }
    }
}
