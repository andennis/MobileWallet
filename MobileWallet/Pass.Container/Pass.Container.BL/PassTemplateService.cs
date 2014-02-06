using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Repository;
using FileStorage.BL;
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
        private readonly IPassContainerConfig _pcConfig;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        //Repositories
        IRepository<PassTemplate> _repPassTemplate;
        IRepository<PassTemplateNative> _repTemplateNative;
        IRepository<PassTemplateApple> _repPassTemplateApple;
        IRepository<PassField> _repPassField;
        //Native pass template generators
        private readonly IDictionary<PassTemplateType, INativePassTemplateGenerator> _passTemplateGenerators;

        public PassTemplateService(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
            _pcConfig = config;
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
            if (string.IsNullOrEmpty(passTemplatePath))
                throw new ArgumentNullException("passTemplatePath");

            if (!Directory.Exists(passTemplatePath))
                throw new ArgumentException("Directory does not exist.");

            string templateFilePath = Path.Combine(passTemplatePath, _pcConfig.PassTemplateFileName);
            if (!File.Exists(templateFilePath))
                throw new PassTemplateException(string.Format("Pass template file was not found. File path: {0}", templateFilePath));

            //Validate pass template file
            bool isValidTemplate = ValidatePassTemplate(templateFilePath);
            if (!isValidTemplate)
                throw new PassTemplateException(string.Format("Pass template file does not valid. File path: {0}", templateFilePath));

            //Check all files in PassTemplate folder
            //TODO Check files in passTemplatePath

            //Put all pass template files into FileStorage
            string templateStorageItemPath;
            int templateStorageItemId = _fsService.CreateStorageFolder(out templateStorageItemPath);
            _fsService.PutToStorageFolder(templateStorageItemId, passTemplatePath, _pcConfig.PassTemplateFolderName, true);

            //Generate native pass templates
            string passTemplateFilePath = Path.Combine(templateStorageItemPath, _pcConfig.PassTemplateFolderName, _pcConfig.PassTemplateFileName);
            var generalPassTemplate = passTemplateFilePath.LoadFromXml<GeneralPassTemplate>();
            foreach (var nativePassTemplateGenerator in _passTemplateGenerators)
            {
                nativePassTemplateGenerator.Value.Generate(generalPassTemplate, templateStorageItemPath);
            }

            //Get dynamic fields
            IEnumerable<string> dynamicFields = GetDynamicFields(generalPassTemplate);

            //Save nessesary information into DB
            var passTemplate = new PassTemplate { Name = generalPassTemplate.TemplateName, PackageId = templateStorageItemId };
            var passTemplateApple = new PassTemplateApple { Template = passTemplate };
            foreach (var dynamicFieldName in dynamicFields)
            {
                _repPassField.Insert(new PassField { Name = dynamicFieldName, Template = passTemplate});
            }
            _repPassTemplate.Insert(passTemplate);
            _repPassTemplateApple.Insert(passTemplateApple);
            _pcUnitOfWork.Save();

            return templateStorageItemId;
        }

        private IEnumerable<string> GetDynamicFields(GeneralPassTemplate generalPassTemplate)
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
                where field.IsDynamic
                select field.Key);
        }

        public void DeletePassTemplate(int passTemplateId)
        {
            throw new NotImplementedException();
        }

        public string GetNativePassTemplate(int passTemplateId, Core.Entities.Enums.PassTemplateType passTemplateType)
        {
            throw new NotImplementedException();
        }

        public IList<Core.Entities.PassField> GetPassFields(int passTemplateId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassTemlate(string passTemplatePath)
        {
            throw new NotImplementedException();
        }


        public bool ValidatePassTemplate(string passTemplateFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
