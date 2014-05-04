using System.IO;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using Pass.Container.BL.PassGenerators;
using Pass.Container.Core.Entities;

namespace Pass.Container.BL.Tests.PassGenerators
{
    [TestFixture]
    public class ApplePassGeneratorTests
    {
        private const string TemplatePath = @"PassGenerators\Data\AppleTemplate";

        [Test]
        public void GeneratePassTest()
        {
            var cert = new X509Certificate2(TestHelper.CertificateFileApple, 
                TestHelper.CertificateApplePassword, 
                X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            var passGenerator = new ApplePassGenerator2(new PassContainerConfig(), TemplatePath, cert);
            var fields = new PassFieldInfo[]
                         {
                             new PassFieldInfo() {Name = "Key01", Label = "L1", Value = "V1"}
                         };

            string dstPackagePath = Path.Combine(TemplatePath, "Passes");
            if (Directory.Exists(dstPackagePath))
                Directory.Delete(dstPackagePath, true);
            Directory.CreateDirectory(dstPackagePath);

            string fileName = passGenerator.GeneratePass("SN0123", fields, dstPackagePath);
            Assert.IsNotNullOrEmpty(fileName);
            Assert.AreEqual(".pkpass", Path.GetExtension(fileName));
            Assert.True(File.Exists(fileName));
        }
    }
}
