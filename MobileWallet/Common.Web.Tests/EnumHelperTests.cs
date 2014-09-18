using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace Common.Web.Tests
{
    [TestFixture]
    public class EnumHelperTests
    {
        private enum MyEnum
        {
            V1 = 1,
            V2 = 2
        }

        [Test]
        public void ToSelectListItemsTest()
        {
            IEnumerable<SelectListItem> items = EnumHelper.ToSelectListItems<MyEnum>();
            Assert.AreEqual(2, items.Count());
            SelectListItem[] arr = items.ToArray();
            Assert.AreEqual("V1", arr[0].Text);
            Assert.AreEqual("1", arr[0].Value);
        }

        [Test]
        public void ToSelectListTest()
        {
            SelectList sl = EnumHelper.ToSelectList<MyEnum>();
            Assert.AreEqual(2, sl.Count());
            SelectListItem[] arr = sl.ToArray();
            Assert.AreEqual("V1", arr[0].Text);
            Assert.AreEqual("1", arr[0].Value);
        }

    }
}
