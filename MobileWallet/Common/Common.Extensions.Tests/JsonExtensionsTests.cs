using System;
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
            public string P1 { get; set; }
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
            var obj = json.JsonToObject<TestClass>();
            Assert.AreEqual("Name1", obj.Name);
            Assert.AreEqual(1, obj.Value);
        }

        [Test]
        public void MergeJsonTest()
        {
            const string json1 = "{\"Name\":\"Name1\"}";
            const string json2 = "{\"Value\":5}";
            string json3 = json1.MergeJson(json2);
            Assert.IsNotNullOrEmpty(json3);

            var obj = json3.JsonToObject<TestClass>();
            Assert.AreEqual("Name1", obj.Name);
            Assert.AreEqual(5, obj.Value); 
        }

        [Test]
        public void ObjectToJsonMergeTest()
        {
            var obj1 = new { Name = "Name1" };
            var obj2 = new { Value = 5 };
            var obj3 = new { P1 = "123" };
            string json = obj1.ObjectToJsonMerge(obj2, obj3);
            Assert.IsNotNullOrEmpty(json);

            var obj = json.JsonToObject<TestClass>();
            Assert.AreEqual("Name1", obj.Name);
            Assert.AreEqual(5, obj.Value);
            Assert.AreEqual("123", obj.P1); 
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
