using System;
using NUnit.Framework;

namespace Common.Extensions.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void TruncateMilisecondsTest()
        {
            var dt1 = new DateTime(2014, 6, 2, 20, 33, 44, 55);
            DateTime dt2 = dt1.TruncateMiliseconds();
            Assert.AreEqual(dt1.AddMilliseconds(-55), dt2);
        }
    }
}
