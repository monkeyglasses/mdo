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
    public class OrderType
    {
        string id;
        string name1;
        string name2;

        public OrderType() { }

        public OrderType(string id, string name1, string name2)
        {
            Id = id;
            Name1 = name1;
            Name2 = name2;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name1
        {
            get { return name1; }
            set { name1 = value; }
        }


        public string Name2
        {
            get { return name2; }
            set { name2 = value; }
        }

    }
}
