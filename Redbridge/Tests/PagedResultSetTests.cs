using System.Collections.Generic;
using NUnit.Framework;
using Redbridge.SDK;

namespace Redbridge.Tests
{
    [TestFixture]
    public class PagedResultSetTests
    {
        [Test]
        public void CreatePagedResultSetAllExpectEmptySet()
        {
            var records = new List<string>();
            var set = PagedResultSet.All(records);
            Assert.AreEqual(0, set.TotalItems);
        }

        [Test]
        public void CreatePagedResultSetAllExpectFiveRecords()
        {
            var records = new List<string>();
            records.AddRange(new[] { "A", "B", "C", "D", "E" });
            var set = PagedResultSet.All(records);
            Assert.AreEqual(5, set.TotalItems);
        }

        [Test]
        public void CreatePagedResultSetAllExpectFiveRecordsAsPageSize()
        {
            var records = new List<string>();
            records.AddRange(new[] { "A", "B", "C", "D", "E" });
            var set = PagedResultSet.All(records);
            Assert.AreEqual(5, set.PageSize);
        }

        [Test]
        public void CreatePagedResultSetAllExpectOnePage()
        {
            var records = new List<string>();
            records.AddRange(new[] { "A", "B", "C", "D", "E" });
            var set = PagedResultSet.All(records);
            Assert.AreEqual(1, set.TotalPages);
        }
    }
}
