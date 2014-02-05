using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Pass.Container.BL.NativePassTemplateGenerators;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class NativePassTemplateGeneratorsTests
    {
        [Test]
        public void ApplePassTemplateGenerator()
        {
            MethodInfo method = typeof(ApplePassTemplateGenerator).GetMethod("CreateApplePassTemplate", BindingFlags.Public | BindingFlags.NonPublic);
            if (method != null)
                method.Invoke(this, new object[] { TestHelper.GetPassTemplateObject() });
        }
    }
}
