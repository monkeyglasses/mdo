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
using System.Text;

namespace gov.va.medora.mdo
{
    public class NoteDefinition
    {
        string id;
        ArrayList localTitles;
        string standardTitle;
        string type;
        string vuid;

        public NoteDefinition() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string[] LocalTitles
        {
            get { return (string[])localTitles.ToArray(typeof(string)); }
            set
            {
                localTitles = new ArrayList();
                for (int i = 0; i < ((string[])value).Length; i++)
                {
                    localTitles.Add(((string[])value)[i]);
                }
            }
        }

        public void addLocalTitle(string localTitle)
        {
            if (localTitles == null)
            {
                localTitles = new ArrayList();
            }
            localTitles.Add(localTitle);
        }

        public string StandardTitle
        {
            get { return standardTitle; }
            set { standardTitle = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Vuid
        {
            get { return vuid; }
            set { vuid = value; }
        }
    }
}
