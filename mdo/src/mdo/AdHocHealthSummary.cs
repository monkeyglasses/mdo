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

#define REFACTORING_2883 // #2883 HealthSummary

using System;
using System.Collections.Generic;
using System.Text;

namespace gov.va.medora.mdo
{
    public class AdHocHealthSummary : HealthSummary
    {
#if !REFACTORING_2883
        string occurrenceLimit;
        string timeLimit;
        string header;
        string segment;
        string file;
        string ifn;
        string zerothNode;

        public AdHocHealthSummary() { }

        public string OccurrenceLimit
        {
            get { return occurrenceLimit; }
            set { occurrenceLimit = value; }
        }

        public string TimeLimit
        {
            get { return timeLimit; }
            set { timeLimit = value; }
        }

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public string Segment
        {
            get { return segment; }
            set { segment = value; }
        }

        public string File
        {
            get { return file; }
            set { file = value; }
        }

        public string Ifn
        {
            get { return ifn; }
            set { ifn = value; }
        }

        public string ZerothNode
        {
            get { return zerothNode; }
            set { zerothNode = value; }
        }
#endif // !REFACTORING
    }
}
