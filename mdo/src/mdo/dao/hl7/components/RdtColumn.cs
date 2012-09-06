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
    public class RdtColumn
    {
        ColumnDescription desc;
        string[] values;

        public RdtColumn() { }

        public RdtColumn(ColumnDescription desc)
        {
            Description = desc;
            Values = null;
        }

        public RdtColumn(ColumnDescription desc, string value)
        {
            Description = desc;
            Values = new string[] { value };
        }

        public RdtColumn(ColumnDescription desc, string[] values)
        {
            Description = desc;
            Values = values;
        }

        public ColumnDescription Description
        {
            get { return desc; }
            set { desc = value; }
        }

        public string[] Values
        {
            get { return values; }
            set { values = value; }
        }

    }
}
