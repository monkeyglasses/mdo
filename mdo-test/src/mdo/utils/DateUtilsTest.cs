using System;
using System.Collections.Generic;
using System.Text;
using gov.va.medora; // for StringTestObject
using NUnit.Framework;

using Spring.Context;
using Spring.Context.Support;
using Common.Logging;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.utils
{
    [TestFixture]
    public class DateUtilsTest
    {
        private static readonly ILog LOG
            = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Test]
        public void testIsLeapYear()
        {
            Assert.IsTrue(DateUtils.isLeapYear(2000));
            Assert.IsFalse(DateUtils.isLeapYear(2001));
            Assert.IsTrue(DateUtils.isLeapYear(2012));
            Assert.IsTrue(DateUtils.isLeapYear(6024));
            Assert.IsFalse(DateUtils.isLeapYear(0));
        }

        [Test]
        public void testIsValidMonth()
        {
            Assert.IsFalse(DateUtils.isValidMonth(-12));
            Assert.IsFalse(DateUtils.isValidMonth(13));
            Assert.IsTrue(DateUtils.isValidMonth(1));
            Assert.IsTrue(DateUtils.isValidMonth(2));
            Assert.IsTrue(DateUtils.isValidMonth(3));
            Assert.IsTrue(DateUtils.isValidMonth(4));
            Assert.IsTrue(DateUtils.isValidMonth(5));
            Assert.IsTrue(DateUtils.isValidMonth(6));
            Assert.IsTrue(DateUtils.isValidMonth(7));
            Assert.IsTrue(DateUtils.isValidMonth(8));
            Assert.IsTrue(DateUtils.isValidMonth(9));
            Assert.IsTrue(DateUtils.isValidMonth(10));
            Assert.IsTrue(DateUtils.isValidMonth(11));
            Assert.IsTrue(DateUtils.isValidMonth(12));
        }

        [Test]
        public void testIsValidDay()
        {
            Assert.IsFalse(DateUtils.isValidDay(1900, 2, 0));
            Assert.IsFalse(DateUtils.isValidDay(1900, 2, 32));
            Assert.IsFalse(DateUtils.isValidDay(1900, 4, 31));
            Assert.IsTrue(DateUtils.isValidDay(1900, 1, 30));
            Assert.IsFalse(DateUtils.isValidDay(1900, 2, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 3, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 4, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 5, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 6, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 7, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 8, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 9, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 10, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 11, 30));
            Assert.IsTrue(DateUtils.isValidDay(1900, 12, 30));
            Assert.IsFalse(DateUtils.isValidDay(2001, 2, 29));
            Assert.IsTrue(DateUtils.isValidDay(2004, 2, 29));
        }

        [Test]
        public void testIs30DayMonth()
        {
            Assert.IsTrue(DateUtils.is30DayMonth(4));
            Assert.IsTrue(DateUtils.is30DayMonth(6));
            Assert.IsTrue(DateUtils.is30DayMonth(9));
            Assert.IsTrue(DateUtils.is30DayMonth(11));
            Assert.IsFalse(DateUtils.is30DayMonth(1));
            Assert.IsFalse(DateUtils.is30DayMonth(2));
            Assert.IsFalse(DateUtils.is30DayMonth(3));
            Assert.IsFalse(DateUtils.is30DayMonth(5));
            Assert.IsFalse(DateUtils.is30DayMonth(7));
            Assert.IsFalse(DateUtils.is30DayMonth(8));
            Assert.IsFalse(DateUtils.is30DayMonth(10));
            Assert.IsFalse(DateUtils.is30DayMonth(12));
        }

        [Test]
        public void testIsWellFormedDatePart()
        {
            Assert.IsFalse(DateUtils.isWellFormedDatePart(""));
            Assert.IsFalse(DateUtils.isWellFormedDatePart(null));
            Assert.IsFalse(DateUtils.isWellFormedDatePart("09121978"));
            Assert.IsFalse(DateUtils.isWellFormedDatePart("1978"));
            Assert.IsFalse(DateUtils.isWellFormedDatePart("1978052212"));
            Assert.IsFalse(DateUtils.isWellFormedDatePart("theeight"));
            
            Assert.IsFalse(DateUtils.isWellFormedDatePart("1978.0912."));            

            Assert.IsFalse(DateUtils.isWellFormedDatePart("19782112")); // bad month
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19782112")); // bad day
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19780499")); // too many days

            Assert.IsFalse(DateUtils.isWellFormedDatePart("19780431")); // too many days
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19780631")); // too many days
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19780931")); // too many days
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19781131")); // too many days
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19780230")); // too many days
            Assert.IsFalse(DateUtils.isWellFormedDatePart("19780229")); // not leap year

            Assert.IsTrue(DateUtils.isWellFormedDatePart("19780912"));
            Assert.IsTrue(DateUtils.isWellFormedDatePart("19780912."));
            Assert.IsTrue(DateUtils.isWellFormedDatePart("19800229")); 

        }

        [Test]
        public void testIsWellFormedTimePart()
        {
            Assert.IsTrue(DateUtils.isWellFormedTimePart("asdf"));
            Assert.IsFalse(DateUtils.isWellFormedTimePart("200405.221234"));

            Assert.IsTrue(DateUtils.isWellFormedTimePart("20040522.1234"));

            Assert.IsFalse(DateUtils.isWellFormedTimePart("20040522.12345678"));
            Assert.IsFalse(DateUtils.isWellFormedTimePart("20040522.12345678"));
            Assert.IsFalse(DateUtils.isWellFormedTimePart("20040522.asdfgh"));

            Assert.IsFalse(DateUtils.isWellFormedTimePart("20040522.443456"));
            Assert.IsFalse(DateUtils.isWellFormedTimePart("20040522.129956"));
            Assert.IsFalse(DateUtils.isWellFormedTimePart("20040522.123499"));
            
            Assert.IsTrue(DateUtils.isWellFormedTimePart("20040522.123456"));
        }

        [Test]
        public void testIsWellFormedUtcDateTime()
        {
            Assert.IsTrue(DateUtils.isWellFormedUtcDateTime("20040522.123456"));
            Assert.IsFalse(DateUtils.isWellFormedUtcDateTime("200405.123456"));
            Assert.IsTrue(DateUtils.isWellFormedUtcDateTime("20040522.1234"));
        }

        [Test]
        public void testTrimSeconds() {
            
            Assert.AreEqual("20040522123456", DateUtils.trimSeconds("20040522123456"));
            Assert.AreEqual("20040522.1234", DateUtils.trimSeconds("20040522.1234"));
            Assert.AreEqual("20040522.1234", DateUtils.trimSeconds("20040522.123456"));

        }

        [Test]
        public void testZeroSeconds()
        {
            Assert.AreEqual("20040522123456.000000", DateUtils.zeroSeconds("20040522123456"));
            Assert.AreEqual("20040522.123400", DateUtils.zeroSeconds("20040522.1234"));
            Assert.AreEqual("20040522.123400", DateUtils.zeroSeconds("20040522.123456"));
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException", "Invalid 'from' date: ")]
        public void testIsValidDateRangesNull1stArg()
        {
            DateUtils.CheckDateRange(null, "20070102");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException", "Invalid 'to' date: ")]
        public void testIsValidDateRangesNull2ndArg()
        {
            DateUtils.CheckDateRange("20070102", null);
        }

        [Test]
        [ExpectedException(typeof(MdoException), "Invalid 'from' date: abcd")]
        public void testIsValidDateRangesBadFrom()
        {
            DateUtils.CheckDateRange("abcd", "20070102");
        }

        [Test]
        [ExpectedException(typeof(MdoException), "Invalid 'to' date: abcd")]
        public void testIsValidDateRangesBadTo()
        {
            DateUtils.CheckDateRange("20070102", "abcd");
        }

        [Test]
        public void testValidDateRanges()
        {
            

            DateUtils.CheckDateRange("20070102", "20080102");
            DateUtils.CheckDateRange("20070102.012345", "20080102");
            DateUtils.CheckDateRange("20070102", "20080102.012345");
            DateUtils.CheckDateRange("20070102.012345", "20080102.012345");
        }

        [Test]
        [ExpectedException(typeof(InvalidDateRangeException), "Invalid date range")]
        public void testInvalidDateRange_starttimeBeforeEnd()
        {
            DateUtils.CheckDateRange("20070102.022345", "20070102.012345");
        }

        [Test]
        [ExpectedException(typeof(InvalidDateRangeException), "Invalid date range")]
        public void testInvalidDateRange_startBeforeEnd()
        {
            DateUtils.CheckDateRange("20070103", "20070102");
        }

        [Test]
        [ExpectedException(typeof(InvalidDateRangeException), "Invalid date range")]
        public void testInvalidDateRange_startEqualsEnd()
        {            
            DateUtils.CheckDateRange("20070102", "20070102"); //same date
        }


        [Test]
        public void TestIsoDateStringToDateTime()
        {
            String dateString = "20070102";
            DateTime testTime = DateUtils.IsoDateStringToDateTime(dateString);
            Assert.AreEqual(new DateTime(2007, 01, 02), testTime, "expect new DateTime of 2007/01/02");
        }

        [Test]
        [Category("real-sites")]
        public void TestIsoDateStringToDateTimeWithHours()
        {
            String dateString = "20070102.123456789";
            DateTime testTime = DateUtils.IsoDateStringToDateTime(dateString);
            Assert.AreEqual(new DateTime(2007, 01, 02,12,34,56,789), testTime, "unexpected DateTime");
        }

        [Test]
        [Category("real-sites")]
        public void TestIsoDateStringToDateTimeWithHoursMissingSeconds()
        {
            String dateString = "20070102.123400789";
            DateTime testTime = DateUtils.IsoDateStringToDateTime(dateString);
            Assert.AreEqual(new DateTime(2007, 01, 02, 12, 34, 0, 789), testTime, "unexpected DateTime");
        }

        [Test]
        [Category("unit-only")]
        public void TestTrimTime_NormalDateTime()
        {
            string DATE_TIME = "20091123.123456";
            Assert.AreEqual("20091123", DateUtils.trimTime(DATE_TIME));
        }

        [Test]
        [Category("unit-only")]
        public void TestTrimTime_NormalDateOnly()
        {
            string DATE_TIME = "20091123";
            Assert.AreEqual("20091123", DateUtils.trimTime(DATE_TIME));
        }

        /// <summary>Function will return an empty date if nothing present before the '.' separator
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestTrimTime_SeparatorAndTimeOnly()
        {
            string DATE_TIME = ".123456";
            Assert.AreEqual("", DateUtils.trimTime(DATE_TIME));
        }
    }
}
