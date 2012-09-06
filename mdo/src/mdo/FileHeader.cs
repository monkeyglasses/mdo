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
using System.Collections;
using System.Text;

namespace gov.va.medora.mdo
{
    public class FileHeader
    {
        string name;
        string alternateName;
        string latestId;
        Int64 nrex;
        ArrayList characteristics;

        public FileHeader() { }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string AlternateName
        {
            get { return alternateName; }
            set { alternateName = value; }
        }

        public string LatestId
        {
            get { return latestId; }
            set { latestId = value; }
        }

        public long NumberOfRecords
        {
            get { return nrex; }
            set { nrex = value; }
        }

        public ArrayList Characteristics
        {
            get { return characteristics; }
            set { characteristics = value; }
        }
    }
}
