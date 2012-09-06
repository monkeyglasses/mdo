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
    public class LabObservation
    {
        LabObservationType type;
        String value;
        String value1;
        String value2;
        String method;
        String qualifier;
        String standardized;
        String device;
        String status;
        DateTime timestamp;

        public LabObservation() {}

        public LabObservationType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public String Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public String Value1
        {
            get
            {
                return value1;
            }
            set
            {
                value1 = value;
            }
        }

        public String Value2
        {
            get
            {
                return value2;
            }
            set
            {
                value2 = value;
            }
        }

        public String Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        public String Qualifier
        {
            get
            {
                return qualifier;
            }
            set
            {
                qualifier = value;
            }
        }

        public String Standardized
        {
            get
            {
                return standardized;
            }
            set
            {
                standardized = value;
            }
        }

        public String Device
        {
            get
            {
                return device;
            }
            set
            {
                device = value;
            }
        }

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
            set
            {
                timestamp = value;
            }
        }
    }
}
