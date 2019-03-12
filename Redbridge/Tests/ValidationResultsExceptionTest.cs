using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests
{
    [TestFixture]
    public class ValidationResultsExceptionTest
    {
        [Test]
        public void CreateDefaultValidationResultsExceptionExpectSuccess()
        {
            ValidationResultCollection collection = new ValidationResultCollection();
            new ValidationResultsException(collection);
        }
        
        [Test]
        public void CreateDefaultValidationResultsExceptionCheckDefaultMessage()
        {
            ValidationResultCollection collection = new ValidationResultCollection(false);
            var exception = new ValidationResultsException(collection);
            Assert.AreEqual("Validation failed. Please see inner results for details.", exception.Message);
        }
        
        [Test]
        public void CreateDefaultValidationResultsExceptionCheckExceptionMessageFromValidationError()
        {
            ValidationResultCollection collection = new ValidationResultCollection();
            collection.AddResult(ValidationResult.Fail("My Field", "Something terrible has happened."));
            var exception = new ValidationResultsException(collection);
            Assert.AreEqual("Something terrible has happened.", exception.Message);
        }
    }
}
