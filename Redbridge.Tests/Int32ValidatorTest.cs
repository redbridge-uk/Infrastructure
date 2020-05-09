using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests
{
    [TestFixture]
    public class Int32ValidatorTest
    {
        [Test]
        public void ConstructInt32Default_CheckMinimumValue()
        {
            var validator = new Int32Validator();
            Assert.AreEqual(int.MinValue, validator.Minimum, "Unexpected minimum value.");
        }
        
        [Test]
        public void ConstructInt32Default_CheckMaximumValue()
        {
            var validator = new Int32Validator();
            Assert.AreEqual(int.MaxValue, validator.Maximum, "Unexpected maximum value.");
        }
        
        [Test]
        public void ConstructInt32Range_ValidValue_ExpectSuccess()
        {
            var validator = new Int32Validator(5, 10);
            var result = validator.Validate(7);
            Assert.IsTrue(result.Success, "Expected a successful validation result.");
        }
        
        [Test]
        public void ConstructInt32Range_OutOfRangeValue_ExpectFailure_Above()
        {
            var validator = new Int32Validator(5, 10);
            var result = validator.Validate(12);
            Assert.IsFalse(result.Success, "Expected a failed validation result.");
        }
        
        [Test]
        public void ConstructInt32Range_OutOfRangeValue_ExpectFailure_Below()
        {
            var validator = new Int32Validator(5, 10);
            var result = validator.Validate(3);
            Assert.IsFalse(result.Success, "Expected a failed validation result.");
        }
    }
}
