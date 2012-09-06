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
    public class Service
    {
        string id;
        string name;
        string abbr;
        User chief;
        Service parent;
        string location;
        string mailSymbol;
        string type;

        public Service() { }

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

        public string Abbreviation
        {
            get { return abbr; }
            set { abbr = value; }
        }

        public User Chief
        {
            get { return chief; }
            set { chief = value; }
        }

        public Service ParentService
        {
            get { return parent; }
            set { parent = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string MailSymbol
        {
            get { return mailSymbol; }
            set { mailSymbol = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
