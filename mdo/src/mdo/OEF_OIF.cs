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
    public class OEF_OIF
    {
        string location;
        DateTime fromDate;
        DateTime toDate;
        bool dataLocked;
        DateTime recordedDate;
        KeyValuePair<string, string> recordingSite;

        public OEF_OIF() { }

        public String Location
        {
            get { return location; }
            set { location = value; }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        public bool DataLocked
        {
            get { return dataLocked; }
            set { dataLocked = value; }
        }

        public DateTime RecordedDate
        {
            get { return recordedDate; }
            set { recordedDate = value; }
        }

        public KeyValuePair<string, string> RecordingSite
        {
            get { return recordingSite; }
            set { recordingSite = value; }
        }
    }
}
