using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using Org.BouncyCastle.Cms;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities;

namespace Pass.Container.BL.PassGenerators
{
    public class ApplePassGenerator2
    {
        private static readonly MemCache<string, CmsSignedDataGenerator> _cacheSignedDataGenerators = new MemCache<string, CmsSignedDataGenerator>("SignedDataGenerators", new TimeSpan(0, 5, 0));

        private readonly IApplePassGeneratorConfig _config;
        private readonly string _srcTemplatePath;
        private readonly X509Certificate2 _signCert;

        public ApplePassGenerator2(IApplePassGeneratorConfig config, string srcTemplatePath, X509Certificate2 signCert)
        {
            _config = config;
            _srcTemplatePath = srcTemplatePath;
            _signCert = signCert;
        }

        public void GeneratePass(string serialNumber, IEnumerable<PassFieldInfo> fields, string dstPackageFile)
        {

        }

        private CmsSignedDataGenerator GetSignedDataGenerator(X509Certificate2 signCert)
        {
            CmsSignedDataGenerator generator = _cacheSignedDataGenerators[signCert.SerialNumber];
            if (generator != null)
                return generator;

            var appleCert = new X509Certificate2(_config.AppleWWDRCAPath);
            generator = ApplePassGeneratorHelper.GetCmsSignedDataGenerator(signCert, appleCert);
            _cacheSignedDataGenerators.Add(signCert.SerialNumber, generator);

            return generator;
        }
    }
}
