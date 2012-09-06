#region CopyrightHeader
//
//  Copyright by Contributors
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0.txt
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
#endregion

using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading;

namespace gov.va.medora.utils
{
    [TestFixture]
    public class StringUtilsTest
    {
        [Test]
        public void testVarPack()
        {
            string version = "1.108";
            string expected = "|\x00051.108";
            string actual = StringUtils.varPack(version);
            Assert.AreEqual(expected, actual, Utils.errmsg(expected, actual));
        }

        [Test]
        public void testVarPackEmpty()
        {
            string expected = "|" + Convert.ToChar(1) + "0";
            Assert.AreEqual(expected, StringUtils.varPack(null));
            Assert.AreEqual(expected, StringUtils.varPack(""));
        }

        [Test]
        public void testLPack()
        {
            string val = "SomeValue";
            int countWidth = 3;
            string expected = "009SomeValue";
            string actual = StringUtils.LPack(val, countWidth);
            Assert.AreEqual(expected, actual, Utils.errmsg(expected, actual));
        }


        [Test]
        public void testSPack()
        {
            string val = "SomeString";
            string expected = "\x000ASomeString";
            string actual = StringUtils.SPack(val);
            Assert.AreEqual(expected, actual, Utils.errmsg(expected, actual));
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testLPackTooFew()
        {
            string val = "SomeString";
            StringUtils.LPack(val, 0);           
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void testSPackTooBig()
        {
            string val = new String('a', 300); 
            StringUtils.SPack(val);
        }

        [Test]
        public void testSplit()
        {
            string val = "a,b,c,d,e,f,g";
            string[] split = StringUtils.split(val, ',');
            Assert.AreEqual(7, split.Length);
            Assert.AreEqual("a", split[0]);
            Assert.AreEqual("b", split[1]);
            Assert.AreEqual("c", split[2]);
            Assert.AreEqual("d", split[3]);
            Assert.AreEqual("e", split[4]);
            Assert.AreEqual("f", split[5]);
            Assert.AreEqual("g", split[6]);            
        }

        [Test]
        public void testSplitWithoutMatch()
        {
            string val = "a,b,c,d,e,f,g";
            string[] split = StringUtils.split(val, '.');
            Assert.AreEqual(1, split.Length);
            Assert.AreEqual("a,b,c,d,e,f,g", split[0]);
        }

        [Test]
        public void testTrimArray()
        {
            // note that trim is right trim only
            string[] arr = { "a", "b", null, "" };
            string[] trim = StringUtils.trimArray(arr);            
            Assert.AreEqual(2, trim.Length);                       
        }

        [Test]
        public void testIsNumeric()
        {
            Assert.IsFalse(StringUtils.isNumeric(""));
            Assert.IsFalse(StringUtils.isNumeric(null));
            Assert.IsFalse(StringUtils.isNumeric("an@34;asdf"));
            Assert.IsFalse(StringUtils.isNumeric("123adsf123"));
            Assert.IsFalse(StringUtils.isNumeric("1231324.1234"));
            Assert.IsFalse(StringUtils.isNumeric("1a"));
            Assert.IsFalse(StringUtils.isNumeric("1!"));
            Assert.IsFalse(StringUtils.isNumeric("1+"));
            Assert.IsFalse(StringUtils.isNumeric("1^"));
            Assert.IsFalse(StringUtils.isNumeric("1~"));
            Assert.IsTrue(StringUtils.isNumeric("86542"));
        }

        [Test]
        public void testIsAlpha()
        {
            Assert.IsFalse(StringUtils.isAlpha(""));
            Assert.IsFalse(StringUtils.isAlpha("123asdf"));
            Assert.IsFalse(StringUtils.isAlpha("654"));
            Assert.IsFalse(StringUtils.isAlpha("!(#*&$SDF!@#@#$"));
            Assert.IsFalse(StringUtils.isAlpha("abCDef@#"));
            Assert.IsTrue(StringUtils.isAlpha("abcdefghijklmnopqrstuvwxyz"));
            Assert.IsTrue(StringUtils.isAlpha("QWERTYUIOPLKJHGFDSAZXCVBNM"));
            Assert.IsTrue(StringUtils.isAlpha("abcdefghijklmnopqrstuvwxyzQWERTYUIOPLKJHGFDSAZXCVBNM"));
        }

        [Test]
        public void testIsAlphaNumericChar()
        {
            Assert.IsFalse(StringUtils.isAlphaNumericChar('%'));
            Assert.IsFalse(StringUtils.isAlphaNumericChar('@'));
            Assert.IsTrue(StringUtils.isAlphaNumericChar('A'));
            Assert.IsTrue(StringUtils.isAlphaNumericChar('a'));
            Assert.IsTrue(StringUtils.isAlphaNumericChar('5'));
            Assert.IsTrue(StringUtils.isAlphaNumericChar('0'));            
        }

        [Test]
        public void testIsWhiteSpace()
        {
            Assert.IsTrue(StringUtils.isWhiteSpace(' '));
            Assert.IsTrue(StringUtils.isWhiteSpace('\n'));
            Assert.IsTrue(StringUtils.isWhiteSpace('\r'));
            Assert.IsTrue(StringUtils.isWhiteSpace('\t'));
            Assert.IsFalse(StringUtils.isWhiteSpace('0'));
            Assert.IsFalse(StringUtils.isWhiteSpace('a'));
            Assert.IsFalse(StringUtils.isWhiteSpace('A'));
            Assert.IsFalse(StringUtils.isWhiteSpace('%'));
        }

        [Test]
        public void testRemoveNonNumericChars()
        {
            Assert.AreEqual("12345", StringUtils.removeNonNumericChars("abcdDEFG12345!@#$%&"));
            Assert.AreEqual(String.Empty, StringUtils.removeNonNumericChars("abAD!@#"));
            Assert.IsFalse(StringUtils.removeNonNumericChars(")(123*987^asdf").Contains("123*987"));
            Assert.AreEqual(null, StringUtils.removeNonNumericChars(null));
        }

        [Test]
        public void testPiece()
        {
            string val = "12345^POIUY^%#!@$^asdfw";
            string del = "^";
           
            Assert.AreEqual("12345", StringUtils.piece(val, del, 1));
            Assert.AreEqual("POIUY", StringUtils.piece(val, del, 2));
            Assert.AreEqual("%#!@$", StringUtils.piece(val, del, 3));
            Assert.AreEqual("asdfw", StringUtils.piece(val, del, 4));
            Assert.AreEqual(null, StringUtils.piece(val, del, 5));
            Assert.AreEqual(null, StringUtils.piece(val, "^%", 5));
            Assert.AreEqual("12345^POIUY", StringUtils.piece(val, "^%", 1));
        }

        [Test]
        public void testIsEmpty()
        {
            Assert.IsTrue(StringUtils.isEmpty(""));
            Assert.IsTrue(StringUtils.isEmpty(null));
            Assert.IsFalse(StringUtils.isEmpty("asdfas"));
            Assert.IsFalse(StringUtils.isEmpty("1"));
        }
        
        [Test]
        public void testStrPack()
        {
            string val = "something!5here";
            string pack = StringUtils.strPack(val, 10);
            Assert.AreEqual("0000000015something!5here", pack);
        }

        [Test]
        public void testGetFirstWhiteSpaceAfter()
        {
            Assert.AreEqual(-1, StringUtils.getFirstWhiteSpaceAfter("therearenowhitespaces", 0));

            string val = "ab\tcdefghij k\rlm\nnop";
            Assert.AreEqual(-1, StringUtils.getFirstWhiteSpaceAfter(val, 0));
            Assert.AreEqual(2, StringUtils.getFirstWhiteSpaceAfter(val, 4));
            Assert.AreEqual(11, StringUtils.getFirstWhiteSpaceAfter(val, 12));            
        }

        [Test]
        public void testfilteredString()
        {
            string val = "";

            for (int i = 0; i < 255; i++)
            {
                val += Convert.ToChar(i);
            }

            var expected = "           !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~???????????????????????????????? ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþ";
            Assert.AreEqual(expected, StringUtils.filteredString(val));
        }

        [Test]
        public void testAsciiAt() 
        {
            string val = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~ ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþ";
            Assert.AreEqual(43, StringUtils.asciiAt(val, 10));
            Assert.AreEqual(77, StringUtils.asciiAt(val, 44));
            Assert.AreEqual(50, StringUtils.asciiAt(val, 17));
            Assert.AreEqual(126, StringUtils.asciiAt(val, 93));
            Assert.AreEqual(168, StringUtils.asciiAt(val, 102));
        }

        [Test]
        public void testPrependChars()
        {
            string val = "IAmTheBase";
            char prepend = 'w';
            int count = 22;

            string expected = "wwwwwwwwwwwwIAmTheBase";

            Assert.AreEqual(expected, StringUtils.prependChars(val, prepend, count));
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void testPrependCharsLenLessValLen()
        {
            string val = "IAmTheBase";
            char prepend = 'w';
            StringUtils.prependChars(val, prepend, 2);
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void testPrependCharsNullVal()
        {
            char prepend = 'w';
            StringUtils.prependChars(null, prepend, 5);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void testPrependCharsNegCount()
        {
            string val = "IAmTheBase";
            char prepend = 'w';
            StringUtils.prependChars(val, prepend, -22);
        }

        [Test]
        public void testGetIdx()
        {
            string[] val = { "there once was a", "man from nantucket", "who kept all his cash in a bucket", "But his daughter, named Nan,", "Ran away with a man", "And as for the bucket, Nantucket." };
            string find = "who kept all his cash";

            Assert.AreEqual(2, StringUtils.getIdx(val, find, 0));
            Assert.AreEqual(-1, StringUtils.getIdx(val, find, 3));
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void testGetIdxNullTarget()
        {
            string[] val = { "there once was a", "man from nantucket", "who kept all his cash in a bucket", "But his daughter, named Nan,", "Ran away with a man", "And as for the bucket, Nantucket." };
            StringUtils.getIdx(val, null, 0);            
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void testGetIdxNullVal()
        {
            string[] val = { "there once was a", "man from nantucket", "who kept all his cash in a bucket", "But his daughter, named Nan,", "Ran away with a man", "And as for the bucket, Nantucket." };
            StringUtils.getIdx(null, "who kept", 0);
        }

        [Test]
        [ExpectedException("System.IndexOutOfRangeException")]
        public void testGetIdxNegStart()
        {
            string[] val = { "there once was a", "man from nantucket", "who kept all his cash in a bucket", "But his daughter, named Nan,", "Ran away with a man", "And as for the bucket, Nantucket." };
            StringUtils.getIdx(val, "who kept", -20);
        }

        [Test]
        public void testTrimTrailingZeroes()
        {
            string val = "0000-as0%%df-123000";
            Assert.AreEqual("0000-as0%%df-123", StringUtils.trimTrailingZeroes(val));
           
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void testTrimTrailingZeroesNullString()
        {
            StringUtils.trimTrailingZeroes(null);
        }

        [Test]
        [ExpectedException("System.IndexOutOfRangeException")]
        public void testTrimTrailingZeroesEmptyString()
        {
           StringUtils.trimTrailingZeroes("");
        }

        [Test]
        public void testReverse()
        {
            string val = "abcd1234&*()";
            Assert.AreEqual(")(*&4321dcba", StringUtils.reverse(val));
            Assert.AreEqual("", StringUtils.reverse(String.Empty));
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void testReverseNull()
        {
            StringUtils.reverse(null);
        }

        [Test]
        public void testStripInvalidXmlCharacters()
        {
            string val = "12345" + Convert.ToChar(0xFFFE) + "abcdefg" + Convert.ToChar(0x02);
            Assert.AreEqual("12345abcdefg", StringUtils.stripInvalidXmlCharacters(val));
            Assert.AreEqual("", StringUtils.stripInvalidXmlCharacters(String.Empty));
            Assert.AreEqual("", StringUtils.stripInvalidXmlCharacters(null));
        }

        [Test]
        public void testGetMCharRandom()
        {
            int chars = 10;
            Assert.AreEqual(chars, StringUtils.getNCharRandom(chars).Length);
            Assert.AreNotEqual(StringUtils.getNCharRandom(chars), StringUtils.getNCharRandom(chars));           
        }

        [Test]
        public void testIsDecimal()
        {
            Assert.IsTrue(StringUtils.isDecimal("1234.1234"));
            Assert.IsTrue(StringUtils.isDecimal("1234.1234.1234"));
            Assert.IsFalse(StringUtils.isDecimal("ABCD.1234.1234"));
            Assert.IsFalse(StringUtils.isDecimal("."));
            Assert.IsFalse(StringUtils.isDecimal("1234"));
            Assert.IsFalse(StringUtils.isDecimal("ABCDS"));
            Assert.IsFalse(StringUtils.isDecimal("092%%"));
            Assert.IsFalse(StringUtils.isDecimal("!2*&*%"));
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void testIsDecimalNull()
        {
            StringUtils.isDecimal(null);
        }

        [Test]
        public void testStripNonPrintableChars()
        {
            Assert.AreEqual("", StringUtils.stripNonPrintableChars(null));
            Assert.AreEqual("", StringUtils.stripNonPrintableChars(""));

            string value = "123" + Convert.ToChar(133) + "456";
            Assert.AreEqual("123456", StringUtils.stripNonPrintableChars(value));

            Assert.AreEqual("123456", StringUtils.stripNonPrintableChars("123456"));
        }

        [Test]
        public void testFirstIndexOfNum()
        {
            Assert.AreEqual(-1, StringUtils.firstIndexOfNum(null));
            Assert.AreEqual(-1, StringUtils.firstIndexOfNum("abcdeasdf$%^&"));
            Assert.AreEqual(0, StringUtils.firstIndexOfNum("1asdfkjh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk0jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk1jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk2jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk3jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk4jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk5jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk6jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk7jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk8jh2"));
            Assert.AreEqual(5, StringUtils.firstIndexOfNum("asdfk9jh2"));
        }

        [Test]
        public void testGetMd5Hash()
        {
            string md5Hash = StringUtils.getMD5Hash("Zippudeedoodah");
            Assert.IsFalse(String.IsNullOrEmpty(md5Hash));
            Assert.AreEqual(md5Hash.Length, 32);
        }

        [Test]
        public void testHasherThreadSafety()
        {
            string hashString = "threaded hash string - should always hash to the same 32 character value";
            string hashedValue = StringUtils.getMD5Hash(hashString);
            int numThreads = 1024;
            IList<Thread> threads = new List<Thread>(numThreads);
            HashStringSwallower[] hsses = new HashStringSwallower[numThreads];

            for (int i = 0; i < numThreads; i++)
            {
                hsses[i] = new HashStringSwallower(hashString);
                threads.Add(new Thread(new ThreadStart(hsses[i].go)));
            }

            for (int i = 0; i < numThreads; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < numThreads; i++)
            {
                threads[i].Join();
            }

            for (int i = 0; i < numThreads; i++)
            {
                Assert.IsTrue(String.Equals(hsses[i].Hash, hashedValue));
            }

        }
    }

    public class HashStringSwallower
    {
        string _input;
        string _hash;
        public string Hash { get { return _hash; } }

        public HashStringSwallower(string input)
        {
            _input = input;
        }

        public void go()
        {
            _hash = StringUtils.getMD5Hash(_input);
        }
    }
}
