using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Repository.EF;
using Pass.Container.Core;
using Pass.Container.Core.Exceptions;

namespace Pass.Container.BL
{
    public class PassTemplateService : IPassTemplateService
    {
        private readonly IPassContainerConfig _pcConfig;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;

        public PassTemplateService(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
            _pcConfig = config;
            _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;
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

            //TODO Check files in passTemplatePath

            int storageItemId = _fsService.PutFolder(passTemplatePath, true);
            string storageItemPath = _fsService.GetStorageItemPath(storageItemId);

            return 0;

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
    }
}
