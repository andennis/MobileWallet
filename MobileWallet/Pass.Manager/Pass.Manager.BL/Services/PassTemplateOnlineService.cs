using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Common.Extensions;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.Services;
using IPassCertificateService = Pass.Manager.Core.Services.IPassCertificateService;

namespace Pass.Manager.BL.Services
{
    public class PassTemplateOnlineService : IPassTemplateOnlineService
    {
        private const string TemplateFileName = "template.xml";
        private const string TemplateImagesFolderName = "Images";

        private readonly IPassManagerConfig _pmConfig;
        private readonly IPassContentTemplateService _contentTemplateService;
        private readonly IPassTemplateService _passTemplateService;
        private readonly IPassImageService _passImageService;
        private readonly IPassCertificateService _certificateService;

        public PassTemplateOnlineService(IPassManagerConfig pmConfig, 
            IPassContentTemplateService contentTemplateService, 
            IPassTemplateService passTemplateService, 
            IPassImageService passImageService,
            IPassCertificateService certificateService)
        {
            _pmConfig = pmConfig;
            _contentTemplateService = contentTemplateService;
            _passTemplateService = passTemplateService;
            _passImageService = passImageService;
            _certificateService = certificateService;
        }

        #region Operate single pass content templates
        public int Register(int passContentTempleteId)
        {
            PassContentTemplate pct = _contentTemplateService.GetDetails(passContentTempleteId);
            if (pct.PassContainerTemplateId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentTempleteId: {0} has already been registered", passContentTempleteId));

            GeneralPassTemplate onlineTemplete = MapToContainerTemplete(pct);

            string tempFolder = CreateWorkingSubFolder();
            onlineTemplete.SaveToXml(Path.Combine(tempFolder, TemplateFileName));
            SaveImagesToTemplate(pct.PassImages, tempFolder);

            pct.PassContainerTemplateId = _passTemplateService.CreatePassTemlate(tempFolder);

            try
            {
                _contentTemplateService.Update(pct);
                return pct.PassContainerTemplateId.Value;
            }
            catch (Exception)
            {
                _passTemplateService.DeletePassTemplate(pct.PassContentTemplateId);
                throw;
            }
            finally
            {
                Directory.Delete(tempFolder, true);    
            }
            
        }

        private void SaveImagesToTemplate(IEnumerable<PassImage> passImages, string templateFolderPath)
        {
            string imageFolder = Path.Combine(templateFolderPath, TemplateImagesFolderName);
            Directory.CreateDirectory(imageFolder);
            foreach (PassImage passImage in passImages)
            {
                using (PassImage pi = _passImageService.GetDetails(passImage.PassImageId))
                {
                    if (pi == null)
                        continue;

                    string imageName = pi.ImageType.ToString().ToLower();
                    if (pi.ImageFile != null)
                    {
                        string fileName = string.Format("{0}{1}", imageName, Path.GetExtension(pi.ImageFile.FileName));
                        pi.ImageFile.ContentStream.SaveToFile(Path.Combine(imageFolder, fileName));
                    }
                    if (pi.ImageFile2x != null)
                    {
                        string fileName = string.Format("{0}@2x{1}", imageName, Path.GetExtension(pi.ImageFile2x.FileName));
                        pi.ImageFile2x.ContentStream.SaveToFile(Path.Combine(imageFolder, fileName));
                    }
                }
            }
        }

        public void Unregister(int passContentTempleteId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOnline(int passContentTempleteId)
        {
            PassContentTemplate pct = _contentTemplateService.GetDetails(passContentTempleteId);
            if (!pct.PassContainerTemplateId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentTempleteId: {0} has not been registered yet", passContentTempleteId));

            GeneralPassTemplate onlineTemplete = MapToContainerTemplete(pct);

            string tempFolder = CreateWorkingSubFolder();
            onlineTemplete.SaveToXml(Path.Combine(tempFolder, TemplateFileName));
            SaveImagesToTemplate(pct.PassImages, tempFolder);

            _passTemplateService.UpdatePassTemlate(pct.PassContainerTemplateId.Value, tempFolder);
            Directory.Delete(tempFolder, true);
        }

        public void SetOnlineStatus(int passContentTempleteId, Common.Repository.EntityStatus status)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Operate all pass content templates of specified project
        public void RegisterProjectTempletes(int passProjectId)
        {
            throw new NotImplementedException();
        }

        public void UnregisterProjectTempletes(int passProjectId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOnlineProjectTempletes(int passProjectId)
        {
            throw new NotImplementedException();
        }
        #endregion

        public FileContentInfo GetTemplateArchive(int passContentTempleteId)
        {
            PassContentTemplate pct = _contentTemplateService.Get(passContentTempleteId);
            if (!pct.PassContainerTemplateId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentTempleteId: {0} has not been online yet", passContentTempleteId));

            string tempFolder = CreateWorkingSubFolder();
            string templateFolder = Path.Combine(tempFolder, "TemplateFiles");
            Directory.CreateDirectory(templateFolder);

            _passTemplateService.CopyPassTemplateFiles(pct.PassContainerTemplateId.Value, templateFolder);
            string fileName = pct.Name + ".zip";
            string zipFilePath = Path.Combine(tempFolder, fileName);
            Compress.CompressDirectory(templateFolder, zipFilePath);
            Directory.Delete(templateFolder, true);
            var fs = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read);
            return new FileContentInfo()
                   {
                       ContentStream = fs,
                       FileName = fileName,
                       ContentType = MimeMapping.GetMimeMapping(fileName)
                   };
        }

        private GeneralPassTemplate MapToContainerTemplete(PassContentTemplate template)
        {
            PassCertificateApple cert = _certificateService.Get(template.PassProject.PassCertificateId);
            
            return new GeneralPassTemplate()
            {
                TeamIdentifier = cert.TeamId,
                PassTypeIdentifier = cert.PassTypeId,
                TemplateName = template.Name,
                TemplateDescription = template.Description,
                PassDescription = template.Description,
                OrganizationName = template.OrganizationName,
                PassStyle = ConvertTo(template.PassStyle),
                BackgroundColor = template.BackgroundColor.HasValue ? Color.FromArgb(template.BackgroundColor.Value) : (Color?)null,
                ValueTextColor = template.ForegroundColor.HasValue ? Color.FromArgb(template.ForegroundColor.Value) : (Color?)null,
                LabelTextColor = template.LabelColor.HasValue ? Color.FromArgb(template.LabelColor.Value) : (Color?)null,
                SuppressStripShine = template.SuppressStripShine,
                LogoText = template.LogoText,
                GroupingIdentifier = template.GroupingIdentifier,
                //DistributionDetails = new DistributionDetails() { }
                LocationDetails = new LocationDetails()
                {
                    MaxDistance = template.MaxDistance,
                    Locations = template.Locations.Select(ConvertTo).ToList()
                },
                BeaconDetails = new BeaconDetails()
                {
                    Beacons = template.Beacons.Select(ConvertTo).ToList()
                },
                //BarcodeDetails = new BarcodeDetails() { }
                //RelevantDate
                FieldDetails = ConvertTo(template.PassContentTemplateFields, template.TransitType)
            };
        }

        private FieldDetails ConvertTo(ICollection<PassContentTemplateField> passFields, PassTransitType? transitType)
        {
            var templFields = new FieldDetails();
            templFields.AuxiliaryFields = passFields.Where(x => x.FieldKind == PassContentFieldKind.Auxiliary).Select(ConvertTo).ToList();
            templFields.BackFields = passFields.Where(x => x.FieldKind == PassContentFieldKind.Back).Select(ConvertTo).ToList();
            templFields.HeaderFields = passFields.Where(x => x.FieldKind == PassContentFieldKind.Header).Select(ConvertTo).ToList();
            templFields.PrimaryFields = passFields.Where(x => x.FieldKind == PassContentFieldKind.Primary).Select(ConvertTo).ToList();
            templFields.SecondaryFields = passFields.Where(x => x.FieldKind == PassContentFieldKind.Secondary).Select(ConvertTo).ToList();
            templFields.TransitType = transitType.HasValue ? ConvertTo(transitType.Value) : (Transit?)null;
            return templFields;
        }
        private Transit ConvertTo(PassTransitType transitType)
        {
            switch (transitType)
            {
                case PassTransitType.Air:
                    return Transit.Air;
                case PassTransitType.Boat:
                    return Transit.Boat;
                case PassTransitType.Bus:
                    return Transit.Bus;
                case PassTransitType.Train:
                    return Transit.Train;
            }

            return Transit.Generic;
        }
        private GeneralField ConvertTo(PassContentTemplateField passField)
        {
            return new GeneralField()
            {
                Key = passField.PassProjectField.Name,
                Label = passField.Label,
                Value = passField.PassProjectField.DefaultValue,
                TextAlignment = passField.TextAlignment.HasValue ? ConvertTo(passField.TextAlignment.Value) : (GeneralField.TextAlignmentType?)null,
                AttributedValue = passField.AttributedValue,
                ChangeMessage = passField.ChangeMessage,
                IsDynamicValue = true,
                IsDynamicLabel = true
            };
        }
        private GeneralField.TextAlignmentType ConvertTo(TextAlignment txtAlign)
        {
            switch (txtAlign)
            {
                case TextAlignment.Left:
                    return GeneralField.TextAlignmentType.Left;
                case TextAlignment.Center:
                    return GeneralField.TextAlignmentType.Center;
                case TextAlignment.Right:
                    return GeneralField.TextAlignmentType.Right;
            }

            return GeneralField.TextAlignmentType.Natural;
        }
        private GeneralBeacon ConvertTo(PassBeacon beacon)
        {
            return new GeneralBeacon()
            {
                Name = beacon.Name,
                Major = beacon.Major,
                Minor = beacon.Minor,
                ProximityUuid = beacon.ProximityUuid,
                RelevantText = beacon.RelevantText
            };
        }
        private GeneralLocation ConvertTo(PassLocation location)
        {
            return new GeneralLocation()
            {
                Altitude = location.Altitude,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                RelevantText = location.RelevantText
            };
        }
        private PassStyle ConvertTo(PassContentStyle passStyle)
        {
            switch (passStyle)
            {
                case PassContentStyle.Coupon:
                    return PassStyle.Coupon;
                case PassContentStyle.BoardingPass:
                    return PassStyle.BoardingPass;
                case PassContentStyle.EventTicket:
                    return PassStyle.EventTicket;
                case PassContentStyle.StoreCard:
                    return PassStyle.StoreCard;
            }

            return PassStyle.Generic;
        }

        private string CreateWorkingSubFolder()
        {
            string tempFolder = Path.Combine(_pmConfig.WorkingFolder, FileHelper.GetRandomFolderName());
            Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

    }
}
