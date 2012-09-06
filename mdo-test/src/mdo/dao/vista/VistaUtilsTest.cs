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
using System.Text;
using NUnit.Framework;
using gov.va.medora.TOReflection;
using System.Collections.Specialized;
using gov.va.medora.utils;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.dao.vista
{
    [TestFixture]
    public class VistaUtilsTest
    {
        [Test]
        public void adjustForNameSearch()
        {
            string target = "hans tischner";
            string result = VistaUtils.adjustForNameSearch(target);
            Assert.AreEqual("hans tischneq~", result);

            target = "sam two toes";
            result = VistaUtils.adjustForNameSearch(target);
            Assert.AreEqual("sam two toer~", result);

            target = "1234";
            result = VistaUtils.adjustForNameSearch(target);
            Assert.AreEqual("1233~", result);

            target = "!@#$%^&*()";
            result = VistaUtils.adjustForNameSearch(target);
            Assert.AreEqual("!@#$%^&*((~", result);
        }

        [Test]
        public void adjustForNameSearch_Emtpy()
        {
            string target = "";
            string result = VistaUtils.adjustForNameSearch(target);
            Assert.AreEqual("", result);
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void adjustForNameSearch_Null()
        {
            string target = null;
            string result = VistaUtils.adjustForNameSearch(target);
            Assert.IsNull(result);
        }

        [Test]
        public void adjustForNumericSearch()
        {
            string target = "404";
            string result = VistaUtils.adjustForNumericSearch(target);
            Assert.AreEqual("403", result);
        }

        [Test]
        [ExpectedException("System.FormatException")]
        public void adjustForNumericSearch_Empty()
        {
            string target = "";
            string result = VistaUtils.adjustForNumericSearch(target);
            Assert.IsNull(result);
        }

        [Test]
        [ExpectedException("System.FormatException")]
        public void adjustForNumericSearch_NAN()
        {
            string target = "asdfjk12";
            string result = VistaUtils.adjustForNumericSearch(target);
            Assert.IsNull(result);
        }

        [Test]
        public void adjustForNumericSearch_Null()
        {
            string target = null;
            string result = VistaUtils.adjustForNumericSearch(target);
            Assert.AreEqual("-1", result);
        }

        [Test]
        public void setDirectionParam()
        {
            string target = null;
            string result = VistaUtils.setDirectionParam(target);
            Assert.AreEqual("1", result);

            target = "";
            result = VistaUtils.setDirectionParam(target);
            Assert.AreEqual("1", result);

            target = "1";
            result = VistaUtils.setDirectionParam(target);
            Assert.AreEqual("1", result);

            target = "-1";
            result = VistaUtils.setDirectionParam(target);
            Assert.AreEqual("-1", result);
        }

        [Test]
        [ExpectedException(typeof(MdoException), "Invalid direction.  Must be 1 or -1.")]
        public void setDirectionParam_Not1orMinus1()
        {
           string target = "left";
           VistaUtils.setDirectionParam(target);          
        }

        [Test]
        [ExpectedException(typeof(InvalidlyFormedRecordIdException), "Invalidly formed record ID: abcdefg")]
        public void buildReportTextRequest_InvalidDfn()
        {
            VistaUtils.buildReportTextRequest("abcdefg", "20061219.122200", "20071219.122200", 10, "some report");
        }

        [Test]
        [ExpectedException(typeof(InvalidlyFormedRecordIdException), "Invalidly formed record ID: ")]
        public void buildReportTextRequest_EmptyDfn()
        {
            VistaUtils.buildReportTextRequest("", "20061219.122200", "20071219.122200", 10, "some report");
        }

        [Test]
        [ExpectedException(typeof(InvalidlyFormedRecordIdException), "Invalidly formed record ID: ")]
        public void buildReportTextRequest_NullDfn()
        {
            VistaUtils.buildReportTextRequest(null, "20061219.122200", "20071219.122200", 10, "some report");
        }

        [Test]
        [ExpectedException(typeof(InvalidDateRangeException), "Invalid date range")]
        public void buildReportTextRequest_InvalidRange()
        {
            VistaUtils.buildReportTextRequest("1234567", "20071219.122200", "20061219.122200", 10, "some report");            
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyParamException), "Null or empty input parameter: routine name")]
        public void buildReportTextRequest_EmptyArg()
        {
            VistaUtils.buildReportTextRequest("1234567", "20061219.122200", "20071219.122200", 10, "");
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyParamException), "Null or empty input parameter: routine name")]
        public void buildReportTextRequest_NullArg()
        {
            VistaUtils.buildReportTextRequest("1234567", "20061219.122200", "20071219.122200", 10, null);
        }

        [Test]
        [ExpectedException(typeof(InvalidlyFormedRecordIdException), "Invalidly formed record ID: ")]
        public void buildReportTextRequest_AllResults_EmptyDfn()
        {
            VistaUtils.buildReportTextRequest_AllResults("", "some report");
        }

        [Test]
        [ExpectedException(typeof(InvalidlyFormedRecordIdException), "Invalidly formed record ID: ")]
        public void buildReportTextRequest_AllResults_NullDfn()
        {
            VistaUtils.buildReportTextRequest_AllResults(null, "some report");
        }

        [Test]
        [ExpectedException(typeof(InvalidlyFormedRecordIdException), "Invalidly formed record ID: abcd")]
        public void buildReportTextRequest_AllResults_BadDfn()
        {
            VistaUtils.buildReportTextRequest_AllResults("abcd", "some report");
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyParamException), "Null or empty input parameter: routine name")]
        public void buildReportTextRequest_AllResults_EmptyArg()
        {
            VistaUtils.buildReportTextRequest_AllResults("1234567", "");
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyParamException), "Null or empty input parameter: routine name")]
        public void buildReportTextRequest_AllResults_NullArg()
        {
            VistaUtils.buildReportTextRequest_AllResults("1234567", null);
        }


        [Test]
        public void responseOrOk()
        {
            Assert.AreEqual("OK", VistaUtils.responseOrOk(""));
            Assert.AreEqual("special response", VistaUtils.responseOrOk("special response"));
        }

        [Test]
        public void errMsgOrOK()
        {
            string response = "0^my error^is^not^your error";
            Assert.AreEqual("my error", VistaUtils.errMsgOrOK(response));
           
            response = "1^this should be^ok";
            Assert.AreEqual("OK", VistaUtils.errMsgOrOK(response));
        }

        [Test]
        [ExpectedException("System.IndexOutOfRangeException","Index was outside the bounds of the array.")]
        public void errMsgOrOK_1Field()
        {
            string response = "no error code";
            VistaUtils.errMsgOrOK(response);
        }

        [Test]
        public void errMsgOr0()
        {
            string response = "1^my error^is^not^your error";
            Assert.AreEqual("my error", VistaUtils.errMsgOrZero(response));

            response = "0^this should be^ok";
            Assert.AreEqual("OK", VistaUtils.errMsgOrZero(response));
        }

        [Test]
        [ExpectedException("System.IndexOutOfRangeException", "Index was outside the bounds of the array.")]
        public void errMsgOr0_1Field()
        {
            string response = "no error code";
            VistaUtils.errMsgOrZero(response);
        }

        [Test]
        public void errMsgOrIen()
        {
            string response = "7008";
            Assert.AreEqual(response, VistaUtils.errMsgOrIen(response));
        }

        [Test]
        [ExpectedException("System.ArgumentException", "Non-numeric IEN")]
        public void errMsgOrIen_Nan()
        {
            string response = "no error code";
            VistaUtils.errMsgOrIen(response);
        }

        [Test]
        [ExpectedException("System.ArgumentException", "Non-numeric IEN")]
        public void errMsgOrIen_Null()
        {
            VistaUtils.errMsgOrIen(null);
        }

        [Test]
        public void errMsgOrIen_Empty()
        {
            Assert.AreEqual("", VistaUtils.errMsgOrIen(""));
        }
        
        [Test]
        public void testRemoveCtlChars()
        {
            string s = "A\x0009B _~\\1\x000A\x000D2\x0001Cv\x001Aend";
            string result = VistaUtils.removeCtlChars(s);
            Assert.AreEqual("A\tB _~\\1\n\r2Cvend", result);
        }

        [Test]
        public void testRemoveCtlChars_Null()
        {
            string result = VistaUtils.removeCtlChars(null);
            Assert.AreEqual("", result);
        }

        [Test]
        public void testRemoveCtlChars_Empty()
        {
            string result = VistaUtils.removeCtlChars("");
            Assert.AreEqual("", result);
        }

        /// <summary>
        /// Current functionality only worked up to Int32 -- this caused problems with
        /// things like larger DFNS
        /// </summary>
        [Test]
        [Category("unit-only")]
        public void TestAdjustForNumericSearchInt64()
        {
            string longDfn = "5587247575";
            string output = VistaUtils.adjustForNumericSearch(longDfn);
            Assert.AreEqual("5587247574", output);
        }

        [Test]
        public void getVistaName()
        {
            Assert.AreEqual("", VistaUtils.getVistaName(""));

            Assert.AreEqual("", VistaUtils.getVistaName(null));

            Assert.AreEqual("", VistaUtils.getVistaName("joe shmoe"));

            Assert.AreEqual("", VistaUtils.getVistaName("shmoe,joe,bob"));

            Assert.AreEqual("SHMOE,JOE", VistaUtils.getVistaName("shmoe,joe"));

            Assert.AreEqual("", VistaUtils.getVistaName("2shmoe,joe"));
        }

        [Test]
        public void getVistaGender()
        {
            Assert.AreEqual("", VistaUtils.getVistaGender(""));

            Assert.AreEqual("", VistaUtils.getVistaGender(null));

            Assert.AreEqual("", VistaUtils.getVistaGender("joe shmoe"));

            Assert.AreEqual("", VistaUtils.getVistaGender("shmoe,joe,bob"));

            Assert.AreEqual("", VistaUtils.getVistaGender("2shmoe,joe"));

            Assert.AreEqual("M", VistaUtils.getVistaGender("mshmoe,joe"));

            Assert.AreEqual("M", VistaUtils.getVistaGender("Mshmoe,joe"));

            Assert.AreEqual("F", VistaUtils.getVistaGender("fshmoe,jane"));

            Assert.AreEqual("F", VistaUtils.getVistaGender("Fshmoe,jane"));
        }

        [Test]
        public void getVisitString()
        {
            Encounter x = new Encounter();
            x.LocationId = "my location id";
            x.Timestamp = "20101118.140400";
            x.Type = "the third kind";

            Assert.AreEqual("my location id;3101118.140400;the third kind", VistaUtils.getVisitString(x));
        }

        [Test]
        [ExpectedException("System.FormatException","Input string was not in a correct format.")]
        public void getVisitString_BadTS()
        {
            Encounter x = new Encounter();
            x.LocationId = "my location id";
            x.Timestamp = "hubahub";
            x.Type = "the third kind";

            VistaUtils.getVisitString(x);
        }

        [Test]
        public void toStringDictionary()
        {
            Assert.IsNull(VistaUtils.toStringDictionary(null));
            Assert.IsNull(VistaUtils.toStringDictionary(new String[] {}));

            StringDictionary expected = new StringDictionary();
            expected.Add("lower1", "lower1value");
            expected.Add("lower2", "lower2value");
            expected.Add("lower3", "lower3value");
            
            StringDictionary result = VistaUtils.toStringDictionary(
                new String[] {
                    "lower1^lower1value",
                    "lower2^lower2value",
                    "lower3^lower3value"
                }
            );

            Assert.AreEqual(new TOEqualizer(expected).HashCode, new TOEqualizer(result).HashCode);
        }

        [Test]
        [ExpectedException("System.IndexOutOfRangeException", "Index was outside the bounds of the array.")]
        public void toStringDictionary_BadResponse()
        {
            VistaUtils.toStringDictionary(
                new String[] {
                    "nokeyvalue"
                }
            );
        }

        [Test]
        [ExpectedException("System.ArgumentException", "Item has already been added. Key in dictionary: 'lower1'  Key being added: 'lower1'")]
        public void toStringDictionary_DuplicateKeys()
        {
            VistaUtils.toStringDictionary(
                new String[] {
                    "lower1^lower1value",
                    "lower2^lower2value",
                    "LOWER1^LOWER1value"
                }
            );
        }

        [Test]
        [ExpectedException(typeof(NullOrEmptyParamException), "Null or empty input parameter: arg")]
        public void buildGetVariableValueRequestEmptyArg()
        {
            string arg = "";
            Assert.AreEqual(-1273759458, VistaUtils.buildGetVariableValueRequest(arg).buildMessage().GetHashCode());
        }

        [Test]
        [ExpectedException(typeof(MdoException), "Invalid 'from' date: asdf")]
        public void buildFromToDateScreenParam_BadFrom()
        {
            VistaUtils.buildFromToDateScreenParam("asdf", "", 0, 0);
        }

        [Test]
        public void buildFromToDateScreenParam_EmptyFrom()
        {
            var result = VistaUtils.buildFromToDateScreenParam("", "20081118.140400", 0, 0);
            Assert.IsNotNull(result);
            Assert.AreEqual("", result);
        }

        [Test]
        [ExpectedException(typeof(MdoException), "Invalid 'to' date: adf")]
        public void buildFromToDateScreenParam_BadTo()
        {
            VistaUtils.buildFromToDateScreenParam("20101118.140400", "adf", 0, 0);
        }

        [Test]
        [ExpectedException(typeof(MdoException), "Invalid 'from' date: ")]
        public void buildFromToDateScreenParam_NullFrom()
        {
            VistaUtils.buildFromToDateScreenParam(null, "", 0, 0);
        }

        [Test]
        public void encrypt()
        {
            string[] ss = {"12345abcd", "", "%!@*\"#$^@!"};
            foreach (string s in ss)
            {
                string result = VistaUtils.encrypt(s);
                Assert.IsNotNull(result);
                Assert.AreNotEqual(result, s);
                Assert.AreEqual(result.Length, s.Length + 2);
            }            
        }

        [Test]
        [ExpectedException("System.NullReferenceException","Object reference not set to an instance of an object.")]
        public void encrypt_null()
        {
            VistaUtils.encrypt(null);
        }

        [Test]
        public void isWellFormedDuz()
        {
            Assert.IsFalse(VistaUtils.isWellFormedDuz(null));
            Assert.IsFalse(VistaUtils.isWellFormedDuz("basdf12"));
            Assert.IsFalse(VistaUtils.isWellFormedDuz(""));
            Assert.IsFalse(VistaUtils.isWellFormedDuz("!@#$%"));
            Assert.IsTrue(VistaUtils.isWellFormedDuz("12354"));
        }

        [Test]
        public void reverseKeyValue()
        {
            StringDictionary sd = new StringDictionary();
            sd.Add("1","one");
            sd.Add("2","two");

            StringDictionary ds = VistaUtils.reverseKeyValue(sd);
            foreach (string key in ds.Keys)
            {
                Assert.IsNotNull(sd[ds[key]]);
            }
            Assert.AreEqual(1995535015, new TOEqualizer(ds).HashCode);
        }

        [Test]
        [ExpectedException("System.ArgumentException", "Item has already been added. Key in dictionary: 'one'  Key being added: 'one'")]
        public void reverseKeyValue_ReverseCausesDuplicates()
        {
            StringDictionary sd = new StringDictionary();
            sd.Add("1", "one");
            sd.Add("2", "ONE");

            VistaUtils.reverseKeyValue(sd);            
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void reverseKeyValue_null()
        {
            VistaUtils.reverseKeyValue(null);
        }

        [Test]
        public void checkValidDFN_EmptyString()
        {
            string dfn = "";
            Assert.IsFalse(VistaUtils.isWellFormedIen(dfn));
        }

        [Test]
        public void checkValidDFN_Decimal()
        {
            string dfn = "123.234";
            Assert.IsTrue(VistaUtils.isWellFormedIen(dfn));
        }

        [Test]
        public void checkValidDFN_Integer()
        {
            string dfn = "123";
            Assert.IsTrue(VistaUtils.isWellFormedIen(dfn));
        }

        [Test]
        public void checkValidDFN_Zero()
        {
            string dfn = "0";
            Assert.IsFalse(VistaUtils.isWellFormedIen(dfn));
        }

        [Test]
        public void checkValidDFN_AlphaNumeric()
        {
            string dfn = "asdf234234";
            Assert.IsFalse(VistaUtils.isWellFormedIen(dfn));
        }

        [Test]
        public void checkValidDFN_Null()
        {
            string dfn = null;
            Assert.IsFalse(VistaUtils.isWellFormedIen(dfn));
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.NullOrEmptyParamException")]
        public void testCheckVisitStringEmptyString()
        {
            VistaUtils.CheckVisitString("");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException","Invalid visit string (need 3 semi-colon delimited pieces): 123;456;789;abc;def")]
        public void testCheckVisitStringTooManyPiecesString()
        {
            VistaUtils.CheckVisitString("123;456;789;abc;def");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException", "Invalid visit string (need 3 semi-colon delimited pieces): 123456789;abcdef")]
        public void testCheckVisitStringTooFewPiecesString()
        {
            VistaUtils.CheckVisitString("123456789;abcdef");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException","Invalid visit string (need 3 semi-colon delimited pieces): 123456789abcdef")]
        public void testCheckVisitStringNoPiecesString()
        {
            VistaUtils.CheckVisitString("123456789abcdef");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException", "Invalid visit string (invalid VistA timestamp): 123456789;abcdef;H")]
        public void testCheckVisitStringValidMidPieceString()
        {
            VistaUtils.CheckVisitString("123456789;abcdef;H");
        }

        [Test]
        public void testCheckVisitStringValidString()
        {
            VistaUtils.CheckVisitString("70;3110502.195436;H");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException", "Invalid visit string (type must be 'A', 'H' or 'E'): 70;3110502.195436;Y")]
        public void testCheckVisitStringInvalidVisitString()
        {
            VistaUtils.CheckVisitString("70;3110502.195436;Y");
        }

        [Test]
        [ExpectedException("gov.va.medora.mdo.exceptions.MdoException", "Invalid visit string (invalid location IEN): 70A;3110502.195436;H")]
        public void testCheckVisitStringInvalidLocationString()
        {
            VistaUtils.CheckVisitString("70A;3110502.195436;H");
        }

    }
}
