using System.Linq;
using NUnit.Framework;
using Redbridge.SDK;

namespace Redbridge.Tests
{
    [TestFixture]
    public class PageRequestParserTests
    {
        [Test]
        public void PageRequestParserCreateNotSortOrFilterExpectSuccess()
        {
            var request = PageRequestParser.ParseUrlRequest(1, 1);
            Assert.IsNotNull(request);
        }

        [Test]
        public void PageRequestParserCreateNoSortOrFilterExpectQueryString()
        {
            var request = PageRequestParser.ParseUrlRequest(1, 1);
            Assert.AreEqual("page=1&size=1", request.ToQueryString());
        }

        [Test]
        public void PageRequestParserCreateNoSortOrFilterZeroPageAndZeroSizeExpectNormalisedQueryString()
        {
            var request = PageRequestParser.ParseUrlRequest(0, 0);
            Assert.AreEqual("page=1&size=20", request.ToQueryString());
        }

        [Test]
        public void PageRequestParserCreateWithSortNoFilterExpectQueryString()
        {
            var request = PageRequestParser.ParseUrlRequest(1, 1, "+created");
            Assert.AreEqual("page=1&size=1&sort=+created", request.ToQueryString());
        }

        [Test]
        public void PageRequestParserCreateWithSortAndFilterExpectSingleFilterItem()
        {
            var request = PageRequestParser.ParseUrlRequest(1, 1, "+created", "logId=1234");
            Assert.IsNotNull(request.Filter);
            Assert.AreEqual(1, request.Filter.Parameters.Count());
        }

        [Test]
        public void PageRequestParserCreateWithSortAndFilterExpectSingleFilterItemNameValue()
        {
            var request = PageRequestParser.ParseUrlRequest(1, 1, "+created", "logId=1234");
            Assert.IsNotNull(request.Filter);
            var filterParameter = request.Filter.Parameters.First();
            Assert.AreEqual("logId", filterParameter.Property);
            Assert.AreEqual("1234", filterParameter.Search);
            Assert.AreEqual(WhereOperation.Equal, filterParameter.WhereOperator);
        }

        [Test]
        public void PageRequestParserCreateWithSortAndFilterExpectQueryString()
        {
            var request = PageRequestParser.ParseUrlRequest(1, 1, "+created", "logId=1234");
            Assert.AreEqual("page=1&size=1&sort=+created&filter=logId%3D1234", request.ToQueryString());
        }
    }
}
