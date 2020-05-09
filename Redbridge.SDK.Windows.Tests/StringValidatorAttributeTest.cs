using NUnit.Framework;
using Redbridge.Validation.Markup;

namespace Redbridge.Windows.Tests
{
    /// <summary>
    /// Suite of tests for the string validator.
    /// </summary>
    [TestFixture]
    public class StringValidatorAttributeTest
    {
        public class StringParameterTestClass
        {
            [StringValidator(0, 10)]
            public string Name { get; set; }
        }

        [Test]
        public void CreateStringValidatorAttributeInstance_DefaultConstructor_DefaultMinimumLength()
        {
            var attribute = new StringValidatorAttribute();
            Assert.AreEqual(0, attribute.MinimumLength, "Unexpected minimum length.");
        }

        [Test]
        public void CreateStringValidatorAttributeInstance_DefaultConstructor_DefaultMaximumLength()
        {
            StringValidatorAttribute attribute = new StringValidatorAttribute();
            Assert.AreEqual(int.MaxValue, attribute.MaximumLength, "Unexpected maximum length.");
        }
    }
}
