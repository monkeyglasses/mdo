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
    public class Diagnosis
    {
        string icd9;
        string text;
        bool primary;

        public Diagnosis() { }

        public Diagnosis(string icd0, string text, bool primary)
        {
            Icd9 = icd9;
            Text = text;
            Primary = primary;
        }

        public string Icd9
        {
            get { return icd9; }
            set { icd9 = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool Primary
        {
            get { return primary; }
            set { primary = value; }
        }
    }
}
