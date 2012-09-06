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
using System.Collections;
using System.Text;

namespace gov.va.medora.mdo.dao
{
    public abstract class AbstractAccount
    {
        string id;
        AbstractConnection cxn;
        Dictionary<string, AbstractPermission> permissions = new Dictionary<string, AbstractPermission>();
        protected bool isAuthenticated;
        protected bool isAuthorized;
        string authMethod;

        public AbstractAccount() { }

        public AbstractAccount(AbstractConnection cxn)
        {
            this.cxn = cxn;
            isAuthenticated = false;
            isAuthorized = false;
        }

        public string AccountId
        {
            get { return id; }
            set { id = value; }
        }

        public AbstractConnection Cxn
        {
            get { return cxn; }
            set { cxn = value; }
        }

        public Dictionary<string, AbstractPermission> Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { isAuthenticated = value; }
        }

        public bool IsAuthorized
        {
            get { return isAuthorized; }
        }

        public string AuthenticationMethod
        {
            get { return authMethod; }
            set { authMethod = value; }
        }

        public AbstractPermission PrimaryPermission
        {
            get
            {
                if (permissions == null || permissions.Count == 0)
                {
                    return null;
                }
                if (permissions.Count == 1)
                {
                    string[] key = new string[1];
                    permissions.Keys.CopyTo(key, 0);
                    return (AbstractPermission)permissions[key[0]];
                }
                foreach (KeyValuePair<string, AbstractPermission> kvp in permissions)
                {
                    AbstractPermission p = (AbstractPermission)kvp.Value;
                    if (p.IsPrimary)
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        public abstract string authenticate(AbstractCredentials credentials, DataSource validationDataSource = null);
        public abstract User authorize(AbstractCredentials credentials, AbstractPermission permission);
        public abstract User authenticateAndAuthorize(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource = null);
    }
}
