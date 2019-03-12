using NUnit.Framework;
using Redbridge.Validation.Markup;

namespace Easilog.Tests
{
    /// <summary>
    /// Suite of tests for the string validator.
    /// </summary>
    [TestFixture]
    public class StringValidatorAttributeTest
    {
        /// <summary>
        /// Test class for proving the string validator markup
        /// </summary>
        public class StringParameterTestClass
        {
            /// <summary>
            /// Gets/sets the name.
            /// </summary>
            [StringValidator(0, 10)]
            public string Name { get; set; }
        }

        /// <summary>
        /// Test Condition:
        /// </summary>
        [Test]
        public void CreateStringValidatorAttributeInstance_DefaultConstructor_DefaultMinimumLength()
        {
            StringValidatorAttribute attribute = new StringValidatorAttribute();
            Assert.AreEqual(0, attribute.MinimumLength, "Unexpected minimum length.");
        }

        /// <summary>
        /// Test Condition:
        /// </summary>
        [Test]
        public void CreateStringValidatorAttributeInstance_DefaultConstructor_DefaultMaximumLength()
        {
            StringValidatorAttribute attribute = new StringValidatorAttribute();
            Assert.AreEqual(int.MaxValue, attribute.MaximumLength, "Unexpected maximum length.");
        }


    }
}
