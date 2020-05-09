using System;
using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests.Validation
{
    [TestFixture()]
    public class DateTimeValidatorTests
    {
        [Test()]
        public void CreateDateTimeValidatorExpectSuccess()
        {
            var validator = new DateTimeValidator();
            Assert.IsFalse(validator.AllowNullValues);
        }

		[Test()]
		public void CreateDateTimeValidatorValidateBadValueExpectValidationError()
		{
			var validator = new DateTimeValidator();
			var result = validator.Validate("balls");
			Assert.IsFalse(result.Success);
		}

		[Test()]
		public void CreateDateTimeValidatorValidateBadNumberValueExpectValidationError()
		{
			var validator = new DateTimeValidator();
			var result = validator.Validate("23");
			Assert.IsFalse(result.Success);
		}

		[Test()]
		public void CreateDateTimeValidatorValidateDateTimeSuccessfullyWithinRangeSupplied()
		{
            var validator = new DateTimeValidator(new DateTime(1978, 1, 1), new DateTime(1980, 1, 1));
			var result = validator.Validate(new DateTime(1979, 05, 06));
			Assert.IsTrue(result.Success);
		}

		[Test()]
		public void CreateDateTimeValidatorValidateDateTimeSuccessfullyNoValidationRangeSupplied()
		{
			var validator = new DateTimeValidator();
			var result = validator.Validate(new DateTime(1979, 05, 06));
			Assert.IsTrue(result.Success);
		}
    }
}
