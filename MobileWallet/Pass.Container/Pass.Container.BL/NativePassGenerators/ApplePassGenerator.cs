﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Utils;
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
        private const string CertificatFilePath = @"C:\Certificate.cer";
        private const string ApplePassTemplateFolderName = "ApplePassTemplate";
        private const string ApplePassTempFolderName = "Apple";
        private const string ApplePassTemplateJson = "pass.json";
        private readonly IPassContainerConfig _config;
        private Core.Entities.Pass _pass;
        private ApplePassGeneratorHelper _generatorHelper;

        public ApplePassGenerator(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService, Core.Entities.Pass pass)
            :base(pcUnitOfWork, fsService, pass)
        {
            _config = config;
            _pass = pass;
            _generatorHelper = new ApplePassGeneratorHelper();
        }

        public ClientType ClientType
        {
            get { return ClientType.Apple; }
        }

        public string GeneratePass()
        {
            //Get storage item path
            var lastUpdateDateTime = new DateTime();
            string storageItemPath = GetStorageItemPath(ref lastUpdateDateTime);
            if (storageItemPath == null)
                throw new PassGenerationException("Apple pass template file storage item was not found.");

            //Copy files from FileStorage into temporary pass template folder (if not exist)
            string templateTempFolderPath = Path.Combine(_config.PassGeneratorTempFolderPath, ApplePassTempFolderName, _pass.PassTemplateId.ToString());
            string lastUpdateTemplateFolder = Path.Combine(templateTempFolderPath, lastUpdateDateTime.ToString());
            if (!Directory.Exists(lastUpdateTemplateFolder))
            {
                if (Directory.Exists(templateTempFolderPath))
                    Directory.Delete(templateTempFolderPath, true);
                Directory.CreateDirectory(lastUpdateTemplateFolder);
                string[] files = Directory.GetFiles(Path.Combine(storageItemPath, ApplePassTemplateFolderName));
                foreach (string sourceFileName in files)
                {
                    string fileName = Path.GetFileName(sourceFileName);
                    string destFileName = Path.Combine(lastUpdateTemplateFolder, fileName);
                    File.Copy(sourceFileName, destFileName, true);
                }
            }

            //Copy files from temporary pass template folder into particular temporary pass folder
            string passFolder = Path.Combine(lastUpdateTemplateFolder, _pass.PassId.ToString());
            string[] templateFiles = Directory.GetFiles(lastUpdateTemplateFolder);
            foreach (string sourceFileName in templateFiles)
            {
                string fileName = Path.GetFileName(sourceFileName);
                string destFileName = Path.Combine(passFolder, fileName);
                File.Copy(sourceFileName, destFileName, true);
            }

            string applePassJsonFilePath = Path.Combine(passFolder, ApplePassTemplateJson);
            if (!File.Exists(applePassJsonFilePath))
                throw new PassGenerationException(String.Format("{0} file was not found. File path: {1}", ApplePassTemplateJson, applePassJsonFilePath));
            string passJsonText = File.ReadAllText(applePassJsonFilePath);

            //Get dynamic fields
            Dictionary<PassField, PassFieldValue> dynamicFields = GetDynamicFields();

            //Replace dynamic labels and values
            foreach (KeyValuePair<PassField, PassFieldValue> passFieldValue in dynamicFields)
            {
                string labelRegExpression = @"Label\${2}" + passFieldValue.Key + @"\${2}";
                passJsonText = Regex.Replace(passJsonText, labelRegExpression, passFieldValue.Value.Label);
                
                string valueRegExpression = @"Value\${2}" + passFieldValue.Key + @"\${2}";
                passJsonText = Regex.Replace(passJsonText, valueRegExpression, passFieldValue.Value.Value);
            }
            File.WriteAllText(applePassJsonFilePath, passJsonText);

            //Get certificate for pass
            X509Certificate2 certificate = _generatorHelper.GetCertificateFromFile(CertificatFilePath);

            //Sign pass files
            _generatorHelper.SignPassFiles(passFolder, certificate);

            //Generate pkpass file
            string pkpassFilePath = Path.Combine(passFolder, _pass.SerialNumber + ".pkpass");
            Compress.CompressDirectory(passFolder, pkpassFilePath);

            return pkpassFilePath;
        }
    }
}
