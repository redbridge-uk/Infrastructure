using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests
{
    [TestFixture]
    public class DecimalValidatorTest
    {
        [Test]
        public void ConstructDecimalDefault_CheckMinimumValue()
        {
            var validator = new DecimalValidator();
            Assert.AreEqual(decimal.MinValue, validator.Minimum, "Unexpected minimum value.");
        }
        
        [Test]
        public void ConstructDecimalDefault_CheckMaximumValue()
        {
            var validator = new DecimalValidator();
            Assert.AreEqual(decimal.MaxValue, validator.Maximum, "Unexpected maximum value.");
        }
        
        [Test]
        public void ConstructDecimalRange_ValidValue_ExpectSuccess()
        {
            var validator = new DecimalValidator(5, 10);
            var result = validator.Validate(7M);
            Assert.IsTrue(result.Success, "Expected a successful validation result.");
        }
        
        [Test]
        public void ConstructDecimalRange_OutOfRangeValue_ExpectFailure_Above()
        {
            var validator = new DecimalValidator(5, 10);
            var result = validator.Validate(12M);
            Assert.IsFalse(result.Success, "Expected a failed validation result.");
        }
        
        [Test]
        public void ConstructDecimalRange_OutOfRangeValue_ExpectFailure_ZeroNotAllowed()
        {
            var validator = new DecimalValidator(5, 10) { AllowZero = false };
            var result = validator.Validate(decimal.Zero, string.Empty);
            Assert.IsFalse(result.Success, "Expected a failed validation result.");
        }
        
        [Test]
        public void ConstructDecimalRange_OutOfRangeValue_ExpectFailure_ZeroPermitted()
        {
            var validator = new DecimalValidator(10M) { AllowZero = true };
            var result = validator.Validate(decimal.Zero, string.Empty);
            Assert.IsTrue(result.Success, "Expected a validation success.");
        }
        
        [Test]
        public void ConstructDecimalRange_ZeroPermitted_ByDefault()
        {
            var validator = new DecimalValidator(10M);
            var result = validator.Validate(decimal.Zero);
            Assert.IsTrue(result.Success, "Expected a validation success.");
        }
        
        [Test]
        public void ConstructDecimalRange_OutOfRangeValue_ExpectFailure_AboveAsDecimal()
        {
            var validator = new DecimalValidator(5M, 10M);
            var result = validator.Validate(12.45M);
            Assert.IsFalse(result.Success, "Expected a failed validation result.");
        }
        
        [Test]
        public void ConstructDecimalRange_OutOfRangeValue_ExpectFailure_Below()
        {
            var validator = new DecimalValidator(5M, 10M);
            var result = validator.Validate(3M);
            Assert.IsFalse(result.Success, "Expected a failed validation result.");
        }
    }
}
