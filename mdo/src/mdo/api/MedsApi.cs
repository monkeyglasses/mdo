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
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdo.api
{
    public class MedsApi
    {
        string DAO_NAME = "IPharmacyDao";
    	
	    public MedsApi() {}

        public IndexedHashtable getOutpatientMeds(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getOutpatientMeds", new object[] { });
        }

        public IndexedHashtable getIvMeds(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getIvMeds", new object[] { });
        }

        public IndexedHashtable getUnitDoseMeds(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getUnitDoseMeds", new object[] { });
        }

        public IndexedHashtable getOtherMeds(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getOtherMeds", new object[] { });
        }

        public IndexedHashtable getAllMeds(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getAllMeds", new object[] { });
        }

        public IndexedHashtable getVaMeds(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getVaMeds", new object[] { });
        }

        public string getMedicationDetail(AbstractConnection cxn, string medId)
        {
            return ((IPharmacyDao)cxn.getDao(DAO_NAME)).getMedicationDetail(medId);
        }

        public IndexedHashtable getOutpatientRxProfile(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getOutpatientRxProfile", new object[] { });
        }

        public IndexedHashtable getMedsAdminHx(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getMedsAdminHx", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getMedsAdminLog(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getMedsAdminLog", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getImmunizations(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getImmunizations", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getDiscontinueReasons(ConnectionSet cxns) 
        { 
            return cxns.query(DAO_NAME, "getDiscontinueReasons", new object[] { }); 
        }

        public Medication refillPrescription(AbstractConnection cxn, string rxId)
        {
            return ((IPharmacyDao)cxn.getDao(DAO_NAME)).refillPrescription(rxId);
        }
    }
}
