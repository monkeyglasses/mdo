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
    public class ObservationType
    {
        string id;
        string category;
        string name;
        string shortName;
        string dataId;
        string dataName;
        string dataType;

        public ObservationType(string id, string category, string name)
        {
            Id = id;
            Category = category;
            Name = name;
        }

        public ObservationType() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string DataId
        {
            get { return dataId; }
            set { dataId = value; }
        }

        public string DataName
        {
            get { return dataName; }
            set { dataName = value; }
        }

        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
    }
}
