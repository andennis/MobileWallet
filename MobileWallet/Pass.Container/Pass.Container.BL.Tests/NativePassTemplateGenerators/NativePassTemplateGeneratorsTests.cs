using System.Reflection;
using NUnit.Framework;
using Pass.Container.BL.PassTemplateGenerators;

namespace Pass.Container.BL.Tests.NativePassTemplateGenerators
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
