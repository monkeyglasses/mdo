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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo;

namespace gov.va.medora.mdo.dao.vista
{
    public class MockAccount : AbstractAccount
    {
        public MockAccount(AbstractConnection cxn) : base(cxn) { }

        public override string authenticate(AbstractCredentials credentials, DataSource validationDataSource = null)
        {
            if (Cxn == null || !Cxn.IsConnected)
            {
                throw new ConnectionException("Must have connection");
            }
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            else
            {
                throw new ArgumentException("Invalid credentials");
            }
        }

        public override User authorize(AbstractCredentials credentials, AbstractPermission permission)
        {
            throw new NotImplementedException();
        }

        public override User authenticateAndAuthorize(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            throw new NotImplementedException();
        }

        string login(AbstractCredentials credentials)
        {
            throw new NotImplementedException("Login not yet implemented for MockAccount");
        }
    }
}
