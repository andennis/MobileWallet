using System;
using System.Collections.Generic;
using System.IO;
using Common.Repository;
using FileStorage.Core;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public class PassTemplateStorageService : IPassTemplateStorageService
    {
        private const string BaseTemplateFolder = "BaseTemplate";
        private readonly IDictionary<ClientType, string> _nativeTemplateFolders = new Dictionary<ClientType, string>()
                                                                                      {
                                                                                          {ClientType.Browser, "Browser"},
                                                                                          {ClientType.Apple, "Apple"}
                                                                                      };

        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;

        private readonly IRepository<PassTemplate> _passTemplateRep;

        public PassTemplateStorageService(IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
            _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            _passTemplateRep = _pcUnitOfWork.GetRepository<PassTemplate>();
        }

        public void PutBaseTemplateFiles(int passTemplateId, string srcTemplateFolderPath)
        {
            PutTemplateFiles(passTemplateId, srcTemplateFolderPath, BaseTemplateFolder);
        }

        public void PutNativeTemplateFiles(int passTemplateId, ClientType clientType, string srcTemplateFolderPath)
        {
            if (clientType == ClientType.Unknown)
                throw new PassTemplateException("Unknown client type is not allowed");

            PutTemplateFiles(passTemplateId, srcTemplateFolderPath, _nativeTemplateFolders[clientType]);
        }

        public void GetBaseTemplateFiles(int passTemplateId, string dstFolderPath)
        {
            if (string.IsNullOrEmpty(dstFolderPath))
                throw new ArgumentNullException("dstFolderPath");

            GetTemplateFiles(passTemplateId, BaseTemplateFolder, dstFolderPath);
        }

        public void GetNativeTemplateFiles(int passTemplateId, ClientType clientType, string dstFolderPath)
        {
            if (clientType == ClientType.Unknown)
                throw new PassTemplateException("Unknown client type is not allowed");

            GetTemplateFiles(passTemplateId, _nativeTemplateFolders[clientType], dstFolderPath);
        }

        private void PutTemplateFiles(int passTemplateId, string srcTemplateFolderPath, string dstTemplateFolderName)
        {
            if (string.IsNullOrEmpty(srcTemplateFolderPath))
                throw new ArgumentNullException("srcTemplateFolderPath");

            string siPath = GetTemplateFileStoragePath(passTemplateId);
            string templatePath = Path.Combine(siPath, dstTemplateFolderName);
            if (!Directory.Exists(templatePath))
                Directory.CreateDirectory(templatePath);

            foreach (string srcFilePath in Directory.GetFiles(srcTemplateFolderPath))
            {
                string fileName = Path.GetFileName(srcFilePath);
                File.Copy(srcFilePath, Path.Combine(templatePath, fileName));
            }
        }
        private void GetTemplateFiles(int passTemplateId, string srcTemplateFolderName, string dstTemplateFolderPath)
        {
            if (string.IsNullOrEmpty(dstTemplateFolderPath))
                throw new ArgumentNullException("dstTemplateFolderPath");

            string siPath = GetTemplateFileStoragePath(passTemplateId);
            string templatePath = Path.Combine(siPath, srcTemplateFolderName);
            if (!Directory.Exists(templatePath))
                throw new PassTemplateException(string.Format("Directory '{0}' not found", templatePath));

            foreach (string srcFilePath in Directory.GetFiles(templatePath))
            {
                string fileName = Path.GetFileName(srcFilePath);
                File.Copy(srcFilePath, Path.Combine(dstTemplateFolderPath, fileName));
            }
        }
        private string GetTemplateFileStoragePath(int passTemplateId)
        {
            PassTemplate passTemplate = _passTemplateRep.Find(passTemplateId);
            if (passTemplate == null)
                throw new PassTemplateException(string.Format("Pass template ID: {0} not found", passTemplateId));

            return _fsService.GetStorageItemPath(passTemplate.PackageId);
        }
    }
}
