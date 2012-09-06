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
    public class Appointment
    {
        string id;
        string timestamp;
        string title;
        string status;
        string text;
        SiteId facility;
        HospitalLocation clinic;
        string labDateTime;
        string xrayDateTime;
        string ekgDateTime;
        string purpose;
        string type;
        string currentStatus;

        public Appointment() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public SiteId Facility
        {
            get { return facility; }
            set { facility = value; }
        }

        public HospitalLocation Clinic
        {
            get { return clinic; }
            set { clinic = value; }
        }

        public string LabDateTime
        {
            get { return labDateTime; }
            set { labDateTime = value; }
        }

        public string XrayDateTime
        {
            get { return xrayDateTime; }
            set { xrayDateTime = value; }
        }

        public string EkgDateTime
        {
            get { return ekgDateTime; }
            set { ekgDateTime = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; }
        }
    }
}
