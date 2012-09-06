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
    public class Zipcode
    {
        string code;
        string city;
        string state;
        string stateAbbr;

        public Zipcode(string code, string city, string state, string stateAbbr)
        {
            Code = code;
            City = city;
            State = state;
            StateAbbr = stateAbbr;
        }

        public Zipcode() { }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string StateAbbr
        {
            get { return stateAbbr; }
            set { stateAbbr = value; }
        }
    }
}
