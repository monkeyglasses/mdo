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
using System.Text;

namespace gov.va.medora.mdo
{
    public class Allergy : Observation
    {
        string allergenId;
        string allergenName;
        string allergenType;
        SnoMedConcept snoMedAllergen;
        SnoMedConcept snoMedReaction;
        string severity;
        List<Symptom> reactions;
        StringDictionary drugIngredients;
        StringDictionary drugClasses;

        public string AllergenId
        {
            get { return allergenId; }
            set { allergenId = value; }
        }

        public string AllergenName
        {
            get { return allergenName; }
            set { allergenName = value; }
        }

        public string AllergenType
        {
            get { return allergenType; }
            set { allergenType = value; }
        }

        public SnoMedConcept SnoMedAllergen
        {
            get { return snoMedAllergen; }
            set { snoMedAllergen = value; }
        }

        public List<Symptom> Reactions
        {
            get { return reactions; }
            set { reactions = value; }
        }

        public StringDictionary DrugIngredients
        {
            get { return drugIngredients; }
            set { drugIngredients = value; }
        }

        public StringDictionary DrugClasses
        {
            get { return drugClasses; }
            set { drugClasses = value; }
        }

        public SnoMedConcept SnoMedReaction
        {
            get { return snoMedReaction; }
            set { snoMedReaction = value; }
        }

        public string Severity
        {
            get { return severity; }
            set { severity = value; }
        }
   
    }
}
