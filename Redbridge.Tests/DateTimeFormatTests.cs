using System;
using System.Globalization;
using NUnit.Framework;

namespace Redbridge.Tests
{
    [TestFixture]
    public class DateTimeFormatTests
    {
        [Test]
        public void DateTimeToStringKeepTrailingDayZero()
        {
            var date = new DateTime(2015, 5, 1);
            var asString = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual("01/05/2015", asString);
        }
    }
}
