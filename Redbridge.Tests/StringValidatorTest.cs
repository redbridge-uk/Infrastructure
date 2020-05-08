using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests
{
    [TestFixture]
    public class StringValidatorTest
    {
        [Test]
        public void CreateDefaultStringValidator_MinimumLengthCheck()
        {
            var validator = new StringValidator();
            Assert.AreEqual(0, validator.MinimumLength, "Unexpected default minimum length.");
        }
        
        [Test]
        public void CreateDefaultStringValidatorMaximumLengthCheck()
        {
            var validator = new StringValidator();
            Assert.AreEqual(int.MaxValue, validator.MaximumLength, "Unexpected default maximum length.");
        }
        
        [Test]
        public void CreateDefaultStringValidatorDoesNotAllowNulls()
        {
            var validator = new StringValidator();
            Assert.IsFalse(validator.AllowNullValues, "Default should be not to permit null values.");
        }
        
        [Test]
        public void CreateMaxLengthStringValidatorMinimumLengthCheck()
        {
            var validator = new StringValidator(20);
            Assert.AreEqual(0, validator.MinimumLength, "Unexpected minimum length.");
        }
        
        [Test]
        public void CreateMaxLengthStringValidatorValidateNullNotPermittedDefault()
        {
            var validator = new StringValidator();
            var result = validator.Validate(null, string.Empty);
            Assert.IsFalse(result.Success, "The result should be a failure due to the null value.");
        }
        
        [Test]
        public void CreateMaxLengthStringValidatorValidateNullNullsPermitted()
        {
            var validator = new StringValidator {AllowNullValues = true};
            ValidationResult result = validator.Validate(null, string.Empty);
            Assert.IsTrue(result.Success, "Expected a successful validation result. The field is optional and nulls are permitted.");
        }
        
        [Test]
        public void CreateMaxLengthStringValidatorMaximumLengthCheck()
        {
            var validator = new StringValidator(20);
            Assert.AreEqual(20, validator.MaximumLength, "Unexpected maximum length.");
        }
        
        [Test]
        public void StringValidatorValidateStringSuccessfully()
        {
            var validator = new StringValidator();
            Assert.IsTrue(validator.Validate("Mr String", string.Empty).Success, "Expected a successful validation result.");
        }
        
        [Test]
        public void StringValidatorValidateStringTooShort()
        {
            var validator = new StringValidator(10, 20);
            var result = validator.Validate("short", "Name");
            Assert.IsFalse(result.Success, "The validation should have failed.");
            Assert.AreEqual("The minimum length for field 'Name' is 10, the supplied string ('short') contains only 5 characters", result.Message);
        }
        
        [Test]
        public void StringValidatorValidateStringTooLong()
        {
            var validator = new StringValidator(10, 20);
            var result = validator.Validate("This string however is far too long", "Name");
            Assert.IsFalse(result.Success, "The validation should have failed.");
            Assert.AreEqual("The maximum length for field 'Name' is 20, the supplied string ('This string however is far too long') contains 35 characters", result.Message);
        }
    }
}
