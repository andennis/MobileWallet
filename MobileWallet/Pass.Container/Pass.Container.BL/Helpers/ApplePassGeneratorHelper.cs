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
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;

namespace Pass.Container.BL.Helpers
{
    public static class ApplePassGeneratorHelper
    {
        public static string GenerateHashOfFile(string filePath)
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

        public static string GetManifestJson(string dirPath)
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

        public static void GenerateManifestFile(string dirPath, string manifestJson)
        {
            if (!Directory.Exists(dirPath))
                throw new DirectoryNotFoundException("Directory was not found. Directory path: " + dirPath);

            File.WriteAllText(Path.Combine(dirPath, "manifest.json"), manifestJson);
        }

        public static X509Certificate2 GetCertificate(string nameOfPassCertificate)
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

        public static X509Certificate2 GetCertificateFromFile(string cerPath, string password)
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

        public static byte[] SignByCertificate(string manifest, X509Certificate2 certificate)
        {
            byte[] manifestBytes = Encoding.ASCII.GetBytes(manifest);

            var contentInfo = new ContentInfo(manifestBytes);
            var signedCms = new SignedCms(contentInfo, true);

            var signer = new CmsSigner(certificate);
            signedCms.ComputeSignature(signer);
            return signedCms.Encode();
        }

        public static void SignPassFiles(string dirPath, X509Certificate2 certificate)
        {
            string manifestJson = GetManifestJson(dirPath);
            GenerateManifestFile(dirPath, manifestJson);
            byte[] signatureBytes = SignByCertificate(manifestJson, certificate);
            File.WriteAllBytes(Path.Combine(dirPath, "signature"), signatureBytes);
        }

        public static void SignManigestFile(string certificateFilePath, string certificatePassword, string appleWwdrcaCertificateFilePath,  string dirPath)
        {
            string manifestJson = GetManifestJson(dirPath);
            GenerateManifestFile(dirPath, manifestJson);
            
            Trace.TraceInformation("Signing the manifest file...");

            X509Certificate2 card = GetCertificateFromFile(certificateFilePath, certificatePassword);

            if (card == null)
            {
                throw new FileNotFoundException("Certificate could not be found.");
            }

            try
            {
                Org.BouncyCastle.X509.X509Certificate cert = DotNetUtilities.FromX509Certificate(card);
                Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = DotNetUtilities.GetKeyPair(card.PrivateKey).Private;

                Trace.TraceInformation("Fetching Apple Certificate for signing..");

                X509Certificate2 appleCA = GetCertificateFromFile(appleWwdrcaCertificateFilePath, null);
                Org.BouncyCastle.X509.X509Certificate appleCert = DotNetUtilities.FromX509Certificate(appleCA);

                Trace.TraceInformation("Constructing the certificate chain..");

                var intermediateCerts = new ArrayList() { appleCert, cert };

                var PP = new Org.BouncyCastle.X509.Store.X509CollectionStoreParameters(intermediateCerts);
                Org.BouncyCastle.X509.Store.IX509Store st1 = Org.BouncyCastle.X509.Store.X509StoreFactory.Create("CERTIFICATE/COLLECTION", PP);

                var generator = new CmsSignedDataGenerator();

                generator.AddSigner(privateKey, cert, CmsSignedDataGenerator.DigestSha1);
                generator.AddCertificates(st1);

                Trace.TraceInformation("Processing the signature..");

                CmsProcessable content = new CmsProcessableByteArray(Encoding.ASCII.GetBytes(manifestJson));
                CmsSignedData signedData = generator.Generate(content, false);
                
                byte[] signatureBytes = signedData.GetEncoded();
                File.WriteAllBytes(Path.Combine(dirPath, "signature"), signatureBytes);

                Trace.TraceInformation("The file has been successfully signed!");
            }
            catch (Exception exp)
            {
                Trace.TraceError("Failed to sign the manifest file: [{0}]", exp.Message);
            }
        }

    }
}
