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
    public class LabSpecimen
    {
        string id;
        string name;
        string collectionDate;
        string accessionNum;
        string site;
        SiteId facility;
        string reportDate;

        public LabSpecimen() { }

        public LabSpecimen(string id, string name, string collectionDate, string accessionNum)
        {
            Id = id;
            Name = name;
            CollectionDate = collectionDate;
            AccessionNumber = accessionNum;
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

        public string CollectionDate
        {
            get { return collectionDate; }
            set { collectionDate = value; }
        }

        public string AccessionNumber
        {
            get { return accessionNum; }
            set { accessionNum = value; }
        }

        public string Site
        {
            get { return site; }
            set { site = value; }
        }

        public SiteId Facility
        {
            get { return facility; }
            set { facility = value; }
        }

        public string ReportDate
        {
            get { return reportDate; }
            set { reportDate = value; }
        }
    }
}
