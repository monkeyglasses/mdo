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
    public class City
    {
        string name;
        string state;

        ArrayList sites;

        public City() { }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public Site[] Sites
        {
            get { return (Site[])sites.ToArray(typeof(Site)); }
            set
            {
                sites = new ArrayList();
                for (int i = 0; i < ((Site[])value).Length; i++)
                {
                    sites.Add(((Site[])value)[i]);
                }
            }
        }

        public void addSite(Site site)
        {
            sites.Add(site);
        }
    }
}
