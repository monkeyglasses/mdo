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
    public class Specialty
    {
        string id;
        string name;
        string displayName;
        string service;
        string ptfCode;

        public Specialty() { }

        public Specialty(string[] values)
        {
            Id = values[0];
            Name = values[1];
            DisplayName = values[2];
            Service = values[3];
            PtfCode = values[4];
        }

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

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string Service
        {
            get { return service; }
            set { service = value; }
        }

        public string PtfCode
        {
            get { return ptfCode; }
            set { ptfCode = value; }
        }
    }
}
