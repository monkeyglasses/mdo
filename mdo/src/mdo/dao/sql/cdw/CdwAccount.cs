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
    public class CdwAccount : AbstractAccount
    {
        // TBD - once we get some service accounts, should we verify them somehow?
        
        public override string authenticate(AbstractCredentials credentials, DataSource validationDataSource = null)
        {
            this.isAuthorized = true;
            this.isAuthenticated = true;
            return "OK";
        }

        public override User authorize(AbstractCredentials credentials, AbstractPermission permission)
        {
            this.isAuthorized = true;
            this.isAuthenticated = true;
            return new User();
        }

        public override User authenticateAndAuthorize(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource = null)
        {
            this.isAuthorized = true;
            this.isAuthenticated = true;
            return new User();
        }
    }
}
