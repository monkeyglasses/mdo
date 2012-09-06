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
    public class UserOption
    {
        string number;
        string id;
        string name;
        string displayName;
        string key;
        string reverseKey;
        string type;
        bool primaryOption;

        public UserOption() { }

        public UserOption(string number, string id, string name)
        {
            Number = number;
            Id = id;
            Name = name;
            PrimaryOption = false;
        }

        public UserOption(string number, string id, string name, bool primaryOption)
        {
            Number = number;
            Id = id;
            Name = name;
            PrimaryOption = primaryOption;
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
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

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public string ReverseKey
        {
            get { return reverseKey; }
            set { reverseKey = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool PrimaryOption
        {
            get { return primaryOption; }
            set { primaryOption = value; }
        }
    }
}
