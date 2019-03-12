using NUnit.Framework;
using Redbridge.Validation;

namespace Easilog.Tests
{
    [TestFixture]
    public class ObjectInstanceValidatorTests
    {
        [Test]
        public void ConstructorAllowingNullsPropertyAllowNullHasCorrectValue()
        {
            var validator = new ObjectInstanceValidator<object>(true);
            Assert.IsTrue(validator.AllowNullValues);
        }

        [Test]
        public void ValidateGivenNullAndNullIsAllowedReturnsValidationSuccess()
        {
            var validator = new ObjectInstanceValidator<object>(true);
            var result = validator.Validate(null);
            Assert.IsTrue(result != null &&
                          result.Success);
        }

        [Test]
        public void ValidateGivenNullAndNullsNotAllowedReturnsValidationFailure()
        {
            var validator = new ObjectInstanceValidator<object>();
            var result = validator.Validate(null);
            Assert.IsTrue(result != null &&
                          !result.Success);
        }

        [Test]
        public void ValidateGivenObjectInstanceAndNullIsAllowedReturnsValidationSuccess()
        {
            var validator = new ObjectInstanceValidator<object>(true);
            var result = validator.Validate(new object());
            Assert.IsTrue(result != null &&
                          result.Success);
        }

        [Test]
        public void ValidateGivenObjectInstanceAndNullsNotAllowedReturnsValidationSuccess()
        {
            var validator = new ObjectInstanceValidator<object>();
            var result = validator.Validate(new object());
            Assert.IsTrue(result != null &&
                          result.Success);
        }
    }
}