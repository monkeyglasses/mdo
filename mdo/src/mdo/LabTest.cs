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
    public class LabTest : ObservationType
    {
        string units;
        string lowRef;
        string hiRef;
        string refRange;
        string loinc;

        public LabTest() { }

        public string Units
        {
            get { return units; }
            set { units = value; }
        }

        public string LowRef
        {
            get { return lowRef; }
            set { lowRef = value; }
        }

        public string HiRef
        {
            get { return hiRef; }
            set { hiRef = value; }
        }

        public string RefRange
        {
            get { return refRange; }
            set { refRange = value; }
        }

        public string Loinc
        {
            get { return loinc; }
            set { loinc = value; }
        }
    }
}
