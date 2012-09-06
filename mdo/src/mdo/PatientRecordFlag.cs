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
    public class PatientRecordFlag
    {
        string id;
        string name;
        string actionId;
        string actionName;
        string actionTimestamp;
        string noteId;

        public PatientRecordFlag() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ActionId
        {
            get { return actionId; }
            set { actionId = value; }
        }

        public string ActionName
        {
            get { return actionName; }
            set { actionName = value; }
        }

        public string ActionTimestamp
        {
            get { return actionTimestamp; }
            set { actionTimestamp = value; }
        }

        public string NoteId
        {
            get { return noteId; }
            set { noteId = value; }
        }
    }
}
