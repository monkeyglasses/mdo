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
using System.Text;

namespace gov.va.medora.mdo.dao.vista
{
    public class MockConnectStrategy : IConnectStrategy
    {
        MockConnection _cxn;
        DataSource _dataSource;

        public MockConnectStrategy(AbstractConnection cxn)
        {
            _cxn = (MockConnection)cxn;
            _dataSource = cxn.DataSource;
        }

        #region IConnectStrategy Members

        public void connect()
        {
            if (_dataSource == null)
            {
                throw new ArgumentNullException("No data source");
            }
            string hostname = _dataSource.Provider;
            if (String.IsNullOrEmpty(hostname))
            {
                throw new ArgumentNullException("No provider (hostname)");
            }

            // TODO - authenticate against our MockConnection

            _cxn.IsConnected = true;
        }

        #endregion
    }
}
