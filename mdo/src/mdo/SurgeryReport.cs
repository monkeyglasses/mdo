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
    public class SurgeryReport : TextReport
    {
        KeyValuePair<string,string> specialty;
        string status;
        string preOpDx;
        string postOpDx;
        string labWork;
        string dictationTimestamp;
        string transcriptionTimestamp;

        public SurgeryReport() { }

        public KeyValuePair<string, string> Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string PreOpDx
        {
            get { return preOpDx; }
            set { preOpDx = value; }
        }

        public string PostOpDx
        {
            get { return postOpDx; }
            set { postOpDx = value; }
        }

        public string LabWork
        {
            get { return labWork; }
            set { labWork = value; }
        }

        public string DictationTimestamp
        {
            get { return dictationTimestamp; }
            set { dictationTimestamp = value; }
        }

        public string TranscriptionTimestamp
        {
            get { return transcriptionTimestamp; }
            set { transcriptionTimestamp = value; }
        }


    }
}
