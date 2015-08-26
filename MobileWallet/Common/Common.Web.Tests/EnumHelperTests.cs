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
            SelectList sl = EnumHelper.ToSelectList<MyEnum>(MyEnum.V2);
            Assert.AreEqual(2, sl.Count());
            SelectListItem[] arr = sl.ToArray();
            Assert.AreEqual("V1", arr[0].Text);
            Assert.AreEqual("1", arr[0].Value);
            Assert.NotNull(sl.SelectedValue);
            Assert.AreEqual(sl.SelectedValue.ToString(), ((int)MyEnum.V2).ToString());

            sl = EnumHelper.ToSelectList<MyEnum>();
            Assert.Null(sl.SelectedValue);
        }

        [Test]
        public void ToDictionaryTest()
        {
            IDictionary<string, int> dict = EnumHelper.ToDictionary<MyEnum>();
            Assert.AreEqual(2, dict.Count());
            int val;
            Assert.True(dict.TryGetValue("V1", out val));
            Assert.AreEqual(1, val);
            Assert.True(dict.TryGetValue("V2", out val));
            Assert.AreEqual(2, val);
        }

    }
}
