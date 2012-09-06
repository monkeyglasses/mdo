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
using gov.va.medora.mdo.dao.hl7.segments;

namespace gov.va.medora.mdo.dao.hl7.mpi.messages
{
    public class PatientMatchesRequest
    {
        EncodingCharacters encChars;
        MshSegment msh;
        VtqSegment vtq;
        RdfSegment rdf;

        public PatientMatchesRequest() 
        {
            MSH = new MshSegment();
            VTQ = new VtqSegment();
            RDF = new RdfSegment();
        }

        public EncodingCharacters EncodingChars
        {
            get { return encChars; }
            set { encChars = value; }
        }

        public MshSegment MSH
        {
            get { return msh; }
            set { msh = value; }
        }

        public VtqSegment VTQ
        {
            get { return vtq; }
            set { vtq = value; }
        }

        public RdfSegment RDF
        {
            get { return rdf; }
            set { rdf = value; }
        }

        public string toMessage()
        {
            string result = MSH.toSegment() + VTQ.toSegment() + RDF.toSegment();
            return result.Substring(0, result.Length - 1);  //Peel off last \r
        }
    }
}
