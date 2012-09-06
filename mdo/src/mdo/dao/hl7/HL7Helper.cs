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
using NHapi.Base.Model;
using gov.va.medora.mdo.domain.ccd;
using NHapi.Model.V24.Datatype;

namespace gov.va.medora.mdo.src.mdo.dao.hl7
{
    public static class HL7Helper
    {

        internal static string getString(AbstractSegment segment, int column, int rep)
        {
            NHapi.Base.Model.IType t = segment.GetField(column, rep);

            if (t is Varies)
            {
                return ((Varies)t).Data.ToString();
            }
            else if (t is NHapi.Model.V24.Datatype.TS)
            {
                return ((NHapi.Model.V24.Datatype.TS)t).ToString();
            }
            else if (t is NM)
            {
                return ((NM)t).Value;
            }
            else
            {
                throw new Exception("Unsupported HL7 type: " + t.TypeName);
            }
        }

    }
}
