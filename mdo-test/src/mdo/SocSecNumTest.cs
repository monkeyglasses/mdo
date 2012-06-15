using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo
{
    /// <summary>
    /// Test class for gov.va.medora.mdo.SocSecNum
    /// </summary>
    /// <remarks>
    /// Contributed by Matt Schmidt (vhaindschmim0) and Robert Ruff (vhawpbruffr)
    /// </remarks>

    [TestFixture]
    public class SocSecNumTest
    {
        /// <summary>
        /// Test method for SocSecNum().  Each number should return null.
        /// </summary>
        [Test]
        public void testSocSecNum()
        {
            SocSecNum theSSN = new SocSecNum();
            Assert.IsNull(theSSN.AreaNumber);
            Assert.IsNull(theSSN.GroupNumber);
            Assert.IsNull(theSSN.SerialNumber);
        }

        /// <summary>
        /// Test method for SocSecNum(String).  An invalid input should result in all nulls for area, group, serial number.
        /// </summary>
        [Test]
        public void testSocSecNumString()
        {
            SocSecNum theSSN = new SocSecNum("123456789");
            Assert.AreEqual("123", theSSN.AreaNumber, "Expected area # to be 123");
            Assert.AreEqual("45", theSSN.GroupNumber, "Expected group # to be 45");
            Assert.AreEqual("6789", theSSN.SerialNumber, "Expected serial # to be 6789");
        }

        /// <summary>
        /// First test method for SocSecNum(String).  This tests various issues involving hyphenated ssns.
        /// </summary>
        [Test]
        public void testSocSecNumStringHyphen1()
        {
            SocSecNum theSSN;
            theSSN = new SocSecNum("123-45-6789");
            Assert.AreEqual("123", theSSN.AreaNumber, "Expected dashed area # to be 123");
            Assert.AreEqual("45", theSSN.GroupNumber, "Expected dashed group # to be 45");
            Assert.AreEqual("6789", theSSN.SerialNumber, "Expected dashed serial # to be 6789");
        }

        /// <summary>
        /// Second test method for SocSecNum(String).  This tests various issues involving hyphenated ssns.
        /// </summary>
        [Test]
        public void testSocSecNumStringHyphen2()
        {
            SocSecNum theSSN;
            theSSN = new SocSecNum("12-345-6789");
            Assert.AreEqual("123", theSSN.AreaNumber, "Expected dashed area # to be 123");
            Assert.AreEqual("45", theSSN.GroupNumber, "Expected dashed group # to be 45");
            Assert.AreEqual("6789", theSSN.SerialNumber, "Expected dashed serial # to be 6789");
        }

        /// <summary>
        /// Third test method for SocSecNum(String).  This tests various issues involving hyphenated ssns.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))] 
        public void testSocSecNumStringHyphen3()
        {
            SocSecNum theSSN;
            theSSN = new SocSecNum("12a-45-6789");
        }

        /// <summary>
        /// Fourth test method for SocSecNum(String).  This tests various issues involving hyphenated ssns.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testSocSecNumStringHyphen4()
        {
            SocSecNum theSSN;
            theSSN = new SocSecNum("123-4a-6789");
        }

        /// <summary>
        /// Fifth test method for SocSecNum(String).  This tests various issues involving hyphenated ssns.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testSocSecNumStringHyphen5()
        {
            SocSecNum theSSN;
            theSSN = new SocSecNum("123-45-678a");
        }

        /// <summary>
        /// Test method for SocSecNum(String).  This tests various alphanumeric inputs.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testSocSecNumStringAlpha()
        {
            SocSecNum theSSN;
            theSSN = new SocSecNum("abcdefghi");
        }

        /// <summary>
        /// Test method for the AreaNumber get.
        /// </summary>
        [Test]
        public void testAreaNumberGet()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123";
            Assert.AreEqual("123", theSSN.AreaNumber, "Expected AreaNumber to return 123");
        }
        
        /// <summary>
        /// Test method for the AreaNumber set.  Checks the set value against the contents of the internal AreaNumber property.
        /// </summary>
        [Test]
        public void testAreaNumberSet()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123";
            Assert.AreEqual("123", theSSN.AreaNumber, "Expected AreaNumber to be 123");
        }

        /// <summary>
        /// Test method for the AreaNumber set.  Make sure 819 (Manila) is invalid.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testAreaNumber819()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "819";
        }

        /// <summary>
        /// Test method for the AreaNumber set.  Checks that the exception for a non-numeric arg is thrown.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testAreaNumberSetExArgument()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "abc";
        }

        /// <summary>
        /// Test method for the GroupNumber get.
        /// </summary>
        [Test]
        public void testGroupNumberGet()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "45";
            Assert.AreEqual("45", theSSN.GroupNumber, "Expected GroupNumber to return 45");
        }

        /// <summary>
        /// Test method for the GroupNumber set.  Checks the set value against the contents of the internal GroupNumber property.
        /// </summary>
        [Test]
        public void testGroupNumberSet()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "45";
            Assert.AreEqual("45", theSSN.GroupNumber, "Expected GroupNumber to be 45");
        }

        /// <summary>
        /// Test method for the GroupNumber set.  Checks that the exception for a non-numeric arg is thrown.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testGroupNumberSetExArgument()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "ab";
        }

        /// <summary>
        /// Test method for the SerialNumber get.
        /// </summary>
        [Test]
        public void testSerialNumberGet()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "6789";
            Assert.AreEqual("6789", theSSN.SerialNumber, "Expected SerailNumber to return 6789");
        }

        /// <summary>
        /// Test method for the SerialNumber set.  Checks the set value against the contents of the internal SerialNumber property.
        /// </summary>
        [Test]
        public void testSerialNumberSet()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "6789";
            Assert.AreEqual("6789", theSSN.SerialNumber, "Expected SerialNumber to be 6789");
        }

        /// <summary>
        /// Test method for the SerialNumber set.  Checks that the exception for a non-numeric arg is thrown.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testSerialNumberSetExArgument()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "abcd";
        }

        /// <summary>
        /// First test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        public void testIsValidAreaNumber1()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123";
            Assert.IsTrue(theSSN.IsValidAreaNumber, "Expected 123 to be valid");
        }

        /// <summary>
        /// Second test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber2()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "12";
        }

        /// <summary>
        /// Third test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber3()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "1234";
        }

        /// <summary>
        /// Fourth test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber4()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "000";
        }

        /// <summary>
        /// Fifth test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber5()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "666";
        }

        /// <summary>
        /// Sixth test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber6()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "773";
        }

        /// <summary>
        /// Seventh test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber7()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "abc";
        }

        /// <summary>
        /// Eighth test method for IsValidAreaNumber.  
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAreaNumber8()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "";
        }


        /// <summary>
        /// First test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString1()
        {
            Assert.IsTrue(SocSecNum.isValidAreaNumber("123"), "Expected 123 to be valid"); 
        }

        /// <summary>
        /// Second test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString2()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber("12"), "Expected strings shorter than 3 chars to be invalid");
        }

        /// <summary>
        /// Third test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString3()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber("1234"), "Expected strings longer than 3 chars to be invalid");
        }

        /// <summary>
        /// Fourth test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString4()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber("000"), "Expected 000 to be invalid");
        }
        
        /// <summary>
        /// Fifth test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString5()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber("666"), "Expected 666 to be invalid");
        }

        /// <summary>
        /// Sixth test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString6()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber("773"), "Expected numbers over 772 to be invalid");
        }

        /// <summary>
        /// Seventh test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString7()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber("abc"), "Expected abc to be invalid");
        }

        /// <summary>
        /// Eigth test method for isValidAreaNumber(String).
        /// </summary>
        [Test]
        public void testIsValidAreaNumberString8()
        {
            Assert.IsFalse(SocSecNum.isValidAreaNumber(""), "Expected blank to be invalid");
        }

        /// <summary>
        /// First test method for IsValidGroupNumber.
        /// </summary>
        [Test]
        public void testIsValidGroupNumber1()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "12";
            Assert.IsTrue(theSSN.IsValidGroupNumber, "Expected 12 to be valid");
        }

        /// <summary>
        /// Second test method for IsValidGroupNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidGroupNumber2()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "00";
        }

        /// <summary>
        /// Third test method for IsValidGroupNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidGroupNumber3()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "1";
        }

        /// <summary>
        /// Fourth test method for IsValidGroupNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidGroupNumber4()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "123";
        }

        /// <summary>
        /// Fifth test method for IsValidGroupNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidGroupNumber5()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "ab";
        }

        /// <summary>
        /// Sixth test method for IsValidGroupNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidGroupNumber6()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.GroupNumber = "";
        }

        /// <summary>
        /// First test method for isValidGroupNumber(String).
        /// </summary>
        [Test]
        public void testIsValidGroupNumberString()
        {
            Assert.IsTrue(SocSecNum.isValidGroupNumber("12"), "Expected 12 to be valid");
        }

        /// <summary>
        /// Second test method for isValidGroupNumber(String).
        /// </summary>
        [Test]
        public void testIsValidGroupNumberString2()
        {
            Assert.IsFalse(SocSecNum.isValidGroupNumber("00"), "Expected 00 to be invalid");
        }

        /// <summary>
        /// Third test method for isValidGroupNumber(String).
        /// </summary>
        [Test]
        public void testIsValidGroupNumberString3()
        {
            Assert.IsFalse(SocSecNum.isValidGroupNumber("1"), "Expected strings shorter than 2 chars to be invalid");
        }

        /// <summary>
        /// Fourth test method for isValidGroupNumber(String).
        /// </summary>
        [Test]
        public void testIsValidGroupNumberString4()
        {
            Assert.IsFalse(SocSecNum.isValidGroupNumber("123"), "Expected strings over 2 chars to be invalid");
        }

        /// <summary>
        /// Fifth test method for isValidGroupNumber(String).
        /// </summary>
        [Test]
        public void testIsValidGroupNumberString5()
        {
            Assert.IsFalse(SocSecNum.isValidGroupNumber("ab"), "Expected ab to be invalid");
        }

        /// <summary>
        /// Sixth test method for isValidGroupNumber(String).
        /// </summary>
        [Test]
        public void testIsValidGroupNumberString6()
        {
            Assert.IsFalse(SocSecNum.isValidGroupNumber(""), "Expected blank to be invalid");
        }

        /// <summary>
        /// First test method for IsValidSerialNumber.
        /// </summary>
        [Test]
        public void testIsValidSerialNumber1()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "1234";
            Assert.IsTrue(theSSN.IsValidSerialNumber, "Expected 1234 to be valid");
        }

        /// <summary>
        /// Second test method for IsValidSerialNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidSerialNumber2()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "0000";
        }

        /// <summary>
        /// Third test method for IsValidSerialNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidSerialNumber3()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "123";
        }

        /// <summary>
        /// Fourth test method for IsValidSerialNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidSerialNumber4()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "12345";
        }

        /// <summary>
        /// Fifth test method for IsValidSerialNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidSerialNumber5()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "abcd";
        }

        /// <summary>
        /// Sixth test method for IsValidSerialNumber.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidSerialNumber6()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.SerialNumber = "";
        }

        /// <summary>
        /// First test method for isValidSerialNumber(String).
        /// </summary>
        [Test]
        public void testIsValidSerialNumberString1()
        {
            Assert.IsTrue(SocSecNum.isValidSerialNumber("1234"), "Expected 1234 to be valid");
        }

        /// <summary>
        /// Second test method for isValidSerialNumber(String).
        /// </summary>
        [Test]
        public void testIsValidSerialNumberString2()
        {
            Assert.IsFalse(SocSecNum.isValidSerialNumber("0000"), "Expected 0000 to be invalid");
        }

        /// <summary>
        /// Third test method for isValidSerialNumber(String).
        /// </summary>
        [Test]
        public void testIsValidSerialNumberString3()
        {
            Assert.IsFalse(SocSecNum.isValidSerialNumber("123"), "Expected strings under 4 chars to be invalid");
        }

        /// <summary>
        /// Fourth test method for isValidSerialNumber(String).
        /// </summary>
        [Test]
        public void testIsValidSerialNumberString4()
        {
            Assert.IsFalse(SocSecNum.isValidSerialNumber("12345"), "Expected strings over 4 chars to be invalid");
        }

        /// <summary>
        /// Fifth test method for isValidSerialNumber(String).
        /// </summary>
        [Test]
        public void testIsValidSerialNumberString5()
        {
            Assert.IsFalse(SocSecNum.isValidSerialNumber("abcd"), "Expected abcd to be invalid");
        }

        /// <summary>
        /// Sixth test method for isValidSerialNumber(String).
        /// </summary>
        [Test]
        public void testIsValidSerialNumberString6()
        {
            Assert.IsFalse(SocSecNum.isValidSerialNumber(""), "Expected blank to be invalid");
        }


        /// <summary>
        /// First test method for IsValid.
        /// </summary>
        [Test]
        public void testIsValid1()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123"; theSSN.GroupNumber = "45"; theSSN.SerialNumber = "6789";
            Assert.IsTrue(theSSN.IsValid, "Expected 123456789 to be valid");
        }

        /// <summary>
        /// Second test method for IsValid.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValid2()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123"; theSSN.GroupNumber = "45"; theSSN.SerialNumber = "678";
        }

        /// <summary>
        /// Third test method for IsValid.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValid3()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "1234"; theSSN.GroupNumber = "45"; theSSN.SerialNumber = "6789";
        }

        /// <summary>
        /// Fourth test method for IsValid.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValid4()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123"; theSSN.GroupNumber = "456"; theSSN.SerialNumber = "6789";
        }

        /// <summary>
        /// Fifth test method for IsValid.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValid5()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "123"; theSSN.GroupNumber = "45"; theSSN.SerialNumber = "67890";
        }

        /// <summary>
        /// Test method for IsValid.  This tests issues with alphanumeric input.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testIsValidAlpha()
        {
            SocSecNum theSSN = new SocSecNum();
            theSSN.AreaNumber = "abc"; theSSN.GroupNumber = "de"; theSSN.SerialNumber = "fghi";
        }


        /// <summary>
        /// First test method for isValid(String).
        /// </summary>
        [Test]
        public void testIsValidString1()
        {
            Assert.IsTrue(SocSecNum.isValid("123456789"), "Expected 123456789 to be valid");             
        }

        /// <summary>
        /// Second test method for isValid(String).
        /// </summary>
        [Test]
        public void testIsValidString2()
        {
            Assert.IsFalse(SocSecNum.isValid("12345678"), "Expected 12345678 to be invalid");
        }

        /// <summary>
        /// Third test method for isValid(String).
        /// </summary>
        [Test]
        public void testIsValidString3()
        {
            Assert.IsFalse(SocSecNum.isValid("1234567890"), "Expected 1234567890 to be invalid");
        }

        /// <summary>
        /// Test method for isValid(String).  This tests issues with alphanumeric input.
        /// </summary>
        [Test]
        public void testIsValidStringAlpha()
        {
            Assert.IsFalse(SocSecNum.isValid("abcdefghi"), "Expected abcdefghi to be invalid");     
        }

        /// <summary>
        /// First test method for isValid(String).  This tests issues with hyphenated input.
        /// </summary>
        [Test]
        public void testIsValidStringHyphen1()
        {
            Assert.IsTrue(SocSecNum.isValid("123-45-6789"), "SocSecNum with dashes should be valid");
        }

        /// <summary>
        /// Second test method for isValid(String).  This tests issues with hyphenated input.
        /// </summary>
        [Test]
        public void testIsValidStringHyphen2()
        {
            Assert.IsTrue(SocSecNum.isValid("12-345-6789"), "Don't care about misplaced dashes");
        }

        /// <summary>
        /// Third test method for isValid(String).  This tests issues with hyphenated input.
        /// </summary>
        [Test]
        public void testIsValidStringHyphen3()
        {
            Assert.IsTrue(SocSecNum.isValid("123_45@6789"), "Don't care about odd delimiters - only the 9 numbers");
        }

        /// <summary>
        /// Test method for stripField(String, int).
        /// </summary>
        [Test]
        public void testStripField()
        {
            Assert.AreEqual("123", SocSecNum.stripField("123456789", 1), "Expected valid ssn to strip area num to 123");
            Assert.AreEqual("45", SocSecNum.stripField("123456789", 2), "Expected valid ssn to strip group num to 45");
            Assert.AreEqual("6789", SocSecNum.stripField("123456789", 3), "Expected valid ssn to strip serial num to 6789");
        }

        /// <summary>
        /// Test method for stripField(String, int).  This tests issues with an invalid fldnum argument.
        /// </summary>
        [Test]
        public void testStripFieldFldnum()
        {
            Assert.AreEqual("", SocSecNum.stripField("123456789", 4), "Expected invalid fldnum of stripField to return blank");
        }

        /// <summary>
        /// Test method for stripField(String, int).  This tests issues with an input string being too short.
        /// </summary>
        [Test]
        public void testStripFieldShort()
        {
            Assert.AreEqual("", SocSecNum.stripField("12", 1), "Expected short ssn to strip area number to be blank");
            Assert.AreEqual("", SocSecNum.stripField("1234", 2), "Expected short ssn to strip group number to be blank");
            Assert.AreEqual("", SocSecNum.stripField("12345678", 3), "Expected short ssn to strip serial number to be blank");
        }

        /// <summary>
        /// Test method for stripField(String, int).  This tests issues with an input string being too long.
        /// </summary>
        [Test]
        public void testStripFieldLong()
        {
            Assert.AreEqual("123", SocSecNum.stripField("1234", 1), "Expected long ssn to strip area number to be 123");
            Assert.AreEqual("45", SocSecNum.stripField("123456", 2), "Expected long ssn to strip group number to be 45");
            Assert.AreEqual("6789", SocSecNum.stripField("1234567890", 3), "Expected long ssn to strip serial number to be 6789");
        }

        /// <summary>
        /// Test method for stripField(String, int).  This tests issues with a hyphenated input string.
        /// </summary>
        [Test]
        public void testStripFieldHyphen()
        {
            Assert.AreEqual("123", SocSecNum.stripField("123-45-6789", 1), "Expected hyphenated ssn to strip area num to 123");
            Assert.AreEqual("45", SocSecNum.stripField("123-45-6789", 2), "Expected hyphenated ssn to strip group num to 45");
            Assert.AreEqual("6789", SocSecNum.stripField("123-45-6789", 3), "Expected hyphenated ssn to strip serial num to 6789");
        }

        #region PSEUDO_SSN_HANDLING

        /// <summary>
        /// Need to handle Pseudo SSNs as well. These should be indicated by a P (case-insensitive?) at the end of the number
        /// This tests what would otherwise be a valid SSN
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestPseudoSSNValidSSN()
        {
            SocSecNum testSsn = new SocSecNum("123456789p");
            Assert.AreEqual("123", testSsn.AreaNumber, "Expected dashed area # to be 123");
            Assert.AreEqual("45", testSsn.GroupNumber, "Expected dashed group # to be 45");
            Assert.AreEqual("6789", testSsn.SerialNumber, "Expected dashed serial # to be 6789");
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
            SocSecNum testSsn = new SocSecNum("666456789p");
            Assert.IsFalse(testSsn.IsValid, "Should be an invalid SSN, even with the P stripped");
        }

        #endregion

        #region WOOLWORTHS_AND_PAMPHLET_SSNS

        [Test]
        [Category("InvalidSocialSecurityNumbers")]
        public void testSocialSecurityNumberInvalidNoDashesWoolworth()
        {
            SocSecNum theSSN = new SocSecNum("078051120");
            Assert.IsFalse(theSSN.IsValid, "For social security number 078051120, expected IsValid to hold false");
        }

        [Test]
        [Category("InvalidSocialSecurityNumbers")]
        public void testSocialSecurityNumberInvalidNoDashesPamphlet()
        {
            SocSecNum theSSN = new SocSecNum("219099999");
            Assert.IsFalse(theSSN.IsValid, "For social security number 219099999, expected IsValid to hold false");
        }


        [Test]
        [Category("InvalidSocialSecurityNumbers")]
        public void testSocialSecurityNumberInvalidDashesWoolworth()
        {
            SocSecNum theSSN = new SocSecNum("078-05-1120");
            Assert.IsFalse(theSSN.IsValid, "For social security number 078-05-1120, expected IsValid to hold false");
        }

        [Test]
        [Category("InvalidSocialSecurityNumbers")]
        public void testSocialSecurityNumberInvalidDashesPamphlet()
        {
            SocSecNum theSSN = new SocSecNum("219-09-9999");
            Assert.IsFalse(theSSN.IsValid, "For social security number 219-09-9999, expected IsValid to hold false");
        }


        #endregion // WOOLWORTHS_AND_PAMPHLET_SSNS

        /// <summary>
        /// Test method for toString().
        /// </summary>
        [Test]
        public void testtoString()
        {
            SocSecNum theSSN = new SocSecNum("123456789");
            Assert.AreEqual("123456789", theSSN.toString(), "Expected 123456789");
        }

        /// <summary>
        /// Overriden ToString() returns unhyphenated SSN
        /// </summary>
        [Test]
        public void testToString()
        {
            SocSecNum theSSN = new SocSecNum("123456789");
            Assert.AreEqual("123456789", theSSN.ToString(), "Expected 123456789");
        }

        /// <summary>
        /// Test method for toHyphenatedString().
        /// </summary>
        [Test]
        public void testToHyphenatedString()
        {
            SocSecNum theSSN = new SocSecNum("123456789");
            Assert.AreEqual("123-45-6789", theSSN.toHyphenatedString(), "Expected 123-45-6789");
        }

        [Test]
        public void testSensitiveSsn()
        {
            SocSecNum theSSN = new SocSecNum("*SENSITIVE*");
            Assert.IsTrue(theSSN.Sensitive);
            Assert.IsTrue(theSSN.SensitivityString == theSSN.ToString());
        }

        [Test]
        public void testSensitivityConstructor()
        {
            SocSecNum theSSN = new SocSecNum(true);
            Assert.IsTrue(theSSN.Sensitive);
            Assert.IsTrue(theSSN.SensitivityString == theSSN.ToString());
        }

    }
}
