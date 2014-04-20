using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FileStorage.Core;
using Org.BouncyCastle.Cms;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.PassGenerators
{
    public class ApplePassGenerator : BasePassGenerator, IPassGenerator
    {
        private const string CertificatePassword = "Pass3";
        private const string CertificateFilesPath = @"e:\Wallet_SVN\MobileWallet\Documentation\Passbook\Certificates\pass.com.passlight.dev.test";
        private const string ApplePassTemplateFolderName = "ApplePassTemplate";
        private const string ApplePassTempFolderName = "Apple";
        private const string ApplePassTemplateJson = "pass.json";
        private readonly IPassContainerConfig _config;
        private readonly Dictionary<string, CmsSignedDataGenerator> _certificates = new Dictionary<string, CmsSignedDataGenerator>();

        public ApplePassGenerator(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
            :base(pcUnitOfWork, fsService)
        {
            _config = config;
        }

        public string GeneratePass(int passId)
        {
            RepEntities.Pass pass = GetPass(passId);

            //Certificate processing
            if (!_certificates.ContainsKey(pass.PassTypeIdentifier))
            {
                var generator = ApplePassGeneratorHelper.GetCmsSignedDataGenerator(CertificateFilesPath, CertificatePassword);
                if (generator == null)
                    throw new PassGenerationException("Certificate processing was failed.");
                _certificates.Add(pass.PassTypeIdentifier, generator);
            }

            //Get storage item path
            DateTime lastUpdateDateTime;
            string storageItemPath = GetStorageItemPath(pass.PassTemplateId, out lastUpdateDateTime);
            if (storageItemPath == null)
                throw new PassGenerationException("Apple pass template file storage item was not found.");

            //Copy files from FileStorage into temporary pass template folder (if not exist)
            string templateTempFolderPath = Path.Combine(_config.PassGeneratorTempFolderPath, ApplePassTempFolderName, pass.PassTemplateId.ToString());
            string lastUpdateTemplateFolder = Path.Combine(templateTempFolderPath, lastUpdateDateTime.ToString("dd-MM-yyyy h-mm-ss"));
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
            string passFolder = Path.Combine(lastUpdateTemplateFolder, pass.PassId.ToString());
            if (Directory.Exists(passFolder))
                Directory.Delete(passFolder, true);
            Directory.CreateDirectory(passFolder);
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

            //Set serial number if SerialNumberType.AutoGgenerated
            if (passJsonText.Contains("SerialNumber_SN_"))
            {
                string snRegExpression = @"SerialNumber_SN_";
                passJsonText = Regex.Replace(passJsonText, snRegExpression, Guid.NewGuid().ToString());
            }

            //Get dynamic fields
            Dictionary<PassField, PassFieldValue> dynamicFields = GetDynamicFields(pass);

            //Replace dynamic labels and values
            foreach (KeyValuePair<PassField, PassFieldValue> passFieldValue in dynamicFields)
            {
                if (passFieldValue.Value.Label != null)
                {
                    string labelRegExpression = @"Label\${2}" + passFieldValue.Key.Name + @"\${2}";
                    passJsonText = Regex.Replace(passJsonText, labelRegExpression, passFieldValue.Value.Label);
                }

                if (passFieldValue.Value.Value != null)
                {
                    string valueRegExpression = @"Value\${2}" + passFieldValue.Key.Name + @"\${2}";
                    passJsonText = Regex.Replace(passJsonText, valueRegExpression, passFieldValue.Value.Value);
                }
            }
            File.WriteAllText(applePassJsonFilePath, passJsonText);

            //string pkpassFilePath = ApplePassGeneratorHelper.GenaratePassPackage(passFolder, CertificateFilesPath, CertificatePassword);
            string pkpassFilePath = ApplePassGeneratorHelper.GenaratePassPackage(passFolder,_certificates[pass.PassTypeIdentifier]);

            return pkpassFilePath;
        }
    }
}
