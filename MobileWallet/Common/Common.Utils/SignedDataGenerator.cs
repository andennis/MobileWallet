using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Common.Extensions;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;

namespace Common.Utils
{
    public class SignedDataGenerator
    {
        private readonly CmsSignedDataGenerator _signedDataGenerator;

        public SignedDataGenerator(IList<X509Certificate2> certificatesChain)
        {
            if (certificatesChain == null)
                throw new ArgumentNullException("certificatesChain");
            if (certificatesChain.Count == 0)
                throw new ArgumentException("Certificates chain cannot be empty");
            if (certificatesChain.Any(x => x == null))
                throw new ArgumentException("Certificates should be specified in chain");

            //Build certificates chain
            var certChain = new List<Org.BouncyCastle.X509.X509Certificate>();
            for (int i = 0; i < certificatesChain.Count - 1; i++)
            {
                Org.BouncyCastle.X509.X509Certificate utlCert = DotNetUtilities.FromX509Certificate(certificatesChain[i]);
                certChain.Add(utlCert);
            }

            X509Certificate2 signingCert = certificatesChain[certificatesChain.Count - 1];
            Org.BouncyCastle.X509.X509Certificate utlSigningCert = DotNetUtilities.FromX509Certificate(signingCert);
            certChain.Add(utlSigningCert);

            var pp = new Org.BouncyCastle.X509.Store.X509CollectionStoreParameters(certChain);
            Org.BouncyCastle.X509.Store.IX509Store st1 = Org.BouncyCastle.X509.Store.X509StoreFactory.Create("CERTIFICATE/COLLECTION", pp);

            _signedDataGenerator = new CmsSignedDataGenerator();
            Org.BouncyCastle.Crypto.AsymmetricKeyParameter privateKey = DotNetUtilities.GetKeyPair(signingCert.PrivateKey).Private;
            _signedDataGenerator.AddSigner(privateKey, utlSigningCert, CmsSignedGenerator.DigestSha1);
            _signedDataGenerator.AddCertificates(st1);
        }

        public byte[] SignData(byte[] data)
        {
            CmsProcessable content = new CmsProcessableByteArray(data);
            CmsSignedData signedData = _signedDataGenerator.Generate(content, false);
            return signedData.GetEncoded();
        }

        public byte[] SignText(string text)
        {
            byte[] data = new UTF8Encoding().GetBytes(text);
            return SignData(data);
        }
    }
}
