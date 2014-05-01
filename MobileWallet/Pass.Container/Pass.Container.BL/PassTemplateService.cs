using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Pass.Container.BL.Helpers;
using Pass.Container.BL.PassTemplateGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
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
        //Repositories
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;
        //Native pass template generators
        private readonly IList<IPassTemplateGenerator> _passTemplateGenerators;

        public PassTemplateService(IPassTemplateConfig config, IPassContainerUnitOfWork pcUnitOfWork, IPassTemplateStorageService ptsService)
        {
            _ptConfig = config;
            _pcUnitOfWork = pcUnitOfWork;
            _ptsService = ptsService;

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
            //Create template location
            int templateStorageId = _ptsService.CreateTemplateStorage();
            _ptsService.PutBaseTemplateFiles(templateStorageId, passTemplatePath);

            //Generate native pass templates
            string srcImageFilesPath = Path.Combine(passTemplatePath, ApplePass.TemplateImageFolder);
            string srcTemplateFilePath = Path.Combine(passTemplatePath, TemplateFileName);
            var generalPassTemplate = srcTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            string dstTemplatePath = Path.Combine(_ptConfig.PassTemplateWorkingFolder, GetTemporaryTemplateFolder());
            foreach (IPassTemplateGenerator templateGenerator in _passTemplateGenerators)
            {
                string dstNativeTemplatePath = Path.Combine(dstTemplatePath, templateGenerator.ClientType.ToString());
                Directory.CreateDirectory(dstNativeTemplatePath);

                FileHelper.DirectoryCopy(srcImageFilesPath, Path.Combine(dstNativeTemplatePath, ApplePass.TemplateImageFolder), false);

                templateGenerator.Generate(generalPassTemplate, Directory.EnumerateFiles(srcImageFilesPath), dstNativeTemplatePath);
                _ptsService.PutNativeTemplateFiles(templateStorageId, templateGenerator.ClientType, dstNativeTemplatePath);
            }
            Directory.Delete(dstTemplatePath, true);

            //Get dynamic fields
            IEnumerable<string> dynamicFields = GetDynamicFields(generalPassTemplate);

            //Save nessesary information into DB
            var passTemplate = new PassTemplate
                               {
                                   Name = generalPassTemplate.TemplateName,
                                   PackageId = templateStorageId, 
                                   Status = EntityStatus.Active
                               };
            var passTemplateApple = new PassTemplateApple
                                    {
                                        PassTemplateId = passTemplate.PassTemplateId, 
                                        PassTypeId = generalPassTemplate.PassTypeIdentifier,
                                        CertificateId = generalPassTemplate.CertificateId
                                    };
            foreach (var dynamicFieldName in dynamicFields)
            {
                _repPassField.Insert(new PassField
                                     {
                                         Name = dynamicFieldName,
                                         //DefaultLabel = ???, //TODO Default label and default value should be taken from GeneralPassTemplate
                                         //DefaultValue = ???,
                                         PassTemplateId = passTemplate.PassTemplateId
                                     });
            }
            _repPassTemplate.Insert(passTemplate);
            _repPassTemplateApple.Insert(passTemplateApple);
            _pcUnitOfWork.Save();

            return passTemplate.PassTemplateId;
        }

        private string GetTemporaryTemplateFolder()
        {
            return FileHelper.GetRandomFolderName();
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
                .Filter(x => x.PassTemplateId == passTemplateId).Get()
                .Select(x => new PassFieldInfo()
                                 {
                                     PassFieldId = x.PassFieldId,
                                     Name = x.Name,
                                     Label = x.DefaultLabel ?? x.Name,
                                     Value = x.DefaultValue
                                 })
                .ToList();
        }

        public void UpdatePassTemlate(int passTemplateId, string passTemplatePath)
        {
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            if (passTemplate == null)
                throw new PassTemplateException(string.Format("Template ID:{0} not found", passTemplateId));

            /*
            //Check pass template and put in file storage
            string templateStorageItemPath;
            int templateStorageItemId = CreatePassTemplateFileStorageItem(passTemplatePath, out templateStorageItemPath);

            //Generate native pass templates
            string passTemplateFilePath = Path.Combine(templateStorageItemPath, _ptConfig.PassTemplateFolderName, _ptConfig.PassTemplateFileName);
            var generalPassTemplate = passTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            foreach (var nativePassTemplateGenerator in _passTemplateGenerators)
            {
                nativePassTemplateGenerator.Generate(generalPassTemplate, templateStorageItemPath);
            }
            
            //Update pass template in DB
            passTemplate.Name = generalPassTemplate.TemplateName;
            passTemplate.PackageId = templateStorageItemId;
            _repPassTemplate.Update(passTemplate);
            
            //Get dynamic fields
            List<string> dynamicFields = GetDynamicFields(generalPassTemplate);

            //Update dynamic fields in DB
            IQueryable<PassField> passFields = _repPassField.Query()
                                                      .Filter(x => x.PassTemplateId == passTemplateId)
                                                      .Get();
            if (passFields == null)
            {
                foreach (var dynamicField in dynamicFields)
                    _repPassField.Insert(new PassField {Name = dynamicField});
            }
            else
            {
                if (dynamicFields == null)
                {
                    foreach (PassField passField in passFields)
                        _repPassField.Delete(passField);
                }
                else
                {
                    foreach (PassField passField in passFields.Where(passField => !dynamicFields.Contains(passField.Name)))
                        _repPassField.Delete(passField);

                    IEnumerable<string> passFieldNames = passFields.Select(x => x.Name);
                    foreach (var dynamicField in dynamicFields.Where(dynamicField => !passFieldNames.Contains(dynamicField)))
                        _repPassField.Insert(new PassField {Name = dynamicField});
                }
            }

            //TODO Set passes as need for update

            //Save DB changes
            _pcUnitOfWork.Save();
            */
        }

        public bool ValidatePassTemplate(string passTemplateFilePath)
        {
            //TODO Pass template validation
            //check different key in field
            return true;
        }
        /*
        private int CreatePassTemplateFileStorageItem(string passTemplatePath, out string templateStorageItemPath)
        {
            if (string.IsNullOrEmpty(passTemplatePath))
                throw new ArgumentNullException("passTemplatePath");

            if (!Directory.Exists(passTemplatePath))
                throw new ArgumentException("Directory does not exist.");

            string templateFilePath = Path.Combine(passTemplatePath, _ptConfig.PassTemplateFileName);
            if (!File.Exists(templateFilePath))
                throw new PassTemplateException(string.Format("Pass template file was not found. File path: {0}", templateFilePath));

            //Validate pass template file
            bool isValidTemplate = ValidatePassTemplate(templateFilePath);//TODO implement validate pass template
            if (!isValidTemplate)
                throw new PassTemplateException(string.Format("Pass template file does not valid. File path: {0}", templateFilePath));

            //Check all files in PassTemplate folder
            //TODO Check files in passTemplatePath

            //Put all pass template files in FileStorage
            int templateStorageItemId = _fsService.CreateStorageFolder(out templateStorageItemPath);
            string[] templatefiles = Directory.GetFiles(passTemplatePath);
            foreach (string file in templatefiles)
            {
                _fsService.PutToStorageFolder(templateStorageItemId, file, _ptConfig.PassTemplateFolderName, true);
            }

            return templateStorageItemId;
        }
        */
        private List<string> GetDynamicFields(GeneralPassTemplate generalPassTemplate)
        {
            var dynamicFields = new List<string>();
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
        private void GetDynamicFields(IEnumerable<GeneralField> fields, List<string> dynamicFields)
        {
            dynamicFields.AddRange(fields.Where(x => x.IsDynamicLabel == true || x.IsDynamicValue == true).Select(x => x.Key));
        }

        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}
