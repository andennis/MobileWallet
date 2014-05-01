using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Common.Utils;
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
        //private const string CertificatePassword = "Pass3";
        //private const string CertificateFilesPath = @"e:\Wallet_SVN\MobileWallet\Documentation\Passbook\Certificates\pass.com.passlight.dev.test";
        private const string ApplePassTemplateFolderName = "ApplePassTemplate";
        private const string ApplePassTempFolderName = "Apple";
        private const string ApplePassTemplateJson = "pass.json";
        private readonly IApplePassGeneratorConfig _config;
        //private readonly IDictionary<string, CmsSignedDataGenerator> _signedDataGenerators = new Dictionary<string, CmsSignedDataGenerator>();
        private readonly MemCache<string, CmsSignedDataGenerator> _signedDataGeneratorsCache = new MemCache<string, CmsSignedDataGenerator>("SignedDataGenerators", new TimeSpan(0, 5, 0));

        public ApplePassGenerator(IApplePassGeneratorConfig config, 
            IPassContainerUnitOfWork pcUnitOfWork,
            IFileStorageService fsService,
            IPassCertificateService certService)
            :base(pcUnitOfWork, fsService, certService)
        {
            _config = config;
        }

        private CmsSignedDataGenerator GetSignedDataGenerator(X509Certificate2 signCert)
        {
            CmsSignedDataGenerator generator = _signedDataGeneratorsCache[signCert.SerialNumber];
            if (generator != null)
                return generator;

            var appleCert = new X509Certificate2(_config.AppleWWDRCAPath);
            generator = ApplePassGeneratorHelper.GetCmsSignedDataGenerator(signCert, appleCert);
            _signedDataGeneratorsCache.Add(signCert.SerialNumber, generator);

            return generator;
        }

        public string GeneratePass(int passId)
        {
            return null;
            /*
            RepEntities.Pass pass = GetPass(passId);

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

            //Set serial number if SerialNumberType.AutoGenerated
            const string snRegExpression = "SerialNumber_SN_";
            if (passJsonText.Contains(snRegExpression))
                passJsonText = Regex.Replace(passJsonText, snRegExpression, Guid.NewGuid().ToString());

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


            int certId = pass.Template.NativeTemplates.OfType<PassTemplateApple>().First().CertificateId;
            X509Certificate2 passCert = _certService.GetCertificate(certId);
            CmsSignedDataGenerator signGenerator = GetSignedDataGenerator(passCert);

            //string pkpassFilePath = ApplePassGeneratorHelper.GenaratePassPackage(passFolder, CertificateFilesPath, CertificatePassword);
            string pkpassFilePath = Path.Combine(passFolder, Path.GetRandomFileName() + ".pkpass");
            ApplePassGeneratorHelper.GenaratePassPackage(passFolder, signGenerator, pkpassFilePath);
            return pkpassFilePath;
            */
        }
    }
}
