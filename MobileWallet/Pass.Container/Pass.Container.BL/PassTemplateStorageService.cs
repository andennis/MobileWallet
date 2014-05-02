using System;
using System.Collections.Generic;
using System.IO;
using Common.Utils;
using FileStorage.Core;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;

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
            string dstTemplatePath = Path.Combine(siPath, dstTemplateFolderName);
            if (Directory.Exists(dstTemplatePath))
                Directory.Delete(dstTemplatePath, true);

            Directory.CreateDirectory(dstTemplatePath);
            FileHelper.DirectoryCopy(srcTemplateFolderPath, dstTemplatePath, true, true);
        }
        private void GetTemplateFiles(int templateStorageId, string srcTemplateFolderName, string dstTemplateFolderPath)
        {
            if (string.IsNullOrEmpty(dstTemplateFolderPath))
                throw new ArgumentNullException("dstTemplateFolderPath");

            string siPath = _fsService.GetStorageItemPath(templateStorageId);
            string srcTemplatePath = Path.Combine(siPath, srcTemplateFolderName);
            if (!Directory.Exists(srcTemplatePath))
                throw new PassTemplateException(string.Format("Directory '{0}' not found", srcTemplatePath));

            FileHelper.DirectoryCopy(srcTemplatePath, dstTemplateFolderPath, true, true);
        }
    }
}
