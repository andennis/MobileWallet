using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Common.Extensions;
using Common.Utils;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;

namespace Pass.Container.BL.Helpers
{
    public static class ApplePassGeneratorHelper
    {
        private const string PassTypeIdCerName = "pass.p12";
        private const string AppleWwdrcaCerName = "AppleWWDRCA.cer";

        public static string GenaratePassPackage(string dirPath, string cerPath, string password)
        {
            string pkpassFilePath = null;

            //Trace.TraceInformation("Generating the manifest file...");
            string manifestJson = GetManifestJson(dirPath);
            GenerateManifestFile(dirPath, manifestJson);

            //Trace.TraceInformation("Signing the manifest file...");
            string certificateFilePath = Path.Combine(cerPath, PassTypeIdCerName);
            X509Certificate2 card = GetCertificateFromFile(certificateFilePath, password);
            if (card == null)
            {
                throw new FileNotFoundException("Certificate could not be found.");
            }
            try
            {
                Org.BouncyCastle.X509.X509Certificate cert = DotNetUtilities.FromX509Certificate(card);
                Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = DotNetUtilities.GetKeyPair(card.PrivateKey).Private;

                //Trace.TraceInformation("Fetching Apple Certificate for signing..");
                string appleWwdrcaCertificateFilePath = Path.Combine(cerPath, AppleWwdrcaCerName);
                X509Certificate2 appleCA = GetCertificateFromFile(appleWwdrcaCertificateFilePath, null);
                Org.BouncyCastle.X509.X509Certificate appleCert = DotNetUtilities.FromX509Certificate(appleCA);

                //Trace.TraceInformation("Constructing the certificate chain..");
                var intermediateCerts = new ArrayList() { appleCert, cert };

                var PP = new Org.BouncyCastle.X509.Store.X509CollectionStoreParameters(intermediateCerts);
                Org.BouncyCastle.X509.Store.IX509Store st1 = Org.BouncyCastle.X509.Store.X509StoreFactory.Create("CERTIFICATE/COLLECTION", PP);

                var generator = new CmsSignedDataGenerator();

                generator.AddSigner(privateKey, cert, CmsSignedDataGenerator.DigestSha1);
                generator.AddCertificates(st1);

                //Trace.TraceInformation("Processing the signature..");
                CmsProcessable content = new CmsProcessableByteArray(Encoding.ASCII.GetBytes(manifestJson));
                CmsSignedData signedData = generator.Generate(content, false);

                byte[] signatureBytes = signedData.GetEncoded();
                File.WriteAllBytes(Path.Combine(dirPath, "signature"), signatureBytes);

                //Trace.TraceInformation("The file has been successfully signed!");
            }
            catch (Exception exp)
            {
                Trace.TraceError("Failed to sign the manifest file: [{0}]", exp.Message);
            }
            //Generate pkpass file
            try
            {
                //Trace.TraceInformation("Generating .pkpass archive..");
                pkpassFilePath = Path.Combine(dirPath, Guid.NewGuid() + ".pkpass");
                Compress.CompressDirectory(dirPath, pkpassFilePath);
                //Trace.TraceInformation("The .pkpass archive has been successfully Generated!");
            }
            catch (Exception exp)
            {
                Trace.TraceError("Failed to generate .pkpass archive: [{0}]", exp.Message);
            }
            return pkpassFilePath;
        }

        public static string GenaratePassPackage(string dirPath, CmsSignedDataGenerator cmsSignedDataGenerator)
        {
            string pkpassFilePath = null;

            //Trace.TraceInformation("Generating the manifest file...");
            string manifestJson = GetManifestJson(dirPath);
            GenerateManifestFile(dirPath, manifestJson);

            //Trace.TraceInformation("Signing the manifest file...");
               
            CmsProcessable content = new CmsProcessableByteArray(Encoding.ASCII.GetBytes(manifestJson));
            CmsSignedData signedData = cmsSignedDataGenerator.Generate(content, false);

            byte[] signatureBytes = signedData.GetEncoded();
            File.WriteAllBytes(Path.Combine(dirPath, "signature"), signatureBytes);

           //Trace.TraceInformation("The file has been successfully signed!");
           
            //Generate pkpass file
           //Trace.TraceInformation("Generating .pkpass archive..");
           pkpassFilePath = Path.Combine(dirPath, Guid.NewGuid() + ".pkpass");
           Compress.CompressDirectory(dirPath, pkpassFilePath);
           //Trace.TraceInformation("The .pkpass archive has been successfully Generated!");
           
            return pkpassFilePath;
        }

        public static CmsSignedDataGenerator GetCmsSignedDataGenerator(string cerPath, string password)
        {
            CmsSignedDataGenerator generator = null;
            string certificateFilePath = Path.Combine(cerPath, PassTypeIdCerName);
            X509Certificate2 card = GetCertificateFromFile(certificateFilePath, password);
            if (card == null)
            {
                throw new FileNotFoundException("Certificate could not be found.");
            }
            try
            {
                Org.BouncyCastle.X509.X509Certificate cert = DotNetUtilities.FromX509Certificate(card);
                Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = DotNetUtilities.GetKeyPair(card.PrivateKey).Private;

                //Trace.TraceInformation("Fetching Apple Certificate for signing..");
                string appleWwdrcaCertificateFilePath = Path.Combine(cerPath, AppleWwdrcaCerName);
                X509Certificate2 appleCA = GetCertificateFromFile(appleWwdrcaCertificateFilePath, null);
                Org.BouncyCastle.X509.X509Certificate appleCert = DotNetUtilities.FromX509Certificate(appleCA);

                //Trace.TraceInformation("Constructing the certificate chain..");
                var intermediateCerts = new ArrayList() { appleCert, cert };

                var PP = new Org.BouncyCastle.X509.Store.X509CollectionStoreParameters(intermediateCerts);
                Org.BouncyCastle.X509.Store.IX509Store st1 = Org.BouncyCastle.X509.Store.X509StoreFactory.Create("CERTIFICATE/COLLECTION", PP);

                generator = new CmsSignedDataGenerator();
                generator.AddSigner(privateKey, cert, CmsSignedDataGenerator.DigestSha1);
                generator.AddCertificates(st1);
                return generator;
            }
            catch (Exception exp)
            {
                Trace.TraceError("Failed to sign the manifest file: [{0}]", exp.Message);
            }
            return null;
        }

        private static string GetManifestJson(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                throw new DirectoryNotFoundException("Directory was not found. Directory path: " + dirPath);

            var hashOfFiles = new Dictionary<string, string>();

            var dirInfo = new DirectoryInfo(dirPath);
            foreach (var file in dirInfo.GetFiles())
            {
                string hash = GenerateHashOfFile(file.FullName);
                hashOfFiles.Add(file.Name, hash);
            }
            if (!(hashOfFiles.Count > 0) || !hashOfFiles.ContainsKey("pass.json"))
                throw new FileNotFoundException("Necessary files does not found in the directory. Directory path: " + dirPath);
            return hashOfFiles.ObjectToJson();
        }

        private static string GenerateHashOfFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File was not found. File path: " + filePath);

            byte[] fileData = File.ReadAllBytes(filePath);
            return GenerateHash(fileData);
        }

        private static string GenerateHash(byte[] fileData)
        {
            var hashText = new StringBuilder();
            byte[] hashData = SHA1.Create().ComputeHash(fileData); // SHA1 or MD5

            foreach (byte b in hashData)
            {
                string hexValue = b.ToString("X").ToLower(); // Lowercase for compatibility on case-sensitive systems
                hashText.Append((hexValue.Length == 1 ? "0" : string.Empty) + hexValue);
            }

            return hashText.ToString();
        }

        private static void GenerateManifestFile(string dirPath, string manifestJson)
        {
            if (!Directory.Exists(dirPath))
                throw new DirectoryNotFoundException("Directory was not found. Directory path: " + dirPath);

            File.WriteAllText(Path.Combine(dirPath, "manifest.json"), manifestJson);
        }

        private static byte[] SignByCertificate(string manifest, X509Certificate2 certificate)
        {
            byte[] manifestBytes = Encoding.ASCII.GetBytes(manifest);

            var contentInfo = new ContentInfo(manifestBytes);
            var signedCms = new SignedCms(contentInfo, true);

            var signer = new CmsSigner(certificate);
            signedCms.ComputeSignature(signer);
            return signedCms.Encode();
        }

        private static X509Certificate2 GetCertificateFromFile(string cerPath, string password)
        {
            if (!File.Exists(cerPath))
                throw new FileNotFoundException("Certificate file was not found. File path: " + cerPath);

            X509Certificate2 certificate;
            if (password == null)
            {
                certificate = new X509Certificate2(cerPath);
            }
            else
            {
                X509KeyStorageFlags flags = X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable;
                certificate = new X509Certificate2(cerPath, password, flags);
            }
            return certificate;
        }

        private static X509Certificate2 GetCertificate(string nameOfPassCertificate)
        {
            // Open the cert store on the Local Machine
            var store = new X509Store(StoreLocation.CurrentUser);

            //open store and search through the certs for the Apple cert
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificates = store.Certificates;

            if (certificates.Count > 0)
                return certificates.Cast<X509Certificate2>().
                    FirstOrDefault(cert => cert.Subject.ToLower().Contains(nameOfPassCertificate));

            return null;
        }
    }
}
