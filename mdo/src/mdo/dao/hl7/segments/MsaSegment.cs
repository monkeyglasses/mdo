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
    public class MsaSegment
    {
        EncodingCharacters encChars = new EncodingCharacters();
        string ackCode = "";
        string msgCtlId = "";
        string textMessage = "";
        string expectedSequenceNumber = "";
        string delayedAckType = "";
        string errorId = "";
        string errorText = "";

        public MsaSegment() { }
	
	    public MsaSegment(string rawSegmentString) 
        {
		    parse(rawSegmentString);
	    }

        public EncodingCharacters EncodingChars
        {
            get { return encChars; }
            set { encChars = value; }
        }

        public string AckCode
        {
            get { return ackCode; }
            set { ackCode = value; }
        }

        public string MessageControlID
        {
            get { return msgCtlId; }
            set { msgCtlId = value; }
        }

        public string TextMessage
        {
            get { return textMessage; }
            set { textMessage = value; }
        }

        public string ExpectedSequenceNumber
        {
            get { return expectedSequenceNumber; }
            set { expectedSequenceNumber = value; }
        }

        public string DelayedAckType
        {
            get { return delayedAckType; }
            set { delayedAckType = value; }
        }

        public string ErrorID
        {
            get { return errorId; }
            set { errorId = value; }
        }

        public string ErrorText
        {
            get { return errorText; }
            set { errorText = value; }
        }

        public void parse(string rawSegmentString)
        {
            string[] flds = StringUtils.split(rawSegmentString, EncodingChars.FieldSeparator);

            if (flds[0] != "MSA")
            {
                throw new Exception("Invalid MSA segment: incorrect header");
            }

            if (flds[1] == "")
            {
                throw new Exception("Invalid MSA segment: missing acknowledgement code");
            }
            AckCode = flds[1];

            if (flds[2] == "")
            {
                throw new Exception("Invalid MSA segment: missing message control ID");
            }
            MessageControlID = flds[2];

            if (flds.Length > 3)
            {
                TextMessage = flds[3];
            }
            if (flds.Length > 4)
            {
                ExpectedSequenceNumber = flds[4];
            }
            if (flds.Length > 5)
            {
                DelayedAckType = flds[5];
            }
            if (flds.Length > 6)
            {
                string[] components = StringUtils.split(flds[6], EncodingChars.ComponentSeparator);
                ErrorID = components[0];
                ErrorText = components[1];
            }
        }

    }
}
