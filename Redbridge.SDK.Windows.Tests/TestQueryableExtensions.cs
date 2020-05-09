using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Redbridge.Linq;

namespace Redbridge.Windows.Tests
{
    [TestFixture]
    public class TestQueryableExtensions
    {
        private class QueryableObject
        {
            public string Name { get; set; }
            public int Age { get; set;}
        }

        private static readonly QueryableObject[] QueryableObjects =
        {
            new QueryableObject() { Name = "Ian Bebbington", Age = 33 },
            new QueryableObject() { Name = "BEN ROSE", Age = 34},
            new QueryableObject() { Name = "Anwen Gorry", Age = 33 },
        };

        [Test]
        public void TestOrderBy()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            var expected = new List<QueryableObject>(new[] { QueryableObjects[2], QueryableObjects[1], QueryableObjects[0] });
            List<QueryableObject> actual = queryable.OrderBy("Name").ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestOrderByDescending()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            var expected = new List<QueryableObject>(new[] { QueryableObjects[0], QueryableObjects[1], QueryableObjects[2] });
            var actual = queryable.OrderByDescending("Name").ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestExplicitOrderBy()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            var expected = new List<QueryableObject>(new[] { QueryableObjects[2], QueryableObjects[1], QueryableObjects[0] });
            var actual = queryable.OrderBy("Name", false).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestExplicitOrderByDescending()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            List<QueryableObject> expected = new List<QueryableObject>(new QueryableObject[] { QueryableObjects[0], QueryableObjects[1], QueryableObjects[2] });
            List<QueryableObject> actual = queryable.OrderBy("Name", true).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestOrderByThenBy()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            List<QueryableObject> expected = new List<QueryableObject>(new QueryableObject[] { QueryableObjects[2], QueryableObjects[0], QueryableObjects[1] });
            List<QueryableObject> actual = queryable.OrderBy("Age").ThenBy("Name").ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestOrderByThenByDescending()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            List<QueryableObject> expected = new List<QueryableObject>(new QueryableObject[] { QueryableObjects[0], QueryableObjects[2], QueryableObjects[1] });
            List<QueryableObject> actual = queryable.OrderBy("Age").ThenByDescending("Name").ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestOrderByExplicitThenBy()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            List<QueryableObject> expected = new List<QueryableObject>(new QueryableObject[] { QueryableObjects[2], QueryableObjects[0], QueryableObjects[1] });
            List<QueryableObject> actual = queryable.OrderBy("Age").ThenBy("Name", false).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestOrderByExplicitThenByDescending()
        {
            IQueryable<QueryableObject> queryable = QueryableObjects.AsQueryable();

            List<QueryableObject> expected = new List<QueryableObject>(new QueryableObject[] { QueryableObjects[0], QueryableObjects[2], QueryableObjects[1] });
            List<QueryableObject> actual = queryable.OrderBy("Age").ThenBy("Name", true).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
