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

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieUserDao : IUserDao
    {
        VistaUserDao vistaDao = null;

        public FhieUserDao(AbstractConnection cxn)
        {
            vistaDao = new VistaUserDao(cxn);
        }

        public string getUserId(KeyValuePair<string, string> param)
        {
            return vistaDao.getUserId(param);
        }

        public User[] providerLookup(KeyValuePair<string, string> param)
        {
            return vistaDao.providerLookup(param);
        }

        public User[] userLookup(KeyValuePair<string, string> param)
        {
            return vistaDao.userLookup(param);
        }

        public User[] userLookup(KeyValuePair<string, string> param, string maxrex)
        {
            return vistaDao.userLookup(param, maxrex);
        }

        public User getUser(string duz)
        {
            return vistaDao.getUser(duz);
        }

        public bool hasPermission(string duz, AbstractPermission permission)
        {
            return vistaDao.hasPermission(duz,permission);
        }

        public Dictionary<string, AbstractPermission> getPermissions(PermissionType type, string duz)
        {
            return vistaDao.getPermissions(type, duz);
        }

        public AbstractPermission addPermission(string duz, AbstractPermission permission)
        {
            return vistaDao.addPermission(duz, permission);
        }

        public void removePermission(string duz, AbstractPermission permission)
        {
            removePermission(duz,permission);
        }

        public bool isValidEsig(string esig)
        {
            return vistaDao.isValidEsig(esig);
        }

        public bool isUser(string duz)
        {
            return vistaDao.isUser(duz);
        }

        public OrderedDictionary getUsersWithOption(string optionName)
        {
            return null;
        }
    }
}
