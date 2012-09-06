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
using gov.va.medora.mdo.dao;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.utils;

namespace gov.va.medora.mdo
{
    public class ChemHemReport : LabReport
    {
        ArrayList results;
        Hashtable labSites;

        public ChemHemReport() 
        {
            results = new ArrayList();
        }

        public LabResult[] Results
        {
            get { return (LabResult[])results.ToArray(typeof(LabResult)); }
            set
            {
                LabResult[] val = (LabResult[])value;
                for (int i = 0; i < val.Length; i++)
                {
                    results.Add(val[i]);
                }
            }
        }

        public void AddResult(LabResult r)
        {
            results.Add(r);
        }

        public Hashtable LabSites
        {
            get 
            {
                if (labSites == null)
                {
                    labSites = new Hashtable();
                }
                return labSites; 
            }
            set { labSites = value; }
        }

        const string DAO_NAME = "IChemHemDao";

        // Gets a single report from a single site on the current patient
        public static ChemHemReport getChemHemReport(AbstractConnection cxn, ref string nextDate)
        {
            return getChemHemReport(cxn, cxn.Pid, ref nextDate);
        }

        // Gets a single report from a single site
        public static ChemHemReport getChemHemReport(AbstractConnection cxn, string pid, ref string nextDate)
        {
            return ((IChemHemDao)cxn.getDao(DAO_NAME)).getChemHemReport(pid, ref nextDate);
        }

        // Gets multiple reports from a single site for the current patient
        public static ChemHemReport[] getChemHemReports(AbstractConnection cxn, string fromDate, string toDate)
        {
            return getChemHemReports(cxn, cxn.Pid, fromDate, toDate);
        }

        // Gets multiple reports from a single site
        public static ChemHemReport[] getChemHemReports(AbstractConnection cxn, string pid, string fromDate, string toDate)
        {
            return ((IChemHemDao)cxn.getDao(DAO_NAME)).getChemHemReports(pid, fromDate, toDate);
        }

        // Gets multiple reports from multiple sites
        public static IndexedHashtable getChemHemReports(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getChemHemReports", new object[] { fromDate, toDate, nrpts });
        }

        // Gets multiple reports from multiple sites
        public static IndexedHashtable getChemHemReports(ConnectionSet cxns, string fromDate, string toDate)
        {
            return cxns.query(DAO_NAME, "getChemHemReports", new object[] { fromDate, toDate });
        }
    }
}
