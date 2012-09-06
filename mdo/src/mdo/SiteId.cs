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
    public class SiteId
    {
        string id;
        string name;
        string lastSeenDate;
        string lastEvent;

        public SiteId() 
        {
            Id = "";
            Name = "";
            LastSeenDate = "";
            LastEvent = "";
        }

        public SiteId(string id)
        {
            Id = id;
            Name = "";
            LastSeenDate = "";
            LastEvent = "";
        }

        public SiteId(string id, string name)
        {
            Id = id;
            Name = name;
            LastSeenDate = "";
            LastEvent = "";
        }

        public SiteId(string id, string name, string lastSeenDate)
        {
            Id = id;
            Name = name;
            LastSeenDate = lastSeenDate;
            LastEvent = "";
        }

        public SiteId(string id, string name, string lastSeenDate, string lastEvent)
        {
            Id = id;
            Name = name;
            LastSeenDate = lastSeenDate;
            LastEvent = lastEvent;
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

        public string LastSeenDate
        {
            get { return lastSeenDate; }
            set { lastSeenDate = value; }
        }

        public string LastEvent
        {
            get { return lastEvent; }
            set { lastEvent = value; }
        }

        public static bool operator == (SiteId id1, SiteId id2)
        {
            if (object.ReferenceEquals(id1,null))
            {
                return object.ReferenceEquals(id2,null);
            }
            if (object.ReferenceEquals(id2, null))
            {
                return false;
            }
            if (id1.GetType() != id2.GetType())
            {
                return false;
            }
            if (id1.Id != id2.Id)
            {
                return false;
            }
            return id1.Name == id2.Name;
        }

        public static bool operator != (SiteId id1, SiteId id2)
        {
            return !(id1 == id2);
        }
    }
}
