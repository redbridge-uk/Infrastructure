using NUnit.Framework;
using Redbridge.ApiManagement;

namespace Redbridge.Tests
{
    [TestFixture]
    public class ApiAttributeTests
    {
        [Test]
        public void Construct_DefaultApiAttribute_ExpectNoName()
        {
            var attribute = new ApiAttribute();
            Assert.IsNotNull(attribute);
            Assert.IsNull(attribute.Name);
        }

        [Test]
        public void Construct_ApiAttributeWithName_ExpectName()
        {
            var attribute = new ApiAttribute("My Api");
            Assert.IsNotNull(attribute);
            Assert.AreEqual("My Api", attribute.Name);
        }
    }
}
