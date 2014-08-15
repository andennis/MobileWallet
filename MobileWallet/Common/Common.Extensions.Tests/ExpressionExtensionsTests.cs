using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Common.Extensions.Tests
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        private class MyClass
        {
            public string Member1;
            public string Func1()
            {
                return string.Empty;

            }
            public void Action1()
            {
                
            }
            public int P1 { get { return 0; }}
        }

        [Test]
        public void GetMethodOrPropertyNameTest()
        {
            Expression<Func<MyClass, string>> expFunc1 = x => x.Func1();
            string name = expFunc1.GetMethodOrPropertyName();
            Assert.AreEqual("Func1", name);

            Expression<Func<MyClass, int>> expP1 = x => x.P1;
            name = expP1.GetMethodOrPropertyName();
            Assert.AreEqual("P1", name);

            Expression<Func<MyClass, string>> expMemeber1 = x => x.Member1;
            Assert.Throws<ArgumentException>(() => expMemeber1.GetMethodOrPropertyName());

            Expression<Action<MyClass>> expAction1 = x => x.Action1();
            name = expAction1.GetMethodOrPropertyName();
            Assert.AreEqual("Action1", name);
        }
    }
}
