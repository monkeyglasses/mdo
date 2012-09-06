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

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieLabsDao : ILabsDao
    {
        VistaLabsDao vistaDao = null;
       
        public FhieLabsDao(AbstractConnection cxn)
        {
            vistaDao = new VistaLabsDao(cxn);
        }

        public CytologyReport[] getCytologyReports(string fromDate, string toDate, int nrpts)
        {
            return vistaDao.getCytologyReports(fromDate, toDate, nrpts);
        }

        public CytologyReport[] getCytologyReports(string pid, string fromDate, string toDate, int nrpts)
        {
            return vistaDao.getCytologyReports(pid, fromDate, toDate, nrpts);
        }

        public SurgicalPathologyReport[] getSurgicalPathologyReports(string fromDate, string toDate, int nrpts)
        {
            return vistaDao.getSurgicalPathologyReports(fromDate, toDate, nrpts);
        }

        public SurgicalPathologyReport[] getSurgicalPathologyReports(string pid, string fromDate, string toDate, int nrpts)
        {
            return vistaDao.getSurgicalPathologyReports(pid, fromDate, toDate, nrpts);
        }

        public MicrobiologyReport[] getMicrobiologyReports(string fromDate, string toDate, int nrpts)
        {
            return vistaDao.getMicrobiologyReports(fromDate, toDate, nrpts);
        }

        public MicrobiologyReport[] getMicrobiologyReports(string pid, string fromDate, string toDate, int nrpts)
        {
            return vistaDao.getMicrobiologyReports(pid, fromDate, toDate, nrpts);
        }

        public string getBloodAvailabilityReport(string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getBloodAvailabilityReport(string pid, string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getBloodTransfusionReport(string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getBloodTransfusionReport(string pid, string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getBloodBankReport()
        {
            return null;
        }

        public string getBloodBankReport(string pid)
        {
            return null;
        }

        public string getElectronMicroscopyReport(string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getElectronMicroscopyReport(string pid, string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getCytopathologyReport()
        {
            return null;
        }

        public string getCytopathologyReport(string pid)
        {
            return null;
        }

        public string getAutopsyReport()
        {
            return null;
        }

        public string getAutopsyReport(string pid)
        {
            return null;
        }

        public string getLrDfn(string pid)
        {
            return null;
        }

        public string getAllLabReports(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getAllLabReports(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }


        public IList<LabTest> getTests(string target)
        {
            throw new NotImplementedException();
        }

        public string getTestDescription(string identifierString)
        {
            throw new NotImplementedException();
        }
    }
}
