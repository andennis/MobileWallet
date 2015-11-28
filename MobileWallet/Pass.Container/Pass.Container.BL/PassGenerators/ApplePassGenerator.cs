using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Common.Extensions;
using Common.Utils;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Exceptions;

namespace Pass.Container.BL.PassGenerators
{
    public class ApplePassGenerator : IPassGenerator
    {
        private static readonly MemCache<string, SignedDataGenerator> _cacheSignedDataGenerators = new MemCache<string, SignedDataGenerator>("SignedDataGenerators", new TimeSpan(0, 5, 0));

        private readonly IApplePassGeneratorConfig _config;
        private readonly string _templateFilesPath;
        private readonly X509Certificate2 _signCert;
        private static X509Certificate2 _appleWWDRCA;

        public ApplePassGenerator(IApplePassGeneratorConfig config, string templateFilesPath, X509Certificate2 signCert)
        {
            _config = config;
            _templateFilesPath = templateFilesPath;
            _signCert = signCert;
        }

        private X509Certificate2 AppleWWDRCA
        {
            get
            {
                return _appleWWDRCA ?? (_appleWWDRCA = new X509Certificate2(_config.AppleWWDRCAPath));
            }
        }

        public string GeneratePass(string authToken, string serialNumber, IEnumerable<PassFieldInfo> fields, string dstPassFilesPath)
        {
            //Pass content
            string passContent = BuildPassContent(authToken, serialNumber, fields);
            string contentFilePath = Path.Combine(dstPassFilesPath, ApplePass.PassFileName);
            File.WriteAllText(contentFilePath, passContent);

            //Manifest
            string manifest = BuildManifest(passContent);
            string manifestFilePath = Path.Combine(dstPassFilesPath, ApplePass.ManifestFileName);
            File.WriteAllText(manifestFilePath, manifest);

            //Signature
            string signatureFilePath = Path.Combine(dstPassFilesPath, ApplePass.SignatureFileName);
            byte[] signature = BuildSignature(manifest);
            File.WriteAllBytes(signatureFilePath, signature);

            //Package
            string packageFileName = Path.ChangeExtension(Path.GetRandomFileName(), ".pkpass");
            string packageFilePath = Path.Combine(dstPassFilesPath, packageFileName);
            BuildPackage(contentFilePath, manifestFilePath, signatureFilePath, packageFilePath);

            //Remove unnecessary files
            File.Delete(contentFilePath);
            File.Delete(manifestFilePath);
            File.Delete(signatureFilePath);

            return packageFilePath;
        }

        private string BuildPassContent(string authToken, string serialNumber, IEnumerable<PassFieldInfo> fields)
        {
            if (serialNumber == null)
                throw new ArgumentNullException("serialNumber");

            //Load pass template content file
            string filePath = Path.Combine(_templateFilesPath, ApplePass.PassTemplateFileName);
            if (!File.Exists(filePath))
                throw new PassContainerException(string.Format("File '{0}' not found", filePath));

            string passContent = File.ReadAllText(filePath);

            //Replace WebServiceUrl
            passContent = passContent.ReplaceFirst(ApplePass.FieldWebServiceUrl, _config.AppleWebServerUrl);

            //Replace authentication token
            passContent = passContent.ReplaceFirst(ApplePass.FieldAuthToken, authToken);

            //Replace serial number
            passContent = passContent.ReplaceFirst(ApplePass.FieldSerialNumber, serialNumber);

            //Replace field labels and values
            foreach (PassFieldInfo pfInfo in fields)
            {
                //Replace label
                string fieldName = string.Format(ApplePass.FieldLabelFormat, pfInfo.Name);
                passContent = passContent.Replace(fieldName, pfInfo.Label);

                //Replace value
                fieldName = string.Format(ApplePass.FieldValueFormat, pfInfo.Name);
                passContent = passContent.Replace(fieldName, pfInfo.Value);
            }

            //Clear missed fields (labels and values)
            MatchCollection missedFields = Regex.Matches(passContent, @"(LB|VL)\$\$.+?\$\$");
            int len = 0;
            int prevInd = 0;
            foreach (Match missedField in missedFields)
            {
                int nextInd = (missedField.Index > prevInd) ? missedField.Index - len : missedField.Index;
                passContent = passContent.ReplaceFirst(missedField.Value, string.Empty, nextInd);
                len += missedField.Length;
                prevInd = nextInd;
                //TODO log a warning regarding missed key
            }

            return passContent;
        }
        private string BuildManifest(string passContent)
        {
            string filePath = Path.Combine(_templateFilesPath, ApplePass.ManifestTemplateFileName);
            IDictionary<string, string> dictManifest;
            if (File.Exists(filePath))
            {
                string manifestJson = File.ReadAllText(filePath);
                dictManifest = manifestJson.JsonToObject<Dictionary<string, string>>();
            }
            else
            {
                dictManifest = new Dictionary<string, string>();
            }

            //Add hash for missed image files
            string imagesPath = Path.Combine(_templateFilesPath, ApplePass.TemplateImageFolder);
            foreach (string imagePath in Directory.EnumerateFiles(imagesPath))
            {
                string fileName = Path.GetFileName(imagePath);
                if (!dictManifest.ContainsKey(fileName))
                {
                    byte[] fileData = File.ReadAllBytes(imagePath);
                    string fileHash = Crypto.CalculateHash(fileData);
                    dictManifest.Add(fileName, fileHash);
                }
            }

            //Add hash of pass content
            Encoding encoding = new UTF8Encoding();
            byte[] contentData = encoding.GetBytes(passContent);
            string contentHash = Crypto.CalculateHash(contentData);
            dictManifest.Add(ApplePass.PassFileName, contentHash);

            return dictManifest.ObjectToJson();
        }
        private byte[] BuildSignature(string manifest)
        {
            SignedDataGenerator signGenerator = GetSignedDataGenerator(_signCert);
            return signGenerator.SignText(manifest);
        }
        private void BuildPackage(string passContentFile, string manifestFile, string signatureFile, string dstFileName)
        {
            IEnumerable<string> files = new string[] {passContentFile, manifestFile, signatureFile};
            string imagesPath = Path.Combine(_templateFilesPath, ApplePass.TemplateImageFolder);
            files = Directory.EnumerateFiles(imagesPath).Union(files);
            Compress.CompressFiles(files, dstFileName);
        }

        private SignedDataGenerator GetSignedDataGenerator(X509Certificate2 signCert)
        {
            SignedDataGenerator generator = _cacheSignedDataGenerators[signCert.SerialNumber];
            if (generator != null)
                return generator;

            generator = new SignedDataGenerator(new X509Certificate2[] { this.AppleWWDRCA, signCert });
            _cacheSignedDataGenerators.Add(signCert.SerialNumber, generator);

            return generator;
        }

    }
}
