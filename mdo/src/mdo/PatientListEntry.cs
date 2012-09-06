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
using System.Text;

namespace gov.va.medora.mdo
{
    public class PatientListEntry
    {
        int listId;
        string patientName;
        string patientId;
        string ssn;

        public PatientListEntry() { }

        public PatientListEntry(int listId, string patientName, string pid, string ssn)
        {
            ListId = listId;
            PatientName = patientName;
            PatientId = pid;
            SSN = ssn;
        }

        public int ListId
        {
            get { return listId; }
            set { listId = value; }
        }

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string SSN
        {
            get { return ssn; }
            set { ssn = value; }
        }
    }
}
