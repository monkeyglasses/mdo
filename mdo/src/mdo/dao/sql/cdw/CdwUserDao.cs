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

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwUserDao : IUserDao
    {

        public string getUserId(KeyValuePair<string, string> param)
        {
            throw new NotImplementedException();
        }

        public User[] providerLookup(KeyValuePair<string, string> param)
        {
            throw new NotImplementedException();
        }

        public User[] userLookup(KeyValuePair<string, string> param)
        {
            throw new NotImplementedException();
        }

        public User[] userLookup(KeyValuePair<string, string> param, string maxrex)
        {
            throw new NotImplementedException();
        }

        public User getUser(string uid)
        {
            throw new NotImplementedException();
        }

        public bool hasPermission(string uid, AbstractPermission permission)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, AbstractPermission> getPermissions(PermissionType type, string uid)
        {
            throw new NotImplementedException();
        }

        public AbstractPermission addPermission(string uid, AbstractPermission permission)
        {
            throw new NotImplementedException();
        }

        public void removePermission(string uid, AbstractPermission permission)
        {
            throw new NotImplementedException();
        }

        public bool isValidEsig(string esig)
        {
            throw new NotImplementedException();
        }

        public bool isUser(string uid)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Specialized.OrderedDictionary getUsersWithOption(string optionName)
        {
            throw new NotImplementedException();
        }
    }
}
