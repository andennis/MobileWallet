using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileStorage.Core;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;

namespace Pass.Container.BL.NativePassGenerators
{
    public class ApplePassGenerator : BasePassGenerator
    {
        private const string ApplePassTemplateFolderName = "ApplePassTemplate";
        private const string ApplePassTemplateJson = "pass.json";
        private readonly IPassContainerConfig _config;
        private ApplePassGeneratorHelper _generatorHelper;

        public ApplePassGenerator(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService, Core.Entities.Pass pass)
            :base(pcUnitOfWork, fsService, pass)
        {
            _config = config;
            _generatorHelper = new ApplePassGeneratorHelper();
        }

        public ClientType ClientType
        {
            get { return ClientType.Apple; }
        }

        public string GeneratePass()
        {
            //Get storage item path
            string storageItemPath = GetStorageItemPath();
            if (storageItemPath == null)
                throw new PassGenerationException("Apple pass template file storage item was not found.");


            //Copy files from FileStorage into temporary pass generator folder
            if (!Directory.Exists(_config.PassGeneratorTempFolderPath))
                Directory.CreateDirectory(_config.PassGeneratorTempFolderPath);

            string[] files = Directory.GetFiles(Path.Combine(storageItemPath, ApplePassTemplateFolderName));
            foreach (string sourceFileName in files)
            {
                string fileName = Path.GetFileName(sourceFileName);
                string destFileName = Path.Combine(_config.PassGeneratorTempFolderPath, fileName);
                File.Copy(sourceFileName, destFileName, true);
            }

            string passJsonFilePath = Path.Combine(_config.PassGeneratorTempFolderPath, ApplePassTemplateJson);
            if (!File.Exists(passJsonFilePath))
                throw new PassGenerationException(String.Format("{0} file was not found. File path: {1}", ApplePassTemplateJson, passJsonFilePath));
            string passJsonText = File.ReadAllText(passJsonFilePath);

            //Get dynamic fields
            Dictionary<PassField, PassFieldValue> dynamicFields = GetDynamicFields();
            foreach (KeyValuePair<PassField, PassFieldValue> passFieldValue in dynamicFields)
            {
                //Label$$" + templatefield.Key + "$$
                //passJsonText = Regex.Replace(passJsonText, @"\bplay\b", "123");
                //File.WriteAllText(@"c:\File1.txt", text);

               
            }
            return null;
        }
    }
}
