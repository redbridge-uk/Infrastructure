using NUnit.Framework;
using Redbridge.Validation;

namespace Redbridge.Tests
{
    [TestFixture]
    public class PropertyValidationResultTest
    {
        [Test]
        public void NullArgumentOnPropertyValidationResult()
        {
            var result = PropertyValidationResult.Pass(null);
            Assert.IsNotNull(result, "A property validation result should be possible with a null property value.");
        }
    }
}
