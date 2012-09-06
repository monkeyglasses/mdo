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
    public class LabResult
    {
        LabTest test;
        string specimenType;
        string comment;
        string m_value;
        string boundaryStatus;
        string labSiteId;

        public LabResult() { }

        public LabTest Test
        {
            get { return test; }
            set { test = value; }
        }

        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public string BoundaryStatus
        {
            get { return boundaryStatus; }
            set { boundaryStatus = value; }
        }

        public string LabSiteId
        {
            get { return labSiteId; }
            set { labSiteId = value; }
        }

        public string SpecimenType
        {
            get { return specimenType; }
            set { specimenType = value; }
        }
    }
}
