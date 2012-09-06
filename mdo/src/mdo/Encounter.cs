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
    public class Encounter
    {
        string id;
        string patientId;
        string providerId;
        string timestamp;
        string locationId;
        string type;
        KeyValuePair<string, string> service;
        string purpose;
        string description;

        public Encounter() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string ProviderId
        {
            get { return providerId; }
            set { providerId = value; }
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public KeyValuePair<string, string> Service
        {
            get { return service; }
            set { service = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
