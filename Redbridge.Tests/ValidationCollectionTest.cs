using System.Linq;
using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests
{
    [TestFixture]
    public class ValidationResultCollectionTest
    {
        [Test]
        public void ConstructValidationResultCollection_NoErrors_ExpectSuccess()
        {
            new ValidationResultCollection();
        }
        
        [Test]
        public void ConstructValidationResultCollection_NoErrors_ExpectSuccessIsTrue()
        {
            var result = new ValidationResultCollection();
            Assert.IsTrue(result.Success);
        }
        
        [Test]
        public void ConstructValidationResultCollection_ProvideFalseConstructorArgument_NoErrors_ExpectSuccessIsFalse()
        {
            var result = new ValidationResultCollection(false);
            Assert.IsFalse(result.Success);
        }
        
        [Test]
        public void ConstructValidationResultCollection_NoErrors_ExpectEmptyMessage()
        {
            var result = new ValidationResultCollection();
            Assert.AreEqual(null, result.Message);
        }
        
        [Test]
        public void ConstructValidationResultCollection_NoErrors_ExpectCollectionInstance()
        {
            var result = new ValidationResultCollection();
            Assert.IsNotNull(result.Results);
        }
        
        [Test]
        public void ConstructValidationResultCollectionNoErrorsExpectEmptyCollection()
        {
            var result = new ValidationResultCollection();
            Assert.AreEqual(0, result.Results.Count());
        }
        
        [Test]
        public void ConstructValidationResultCollectionOneErrorExpectFalseSuccessResult()
        {
            var result = new ValidationResultCollection();
            result.AddResult(new ValidationResult(false, "Unable to find my trousers"));
            Assert.IsFalse(result.Success);
        }
        
        [Test]
        public void ConstructValidationResultCollectionOneErrorExpectResultFailureMessage()
        {
            var result = new ValidationResultCollection();
            result.AddResult(new ValidationResult(false, "Unable to find my trousers"));
            Assert.AreEqual("Unable to find my trousers", result.Message);
        }
        
        [Test]
        public void ConstructValidationResultCollectionOneErrorViaConstructorExpectResultFailureMessage()
        {
            var result = new ValidationResultCollection(new [] { new ValidationResult(false, "Unable to find my trousers") });
            Assert.AreEqual("Unable to find my trousers", result.Message);
        }
    }
}
