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
    public class InpatientStay
    {
        Patient patient;
        HospitalLocation location;
        string admitTimestamp;
        string dischargeTimestamp;
        DischargeDiagnoses dischargeDiagnoses;
        string type;
        Adt[] adts;
        string movementCheckinId;

        public InpatientStay() { }

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        public HospitalLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        public string AdmitTimestamp
        {
            get { return admitTimestamp; }
            set { admitTimestamp = value; }
        }

        public string DischargeTimestamp
        {
            get { return dischargeTimestamp; }
            set { dischargeTimestamp = value; }
        }

        public DischargeDiagnoses DischargeDiagnoses
        {
            get { return dischargeDiagnoses; }
            set { dischargeDiagnoses = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Adt[] Adts
        {
            get { return adts; }
            set { adts = value; }
        }

        public string MovementCheckinId
        {
            get { return movementCheckinId; }
            set { movementCheckinId = value; }
        }
    }
}
