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
    public class Observation
    {
        Author observer;
        Author recorder;
        string timestamp;
        SiteId facility;
        HospitalLocation location;
        ObservationType type;
        string comment;

        public Observation() { }

        public Author Observer
        {
            get { return observer; }
            set { observer = value; }
        }

        public Author Recorder
        {
            get { return recorder; }
            set { recorder = value; }
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public SiteId Facility
        {
            get { return facility; }
            set { facility = value; }
        }

        public HospitalLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        public ObservationType Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
    }
}
