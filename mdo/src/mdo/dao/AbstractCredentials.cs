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

namespace gov.va.medora.mdo.dao
{
    public abstract class AbstractCredentials
    {
        string acctName;
        string acctPwd;
        string authToken;
        DataSource authSrc;
        string fedId;
        string locId;
        string subjName;
        string subjPhone;

        string securityPhrase;

        public AbstractCredentials() { }

        public string AccountName
        {
            get { return acctName; }
            set { acctName = value; }
        }

        public string AccountPassword
        {
            get { return acctPwd; }
            set { acctPwd = value; }
        }

        public DataSource AuthenticationSource
        {
            get { return authSrc; }
            set { authSrc = value; }
        }

        public string FederatedUid
        {
            get { return fedId; }
            set { fedId = value; }
        }

        public string LocalUid
        {
            get { return locId; }
            set { locId = value; }
        }

        public string SubjectName
        {
            get { return subjName; }
            set { subjName = value; }
        }

        public string SubjectPhone
        {
            get { return subjPhone; }
            set { subjPhone = value; }
        }

        public string AuthenticationToken
        {
            get { return authToken; }
            set { authToken = value; }
        }

        public string SecurityPhrase
        {
            get { return securityPhrase; }
            set { securityPhrase = value; }
        }

        public abstract bool AreTest
        {
            get;
        }

        public abstract bool Complete
        {
            get;
        }

        public static AbstractCredentials getCredentialsForCxn(AbstractConnection cxn)
        {
            string protocol = cxn.DataSource.Protocol;
            if (protocol == "VISTA" || protocol == "FHIE" || protocol == "RPMS" || protocol == "MOCK" || protocol == "XVISTA")
            {
                return new gov.va.medora.mdo.dao.vista.VistaCredentials();
            }
            if (String.Equals("CDW", protocol, StringComparison.CurrentCultureIgnoreCase))
            {
                return new gov.va.medora.mdo.dao.sql.cdw.CdwCredentials();
            }
            return null;
        }
    }
}
