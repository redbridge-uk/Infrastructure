using NUnit.Framework;
using Redbridge.Data;
using Redbridge.Linq;

namespace Redbridge.Tests
{
    [TestFixture]
    public class SearchParameterTests
    {
        [Test]
        public void CreateSearchParameterExpectSuccess()
        {
            var parameter = new SearchParameter("logId", "1234", WhereOperation.Equal);
            Assert.IsNotNull(parameter);
        }

        [Test]
        public void CreateSearchParameterExpectParametersAssigned()
        {
            var parameter = new SearchParameter("logId", "1234", WhereOperation.Equal);
            Assert.AreEqual("logId", parameter.Property);
            Assert.AreEqual("1234", parameter.Search);
            Assert.AreEqual(WhereOperation.Equal, parameter.WhereOperator);
        }

        [Test]
        public void CreateSearchParameterCreateQueryStringExpectEscaped()
        {
            var parameter = new SearchParameter("logId", "1234", WhereOperation.Equal);
            Assert.AreEqual("logId%3D1234", parameter.AsQueryString());
        }
    }
}
