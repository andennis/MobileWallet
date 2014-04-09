using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Common.Repository;
using Common.Utils;
using FileStorage.Core;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.NativePassGenerators
{
    public class ApplePassGenerator : BasePassGenerator
    {
        private const string CertificatePassword = "Pass3";
        private const string CertificateFilePath = @"e:\Wallet_SVN\MobileWallet\Documentation\Passbook\Certificates\pass.com.passlight.dev.test\pass.p12";
        private const string AppleWwdrcaCertificateFilePath = @"e:\Wallet_SVN\MobileWallet\Documentation\Passbook\Certificates\pass.com.passlight.dev.test\AppleWWDRCA.cer";
        private const string ApplePassTemplateFolderName = "ApplePassTemplate";
        private const string ApplePassTempFolderName = "Apple";
        private const string ApplePassTemplateJson = "pass.json";
        private readonly IPassContainerConfig _config;
        private readonly ApplePassGeneratorHelper _generatorHelper;       

        public ApplePassGenerator(IPassContainerConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService, int passId)
            :base(pcUnitOfWork, fsService, passId)
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
            var lastUpdateDateTime = new DateTime();
            string storageItemPath = GetStorageItemPath(ref lastUpdateDateTime);
            if (storageItemPath == null)
                throw new PassGenerationException("Apple pass template file storage item was not found.");

            //Copy files from FileStorage into temporary pass template folder (if not exist)
            string templateTempFolderPath = Path.Combine(_config.PassGeneratorTempFolderPath, ApplePassTempFolderName, _pass.PassTemplateId.ToString());
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
            string passFolder = Path.Combine(lastUpdateTemplateFolder, _pass.PassId.ToString());
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

            //Get dynamic fields
            Dictionary<PassField, PassFieldValue> dynamicFields = GetDynamicFields();

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



            _generatorHelper.SignManigestFile(CertificateFilePath, CertificatePassword, AppleWwdrcaCertificateFilePath, passFolder);

            ////Get certificate for pass
            //X509Certificate2 certificate = _generatorHelper.GetCertificateFromFile(CertificateFilePath, CertificatePassword);

            ////Sign pass files
            //_generatorHelper.SignPassFiles(passFolder, certificate);

            //Generate pkpass file
            string pkpassFilePath = Path.Combine(passFolder, _pass.SerialNumber + ".pkpass");
            Compress.CompressDirectory(passFolder, pkpassFilePath);

            return pkpassFilePath;
        }
    }
}
