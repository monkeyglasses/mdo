using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo.dao.hl7.rxRefill
{
    [TestFixture]
    public class RxRefillDecoderTest
    {
        [Test]
        public void testDecode01()
        {
            string message = "MSH|^~\\&|MHV VISTA|989^127.0.0.1^DNS|MHV EVAULT|200MHS^127.0.0.1^DNS|20120125095353-0400||ORP^O10|98958809650|T|2.4|||||\r" +
                "MSA|AA|6367424|\r" +
                "PID|||1012647112V777879^^^USVHA&&HL70363^NI^VA FACILITY ID&989&L~138352^^^USVHA&&HL70363^PI^VA FACILITY ID&989&L~578787893^^^USSSA&&HL70363^SS^VA FACILITY ID&989&L||MHVALAROBTHIRTEEN^TESTTHIRTEEN^^^^\r" +
                "ORC|OK|2719090-20120125085353|2719090-20120125085353\r" +
                "RXE|1^^^20120125085353|OK^1^HL70119|1||1 refill unit||||||||||2719090";

            RxRefillDecoder decoder = new RxRefillDecoder();
            decoder.parse(message);
        }
    }
}
