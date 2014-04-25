using System;
using System.Threading;
using NUnit.Framework;

namespace Common.Utils.Tests
{
    [TestFixture]
    public class MemCacheTests
    {
        [Test]
        public void Add_Remove_Test()
        {
            var mc = new MemCache<string, string>("MC1", DateTimeOffset.Now.AddHours(1));
            mc.Add("key1", "val1");
            Assert.AreEqual("val1", mc["key1"]);
            mc.Remove("key1");
            Assert.Null(mc["key1"]);
        }

        [Test]
        public void AbsoluteExpirationTest()
        {
            var mc = new MemCache<string, string>("MC1", DateTimeOffset.Now.AddSeconds(3));
            mc.Add("key1", "val1");

            Thread.Sleep(2 * 1000);
            Assert.True(mc.Contains("key1"));
            Assert.AreEqual("val1", mc["key1"]);

            Thread.Sleep(2 * 1000);
            Assert.False(mc.Contains("key1"));
            Assert.Null(mc["key1"]);
        }

        [Test]
        public void SlidingExpirationTest()
        {
            var mc = new MemCache<int, int?>("MC1", new TimeSpan(0, 0, 0, 3));
            const int key = 1;
            const int val = 10;
            mc[key] = val;

            Thread.Sleep(1 * 1000);
            Assert.True(mc.Contains(key));
            Assert.AreEqual(val, mc[key]);

            Thread.Sleep(1 * 1000);
            Assert.True(mc.Contains(key));
            Assert.AreEqual(val, mc[key]);

            Thread.Sleep(4 * 1000);
            Assert.False(mc.Contains(key));
            Assert.Null(mc[key]);
        }
    }
}
