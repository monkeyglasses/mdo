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
using NHapi.Model.V24;
using NHapi.Base.Parser;
using NHapi.Model.V24.Segment;
using NHapi.Model.V24.Datatype;
using NHapi.Model.V24.Message;
using NHapi.Base.Model;

namespace gov.va.medora.mdo.dao.hl7.rxRefill
{
    public class RxRefillPharmacyDao : IPharmacyDao
    {	
        private static string[][] DATA_FIELDs = 
        {
		    new string[] { "Prescription Number", "NM", "20"},
		    new string[] {"IEN", "NM", "30"},
		    new string[] {"Drug Name", "ST", "40"},
		    new string[] {"Issue Date/Time", "TS", "26"},
		    new string[] {"Last Fill Date", "TS", "26"},
		    new string[] {"Release Date/Time", "TS", "26"},
		    new string[] {"Expiration or Cancel Date", "TS", "26"},
		    new string[] {"Status", "ST", "25"},
		    new string[] {"Quantity", "NM", "11"},
		    new string[] {"Days Supply", "NM", "3"},
		    new string[] {"Number of Refills", "NM", "3"},
		    new string[] {"Provider", "XPN", "150"},
		    new string[] {"Place Order Number", "ST", "30"},
		    new string[] {"Mail/Window", "ST", "1"},
		    new string[] {"Division", "NM", "3"},
		    new string[] {"Division Name", "ST", "20"},
		    new string[] {"MHV Request Status", "NM", "3"},
		    new string[] {"MHV Status Date", "TS", "26"},
		    new string[] {"Remarks", "ST", "75"},
		    new string[] {"SIG", "TX", "1024"}
	    };

        AbstractConnection _cxn;

        public RxRefillPharmacyDao(AbstractConnection cxn)
        {
            _cxn = cxn;
        }


        public Medication refillPrescription(string rxId)
        {
            return refillMeds(new List<string>() { rxId })[0];
        }

        internal IList<Medication> refillMeds(IList<string> medsToRefill)
        {
            OMP_O09_PID omp_o09 = new OMP_O09_PID();

            fillMshSegment(omp_o09.MSH, HL7Constants.FIELD_SEPARATOR.ToString(), "MHV EVAULT", "200MHS", "127.0.0.1", "DNS", "MHV VISTA",
                _cxn.DataSource.SiteId.Id, _cxn.DataSource.Provider, "DNS", "", "OMP", "O09", "OMP_O09", "", "P", "2.4");
            addIdToPID(omp_o09.getPid(), _cxn.Pid, 1, "", "");
            buildRxeAndOrcSegments(omp_o09, medsToRefill);

            PipeParser pp = new PipeParser();
            string request = pp.Encode(omp_o09);
            System.Console.WriteLine(request);
            //string response = (string)_cxn.query(request);
            //return new RxRefillDecoder().parse(response);
            throw new NotImplementedException("This function needs to be completed.");
        }

        internal IList<Medication> getMedsHl7()
        {
            QBP_Q13_PID msg = new QBP_Q13_PID();

            fillMshSegment(msg.MSH, HL7Constants.FIELD_SEPARATOR.ToString(), "MHV EVAULT", "200MHS", "127.0.0.1", "DNS", "MHV VISTA", _cxn.DataSource.SiteId.Id, 
                _cxn.DataSource.Provider, "DNS", "", "QBP", "Q13", "QBP_Q13", "", "P", "2.4");
            fillQpdSegment(msg, msg.QPD, "0", new DateTime(), new DateTime(), "", "Q13", "RxList");
            addIdToPID(msg.getPid(), _cxn.Pid, 1, "", "");
            fillRdfSegment(msg.RDF, DATA_FIELDs);
            fillRcpSegment(msg.RCP);

            string response = (string)_cxn.query(msg.encode());
            return toMedsFromHL7(response);
        }

        internal IList<Medication> toMedsFromHL7(string response)
        {
            IList<Medication> meds = new List<Medication>();
            RxProfileDecoder decoder = new RxProfileDecoder();
            return decoder.parse(response);
        }

        void fillMshSegment(MSH msh, string fieldSeparator, string sendingAppNamespace, string sendingFacilityNamespace, string sendingFacilityId, string sendingFacilityIdType,
            string receivingAppNamespace, string receivingFacilityNamespace, string receivingFacilityId, string receivingFacilityIdType, string dateTimeOfMessage,
            string messageType, string triggerEvent, string messageStructure, string messageControlId, string processingId, string versionId)
        {
            msh.FieldSeparator.Value = Convert.ToString(HL7Constants.FIELD_SEPARATOR);
            msh.EncodingCharacters.Value = HL7Constants.DEFAULT_DELIMITER;

            msh.SendingApplication.NamespaceID.Value = "MHV EVAULT";
            msh.SendingFacility.NamespaceID.Value = "200MH";
            msh.SendingFacility.UniversalID.Value = "127.0.0.1";
            msh.SendingFacility.UniversalIDType.Value = "DNS";

            msh.ReceivingApplication.NamespaceID.Value = "MHV VISTA";
            msh.ReceivingFacility.UniversalIDType.Value = "DNS";
            msh.ReceivingFacility.NamespaceID.Value = receivingFacilityNamespace; // site code
            msh.ReceivingFacility.UniversalID.Value = receivingFacilityId;
            msh.DateTimeOfMessage.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmsszzz").Replace(":", ""); // timezone offset comes with ':' via toString // dateTimeOfMessage;

            msh.MessageType.MessageType.Value = messageType;
            msh.MessageType.TriggerEvent.Value = triggerEvent;
            msh.MessageType.MessageStructure.Value = messageStructure;
            msh.MessageControlID.Value = String.Concat(_cxn.DataSource.SiteId.Id, Convert.ToString(DateTime.Now.Millisecond), new Random().Next());
            
            // it doesn't appear that SiteTable has any code to set this to true if VhaSites.xml specifies the site as a test source - ok for production
            // but possibly could cause a headache if trying to run against a test site and IsTestSource is always false
            if (_cxn.DataSource.IsTestSource)
            {
                msh.ProcessingID.ProcessingID.Value = "T";  
            }
            else
            {
                msh.ProcessingID.ProcessingID.Value = "P";
            }

            msh.VersionID.VersionID.Value = "2.4";
        }


        void addIdToPID(PID pid, string id, int idType, string assigningFacility, string assigningFacilityType)
        {
            CX pidListId = pid.GetPatientIdentifierList(pid.GetPatientIdentifierList().Length);

            pidListId.ID.Value = id;
            if (idType == 0)
            {
                pidListId.AssigningAuthority.NamespaceID.Value = "USSSA";
                pidListId.IdentifierTypeCode.Value = "SS";
            }
            else if (idType == 1)
            {
                pidListId.AssigningAuthority.NamespaceID.Value = "USVHA";
                pidListId.IdentifierTypeCode.Value = "NI";
            }
            else if (idType == 2)
            {
                pidListId.AssigningAuthority.NamespaceID.Value = "USVHA";
                pidListId.IdentifierTypeCode.Value = "PI";
            }

            if (!String.IsNullOrEmpty(assigningFacility) && !String.IsNullOrEmpty(assigningFacilityType))
            {
                pidListId.AssigningFacility.NamespaceID.Value = assigningFacilityType;
                pidListId.AssigningFacility.UniversalID.Value = assigningFacility;
            }
        }

        void fillRdfSegment(RDF rdf, string[][] fieldDefs)
        {
            rdf.NumberOfColumnsPerRow.Value = fieldDefs.Length.ToString();

            for (int i = 0; i < fieldDefs.Length; i++)
            {
                RCD rcd = rdf.GetColumnDescription(i);
                rcd.SegmentFieldName.Value = fieldDefs[i][0];
                rcd.HL7DateType.Value = fieldDefs[i][1];
                rcd.MaximumColumnWidth.Value = fieldDefs[i][2];
            }
        }

        void fillRcpSegment(RCP rcp)
        {
            rcp.QueryPriority.Value = "I";
        }

        void fillRxeSegment(string[] rxNumbers, QBP_Q13 qbp_q13)
        {
            for (int i = 0; i < rxNumbers.Length; i++)
            {
                RXE rxe = (RXE)qbp_q13.GetStructure("RXE", i);
                rxe.PrescriptionNumber.Value = rxNumbers[i];
            }
        }

        void fillQpdSegment(QBP_Q13_PID qbp_q13, QPD qpd, string requestId, DateTime fromDate, DateTime toDate, string icn, string trigger, string subjectArea)
        {
            qpd.MessageQueryName.Identifier.Value = trigger;
            qpd.MessageQueryName.Text.Value = subjectArea;
            qpd.MessageQueryName.NameOfCodingSystem.Value = "HL70471";
            qpd.QueryTag.Value = requestId;

            ST stRequestId = new ST(qbp_q13);
            stRequestId.Value = requestId;
            ((NHapi.Base.Model.Varies)qpd.GetField(3, 0)).Data = stRequestId;

            ST subjectAreaId = new ST(qbp_q13);
            subjectAreaId.Value = subjectArea;
            ((NHapi.Base.Model.Varies)qpd.GetField(4, 0)).Data = subjectAreaId;

            if (fromDate.Year > 1)
            {
                ST stFromDate = new ST(qbp_q13);
                stFromDate.Value = fromDate.ToString("yyyyMMdd");
                ((NHapi.Base.Model.Varies)qpd.GetField(5, 0)).Data = stFromDate;
            }

            if (toDate.Year > 1)
            {
                ST stToDate = new ST(qbp_q13);
                stToDate.Value = toDate.ToString("yyyyMMdd");
                ((NHapi.Base.Model.Varies)qpd.GetField(6, 0)).Data = stToDate;
            }

            if (!String.IsNullOrEmpty(icn))
            {
                ST stIcn = new ST(qbp_q13);
                stIcn.Value = icn;
                ((NHapi.Base.Model.Varies)qpd.GetField(7, 0)).Data = stIcn;
            }

            ST stDfn = new ST(qbp_q13);
            stDfn.Value = "";
            ((NHapi.Base.Model.Varies)qpd.GetField(8, 0)).Data = stDfn;
        }

        void buildRxeAndOrcSegments(OMP_O09_PID omp_o09, IList<string> rxNumbers)
        {
            string nowTimestamp = DateTime.Now.ToString("yyyyMMddhhmmss");

            for (int i = 0; i < rxNumbers.Count; i++)
            {
                RXE rxe = omp_o09.getRxe(i);
                rxe.QuantityTiming.Quantity.Quantity.Value = "1";
                rxe.QuantityTiming.StartDateTime.TimeOfAnEvent.Value = nowTimestamp;
                rxe.GiveCode.Identifier.Value = "RF";
                rxe.GiveCode.NameOfCodingSystem.Value = "HL70119";
                rxe.GiveAmountMinimum.Value = "1";
                rxe.GiveUnits.Identifier.Value = "1 refill unit";
                rxe.PrescriptionNumber.Value = rxNumbers[i];

                ORC orc = omp_o09.getOrc(i);
                orc.PlacerOrderNumber.EntityIdentifier.Value = rxNumbers[i] + "-" + nowTimestamp;
                orc.OrderControl.Value = "RF";
            }
        }


        public Medication[] getOutpatientMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getIvMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getIvMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public Medication[] getUnitDoseMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getUnitDoseMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public Medication[] getOtherMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getOtherMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public Medication[] getAllMeds()
        {
            return getMedsHl7().ToArray<Medication>();
        }

        public Medication[] getAllMeds(string dfn)
        {
            string temp = _cxn.Pid;
            _cxn.Pid = dfn;
            Medication[] meds = getMedsHl7().ToArray<Medication>();
            _cxn.Pid = temp;
            return meds;
        }

        public Medication[] getVaMeds(string dfn)
        {
            throw new NotImplementedException();
        }

        public Medication[] getVaMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getInpatientForOutpatientMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getInpatientForOutpatientMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public string getMedicationDetail(string medId)
        {
            throw new NotImplementedException();
        }

        public string getOutpatientRxProfile()
        {
            throw new NotImplementedException();
        }

        public string getMedsAdminHx(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getMedsAdminLog(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getImmunizations(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

    }
}
