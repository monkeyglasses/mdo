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
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.hl7.segments
{
    public class QakSegment
    {
        EncodingCharacters encChars = new EncodingCharacters();
        string qryTag = "";
        string qryResponseStatus = "";

        public QakSegment() { }

        public QakSegment(string rawSegmentString) 
        {
		    parse(rawSegmentString);
	    }

        public EncodingCharacters EncodingChars
        {
            get { return encChars; }
            set { encChars = value; }
        }

        public string QueryTag
        {
            get { return qryTag; }
            set { qryTag = value; }
        }

        public string QueryResponseStatus
        {
            get { return qryResponseStatus; }
            set { qryResponseStatus = value; }
        }

        public void parse(string rawSegmentString)
	    {
            string[] flds = StringUtils.split(rawSegmentString, EncodingChars.FieldSeparator);

            if (flds.Length < 3)
            {
                throw new Exception("Invalid QAK segment: less than 3 fields");
            }

            if (flds[0] != "QAK")
		    {
			    throw new Exception("Invalid QAK segment: incorrect header");
		    }
    		
		    QueryTag = flds[1];
		    QueryResponseStatus = flds[2].Trim();
	    }
	
    }
}
