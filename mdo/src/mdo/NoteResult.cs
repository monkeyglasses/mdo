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
    public class NoteResult
    {
        String id;
        int totalPages = 0;
        int lastPageRecd = 0;
        String explanation;

        public NoteResult() { }

        public NoteResult(String id, int totalPages, int lastPageRecd)
        {
            Id = id;
            TotalPages = totalPages;
            LastPageRecd = lastPageRecd;
        }

        public NoteResult(String explanation)
        {
            Explanation = explanation;
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public int TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        public int LastPageRecd
        {
            get { return lastPageRecd; }
            set { lastPageRecd = value; }
        }

        public String Explanation
        {
            get { return explanation; }
            set { explanation = value; }
        }

    }
}
