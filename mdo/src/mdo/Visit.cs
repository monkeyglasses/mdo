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
    public class Visit
    {
        string id;
        string type;
        Patient patient;
        User attending;
        User provider;
        string service;
        HospitalLocation location;
        string patientType;
        string visitId;
        string timestamp;
        string status;

        public Visit() {}

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        public User Attending
        {
            get
            {
                return attending;
            }
            set
            {
                attending = value;
            }
        }

        public User Provider
        {
            get
            {
                return provider;
            }
            set
            {
                provider = value;
            }
        }

        public string Service
        {
            get
            {
                return service;
            }
            set
            {
                service = value;
            }
        }

        public HospitalLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        public string PatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                patientType = value;
            }
        }

        public string VisitId
        {
            get
            {
                return visitId;
            }
            set
            {
                visitId = value;
            }
        }

        public string Timestamp
        {
            get
            {
                return timestamp;
            }
            set
            {
                timestamp = value;
            }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
