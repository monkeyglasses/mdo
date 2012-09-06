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
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace gov.va.medora.mdo
{
    public class MentalHealthInstrumentResultSet
    {
        string id;
        string administrationId;
        KeyValuePair<string, string> scale;
        string rawScore;
        StringDictionary transformedScores = new StringDictionary();
        KeyValuePair<string, string> instrument;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string AdministrationId
        {
            get { return administrationId; }
            set { administrationId = value; }
        }

        public KeyValuePair<string, string> Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public string RawScore
        {
            get { return rawScore; }
            set { rawScore = value; }
        }

        public StringDictionary TransformedScores
        {
            get { return transformedScores; }
            set { transformedScores = value; }
        }

        public KeyValuePair<string, string> Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }
    }
}
