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
    public class RatedDisability
    {
        string id;
        string name;
        string percent;
        bool serviceConnected;
        string extremityAffected;
        string originalEffectiveDate;
        string currentEffectiveDate;

        public RatedDisability() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Percent
        {
            get { return percent; }
            set { percent = value; }
        }

        public bool ServiceConnected
        {
            get { return serviceConnected; }
            set { serviceConnected = value; }
        }

        public string ExtremityAffected
        {
            get { return extremityAffected; }
            set { extremityAffected = value; }
        }

        public string OriginalEffectiveDate
        {
            get { return originalEffectiveDate; }
            set { originalEffectiveDate = value; }
        }

        public string CurrenEffectiveDate
        {
            get { return currentEffectiveDate; }
            set { currentEffectiveDate = value; }
        }
    }
}
