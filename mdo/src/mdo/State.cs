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
using System.Xml;

namespace gov.va.medora.mdo
{
    public class State
    {
        string fips;
        string name;
        string abbr;
        SortedList sites;
        SortedList cities;
        string[] visnIds;

        public static string[] abbrs = new string[]
            {
                "AL","AK","AS","AZ","AR","CA","CO","CT","DE","DC","FM","FL",
                "GA","GU","HI","ID","IL","IN","IA","KS","KY","LA","ME","MH",
                "MD","MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ","NM",
                "NY","NC","ND","MP","OH","OK","OR","PW","PA","PR","RI","SC",
                "SD","TN","TX","UT","VT","VI","VA","WA","WV","WI","WY"
            };

        public State() { }

        public State(string name, string abbr)
        {
            Name = name;
            Abbr = abbr;
        }

        public State(string name, string abbr, string fips)
        {
            Name = name;
            Abbr = abbr;
            Fips = fips;
        }

        public string Fips
        {
            get { return fips; }
            set { fips = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Abbr
        {
            get { return abbr; }
            set { abbr = value; }
        }

        public SortedList Sites
        {
            get { return sites; }
            set { sites = value; }
        }

        public SortedList Cities
        {
            get { return cities; }
            set { cities = value; }
        }

        public static bool isValidAbbr(string abbr)
        {
            if (String.IsNullOrEmpty(abbr))
                return false;

            abbr = abbr.ToUpper();
            for (int i = 0; i < abbrs.Length; i++)
            {
                if (abbrs[i] == abbr)
                {
                    return true;
                }
            }
            return false;
        }

        public string[] VisnIds
        {
            get { return visnIds; }
            set { visnIds = value; }
        }
    }
}
