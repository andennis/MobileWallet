using NUnit.Framework;

namespace Common.Utils.Tests
{
    [TestFixture]
    public class CryptoTests
    {
        [Test]
        public void EncryptDecryptStringTest()
        {
            string key = "1234567812345678";
            string iv = "8765432187654321";
            const string textToEncrypt = "Hello Aes!!!123678";

            string encrypted = Crypto.EncryptString(textToEncrypt, key, iv);
            Assert.IsNotNullOrEmpty(encrypted);
            Assert.AreNotEqual(textToEncrypt, encrypted);

            string decrypted = Crypto.DecryptString(encrypted, key, iv);
            Assert.IsNotNullOrEmpty(decrypted);
            Assert.AreEqual(textToEncrypt, decrypted);

            key = "8234567812345678";
            decrypted = Crypto.DecryptString(encrypted, key, iv);
            Assert.IsNull(decrypted);

            key = "1234567812345678";
            iv = "8765432187654328";
            decrypted = Crypto.DecryptString(encrypted, key, iv);
            Assert.AreNotEqual(textToEncrypt, decrypted);
        }
    }
}
