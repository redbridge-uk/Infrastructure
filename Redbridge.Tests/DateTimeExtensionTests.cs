using System;
using NUnit.Framework;
using Redbridge.Data;

namespace Redbridge.Tests
{
    [TestFixture]
    public class DateTimeExtensionTests
    {
        [Test]
        public void RelativeDate_GivenSeconds_ReturnsCorrectStringValues()
        {
            var start = new DateTime(2000, 1, 1, 1, 1, 1);
            var end = start.AddSeconds(1);
            var end2 = start.AddSeconds(10);
            var end3 = start.AddSeconds(60);
            Assert.AreEqual("a second ago", DateTimeExtensions.RelativeDate(start, end));
            Assert.AreEqual("10 seconds ago", DateTimeExtensions.RelativeDate(start, end2));
            Assert.AreNotEqual("60 seconds ago", DateTimeExtensions.RelativeDate(start, end3));
        }

        [Test]
        public void RelativeDate_GivenMinutes_ReturnsCorrectStringValues()
        {
            DateTime start = new DateTime(2000, 1, 1, 1, 1, 1);
            DateTime end = start.AddMinutes(1);
            DateTime end2 = start.AddMinutes(10);
            DateTime end3 = start.AddMinutes(60);
            DateTime end4 = start.AddMinutes(61);
            Assert.AreEqual("a minute ago", DateTimeExtensions.RelativeDate(start, end));
            Assert.AreEqual("10 minutes ago", DateTimeExtensions.RelativeDate(start, end2));
            Assert.AreNotEqual("60 minutes ago", DateTimeExtensions.RelativeDate(start, end3));
            Assert.AreNotEqual("61 minutes ago", DateTimeExtensions.RelativeDate(start, end4));
        }

        [Test]
        public void RelativeDate_GivenHours_ReturnsCorrectStringValues()
        {
            DateTime start = new DateTime(2000, 1, 1, 1, 1, 1);
            DateTime end = start.AddHours(1);
            DateTime end2 = start.AddHours(10);
            DateTime end3 = start.AddHours(24);
            DateTime end4 = start.AddHours(25);
            Assert.AreEqual("an hour ago", DateTimeExtensions.RelativeDate(start, end));
            Assert.AreEqual("10 hours ago", DateTimeExtensions.RelativeDate(start, end2));
            Assert.AreNotEqual("24 hours ago", DateTimeExtensions.RelativeDate(start, end3));
            Assert.AreNotEqual("25 hours ago", DateTimeExtensions.RelativeDate(start, end4));
        }

        [Test]
        public void RelativeDate_GivenDays_ReturnsCorrectStringValues()
        {
            DateTime start = new DateTime(2000, 1, 1, 1, 1, 1);
            DateTime end = start.AddDays(1);
            DateTime end2 = start.AddDays(10);
            DateTime end3 = start.AddDays(32);
            Assert.AreEqual("yesterday", DateTimeExtensions.RelativeDate(start, end));
            Assert.AreEqual("10 days ago", DateTimeExtensions.RelativeDate(start, end2));
            Assert.AreNotEqual("32 days ago", DateTimeExtensions.RelativeDate(start, end3));
        }

        [Test]
        public void RelativeDate_GivenMonths_ReturnsCorrectStringValues()
        {
            int monthInDays = 31;
            DateTime start = new DateTime(2000, 1, 1, 1, 1, 1);
            DateTime end = start.AddDays(monthInDays);
            DateTime end2 = start.AddDays(monthInDays * 10);
            DateTime end3 = start.AddDays(monthInDays * 13);
            Assert.AreEqual("a month ago", DateTimeExtensions.RelativeDate(start, end));
            Assert.AreEqual("10 months ago", DateTimeExtensions.RelativeDate(start, end2));
            Assert.AreNotEqual("23 months ago", DateTimeExtensions.RelativeDate(start, end3));
        }

        [Test]
        public void RelativeDate_GivenYears_ReturnsCorrectStringValues()
        {
            DateTime start = new DateTime(2000, 1, 1, 1, 1, 1);
            DateTime end = start.AddYears(1);
            DateTime end2 = start.AddYears(2);
            DateTime end3 = start.AddYears(10);
            DateTime end4 = start.AddYears(100);
            DateTime end5 = start.AddYears(500);
            Assert.AreEqual("a year ago", DateTimeExtensions.RelativeDate(start, end));
            Assert.AreEqual("2 years ago", DateTimeExtensions.RelativeDate(start, end2));
            Assert.AreEqual("10 years ago", DateTimeExtensions.RelativeDate(start, end3));
            Assert.AreEqual("a centenary ago", DateTimeExtensions.RelativeDate(start, end4));
            Assert.AreEqual("5 centenaries ago", DateTimeExtensions.RelativeDate(start, end5));
        }
        
        [Test]
        public void DayOrdinalExtension_GivenDay1OfMonth_Gives1st()
        {
            DateTime time = new DateTime(2013, 4, 1);
            Assert.AreEqual("1st", time.DayOrdinal());
        }
        
        [Test]
        public void DayOrdinalExtension_GivenDay2OfMonth_Gives2nd()
        {
            DateTime time = new DateTime(2013, 4, 2);
            Assert.AreEqual("2nd", time.DayOrdinal());
        }
        
        [Test]
        public void DayOrdinalExtension_GivenDay3OfMonth_Gives3rd()
        {
            DateTime time = new DateTime(2013, 4, 3);
            Assert.AreEqual("3rd", time.DayOrdinal());
        }
        
        [Test]
        public void DayOrdinalExtension_GivenDay4OfMonth_Gives4th()
        {
            DateTime time = new DateTime(2013, 4, 4);
            Assert.AreEqual("4th", time.DayOrdinal());
        }
        
        [Test]
        public void DayOrdinalExtension_GivenDay21OfMonth_Gives21st()
        {
            DateTime time = new DateTime(2013, 4, 21);
            Assert.AreEqual("21st", time.DayOrdinal());
        }
    }
}
