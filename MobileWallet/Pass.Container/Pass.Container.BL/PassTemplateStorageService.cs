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

        private readonly IFileStorageService _fsService;

        public PassTemplateStorageService(IFileStorageService fsService)
        {
            _fsService = fsService;
        }

        public int CreateTemplateStorage()
        {
            string sf;
            return _fsService.CreateStorageFolder(out sf);
        }

        public void PutBaseTemplateFiles(int templateStorageId, string srcTemplateFolderPath)
        {
            PutTemplateFiles(templateStorageId, srcTemplateFolderPath, BaseTemplateFolder);
        }
        public void PutNativeTemplateFiles(int templateStorageId, ClientType clientType, string srcTemplateFolderPath)
        {
            if (clientType == ClientType.Unknown)
                throw new PassTemplateException("Unknown client type is not allowed");

            PutTemplateFiles(templateStorageId, srcTemplateFolderPath, _nativeTemplateFolders[clientType]);
        }

        public void GetBaseTemplateFiles(int templateStorageId, string dstFolderPath)
        {
            if (string.IsNullOrEmpty(dstFolderPath))
                throw new ArgumentNullException("dstFolderPath");

            GetTemplateFiles(templateStorageId, BaseTemplateFolder, dstFolderPath);
        }
        public void GetNativeTemplateFiles(int templateStorageId, ClientType clientType, string dstFolderPath)
        {
            if (clientType == ClientType.Unknown)
                throw new PassTemplateException("Unknown client type is not allowed");

            GetTemplateFiles(templateStorageId, _nativeTemplateFolders[clientType], dstFolderPath);
        }

        private void PutTemplateFiles(int templateStorageId, string srcTemplateFolderPath, string dstTemplateFolderName)
        {
            if (string.IsNullOrEmpty(srcTemplateFolderPath))
                throw new ArgumentNullException("srcTemplateFolderPath");

            string siPath = _fsService.GetStorageItemPath(templateStorageId);
            string templatePath = Path.Combine(siPath, dstTemplateFolderName);
            if (Directory.Exists(templatePath))
                Directory.Delete(templatePath, true);

            Directory.CreateDirectory(templatePath);

            foreach (string srcFilePath in Directory.GetFiles(srcTemplateFolderPath))
            {
                string fileName = Path.GetFileName(srcFilePath);
                File.Copy(srcFilePath, Path.Combine(templatePath, fileName));
            }
        }
        private void GetTemplateFiles(int templateStorageId, string srcTemplateFolderName, string dstTemplateFolderPath)
        {
            if (string.IsNullOrEmpty(dstTemplateFolderPath))
                throw new ArgumentNullException("dstTemplateFolderPath");

            string siPath = _fsService.GetStorageItemPath(templateStorageId);
            string templatePath = Path.Combine(siPath, srcTemplateFolderName);
            if (!Directory.Exists(templatePath))
                throw new PassTemplateException(string.Format("Directory '{0}' not found", templatePath));

            foreach (string srcFilePath in Directory.GetFiles(templatePath))
            {
                string fileName = Path.GetFileName(srcFilePath);
                File.Copy(srcFilePath, Path.Combine(dstTemplateFolderPath, fileName));
            }
        }
    }
}
