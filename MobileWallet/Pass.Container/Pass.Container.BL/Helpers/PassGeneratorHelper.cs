using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Common.Extensions;

namespace Pass.Container.BL.Helpers
{
    public class PassGeneratorHelper
    {
        public List<string> GetListPossibleFileNames()
        {
            return new List<string> { "background.png", "footer.png", "icon.png", "logo.png", "strip.png", "thumbnail.png" };
        } 

        public string GenerateHashOfFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File was not found. File path: " + filePath);

            byte[] fileData = File.ReadAllBytes(filePath);
            return GenerateHash(fileData);
        }

        private string GenerateHash(byte[] fileData)
        {
            string hashText = "";
            string hexValue = "";

            byte[] hashData = SHA1.Create().ComputeHash(fileData); // SHA1 or MD5

            foreach (byte b in hashData)
            {
                hexValue = b.ToString("X").ToLower(); // Lowercase for compatibility on case-sensitive systems
                hashText += (hexValue.Length == 1 ? "0" : "") + hexValue;
            }

            return hashText;
        }

        public string GetManifestJson(string dirPath)
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

        public void GenerateManifestFile(string dirPath, string manifestJson)
        {
            if (!Directory.Exists(dirPath))
                throw new DirectoryNotFoundException("Directory was not found. Directory path: " + dirPath);

            File.WriteAllText(Path.Combine(dirPath, "manifest.json"), manifestJson);
        }

        public X509Certificate2 GetCertificate(string nameOfPassCertificate)
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

        public X509Certificate2 GetCertificateFromFile(string cerPath)
        {
            if (!File.Exists(cerPath))
                throw new FileNotFoundException("Certificate file was not found. File path: " + cerPath);
            return new X509Certificate2(cerPath);
        }

        public byte[] SignByCertificate(string manifest, X509Certificate2 certificate)
        {
            byte[] manifestBytes = Encoding.ASCII.GetBytes(manifest);

            var contentInfo = new ContentInfo(manifestBytes);
            var signedCms = new SignedCms(contentInfo, true);

            var signer = new CmsSigner(certificate);
            signedCms.ComputeSignature(signer);
            return signedCms.Encode();
        }
    }
}
