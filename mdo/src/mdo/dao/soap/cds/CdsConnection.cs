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
using gov.va.medora.mdo.dao;
using gov.va.medora.mdo.cds;

namespace gov.va.medora.mdo.dao.soap.cds
{
    public class CdsConnection : AbstractConnection
    {
        internal ClinicalDataServiceSynchronousInterface Proxy { get; set; }

        public CdsConnection(DataSource ds)
            : base(ds)
        {
            Proxy = new ClinicalDataServiceSynchronousInterface();
            if (ds != null && !String.IsNullOrEmpty(ds.Provider))
            {
                Uri uri = null;
                if (Uri.TryCreate(ds.Provider, UriKind.Absolute, out uri))
                {
                    Proxy.Url = ds.Provider;
                }
                else
                {
                    throw new ArgumentException("Invalid URI specified: " + ds.Provider);
                }
            }
        }

        public override ISystemFileHandler SystemFileHandler
        {
            get { throw new NotImplementedException(); }
        }

        public override void connect()
        {
            Proxy.isAlive();
        }

        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            throw new NotImplementedException();
        }

        public override string getWelcomeMessage()
        {
            throw new NotImplementedException();
        }

        public override bool hasPatch(string patchId)
        {
            throw new NotImplementedException();
        }

        public override object query(MdoQuery request, AbstractPermission permission = null)
        {
            throw new NotImplementedException("Must use the DAO directly");
        }

        public override object query(string request, AbstractPermission permission = null)
        {
            throw new NotImplementedException("Must use the DAO directly");
        }

        public override string getServerTimeout()
        {
            throw new NotImplementedException();
        }

        public override void disconnect()
        {
            throw new NotImplementedException();
        }

        public override object query(SqlQuery request, Delegate functionToInvoke, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }
    }
}
