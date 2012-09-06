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
using gov.va.medora.mdo.dao.vista;

namespace gov.va.medora.mdo.dao
{
    public interface IUserDao
    {
        string getUserId(KeyValuePair<string, string> param);
        User[] providerLookup(KeyValuePair<string, string> param);
        User[] userLookup(KeyValuePair<string, string> param);
        User[] userLookup(KeyValuePair<string, string> param, string maxrex);
        User getUser(string uid);
        bool hasPermission(string uid, AbstractPermission permission);
        Dictionary<string, AbstractPermission> getPermissions(PermissionType type, string uid);
        AbstractPermission addPermission(string uid, AbstractPermission permission);
        void removePermission(string uid, AbstractPermission permission);
        bool isValidEsig(string esig);
        bool isUser(string uid);
        OrderedDictionary getUsersWithOption(string optionName);
   }
}
