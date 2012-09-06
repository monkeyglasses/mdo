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

namespace gov.va.medora.mdo
{
    public class ReminderReportPatientList
    {
        string reportId;
        string reportName;
        string reportTimestamp;
        List<PatientListEntry> patients;

        public ReminderReportPatientList()
        {
            patients = new List<PatientListEntry>();
        }

        public string ReportId
        {
            get { return reportId; }
            set { reportId = value; }
        }

        public string ReportName
        {
            get { return reportName; }
            set { reportName = value; }
        }

        public string ReportTimestamp
        {
            get { return reportTimestamp; }
            set { reportTimestamp = value; }
        }

        public List<PatientListEntry> Patients
        {
            get { return patients; }
        }

        public void AddPatient(int listId, string name, string pid, string ssn)
        {
            PatientListEntry entry = new PatientListEntry(listId, name, pid, ssn);
            patients.Add(entry);
        }
    }
}
