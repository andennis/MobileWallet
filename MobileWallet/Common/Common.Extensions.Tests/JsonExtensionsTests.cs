using System.Collections.Generic;
using NUnit.Framework;

namespace Common.Extensions.Tests
{
    [TestFixture]
    public class JsonExtensionsTests
    {
        private class TestClass
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        [Test]
        public void ObjectToJsonTest()
        {
            object obj1 = null;
            string json = obj1.ObjectToJson();
            Assert.AreEqual("{}", json);

            var obj = new { Name = "Name1", Value = 1 };
            json = obj.ObjectToJson();
            Assert.AreEqual("{\"Name\":\"Name1\",\"Value\":1}", json);

            //Check global setting NullValueHandling.Ignore
            var obj2 = new { Name = "Name1", Value = (string)null };
            string json2 = obj2.ObjectToJson();
            Assert.AreEqual("{\"Name\":\"Name1\"}", json2);
        }

        [Test]
        public void JsonToObjectTest()
        {
            string json = "{\"Name\":\"Name1\",\"Value\":1}";
            TestClass obj = json.JsonToObject<TestClass>();
            Assert.AreEqual("Name1", obj.Name);
            Assert.AreEqual(1, obj.Value);
        }

        /*
        [Test]
        public void Test1()
        {
            var d = new Dictionary<string, string>()
                    {
                        {"K1", "V1"},
                        {"K2", "V2"}
                    };
            string s = d.ObjectToJson();
            var d2 = s.JsonToObject<Dictionary<string, string>>();
        }
        */

    }
}
