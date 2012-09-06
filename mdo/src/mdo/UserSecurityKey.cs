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
    public class UserSecurityKey
    {
        string id;
        string keyId;
        string name;
        string descriptiveName;
        string creatorId;
        string creatorName;
        DateTime creationDate;
        DateTime reviewDate;

        public UserSecurityKey(string id, string keyId, string name, string creatorId, string creatorName, DateTime creationDate)
        {
            Id = id;
            KeyId = keyId;
            Name = name;
            CreatorId = creatorId;
            CreatorName = creatorName;
            CreationDate = creationDate;
        }

        public UserSecurityKey() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string KeyId
        {
            get { return keyId; }
            set { keyId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string DescriptiveName
        {
            get { return descriptiveName; }
            set { descriptiveName = value; }
        }

        public string CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        public string CreatorName
        {
            get { return creatorName; }
            set { creatorName = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public DateTime ReviewDate
        {
            get { return reviewDate; }
            set { reviewDate = value; }
        }
    }
}
