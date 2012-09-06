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
    public class QuestionnaireQuestion
    {
        string number;
        string text;
        string val;
        string branchFrom;
        string ifCondition;
        List<KeyValuePair<string, string>> choices;

        public string Number
        {
            get{return number;}
            set{number=value;}
        }

        public string Text
        {
            get{return text;}
            set{text=value;}
        }

        public string Value
        {
            get{return val;}
            set{val = value;}
        }

        public string BranchFromQuestion
        {
            get { return branchFrom; }
            set { branchFrom = value; }
        }

        public string BranchCondition
        {
            get { return ifCondition; }
            set { ifCondition = value; }
        }

        public List<KeyValuePair<string, string>> Choices
        {
            get { return choices; }
            set { choices = value; }
        }
    }
}
