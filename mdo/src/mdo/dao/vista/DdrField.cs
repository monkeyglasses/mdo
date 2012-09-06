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

namespace gov.va.medora.mdo.dao.vista
{
    public class DdrField
    {
        String fmNumber;
        bool fExternal;
        String val;
        String externalVal;

        public DdrField() { }

        public DdrField(String fmNumber, bool fExternal)
        {
            FmNumber = fmNumber;
            HasExternal = fExternal;
        }

        public String FmNumber
        {
            get { return fmNumber; }
            set { fmNumber = value; }
        }

        public bool HasExternal
        {
            get {return fExternal;}
            set {fExternal = value;}
        }

        public String Value
        {
            get { return val; }
            set { val = value; }
        }

        public String ExternalValue
        {
            get { return externalVal; }
            set { externalVal = value; }
        }

    }
}
