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
using System.Linq;
using System.Text;
using NHapi.Model.V24.Message;
using NHapi.Model.V24.Segment;

namespace gov.va.medora.mdo.dao.hl7.rxRefill
{
    public class ORP_O10_ORCRXE : ORP_O10
    {

        public ORP_O10_ORCRXE() : base()
        {
            this.add(typeof(ORC), true, true);
            this.add(typeof(RXE), true, true);
        }

        public ORC getOrc(int rep)
        {
            return (ORC)this.GetStructure("ORC", rep);
        }

        public RXE getRxe(int rep)
        {
            return (RXE)this.GetStructure("RXE", rep);
        }

    }
}
