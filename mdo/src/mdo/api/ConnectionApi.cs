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
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdo.api
{
    public class ConnectionApi
    {
        DataSource src;
        Connection cxn;
        MultiSourceQuery msq;
        ConnectionManager mgr;

        public ConnectionApi() { }

        public ConnectionApi(DataSource src)
        {
            this.src = src;
            setConnection();
        }

        public ConnectionApi(DataSource[] sources)
        {
            MultiSourceQuery = new MultiSourceQuery(sources);
        }

        public Connection MdoConnection
        {
            get { return cxn; }
        }

        private void setConnection()
        {
            DaoFactory df = DaoFactory.getDaoFactory(DaoFactory.getConstant(src.Protocol));
            this.cxn = df.getConnection(src);
        }

        public MultiSourceQuery MultiSourceQuery
        {
            get { return msq; }
            set { msq = value; }
        }

        public ConnectionManager CxnMgr
        {
            get { return mgr; }
            set { mgr = value; }
        }

        public void connect()
        {
            cxn.connect();
        }

        public void disconnect()
        {
            cxn.disconnect();
        }

        public bool IsConnected
        {
            get { return cxn.IsConnected; }
            set { cxn.IsConnected = value; }
        }

        public String WelcomeMessage
        {
            get { return cxn.getWelcomeMessage(); }
        }

        public StringDictionary getPatientTypes(Connection cxn)
        {
            return cxn.getPatientTypes();
        }

        public StringDictionary getPatientTypes()
        {
            return cxn.getPatientTypes();
        }

        public DateTime getTimestamp()
        {
            return cxn.getTimestamp();
        }

        public IndexedHashtable getTimestamp(MultiSourceQuery msq)
        {
            return msq.execute("Connection", "getTimestamp", new object[] { });
        }

    }
}
