using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo.dao.vista
{
    [TestFixture]
    public class VistaDateTimeIteratorTest
    {

        [Test]
        [Category("unit-only")]
        public void TestSetIteratorLengthFromDates()
        {
            VistaDateTimeIterator testIterator = new VistaDateTimeIterator("20080102.234556", "20080103");
            Assert.IsTrue(testIterator._iterLength == VistaDateTimeIterator.SECOND_ITERATION);
            Assert.AreEqual((int)VistaDateTimeIterator.PRECISION.SECOND, testIterator._precision);
        }

        [Test]
        [Category("unit-only")]
        public void TestSetIteratorLengthFromEndDate()
        {
            VistaDateTimeIterator testIterator = new VistaDateTimeIterator("20080102", "20080103.01");
            Assert.IsTrue(testIterator._iterLength == VistaDateTimeIterator.HOUR_ITERATION);
            Assert.AreEqual((int)VistaDateTimeIterator.PRECISION.HOUR, testIterator._precision);
        }

        [Test]
        [Category("unit-only")]
        public void TestSetIteratorLengthFromEndDateZero()
        {
            VistaDateTimeIterator testIterator = new VistaDateTimeIterator("20080102", "20080103.00");
            Assert.IsTrue(testIterator._iterLength == VistaDateTimeIterator.HOUR_ITERATION);
            Assert.AreEqual((int)VistaDateTimeIterator.PRECISION.HOUR, testIterator._precision);
        }

        /// <summary>
        /// BEWARE: A DateTime string that gets padded with 0s will cause you to iterate
        /// in seconds.
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestSetIteratorLengthNormalizedNumber()
        {
            VistaDateTimeIterator testIterator = new VistaDateTimeIterator("20080102.000000", "20080103.0000");
            Assert.IsTrue(testIterator._iterLength == VistaDateTimeIterator.SECOND_ITERATION);
            Assert.AreEqual((int)VistaDateTimeIterator.PRECISION.SECOND, testIterator._precision);
        }

        /// <summary>Dummy check...</summary>
        [Test]
        [Category("unit-only")]
        public void TestPrecisionPositions()
        {
            string testDate = VistaTimestamp.fromDateTime(new DateTime(2008, 01, 23, 12, 34, 56, 789));
            Assert.AreEqual("308",testDate.Substring(0,(int)VistaDateTimeIterator.PRECISION.YEAR));
            Assert.AreEqual("30801", testDate.Substring(0,(int)VistaDateTimeIterator.PRECISION.MONTH));
            Assert.AreEqual("3080123", testDate.Substring(0,(int)VistaDateTimeIterator.PRECISION.DAY));
            Assert.AreEqual("3080123.12", testDate.Substring(0, (int)VistaDateTimeIterator.PRECISION.HOUR));
            Assert.AreEqual("3080123.1234", testDate.Substring(0, (int)VistaDateTimeIterator.PRECISION.MINUTE));
            Assert.AreEqual("3080123.123456", testDate.Substring(0, (int)VistaDateTimeIterator.PRECISION.SECOND));
        }

        /// <summary>
        /// Test to see that as we roll over days, that the iterator will do the right thing as well.
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestHourRollOver()
        {
            VistaDateTimeIterator testIterator = new VistaDateTimeIterator(
                new DateTime(2008, 01, 01, 22, 0, 0)
                , new DateTime(2008, 01, 03)
                , new TimeSpan(1, 0, 0)
            );
            List<string> values = new List<string>();
            while (!testIterator.IsDone())
            {
                testIterator.SetIterEndDate(); // put at start of loop
                values.Add(testIterator.GetDdrListerPart());
                testIterator.AdvanceIterStartDate(); // put at end of loop
            }
            int result = string.Concat(values.ToArray()).GetHashCode();
            Assert.AreEqual(1712919498, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationDays()
        {
            int result = VistaDateTimeIterator.IterationDays("1.000000");
            Assert.AreEqual(1, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationDaysPoint()
        {
            int result = VistaDateTimeIterator.IterationDays(".000000");
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationDays1Hour()
        {
            int result = VistaDateTimeIterator.IterationDays(".100000");
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationDays1HourNoPoint()
        {
            int result = VistaDateTimeIterator.IterationDays("100000");
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationHours()
        {
            int result = VistaDateTimeIterator.IterationHours("1.100000");
            Assert.AreEqual(10, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationHoursNoPoint()
        {
            int result = VistaDateTimeIterator.IterationHours("120000");
            Assert.AreEqual(12, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationHoursPoint()
        {
            int result = VistaDateTimeIterator.IterationHours(".345000");
            Assert.AreEqual(34, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationMinutes()
        {
            int result = VistaDateTimeIterator.IterationMinutes(".345000");
            Assert.AreEqual(50, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationSecond()
        {
            int result = VistaDateTimeIterator.IterationSeconds(".345036");
            Assert.AreEqual(36, result);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationTimeSpanFromString()
        {
            TimeSpan result = VistaDateTimeIterator.IterationTimeSpanFromString("1.010230");
            Assert.AreEqual(1, result.Days);
            Assert.AreEqual(1, result.Hours);
            Assert.AreEqual(2, result.Minutes);
            Assert.AreEqual(30, result.Seconds);
        }

        /// <summary>Values will roll over into next greater time range
        /// - e.g. seconds to minutes and hours to days
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestIterationTimeSpanFromStringTooManySeconds()
        {
            TimeSpan result = VistaDateTimeIterator.IterationTimeSpanFromString("1.250290");
            Assert.AreEqual(2, result.Days);
            Assert.AreEqual(1, result.Hours);
            Assert.AreEqual(3, result.Minutes);
            Assert.AreEqual(30, result.Seconds);
        }

        /// <summary>Values will roll over into next greater time range
        /// - e.g. seconds to minutes and hours to days
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestIterationTimeSpanFromStringOneDay()
        {
            TimeSpan result = VistaDateTimeIterator.IterationTimeSpanFromString("1.");
            Assert.AreEqual(1, result.Days);
            Assert.AreEqual(0, result.Hours);
            Assert.AreEqual(0, result.Minutes);
            Assert.AreEqual(0, result.Seconds);
        }

        /// <summary>
        /// This actually gives you a zero timespan, so the default of one day is
        /// used instead.
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestIterationTimeSpanFromStringOne()
        {
            TimeSpan result = VistaDateTimeIterator.IterationTimeSpanFromString("1");
            Assert.AreEqual(1, result.Days);
        }

        /// <summary>
        /// One hour
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestIterationTimeSpanFromStringDotOhOne()
        {
            TimeSpan result = VistaDateTimeIterator.IterationTimeSpanFromString(".01");
            Assert.AreEqual(1, result.Hours);
        }

        [Test]
        [Category("unit-only")]
        public void TestIterationTimeSpanFromStringOhOne()
        {
            TimeSpan result = VistaDateTimeIterator.IterationTimeSpanFromString("01");
            Assert.AreEqual(1, result.Hours);
        }
    }
}
