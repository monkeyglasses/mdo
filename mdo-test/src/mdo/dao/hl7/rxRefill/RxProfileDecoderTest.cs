using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.hl7.rxRefill
{
    [TestFixture]
    public class RxProfileDecoderTest
    {

        [Test]
        [ExpectedException(typeof(MdoException))]
        public void testDecode01()
        {
            string message = "MSH|^~\\&|EVET VISTA|658^127.0.0.1^DNS|EVET EVAULT|200MH^127.0.0.1^DNS|20050204111029-0400||RTB^K13|65810482605|D|2.4|||||US\r" +
               "MSA|AE|1107533501505740330|Missing Patient ID\r" +
               "ERR|PID^1^3^101&Missing Patient ID\r" +
               "QAK|0-96001|AE|Q13^REALTIME_RxList^HL70471|0|0|0\r" +
               "QPD|Q13^REALTIME_RxList^HL70471|0-96001|0|96001||||\r";

            RxProfileDecoder decoder = new RxProfileDecoder();
            decoder.parse(message);
        }

        [Test]
        public void testDecode02()
        {
            string message =
                "MSH|^~\\&|EVET VISTA|658^127.0.0.1^DNS|EVET EVAULT|200MH^127.0.0.1^DNS|20050202152609-0400||RTB^K13|65810482598|D|2.4|||||US\r" +
                "MSA|AA|1107375956189151861|\r" +
                "QAK|0-96001|OK|Q13^REALTIME_RxList^HL70471|3|3|0\r" +
                "QPD|Q13^REALTIME_RxList^HL70471|0-96001|0|96001||||9728\r" +
                "RDF|18|Prescription Number^ST^32~IEN^NM^30~Drug Name^ST^40~Issue Date/Time^TS^26~Last Fill Date^TS^26~Release Date/Time^TS^26~Expiration or Cancel Date^TS^26~Status^ST^20~Quantity^NM^10~Days Supply^NM^3~Number of Refills^NM^3~Provider^XCN^130~Placer Order Number^ST^40~Mail/Window^ST^1~Division^NM^3~MHV Request Status^NM^3~Remarks^ST^75~SIG^TX^1024\r" +
                "RDT|2117571D||DIFLUNISAL 500MG TAB|20040426|20040426||20050427|ACTIVE|180|90|3|9669^PATIENT^ONE^MIDDLE^JR^DR^MD|10068316|W|1||||RENEWED FROM RX # 2117571C|TAKE ONE TABLET BY MOUTH TWICE A DAY WITH FOOD\r" +
                "RDT|2378697||IBUPROFEN 600MG TAB|20040426|20040426||20050427|ACTIVE|270|90|3|22256|10068318|W|1||-2|||TAKE ONE TABLET BY MOUTH TWICE A DAY WITH FOOD|TAKE ONE TABLET BY MOUTH EVERY 8 HOURS FOR PAIN\r" +
                "RDT|2378696||VERAPAMIL HCL 180MG SA TAB|20040426|20040426||20050427|ACTIVE|270|90|3|22256|10068317|W|1||-2|||TAKE ONE TABLET BY MOUTH TWICE A DAY WITH FOOD|TAKE ONE TABLET BY MOUTH EVERY 8 HOURS FOR PAIN|TAKE ONE TABLET BY MOUTH EVERY 8 HOURS\r";

            RxProfileDecoder decoder = new RxProfileDecoder();
            IList<Medication> result = decoder.parse(message);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.IsNull(result[0].Id);
            Assert.AreEqual("DIFLUNISAL 500MG TAB", result[0].Name);
            Assert.AreEqual("2117571D", result[0].RxNumber);
            Assert.IsNull(result[0].PharmacyId);
            Assert.AreEqual("180", result[0].Quantity);
            Assert.AreEqual("20050427", result[0].ExpirationDate);
            Assert.AreEqual("20040426", result[0].IssueDate);
            Assert.AreEqual("10068316", result[0].OrderId);
            Assert.AreEqual("ACTIVE", result[0].Status);
            Assert.AreEqual("3", result[0].Refills);
            Assert.IsFalse(result[0].IsOutpatient);
            Assert.IsFalse(result[0].IsInpatient);
            Assert.IsFalse(result[0].IsIV);
            Assert.IsFalse(result[0].IsUnitDose);
            Assert.IsFalse(result[0].IsNonVA);
            Assert.IsFalse(result[0].IsImo);
            Assert.AreEqual("20040426", result[0].LastFillDate);
            Assert.IsNull(result[0].Remaining);
            Assert.IsNull(result[0].Facility);
            Assert.AreEqual("9669", result[0].Provider.Id);
            Assert.IsNull(result[0].Provider.Signature);
            Assert.IsNull(result[0].Cost);
            Assert.AreEqual("TAKE ONE TABLET BY MOUTH TWICE A DAY WITH FOOD", result[0].Sig);
            Assert.IsNull(result[0].Type);
            Assert.IsNull(result[0].Additives);
            Assert.IsNull(result[0].Solution);
            Assert.IsNull(result[0].Rate);
            Assert.IsNull(result[0].Route);
            Assert.IsNull(result[0].Dose);
            Assert.IsNull(result[0].Instruction);
            Assert.IsNull(result[0].Comment);
            Assert.IsNull(result[0].StartDate);
            Assert.IsNull(result[0].StopDate);
            Assert.IsNull(result[0].DateDocumented);
            Assert.IsNull(result[0].Documentor);
            Assert.AreEqual("RENEWED FROM RX # 2117571C", result[0].Detail);
            Assert.IsNull(result[0].Schedule);
            Assert.AreEqual("90", result[0].DaysSupply);
            Assert.IsNull(result[0].PharmacyOrderableItem.Key);
            Assert.IsNull(result[0].PharmacyOrderableItem.Value);
            Assert.IsNull(result[0].Drug.Key);
            Assert.IsNull(result[0].Drug.Value);
            Assert.IsNull(result[0].Hospital.Key);
            Assert.IsNull(result[0].Hospital.Value);
            Assert.IsNull(result[1].Id);
            Assert.AreEqual("IBUPROFEN 600MG TAB", result[1].Name);
            Assert.AreEqual("2378697", result[1].RxNumber);
            Assert.IsNull(result[1].PharmacyId);
            Assert.AreEqual("270", result[1].Quantity);
            Assert.AreEqual("20050427", result[1].ExpirationDate);
            Assert.AreEqual("20040426", result[1].IssueDate);
            Assert.AreEqual("10068318", result[1].OrderId);
            Assert.AreEqual("ACTIVE", result[1].Status);
            Assert.AreEqual("3", result[1].Refills);
            Assert.IsFalse(result[1].IsOutpatient);
            Assert.IsFalse(result[1].IsInpatient);
            Assert.IsFalse(result[1].IsIV);
            Assert.IsFalse(result[1].IsUnitDose);
            Assert.IsFalse(result[1].IsNonVA);
            Assert.IsFalse(result[1].IsImo);
            Assert.AreEqual("20040426", result[1].LastFillDate);
            Assert.IsNull(result[1].Remaining);
            Assert.IsNull(result[1].Facility);
            Assert.AreEqual("22256", result[1].Provider.Id);
            Assert.IsNull(result[1].Provider.Name);
            Assert.IsNull(result[1].Provider.Signature);
            Assert.IsNull(result[1].Cost);
            Assert.AreEqual("TAKE ONE TABLET BY MOUTH TWICE A DAY WITH FOOD", result[1].Sig);
            Assert.IsNull(result[1].Type);
            Assert.IsNull(result[1].Additives);
            Assert.IsNull(result[1].Solution);
            Assert.IsNull(result[1].Rate);
            Assert.IsNull(result[1].Route);
            Assert.IsNull(result[1].Dose);
            Assert.IsNull(result[1].Instruction);
            Assert.IsNull(result[1].Comment);
            Assert.IsNull(result[1].StartDate);
            Assert.IsNull(result[1].StopDate);
            Assert.IsNull(result[1].DateDocumented);
            Assert.IsNull(result[1].Documentor);
            Assert.IsNull(result[1].Detail);
            Assert.IsNull(result[1].Schedule);
            Assert.AreEqual("90", result[1].DaysSupply);
            Assert.IsNull(result[1].PharmacyOrderableItem.Key);
            Assert.IsNull(result[1].PharmacyOrderableItem.Value);
            Assert.IsNull(result[1].Drug.Key);
            Assert.IsNull(result[1].Drug.Value);
            Assert.IsNull(result[1].Hospital.Key);
            Assert.IsNull(result[1].Hospital.Value);
            Assert.IsNull(result[2].Id);
            Assert.AreEqual("VERAPAMIL HCL 180MG SA TAB", result[2].Name);
            Assert.AreEqual("2378696", result[2].RxNumber);
            Assert.IsNull(result[2].PharmacyId);
            Assert.AreEqual("270", result[2].Quantity);
            Assert.AreEqual("20050427", result[2].ExpirationDate);
            Assert.AreEqual("20040426", result[2].IssueDate);
            Assert.AreEqual("10068317", result[2].OrderId);
            Assert.AreEqual("ACTIVE", result[2].Status);
            Assert.AreEqual("3", result[2].Refills);
            Assert.IsFalse(result[2].IsOutpatient);
            Assert.IsFalse(result[2].IsInpatient);
            Assert.IsFalse(result[2].IsIV);
            Assert.IsFalse(result[2].IsUnitDose);
            Assert.IsFalse(result[2].IsNonVA);
            Assert.IsFalse(result[2].IsImo);
            Assert.AreEqual("20040426", result[2].LastFillDate);
            Assert.IsNull(result[2].Remaining);
            Assert.IsNull(result[2].Facility);
            Assert.AreEqual("22256", result[2].Provider.Id);
            Assert.IsNull(result[2].Provider.Name);
            Assert.IsNull(result[2].Provider.Signature);
            Assert.IsNull(result[2].Cost);
            Assert.AreEqual("TAKE ONE TABLET BY MOUTH TWICE A DAY WITH FOOD", result[2].Sig);
            Assert.IsNull(result[2].Type);
            Assert.IsNull(result[2].Additives);
            Assert.IsNull(result[2].Solution);
            Assert.IsNull(result[2].Rate);
            Assert.IsNull(result[2].Route);
            Assert.IsNull(result[2].Dose);
            Assert.IsNull(result[2].Instruction);
            Assert.IsNull(result[2].Comment);
            Assert.IsNull(result[2].StartDate);
            Assert.IsNull(result[2].StopDate);
            Assert.IsNull(result[2].DateDocumented);
            Assert.IsNull(result[2].Documentor);
            Assert.IsNull(result[2].Detail);
            Assert.IsNull(result[2].Schedule);
            Assert.AreEqual("90", result[2].DaysSupply);
            Assert.IsNull(result[2].PharmacyOrderableItem.Key);
            Assert.IsNull(result[2].PharmacyOrderableItem.Value);
            Assert.IsNull(result[2].Drug.Key);
            Assert.IsNull(result[2].Drug.Value);
            Assert.IsNull(result[2].Hospital.Key);
            Assert.IsNull(result[2].Hospital.Value);
        }
    }
}
