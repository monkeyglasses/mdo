using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo.dao.vista
{
    [TestFixture]
    public class VistaSocSecNumTest
    {
        /// <summary>
        /// Test method for the AreaNumber set.  Make sure 819 (Manila) is valid.
        /// </summary>
        [Test]
        public void testAreaNumber819()
        {
            SocSecNum theSSN = new VistaSocSecNum();
            theSSN.AreaNumber = "819";
            Assert.IsTrue(theSSN.IsValidAreaNumber);
        }

        private class TestSoc : SocSecNum
        {
            public TestSoc(String a, String b, String c)
            {
                myAreaNumber = a;
                myGroupNumber = b;
                mySerialNumber = c;
            }
        };

        [Test]
        public void testIsValid()
        {
            Assert.IsTrue(VistaSocSecNum.isValid(new SocSecNum("555555555")));
            Assert.IsFalse(VistaSocSecNum.isValid(new TestSoc("819", "11", "abcd")));
            Assert.IsTrue(VistaSocSecNum.isValid(new TestSoc("819", "11", "7894")));
            Assert.AreEqual(true, VistaSocSecNum.isValid(new TestSoc("111", "11", "1111")));
            Assert.IsFalse(VistaSocSecNum.isValid(new TestSoc("abc", "11", "1111")));
            Assert.IsFalse(VistaSocSecNum.isValid(new TestSoc("11", "ab", "1111")));
            Assert.IsFalse(VistaSocSecNum.isValid(new TestSoc("111", "11", "abcd")));
            Assert.IsFalse(VistaSocSecNum.isValid(new TestSoc("111", "1", "1111")));          
        }

        [Test]
        public void testIsValidAreaNumber()
        {
            SocSecNum theSSN = new VistaSocSecNum();
            theSSN.AreaNumber = "555";
            Assert.IsTrue(theSSN.IsValidAreaNumber);
        }

        [Test]
        [ExpectedException("System.ArgumentException", "Invalid area number")]
        public void testIsInValidAreaNumber()
        {
            SocSecNum theSSN = new VistaSocSecNum();
            theSSN.AreaNumber = "817";
        }

        /// <summary>
        /// Need to handle Pseudo SSNs as well. These should be indicated by a P (case-insensitive?) at the end of the number
        /// This tests what would otherwise be a valid SSN
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestNonPseudoSSNValidSSN()
        {
            VistaSocSecNum testSsn = new VistaSocSecNum("123456789");
            Assert.AreEqual("123", testSsn.AreaNumber, "Expected dashed area # to be 123");
            Assert.AreEqual("45", testSsn.GroupNumber, "Expected dashed group # to be 45");
            Assert.AreEqual("6789", testSsn.SerialNumber, "Expected dashed serial # to be 6789");
            Assert.AreEqual(false, testSsn.IsPseudo);
        }

        [Test]
        [Category("unit-only")]
        public void TestPseudoSSNValidSSN()
        {
            VistaSocSecNum testSsn = new VistaSocSecNum("123456789p");
            Assert.AreEqual("123", testSsn.AreaNumber, "Expected dashed area # to be 123");
            Assert.AreEqual("45", testSsn.GroupNumber, "Expected dashed group # to be 45");
            Assert.AreEqual("6789", testSsn.SerialNumber, "Expected dashed serial # to be 6789");
            Assert.AreEqual(true, testSsn.IsPseudo);
        }

        /// <summary>
        /// Checking that making validity more inclusive by supporting pseudo SSNs
        /// doesn't make validation more lax across the board for SSNs
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestPseudoSSNInvalidSSN()
        {
            /// the 666 should invalidate the SSN, not the P
            SocSecNum testSsn = new VistaSocSecNum("666456789p");
            Assert.IsFalse(testSsn.IsValid, "Should be an invalid SSN, even with the P stripped");
        }

        [Test]
        [Category("unit-only")]
        public void TestPseudoSSNValidSSNToString()
        {
            SocSecNum testSsn = new VistaSocSecNum("123456789p");
            Assert.AreEqual("123456789p", testSsn.toString());
        }

        [Test]
        [Category("unit-only")]
        public void TestPseudoSSNValidSSNToHyphenatedString()
        {
            SocSecNum testSsn = new VistaSocSecNum("123456789p");
            Assert.AreEqual("123-45-6789p", testSsn.toHyphenatedString());
        }
    }
}
