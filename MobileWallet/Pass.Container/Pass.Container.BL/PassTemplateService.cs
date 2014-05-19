using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Pass.Container.BL.Helpers;
using Pass.Container.BL.PassTemplateGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public class PassTemplateService : IPassTemplateService
    {
        private const string TemplateFileName = "template.xml";

        private readonly IPassTemplateConfig _ptConfig;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IPassTemplateStorageService _ptsService;
        private readonly ICertificateStorageService _csService;

        //Repositories
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;

        //Native pass template generators
        private readonly IList<IPassTemplateGenerator> _passTemplateGenerators;

        public PassTemplateService(IPassTemplateConfig config, 
            IPassContainerUnitOfWork pcUnitOfWork, 
            IPassTemplateStorageService ptsService,
            ICertificateStorageService csService)
        {
            _ptConfig = config;
            _pcUnitOfWork = pcUnitOfWork;
            _ptsService = ptsService;
            _csService = csService;

            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();

            _passTemplateGenerators = new List<IPassTemplateGenerator>
                                        {
                                            new ApplePassTemplateGenerator(config)
                                        };
        }

        public int CreatePassTemlate(string passTemplatePath)
        {
            if (passTemplatePath == null)
                throw  new ArgumentNullException("passTemplatePath");

            int templateStorageId = _ptsService.CreateTemplateStorage();
            _ptsService.PutBaseTemplateFiles(templateStorageId, passTemplatePath);

            string srcTemplateFilePath = Path.Combine(passTemplatePath, TemplateFileName);
            var generalPassTemplate = srcTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            string srcImageFilesPath = Path.Combine(passTemplatePath, ApplePass.TemplateImageFolder);
            BuildNativeTemplates(generalPassTemplate, templateStorageId, srcImageFilesPath);

            var passTemplate = new PassTemplate
                               {
                                   Name = generalPassTemplate.TemplateName,
                                   PackageId = templateStorageId, 
                                   Status = EntityStatus.Active
                               };
            var passTemplateApple = new PassTemplateApple
                                    {
                                        PassTemplateId = passTemplate.PassTemplateId, 
                                        DeviceType = ClientDeviceType.Apple,
                                        PassTypeId = generalPassTemplate.PassTypeIdentifier,
                                        CertificateId = GetCertificateId(generalPassTemplate, ClientType.Apple)
                                    };

            IEnumerable<GeneralField> dynamicFields = GetDynamicFields(generalPassTemplate);
            foreach (var dynamicField in dynamicFields)
            {
                _repPassField.Insert(new PassField
                                     {
                                         Name = dynamicField.Key,
                                         DefaultLabel = dynamicField.Label,
                                         DefaultValue = dynamicField.Value,
                                         PassTemplateId = passTemplate.PassTemplateId,
                                         Status = EntityStatus.Active
                                     });
            }
            _repPassTemplate.Insert(passTemplate);
            _repPassTemplateApple.Insert(passTemplateApple);
            _pcUnitOfWork.Save();

            return passTemplate.PassTemplateId;
        }

        public void UpdatePassTemlate(int passTemplateId, string passTemplatePath)
        {
            if (passTemplatePath == null)
                throw new ArgumentNullException("passTemplatePath");

            PassTemplate passTemplate = _repPassTemplate.Query()
                .Include(x => x.PassFields)
                .Filter(x => x.PassTemplateId == passTemplateId)
                .Get().FirstOrDefault();

            if (passTemplate == null)
                throw new PassTemplateException(string.Format("Template ID:{0} not found", passTemplateId));
            if (passTemplate.Status == EntityStatus.Deleted)
                throw new PassTemplateException(string.Format("Pass template ID:{0} has deleted status", passTemplateId));

            int templateStorageId = _ptsService.CreateTemplateStorage();
            _ptsService.PutBaseTemplateFiles(templateStorageId, passTemplatePath);

            string srcTemplateFilePath = Path.Combine(passTemplatePath, TemplateFileName);
            var generalPassTemplate = srcTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            string srcImageFilesPath = Path.Combine(passTemplatePath, ApplePass.TemplateImageFolder);
            BuildNativeTemplates(generalPassTemplate, templateStorageId, srcImageFilesPath);

            IEnumerable<GeneralField> newFields = GetDynamicFields(generalPassTemplate);
            //Update existing and add new fields
            foreach (var newFiled in newFields)
            {
                PassField pf = passTemplate.PassFields.FirstOrDefault(x => x.Name == newFiled.Key);
                if (pf == null)
                {
                    _repPassField.Insert(new PassField
                                             {
                                                 Name = newFiled.Key,
                                                 DefaultLabel = newFiled.Label,
                                                 DefaultValue = newFiled.Value,
                                                 PassTemplateId = passTemplate.PassTemplateId,
                                                 Status = EntityStatus.Active
                                             });
                }
                else
                {
                    pf.DefaultLabel = newFiled.Label;
                    pf.DefaultValue = newFiled.Value;
                    pf.Status = EntityStatus.Active;
                }
            }

            //Remove fields
            foreach (PassField pf in passTemplate.PassFields)
            {
                if (newFields.Any(x => x.Key == pf.Name))
                    continue;

                pf.Status = EntityStatus.Deleted;
                _repPassField.Update(pf);
            }

            //Update native apple template
            var passTemplateApple = passTemplate.NativeTemplates.OfType<PassTemplateApple>().FirstOrDefault();
            if (passTemplateApple == null)
                throw new PassTemplateException("Apple native template record does not exist");

            passTemplateApple.CertificateId = GetCertificateId(generalPassTemplate, ClientType.Apple);
            passTemplateApple.PassTypeId = generalPassTemplate.PassTypeIdentifier;
            _repPassTemplateApple.Update(passTemplateApple);

            //Update pass template record
            passTemplate.Name = generalPassTemplate.TemplateName;
            passTemplate.PackageId = templateStorageId;
            //passTemplate.PassFields = null;
            //passTemplate.NativeTemplates = null;
            _repPassTemplate.Update(passTemplate);

            _pcUnitOfWork.Save();
        }

        private void BuildNativeTemplates(GeneralPassTemplate generalPassTemplate, int templateStorageId, string srcImageFilesPath)
        {
            string dstTemplatePath = Path.Combine(_ptConfig.PassTemplateWorkingFolder, GetTemporaryTemplateFolder());
            foreach (IPassTemplateGenerator templateGenerator in _passTemplateGenerators)
            {
                string dstNativeTemplatePath = Path.Combine(dstTemplatePath, templateGenerator.ClientType.ToString());
                Directory.CreateDirectory(dstNativeTemplatePath);

                templateGenerator.Generate(generalPassTemplate, Directory.EnumerateFiles(srcImageFilesPath), dstNativeTemplatePath);
                _ptsService.PutNativeTemplateFiles(templateStorageId, templateGenerator.ClientType, dstNativeTemplatePath);
            }
            Directory.Delete(dstTemplatePath, true);
        }

        public void DeletePassTemplate(int passTemplateId)
        {
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            if (passTemplate == null)
                return;
           
            // Set pass template as inActive
            passTemplate.Status = EntityStatus.Deleted;
            _repPassTemplate.Update(passTemplate);
            _pcUnitOfWork.Save();
        }

        public IList<PassFieldInfo> GetPassTemplateFields(int passTemplateId)
        {
            return _repPassField.Query()
                .Filter(x => x.PassTemplateId == passTemplateId && x.Status == EntityStatus.Active).Get()
                .Select(x => new PassFieldInfo()
                                 {
                                     PassFieldId = x.PassFieldId,
                                     Name = x.Name,
                                     Label = x.DefaultLabel ?? x.Name,
                                     Value = x.DefaultValue
                                 })
                .ToList();
        }

        private bool ValidatePassTemplate(string passTemplateFilePath)
        {
            //TODO Pass template validation
            //check different key in field
            return true;
        }

        private IEnumerable<GeneralField> GetDynamicFields(GeneralPassTemplate generalPassTemplate)
        {
            var dynamicFields = new List<GeneralField>();
            if (generalPassTemplate.FieldDetails == null)
                return dynamicFields;
            
            //Get dynamic fields from AuxiliaryFields
            GetDynamicFields(generalPassTemplate.FieldDetails.AuxiliaryFields, dynamicFields);

            //Get dynamic fields from BackFields
            GetDynamicFields(generalPassTemplate.FieldDetails.BackFields, dynamicFields);

            //Get dynamic fields from HeaderFields
            GetDynamicFields(generalPassTemplate.FieldDetails.HeaderFields, dynamicFields);

            //Get dynamic fields from PrimaryFields
            GetDynamicFields(generalPassTemplate.FieldDetails.PrimaryFields, dynamicFields);

            //Get dynamic fields from SecondaryFields
            GetDynamicFields(generalPassTemplate.FieldDetails.SecondaryFields, dynamicFields);

            return dynamicFields;
        }
        private void GetDynamicFields(IEnumerable<GeneralField> fields, List<GeneralField> dynamicFields)
        {
            dynamicFields.AddRange(fields.Where(x => x.IsDynamicLabel == true || x.IsDynamicValue == true));
        }

        private string GetTemporaryTemplateFolder()
        {
            return FileHelper.GetRandomFolderName();
        }

        private int GetCertificateId(GeneralPassTemplate genPassTemplate, ClientType clientType)
        {
            if (clientType != ClientType.Apple)
                throw new PassTemplateException(string.Format("Client type: {0} is not supported", clientType));
            
            string certName = string.Format("TID#{0}/PTID#{1}", genPassTemplate.TeamIdentifier,
                genPassTemplate.PassTypeIdentifier);
            using (CertificateInfo certInfo = _csService.Read(certName))
            {
                return certInfo.CertificateId;
            }
        }

        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}