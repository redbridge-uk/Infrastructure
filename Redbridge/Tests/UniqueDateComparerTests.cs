using System;
using NUnit.Framework;

namespace Redbridge.Tests
{
    [TestFixture]
    public class UniqueDateComparerTests
    {
        [Test]
        public void CompareUniqueDatesExpectTrue()
        {
            var comparer = new UniqueDateComparer();
            Assert.IsTrue(comparer.Equals(new DateTime(2018, 4, 2), new DateTime(2018, 4, 2)));
        }

        [Test]
        public void CompareUniqueDatesDifferentTimeComponentsExpectTrue()
        {
            var comparer = new UniqueDateComparer();
            Assert.IsTrue(comparer.Equals(new DateTime(2018, 4, 2, 12, 3, 23), new DateTime(2018, 4, 2, 12, 3, 45)));
        }

        [Test]
        public void CompareUniqueDatesExpectFalse()
        {
            var comparer = new UniqueDateComparer();
            Assert.IsFalse(comparer.Equals(new DateTime(2018, 4, 1), new DateTime(2018, 4, 2)));
        }
    }
}
