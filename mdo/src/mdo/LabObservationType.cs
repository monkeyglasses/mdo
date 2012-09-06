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
    public class LabObservationType
    {
        String id;
        String title;
        String units;
        String range;

        public LabObservationType(String id, String title, String units, String range)
        {
            Id = id;
            Title = title;
            Units = units;
            Range = range;
        }

        public LabObservationType(String id, String title)
        {
            Id = id;
            Title = title;
        }

        public LabObservationType() { }

        public String Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public String Units
        {
            get
            {
                return units;
            }
            set
            {
                units = value;
            }
        }

        public String Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }

    }
}
