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
using gov.va.medora.mdo.dao.xml.questionnaire;

namespace gov.va.medora.mdo
{
    public class QuestionnaireSet
    {
        string name;
        string title;
        string description;
        List<QuestionnaireQuestion> questions;
        List<Questionnaire> questionnaires;

        public QuestionnaireSet() { }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public List<QuestionnaireQuestion> Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public List<Questionnaire> Questionnaires
        {
            get { return questionnaires; }
            set { questionnaires = value; }
        }

        public static QuestionnaireSet getSet(string name)
        {
            QuestionnaireDao dao = new QuestionnaireDao();
            return dao.getSet(name);
        }
    }
}
