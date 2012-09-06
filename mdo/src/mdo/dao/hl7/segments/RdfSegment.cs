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
using System.Collections;
using gov.va.medora.utils;
using gov.va.medora.mdo.dao.hl7.components;

namespace gov.va.medora.mdo.dao.hl7.segments
{
    public class RdfSegment
    {
        EncodingCharacters encChars = new EncodingCharacters();
        int ncols = 0;
        RdfColumn[] cols;

        public RdfSegment() { }

        public RdfSegment(string rawSeg)
        {
            parse(rawSeg);
        }

        public EncodingCharacters EncodingChars
        {
            get { return encChars; }
            set { encChars = value; }
        }

        public int NColumns
        {
            get { return ncols; }
            set { ncols = value; }
        }

        public RdfColumn[] Columns
        {
            get { return cols; }
            set { cols = value; }
        }

        void parse(string rawSeg)
        {
            string[] flds = StringUtils.split(rawSeg, EncodingChars.FieldSeparator);

            if (flds.Length < 3)
            {
                throw new Exception("Invalid RDF segment: less than 3 fields");
            }

            if (flds[0] != "RDF")
            {
                throw new Exception("Invalid RDF segment: incorrect header");
            }

            string[] rawColumns = StringUtils.split(flds[2], EncodingChars.RepetitionSeparator);
            Columns = new RdfColumn[rawColumns.Length];

            for (int i = 0; i < rawColumns.Length; i++)
            {
                Columns[i] = parseColumn(rawColumns[i]);
            }
        }

	    RdfColumn parseColumn(string rawColumn)
	    {
            string[] subcomponents = StringUtils.split(rawColumn, EncodingChars.ComponentSeparator);
		    if (subcomponents.Length < 3 || subcomponents[0] == "")
		    {
			    throw new Exception("Invalid RDF column: " + rawColumn);
		    }
            int fldLth = Convert.ToInt16(subcomponents[2]);
		    return new RdfColumn(new ColumnDescription(subcomponents[0],subcomponents[1],fldLth));
	    }

        public string toSegment()
        {
            string result = "RDF" +
                EncodingChars.FieldSeparator + NColumns + EncodingChars.FieldSeparator;
            for (int i = 0; i < Columns.Length-1; i++)
            {
                result += Columns[i].Description.toComponent() + EncodingChars.RepetitionSeparator;
            }
            result += Columns[Columns.Length - 1].Description.toComponent();
            return result + '\r';
        }
    }
}
