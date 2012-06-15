using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo
{
    [TestFixture]
    public class DemographicSetTest
    {
        [Test]
        public void testEmptyObjects()
        {
            DemographicSet demogs1 = new DemographicSet();
            DemographicSet demogs2 = new DemographicSet();
            Assert.AreEqual(demogs1.GetHashCode(), demogs2.GetHashCode());
        } 

        [Test]
        public void testDemographicSetHashCode()
        {
            DemographicSet demogs1 = getDemogs1();
            DemographicSet demogs2 = getDemogs2();
            Assert.AreNotEqual(demogs1.GetHashCode(), demogs2.GetHashCode());
        }

        [Test]
        public void testChangedDemographicSetHashCodeValueToNull()
        {
            DemographicSet demogs1 = getDemogs1();
            DemographicSet demogs2 = getDemogs2();

            demogs2.EmailAddresses[0] = null; // null object

            Assert.AreNotEqual(demogs1.GetHashCode(), demogs2.GetHashCode());
        }

        [Test]
        public void testChangedDemographicSetHashCodeValuesToNull()
        {
            DemographicSet demogs1 = getDemogs1();
            DemographicSet demogs2 = getDemogs2();

            demogs1.EmailAddresses[0] = null; // null object
            demogs2.EmailAddresses[0] = null; // null object

            Assert.AreEqual(demogs1.GetHashCode(), demogs2.GetHashCode());
        }

        #region Helper Functions
        private DemographicSet getDemogs1()
        {
            DemographicSet demogs = new DemographicSet();
            demogs.EmailAddresses.Add(new EmailAddress("humpty.dumpty@va.gov"));
            demogs.PhoneNumbers.Add(new PhoneNum("734", "867", "5309"));
            demogs.StreetAddresses.Add(new Address()
            {
                Street1 = "2215 Fuller Rd",
                City = "Ann Arbor",
                State = "MI",
                Zipcode = "48105"
            });
            return demogs;
        }

        private DemographicSet getDemogs2()
        {
            DemographicSet demogs = new DemographicSet();
            demogs.EmailAddresses.Add(new EmailAddress("nightrider@va.gov"));
            demogs.PhoneNumbers.Add(new PhoneNum("734", "867", "5309"));
            demogs.StreetAddresses.Add(new Address()
            {
                Street1 = "2215 Fuller Rd",
                City = "Ann Arbor",
                State = "MI",
                Zipcode = "48105"
            });
            return demogs;
        }
        #endregion

    }
}
