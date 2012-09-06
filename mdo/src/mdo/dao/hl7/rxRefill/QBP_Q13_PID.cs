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
using NHapi.Base.Parser;

namespace gov.va.medora.mdo.dao.hl7.rxRefill
{
    public class QBP_Q13_PID : QBP_Q13
    {

        public QBP_Q13_PID() : base()
        {
            this.add(typeof(PID), true, false); 
        }

        public PID getPid()
        {
            return (PID)this.GetStructure("PID");
        }

        public string encode()
        {
            NHapi.Base.Parser.EncodingCharacters ec = new NHapi.Base.Parser.EncodingCharacters(HL7Constants.FIELD_SEPARATOR, HL7Constants.DEFAULT_DELIMITER);
            StringBuilder sb = new StringBuilder();
            
            sb.Append(PipeParser.Encode(this.MSH, ec));
            sb.Append(HL7Constants.SEGMENT_SEPARATOR);
            sb.Append(PipeParser.Encode(QPD, ec));
            sb.Append(HL7Constants.SEGMENT_SEPARATOR);
            sb.Append(PipeParser.Encode(getPid(), ec));
            sb.Append(HL7Constants.SEGMENT_SEPARATOR);
            sb.Append(PipeParser.Encode(RDF, ec));
            sb.Append(HL7Constants.SEGMENT_SEPARATOR);
            sb.Append(PipeParser.Encode(RCP, ec));

            return sb.ToString();
        }
    }
}
