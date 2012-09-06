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
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo.domain.ccd
{
    [TestFixture]
    public class CCRHelperTest
    {

        [Test]
        public void testToCCRMed()
        {
            CCRHelper helper = new CCRHelper();

            StructuredProductType med = helper.buildMedObject("METOPROLOL TARTRATE TAB", "403949;O", "3100227", "3100528", "3100227.090342", "3100227", "3100528",
                "TAKE ONE TABLET MOUTH TWICE A DAY", "500", "MG", "TABLET", "1", "BID", "PO", "1", "0", "180", "PROVIDER,ONE", "983", "EXPIRED", "O");

            Assert.IsNotNull(med);
            Assert.AreEqual(med.Product.Count, 1);
            Assert.IsTrue(String.Equals(med.Product.First().ProductName.Text, "METOPROLOL TARTRATE TAB"));
            Assert.AreEqual(med.Product.First().IDs.Count, 1);
            Assert.IsTrue(String.Equals(med.Product.First().IDs.First().ID, "403949;O"));
            Assert.AreEqual(med.DateTime.Count, 4);
            Assert.IsTrue(String.Equals(med.DateTime.First().DateTimeRange.First().BeginRange.ExactDateTime, "3100227"));
            Assert.IsTrue(String.Equals(med.DateTime.First().DateTimeRange.First().EndRange.ExactDateTime, "3100528"));
            Assert.IsTrue(String.Equals(med.DateTime[1].ExactDateTime, "3100227.090342"));
            Assert.IsTrue(String.Equals(med.DateTime[1].Type.Text, "Prescription date"));
            Assert.IsTrue(String.Equals(med.DateTime[2].ExactDateTime, "3100227"));
            Assert.IsTrue(String.Equals(med.DateTime[2].Type.Text, "Dispense date"));
            Assert.IsTrue(String.Equals(med.DateTime[3].ExactDateTime, "3100528"));
            Assert.IsTrue(String.Equals(med.DateTime[3].Type.Text, "Expiration date"));
            Assert.IsTrue(String.Equals(med.PatientInstructions.First().Text, "TAKE ONE TABLET MOUTH TWICE A DAY"));
            Assert.IsTrue(String.Equals(med.Product.First().Strength.First().Value, "500"));
            Assert.IsTrue(String.Equals(med.Product.First().Strength.First().Units.Unit, "MG"));
            Assert.IsTrue(String.Equals(med.Product.First().Form.First().Text, "TABLET"));
            Assert.IsTrue(String.Equals(med.Directions.First().Dose.First().Value, "1"));
            Assert.IsTrue(String.Equals(med.Directions.First().Frequency.First().Value, "BID"));
            Assert.IsTrue(String.Equals(med.Directions.First().Route.First().Text, "PO"));
            Assert.IsTrue(String.Equals(med.Refills.First().Number.First(), "0")); // fills remaining
            Assert.IsTrue(String.Equals(med.Refills.First().Quantity.First().Value, "1")); // total refills allowed
            Assert.IsTrue(String.Equals(med.Quantity.First().Value, "180"));
            Assert.IsTrue(String.Equals(med.Source.First().Actor.First().ActorID, "PROVIDER,ONE"));
            Assert.IsTrue(String.Equals(med.Source.First().Actor.First().ActorRole.First().Text, "Prescribing clinician"));
            Assert.IsTrue(String.Equals(med.Status.Text, "EXPIRED"));
            Assert.IsTrue(String.Equals(med.Type.Text, "O"));
        }

        [Test]
        public void testToCCRProblem()
        {
            CCRHelper helper = new CCRHelper();
            ProblemType problem = helper.buildProblemObject("Congestive Heart Failure", "939", "3110103", "", "3110719", "3110719", "VEHU,TEN", "ICD9", "428.0", "", "A", "ACTIVE");

            Assert.IsNotNull(problem);
            Assert.IsTrue(String.Equals(problem.Description.Text, "Congestive Heart Failure"));
            Assert.AreEqual(problem.IDs.Count, 1);
            Assert.IsTrue(String.Equals(problem.IDs.First().ID, "939"));
            Assert.AreEqual(problem.DateTime.Count, 4);
            Assert.IsTrue(String.Equals(problem.DateTime[0].ExactDateTime, "3110103"));
            Assert.IsTrue(String.Equals(problem.DateTime[0].Type.Text, "Start date"));
            Assert.IsTrue(String.Equals(problem.DateTime[1].ExactDateTime, ""));
            Assert.IsTrue(String.Equals(problem.DateTime[1].Type.Text, "Stop date"));
            Assert.IsTrue(String.Equals(problem.DateTime[2].ExactDateTime, "3110719"));
            Assert.IsTrue(String.Equals(problem.DateTime[2].Type.Text, "Entered date"));
            Assert.IsTrue(String.Equals(problem.DateTime[3].ExactDateTime, "3110719"));
            Assert.IsTrue(String.Equals(problem.DateTime[3].Type.Text, "Updated date"));
            Assert.AreEqual(problem.Source.Count, 1);
            Assert.AreEqual(problem.Source.First().Actor.Count, 1);
            Assert.IsTrue(String.Equals(problem.Source.First().Actor.First().ActorID, "VEHU,TEN"));
            Assert.IsTrue(String.Equals(problem.Source.First().Actor.First().ActorRole.First().Text, "Treating clinician"));
            Assert.AreEqual(problem.Description.Code.Count, 1);
            Assert.IsTrue(String.Equals(problem.Description.Code.First().Value, "428.0"));
            Assert.IsTrue(String.Equals(problem.Description.Code.First().CodingSystem, "ICD9"));
            Assert.IsTrue(String.Equals(problem.Description.Code.First().Version, ""));
            Assert.IsTrue(String.Equals(problem.Status.Text, "ACTIVE"));
            Assert.IsTrue(String.Equals(problem.Status.Code.First().Value, "A"));
        }

        [Test]
        public void testBuildLabObject()
        {
            CCRHelper helper = new CCRHelper();
            TestType lab = helper.buildLabObject("CH;6899693.88;47", "CH 0323 1433", 
                "TRIGLYCERIDE", "SERUM", "completed", "3100305.12",
                "3100323.11314", "162", "mg/dL", "2345-7", "LOINC", "60", "110");

            Assert.IsNotNull(lab);
            Assert.AreEqual(lab.IDs.Count, 2);
            Assert.IsTrue(String.Equals(lab.IDs[0].ID, "CH;6899693.88;47"));
            Assert.IsTrue(String.Equals(lab.IDs[0].Type.Text, "ID"));
            Assert.IsTrue(String.Equals(lab.IDs[1].ID, "CH 0323 1433"));
            Assert.IsTrue(String.Equals(lab.IDs[1].Type.Text, "Accession number"));
            Assert.IsNotNull(lab.TestResult);
            Assert.IsTrue(String.Equals(lab.TestResult.Value, "162"));
            Assert.IsTrue(String.Equals(lab.TestResult.Units.Unit, "mg/dL"));
            Assert.AreEqual(lab.DateTime.Count, 2);
            Assert.IsTrue(String.Equals(lab.DateTime[0].ExactDateTime, "3100305.12"));
            Assert.IsTrue(String.Equals(lab.DateTime[0].Type.Text, "Collection date"));
            Assert.IsTrue(String.Equals(lab.DateTime[1].ExactDateTime, "3100323.11314"));
            Assert.IsTrue(String.Equals(lab.DateTime[1].Type.Text, "Completed date"));
            Assert.IsNotNull(lab.NormalResult);
            Assert.IsTrue(String.Equals(lab.NormalResult.First().Value, "60 - 110"));
            Assert.IsTrue(String.Equals(lab.Description.Text, "TRIGLYCERIDE - SERUM"));
            Assert.IsTrue(String.Equals(lab.Description.Code[0].Value, "2345-7"));
            Assert.IsTrue(String.Equals(lab.Description.Code[0].CodingSystem, "LOINC"));
            Assert.IsTrue(String.Equals(lab.Status.Text, "completed"));
        }

        [Test]
        public void testBuildAllergyObject()
        {
            CCRHelper helper = new CCRHelper();
            AlertType allergy = helper.buildAllergyObject("774", "125;GMRD(120.82,", "PENICILLIN", "DRUG", "D",
                "3050317.191", "3050317.191042", "ACTIVE", 
                new List<string>() { "ITCHING,WATERING EYES" });

            Assert.IsNotNull(allergy);
            Assert.AreEqual(allergy.DateTime.Count, 2);
            Assert.IsTrue(String.Equals(allergy.DateTime[0].ExactDateTime, "3050317.191"));
            Assert.IsTrue(String.Equals(allergy.DateTime[0].Type.Text, "Entered date"));
            Assert.IsTrue(String.Equals(allergy.DateTime[1].ExactDateTime, "3050317.191042"));
            Assert.IsTrue(String.Equals(allergy.DateTime[1].Type.Text, "Verified date"));
            Assert.IsTrue(String.Equals(allergy.Status.Text, "ACTIVE"));
            Assert.AreEqual(allergy.IDs.Count, 2);
            Assert.IsTrue(String.Equals(allergy.IDs[0].ID, "774"));
            Assert.IsTrue(String.Equals(allergy.IDs[0].Type.Text, "ID"));
            Assert.IsTrue(String.Equals(allergy.IDs[1].ID, "125;GMRD(120.82,"));
            Assert.IsTrue(String.Equals(allergy.IDs[1].Type.Text, "Local ID"));
            Assert.IsTrue(String.Equals(allergy.Description.Text, "PENICILLIN"));
            Assert.IsTrue(String.Equals(allergy.Type.Text, "DRUG"));
            Assert.IsTrue(String.Equals(allergy.Type.Code[0].Value, "D"));
            Assert.AreEqual(allergy.Reaction.Count, 1);
            Assert.IsTrue(String.Equals(allergy.Reaction[0].Description.Text, "ITCHING,WATERING EYES"));
        }

        [Test]
        public void testBuildPatientObject()
        {
            DemographicSet patientDemogs = new DemographicSet();
            Address addr = new Address() { City = "Hooville", State = "MI", County = "Eggs and Ham", Street1 = "123 Elm St.", Street2 = "Apt 4", Zipcode = "90210" };
            PhoneNum phone = new PhoneNum() { Description = "Cell phone", AreaCode = "555", Exchange = "867", Number = "5309" };
            EmailAddress email = new EmailAddress() { Address = "User.One@va.gov" };
            IList<Address> addresses = new List<Address>() { addr };
            IList<PhoneNum> telephones = new List<PhoneNum>() { phone };
            IList<EmailAddress> emails = new List<EmailAddress>() { email };
            patientDemogs.EmailAddresses = emails.ToList<EmailAddress>();
            patientDemogs.PhoneNumbers = telephones.ToList<PhoneNum>();
            patientDemogs.StreetAddresses = addresses.ToList<Address>();

            CCRHelper helper = new CCRHelper();
            ActorType patient = helper.buildPatientObject("1234567890", "987654321", "USER", "ONE", "O", "0000/12/25", "2011", "M", patientDemogs);

            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Item is ActorTypePerson);
            Assert.AreEqual(patient.Address.Count, 1);
            Assert.AreEqual(patient.EMail.Count, 1);
            Assert.AreEqual(patient.Telephone.Count, 1);
            Assert.AreEqual(patient.IDs.Count, 2);

            Assert.IsTrue(String.Equals(patient.IDs[0].ID, "1234567890"));
            Assert.IsTrue(String.Equals(patient.IDs[0].Type.Text, "ID"));
            Assert.IsTrue(String.Equals(patient.IDs[1].ID, "987654321"));
            Assert.IsTrue(String.Equals(patient.IDs[1].Type.Text, "SSN"));
            Assert.IsTrue(String.Equals(patient.Address[0].City, "Hooville"));
            Assert.IsTrue(String.Equals(patient.Address[0].County, "Eggs and Ham"));
            Assert.IsTrue(String.Equals(patient.Address[0].Line1, "123 Elm St."));
            Assert.IsTrue(String.Equals(patient.Address[0].Line2, "Apt 4"));
            Assert.IsTrue(String.Equals(patient.Address[0].PostalCode, "90210"));
            Assert.IsTrue(String.Equals(patient.Address[0].State, "MI"));
            Assert.IsTrue(String.Equals(patient.EMail[0].Value, "User.One@va.gov"));
            Assert.IsTrue(String.Equals(patient.Telephone[0].Value, "5558675309"));

            ActorTypePerson person = (ActorTypePerson)patient.Item;
            Assert.IsTrue(String.Equals(person.DateOfBirth.ExactDateTime, "0000/12/25"));
            Assert.IsTrue(String.Equals(person.DateOfBirth.Age.Value, "2011"));
            Assert.IsTrue(String.Equals(person.Gender.Text, "M"));
            Assert.IsTrue(String.Equals(person.Name.CurrentName.Family.First(), "ONE"));
            Assert.IsTrue(String.Equals(person.Name.CurrentName.Given.First(), "USER"));
            Assert.IsTrue(String.Equals(person.Name.CurrentName.Middle.First(), "O"));
        }
    }
}
