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

namespace gov.va.medora.mdo
{
    public class Symptom : Observation
    {
        public static string SYMPTOM = "Symptom";

        string id;
        string name;
        bool isNational;
        string vuid;

        public Symptom() { }

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

        public bool IsNational
        {
            get { return isNational; }
            set { isNational = value; }
        }

        public string Vuid
        {
            get { return vuid; }
            set { vuid = value; }
        }
    }
}
