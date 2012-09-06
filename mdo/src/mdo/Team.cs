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
    public class Team
    {
        string id;
        string name;
        string pcpName;
        string attendingName;

        public Team() {}

        public Team(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Team(string id, string name, string pcpName, string attendingName)
        {
            Id = id;
            Name = name;
            PcpName = pcpName;
            AttendingName = attendingName;
        }

        public string Id
        {
            get {return id;}
            set {id = value;}
        }

        public string Name
        {
            get {return name;}
            set {name = value;}
        }

        public string PcpName
        {
            get { return pcpName; }
            set { pcpName = value; }
        }

        public string AttendingName
        {
            get { return attendingName; }
            set { attendingName = value; }
        }
    }
}
