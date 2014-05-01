using NUnit.Framework;

namespace Common.Extensions.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void ReplaceFirstTest()
        {
            const string str1 = "ABC$Field1$123$Field1$";

            string str2 = str1.ReplaceFirst("$Field1$", "Hello");
            Assert.AreEqual("ABCHello123$Field1$", str2);

            str2 = str1.ReplaceFirst("$Field1$", string.Empty);
            Assert.AreEqual("ABC123$Field1$", str2);

            str2 = str1.ReplaceFirst("$Field1$", null);
            Assert.AreEqual("ABC123$Field1$", str2);

            str2 = str1.ReplaceFirst("$Field2$", "Hello");
            Assert.AreEqual(str1, str2);

            str2 = str1.ReplaceFirst("$Field1$", "Hello", 8);
            Assert.AreEqual("ABC$Field1$123Hello", str2);
        }

        [Test]
        public void GetBytesTest()
        {
            const string str = "123";
            byte[] data = str.GetBytes();
            Assert.AreEqual(6, data.Length);
            Assert.AreEqual(49, data[0]);
            Assert.AreEqual(0, data[1]);
            Assert.AreEqual(50, data[2]);
            Assert.AreEqual(0, data[3]);
            Assert.AreEqual(51, data[4]);
            Assert.AreEqual(0, data[5]);
        }
    }
}
