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

namespace gov.va.medora.mdo.domain.sm
{
    [Serializable]
    public class Mailbox
    {
        //public Dictionary<Int32, Folder> Folders { get; set; }
        public List<SystemFolder> SystemFolders { get; set; }
        public List<Folder> UserFolders { get; set; }

        public Mailbox()
        {
            setSystemFolders();
        }

        void setSystemFolders()
        {
            Array systemFolderIds = Enum.GetValues(typeof(domain.sm.enums.SystemFolderEnum));
            string[] systemFolderNames = Enum.GetNames(typeof(domain.sm.enums.SystemFolderEnum));

            SystemFolders = new List<SystemFolder>();

            for (int i = 0; i < systemFolderNames.Length; i++)
            {
                SystemFolder folder = new SystemFolder() { Id = (Int32)systemFolderIds.GetValue(i), Name = systemFolderNames[i] };
                SystemFolders.Add(folder);
            }
        }
    }
}
