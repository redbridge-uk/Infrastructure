using NUnit.Framework;
using Redbridge.Validation;

namespace Easilog.Tests
{
    [TestFixture]
    public class PropertyValidationResultTest
    {
        [Test]
        public void NullArgumentOnPropertyValidationResult()
        {
            PropertyValidationResult result = PropertyValidationResult.Pass(null);
            Assert.IsNotNull(result, "A property validation result should be possible with a null property value.");
        }
    }
}
