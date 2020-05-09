using System.Linq;
using NUnit.Framework;
using Redbridge.Data;
using Redbridge.Linq;

namespace Redbridge.Tests
{
    [TestFixture]
    public class SearchQueryTests
    {
        [Test]
        public void CreateSearchQueryExpectSuccess()
        {
            var query = new SearchQuery();
            Assert.IsNotNull(query);
        }

        [Test]
        public void CreateSearchQueryNameAndValueExpectSingleParameter()
        {
            var query = new SearchQuery(new [] { new SearchParameter("logId", "1234", WhereOperation.Equal) });
            Assert.AreEqual(1, query.Parameters.Count());
        }

        [Test]
        public void CreateSearchQueryNameAndValueExpectQueryStringEscaped()
        {
            var query = new SearchQuery(new[] { new SearchParameter("logId", "1234", WhereOperation.Equal) });
            Assert.AreEqual("logId%3D1234", query.ToQueryString());
        }
    }
}
