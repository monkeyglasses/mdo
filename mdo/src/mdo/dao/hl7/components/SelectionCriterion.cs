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

namespace gov.va.medora.mdo.dao.hl7.components
{
    public class SelectionCriterion
    {
        EncodingCharacters encChars = new EncodingCharacters();
        string fieldName = "";
        string relationalOp = "";
        string value = "";
        string relationalConjunction = "";

        public SelectionCriterion() { }

        public SelectionCriterion(string fieldName, string op, string val, string conj)
        {
            FieldName = fieldName;
            RelationalOperator = op;
            Value = val;
            RelationalConjunction = conj;
        }

        public EncodingCharacters EncodingChars
        {
            get { return encChars; }
            set { encChars = value; }
        }

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public string RelationalOperator
        {
            get { return relationalOp; }
            set { relationalOp = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public string RelationalConjunction
        {
            get { return relationalConjunction; }
            set { relationalConjunction = value; }
        }

        public string toComponent()
        {
            string result = FieldName +
                EncodingChars.ComponentSeparator + RelationalOperator +
                EncodingChars.ComponentSeparator + Value;
            if (RelationalConjunction != "")
            {
                result += EncodingChars.ComponentSeparator + RelationalConjunction
                    + EncodingChars.RepetitionSeparator;
            }
            return result;
        }
    }
}
