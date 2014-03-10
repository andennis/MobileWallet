using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;
using Common.Repository;
using FileStorage.Core;
using Pass.Container.BL.NativePassTemplateGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Core.Exceptions;

namespace Pass.Container.BL
{
    public class PassTemplateService : IPassTemplateService
    {
        private readonly IPassTemplateConfig _ptConfig;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        //Repositories
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassTemplateNative> _repTemplateNative;
        private readonly IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;
        //Native pass template generators
        private readonly IDictionary<PassTemplateType, INativePassTemplateGenerator> _passTemplateGenerators;

        public PassTemplateService(IPassTemplateConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
            _ptConfig = config;
            _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repTemplateNative = _pcUnitOfWork.GetRepository<PassTemplateNative>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();

            _passTemplateGenerators = new Dictionary<PassTemplateType, INativePassTemplateGenerator>
                {
                    {PassTemplateType.AppleTemplate, new ApplePassTemplateGenerator(config)}
                };
        }

        public int CreatePassTemlate(string passTemplatePath)
        {
            //Check pass template and put in file storage
            string templateStorageItemPath;
            int templateStorageItemId = CreatePassTemplateFileStorageItem(passTemplatePath, out templateStorageItemPath);

            //Generate native pass templates
            string passTemplateFilePath = Path.Combine(templateStorageItemPath, _ptConfig.PassTemplateFolderName, _ptConfig.PassTemplateFileName);
            var generalPassTemplate = passTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            foreach (var nativePassTemplateGenerator in _passTemplateGenerators)
            {
                nativePassTemplateGenerator.Value.Generate(generalPassTemplate, templateStorageItemPath);
            }

            //Get dynamic fields
            IEnumerable<string> dynamicFields = GetDynamicFields(generalPassTemplate);

            //Save nessesary information into DB
            var passTemplate = new PassTemplate { Name = generalPassTemplate.TemplateName, PackageId = templateStorageItemId, Status = TemplateStatus.Active };
            var passTemplateApple = new PassTemplateApple { PassTemplateId = passTemplate.PassTemplateId, PassTypeId = ""};
            foreach (var dynamicFieldName in dynamicFields)
            {
                _repPassField.Insert(new PassField { Name = dynamicFieldName, Template = passTemplate });
            }
            _repPassTemplate.Insert(passTemplate);
            _repPassTemplateApple.Insert(passTemplateApple);
            _pcUnitOfWork.Save();

            //Get pass template id
            PassTemplate template = _repPassTemplate.Find(passTemplate.PassTemplateId);
            if (template == null)
                throw new PassTemplateException("Pass template creation failed.");
            int passTemplateId = template.PassTemplateId;

            return passTemplateId;
        }

        
        public void DeletePassTemplate(int passTemplateId)
        {
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            if (passTemplate == null)
                return;
           
            // Set pass template as inActive
            passTemplate.Status = TemplateStatus.Deleted;
            _repPassTemplate.Update(passTemplate);
            _pcUnitOfWork.Save();
        }

        public string GetNativePassTemplate(int passTemplateId, PassTemplateType passTemplateType)
        {
            //PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            //switch (passTemplateType)
            //{
            //     case PassTemplateType.AppleTemplate:
            //        _repPassTemplateApple.Query()
            //                             .Filter(x => x.PassTemplateId == passTemplateId)
            //                             .Get().ToList();
            //}
            throw new NotImplementedException();
        }

        public IList<PassField> GetPassFields(int passTemplateId)
        {
            List<PassField> passFields = _repPassField.Query()
                                                       .Filter(x => x.PassTemplateId == passTemplateId)
                                                       .Get().ToList();

            return passFields;
        }

        public void UpdatePassTemlate(int passTemplateId, string passTemplatePath)
        {
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            if (passTemplate == null)
                throw new ArgumentException(string.Format("Template with id={0} does not exist.", passTemplateId));

            //Check pass template and put in file storage
            string templateStorageItemPath;
            int templateStorageItemId = CreatePassTemplateFileStorageItem(passTemplatePath, out templateStorageItemPath);

            //Generate native pass templates
            string passTemplateFilePath = Path.Combine(templateStorageItemPath, _ptConfig.PassTemplateFolderName, _ptConfig.PassTemplateFileName);
            var generalPassTemplate = passTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            foreach (var nativePassTemplateGenerator in _passTemplateGenerators)
            {
                nativePassTemplateGenerator.Value.Generate(generalPassTemplate, templateStorageItemPath);
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
        }


        public bool ValidatePassTemplate(string passTemplateFilePath)
        {
            //TODO Pass template validation
            //check different key in field
            return true;
        }

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
            bool isValidTemplate = ValidatePassTemplate(templateFilePath);// //TODO implement validate pass template
            if (!isValidTemplate)
                throw new PassTemplateException(string.Format("Pass template file does not valid. File path: {0}", templateFilePath));

            //Check all files in PassTemplate folder
            //TODO Check files in passTemplatePath

            //Put all pass template files in FileStorage
            int templateStorageItemId = _fsService.CreateStorageFolder(out templateStorageItemPath);
            var templatefiles = Directory.GetFiles(passTemplatePath);
            foreach (var file in templatefiles)
            {
                _fsService.PutToStorageFolder(templateStorageItemId, file, _ptConfig.PassTemplateFolderName, true);
            }
            return templateStorageItemId;
        }
        private List<string> GetDynamicFields(GeneralPassTemplate generalPassTemplate)
        {
            var dynamicFields = new List<string>();
            //Get dynamic fields from AuxiliaryFields
            GetDynamicFields(generalPassTemplate.FieldDetails.AuxiliaryFields, ref dynamicFields);
            //Get dynamic fields from BackFields
            GetDynamicFields(generalPassTemplate.FieldDetails.BackFields, ref dynamicFields);
            //Get dynamic fields from HeaderFields
            GetDynamicFields(generalPassTemplate.FieldDetails.HeaderFields, ref dynamicFields);
            //Get dynamic fields from PrimaryFields
            GetDynamicFields(generalPassTemplate.FieldDetails.PrimaryFields, ref dynamicFields);
            //Get dynamic fields from SecondaryFields
            GetDynamicFields(generalPassTemplate.FieldDetails.SecondaryFields, ref dynamicFields);
            return dynamicFields;
        }
        private void GetDynamicFields(IEnumerable<Field> fields, ref List<string> dynamicFields)
        {
            dynamicFields.AddRange(
                from field in fields
                where (field.IsDynamicLabel || field.IsDynamicValue)
                select field.Key);
        }


        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}
