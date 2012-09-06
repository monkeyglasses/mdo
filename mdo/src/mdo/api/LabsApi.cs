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
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdo.api
{
    public class LabsApi
    {
	    string DAO_NAME = "ILabsDao";

        public LabsApi() { }

        public Dictionary<string, HashSet<string>> getUpdatedChemHemReports(AbstractConnection cxn, DateTime fromDate)
        {
            gov.va.medora.mdo.dao.sql.cdw.CdwChemHemDao dao = new dao.sql.cdw.CdwChemHemDao(cxn);
            return dao.getNewChemHemReports(fromDate);
        }

        public IndexedHashtable getCytologyReports(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getCytologyReports", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getSurgicalPathologyReports(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getSurgicalPathologyReports", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getMicrobiologyReports(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getMicrobiologyReports", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getBloodAvailabilityReport(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getBloodAvailabilityReport", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getBloodTransfusionReport(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getBloodTransfusionReport", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getBloodBankReport(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getBloodBankReport", new object[] { });
        }

        public IndexedHashtable getElectronMicroscopyReport(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getElectronMicroscopyReport", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getCytopathologyReport(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getCytopathologyReport", new object[] { });
        }

        public IndexedHashtable getAutopsyReport(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getAutopsyReport", new object[] { });
        }

        public string getLrDfn(AbstractConnection cxn, string pid)
        {
            return ((ILabsDao)cxn.getDao(DAO_NAME)).getLrDfn(pid);
        }

        public IndexedHashtable getTests(ConnectionSet cxns, string target)
        {
            return cxns.query(DAO_NAME, "getTests", new object[] { target });
        }

        public IndexedHashtable getTestDescription(ConnectionSet cxns, string identifierString)
        {
            return cxns.query(DAO_NAME, "getTestDescription", new object[] { identifierString });
        }
    }
}
