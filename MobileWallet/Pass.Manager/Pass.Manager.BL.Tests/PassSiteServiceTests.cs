using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Manager.BL.Tests
{
    [TestFixture]
    public class PassSiteServiceTests
    {
        [Test]
        public void Test1()
        {
            //var dt = new DateTime(2015, 6, 19, 22, 47, 48).ToLocalTime();
            //var t = Convert.ToInt64((dt - new DateTime(1970, 1, 1,0,0,0).ToLocalTime()).TotalSeconds);
            //var dt2 = new DateTime(1434747272);

            var dt = UnixTimeStampToDateTime(1434916100).ToUniversalTime();
        }

        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
