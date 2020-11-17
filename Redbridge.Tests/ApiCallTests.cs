using Moq;
using NUnit.Framework;
using Redbridge.ApiManagement;
using Redbridge.Diagnostics;

namespace Redbridge.Tests
{
    [TestFixture]
    public class ApiCallTests
    {
        public class UnnamedTestApiCall : ApiCall
        {
            public UnnamedTestApiCall(ILogger logger) : base(logger)
            {
            }
        }

        [Api(Name = "Bernard the Grand")]
        public class AttributedNamedTestApiCall : ApiCall
        {
            public AttributedNamedTestApiCall(ILogger logger) : base(logger)
            {
            }
        }

        [Api]
        public class AttributedUnnamedTestApiCall : ApiCall
        {
            public AttributedUnnamedTestApiCall(ILogger logger) : base(logger)
            {
            }
        }

        [Test]
        public void GetApiName_UnnamedTestApiCall_ExpectUnnamedTestApiCall()
        {
            var mockLogger = new Mock<ILogger>();
            var api = new UnnamedTestApiCall(mockLogger.Object);
            var name = api.ApiName;
            Assert.AreEqual("UnnamedTestApiCall", name);
        }

        [Test]
        public void GetApiName_AttributedNamedTestApiCall_ExpectNameFromAttributeApiCall()
        {
            var mockLogger = new Mock<ILogger>();
            var api = new AttributedNamedTestApiCall(mockLogger.Object);
            var name = api.ApiName;
            Assert.AreEqual("Bernard the Grand", name);
        }

        [Test]
        public void GetApiName_AttributedUnnamedTestApiCall_ExpectNameFromTypeApiCall()
        {
            var mockLogger = new Mock<ILogger>();
            var api = new AttributedUnnamedTestApiCall(mockLogger.Object);
            var name = api.ApiName;
            Assert.AreEqual("AttributedUnnamedTestApiCall", name);
        }
    }
}
