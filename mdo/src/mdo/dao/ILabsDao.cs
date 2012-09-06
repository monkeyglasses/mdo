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
using System.Collections.Specialized;
using System.Text;
using System.Collections.Generic;

namespace gov.va.medora.mdo.dao
{
    public interface ILabsDao
    {
       // CytologyReport[] getCytologyReports(string fromDate, string toDate, int nrpts);
       // CytologyReport[] getCytologyReports(string pid, string fromDate, string toDate, int nrpts);
        string getAllLabReports(string fromDate, string toDate, int nrpts);
        string getAllLabReports(string pid, string fromDate, string toDate, int nrpts);
        SurgicalPathologyReport[] getSurgicalPathologyReports(string fromDate, string toDate, int nrpts);
        SurgicalPathologyReport[] getSurgicalPathologyReports(string pid, string fromDate, string toDate, int nrpts);
        MicrobiologyReport[] getMicrobiologyReports(string fromDate, string toDate, int nrpts);
        MicrobiologyReport[] getMicrobiologyReports(string pid, string fromDate, string toDate, int nrpts);
        string getBloodAvailabilityReport(string fromDate, string toDate, int nrpts);
        string getBloodAvailabilityReport(string pid, string fromDate, string toDate, int nrpts);
        string getBloodTransfusionReport(string fromDate, string toDate, int nrpts);
        string getBloodTransfusionReport(string pid, string fromDate, string toDate, int nrpts);
        string getBloodBankReport();
        string getBloodBankReport(string pid);
        string getElectronMicroscopyReport(string fromDate, string toDate, int nrpts);
        string getElectronMicroscopyReport(string pid, string fromDate, string toDate, int nrpts);
        string getCytopathologyReport();
        string getCytopathologyReport(string pid);
        string getAutopsyReport();
        string getAutopsyReport(string pid);
        string getLrDfn(string pid);
        IList<LabTest> getTests(string target);
        string getTestDescription(string identifierString);
    }
}
