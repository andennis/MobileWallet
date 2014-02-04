using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.BL;
using FileStorage.Core;
using Pass.Container.BL.NativePassTemplateGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.PassTemplate;
using Pass.Container.Core.Exceptions;

namespace Pass.Container.BL
{
    public class PassTemplateService : IPassTemplateService
    {
        private const string PassTemplateFolder = "PassTemplate";
        private readonly IPassContainerConfig _pcConfig;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        private readonly IDictionary<PassTemplateType, INativePassTemplateGenerator> _passTemplateGenerators;

        public PassTemplateService(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
            _pcConfig = config;
            _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            _passTemplateGenerators = new Dictionary<PassTemplateType, INativePassTemplateGenerator>
                {
                    {PassTemplateType.AppleTemplate, new ApplePassTemplateGenerator()}
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
            int storageItemId = _fsService.PutFolder(passTemplatePath, true);
            string storageItemPath = _fsService.GetStorageItemPath(storageItemId);
            
            //Generate native pass templates
            //TODO Get passTmplate object from pass template file
            PassTemplate passTemplate = new PassTemplate();
            foreach (var nativePassTemplateGenerator in _passTemplateGenerators)
            {
                nativePassTemplateGenerator.Value.Generate(passTemplate, storageItemPath);
            }
            
            //TODO Store nessesary intormation into DB

            return storageItemId;

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
