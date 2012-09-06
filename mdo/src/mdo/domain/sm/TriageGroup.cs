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

namespace gov.va.medora.mdo.domain.sm
{
    public class TriageGroup : BaseModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private List<Clinician> _clinicians = new List<Clinician>();

        public List<Clinician> Clinicians
        {
            get { return _clinicians; }
            set { _clinicians = value; }
        }
        private List<Patient> _patients = new List<Patient>();

        public List<Patient> Patients
        {
            get { return _patients; }
            set { _patients = value; }
        }
        private List<TriageRelation> _relations = new List<TriageRelation>();

        public List<TriageRelation> Relations
        {
            get { return _relations; }
            set { _relations = value; }
        }
        private string _vistaDiv;

        public string VistaDiv
        {
            get { return _vistaDiv; }
            set { _vistaDiv = value; }
        }
    }
}
