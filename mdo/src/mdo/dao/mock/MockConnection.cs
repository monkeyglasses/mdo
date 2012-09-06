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
using System.Xml;
using gov.va.medora.utils;
using gov.va.medora.utils.mock;
using gov.va.medora.mdo.dao.vista;
using System.IO;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.dao
{
    public class MockConnection : AbstractConnection
    {
        // Need protocols for API level calls that need to use the abstract 
        // factory to create their DAOs.
        public const string VISTA = "VISTA";
        public const string FHIE = "FHIE";
        public const string RPMS = "RPMS";
        private bool verifyRpc = true;
        private bool updateRpc = false;

        public String OverrideMockFile
        {
            set 
            { 
                string fileId = value + this.DataSource.SiteId.Id;
                setXmlSource(fileId, this.updateRpc);
            }
        }

        public bool VerifyRpc 
        {
            get { return verifyRpc; }

            set { verifyRpc = value; } 
        }

        MockXmlSource xmlSource;
        ISystemFileHandler sysFileHandler;

        public void setXmlSource(string siteId, bool updateRpc) 
        {
            if (null != xmlSource)
            {
                xmlSource = null;
            }
            xmlSource = new MockXmlSource(siteId, updateRpc);
        }
        // Need this constructor to compile, but no tests use it.
        public MockConnection(DataSource dataSource) : base(dataSource) 
        {
            throw new ArgumentException("MockConnection does not use DataSource");
        }

        // This constructor is needed for API level tests.
        public MockConnection(string siteId, string protocol, bool updateRpc = false) : base(null)
        {
            this.DataSource = new DataSource();
            this.DataSource.SiteId = new SiteId(siteId, "Mock");
            this.DataSource.Protocol = protocol;

            //DP 3/24/2011
            //I commented out the line below, the pid should not be set to 
            //"0" as the default, as all of the other subclasses of 
            //AbstractCollection do not set it.  This was causing conflicts 
            //b/w the MockConnection.XML file and the DaoX tests.

            //I will fix the affected tests (14 now fail as the exception 
            //message has changed).

            //this.Pid = "0";

            setXmlSource(siteId, updateRpc);

            this.Account = new VistaAccount(this);
            this.updateRpc = updateRpc;

            AbstractCredentials credentials = new VistaCredentials();
            credentials.AccountName = "AccessCode";
            credentials.AccountPassword = "VerifyCode";
            AbstractPermission permission = new MenuOption(VistaConstants.MDWS_CONTEXT);
            permission.IsPrimary = true;
            this.Account.Permissions.Add(permission.Name, permission);
            //permission = new MenuOption(VistaConstants.DDR_CONTEXT);
            //this.Account.Permissions.Add(permission.Name, permission);
            sysFileHandler = new VistaSystemFileHandler(this);
        }
        public override ISystemFileHandler SystemFileHandler
        {
            get
            {
                if (sysFileHandler == null)
                {
                    sysFileHandler = new VistaSystemFileHandler(this);
                }
                return sysFileHandler;
            }
        }
        public override void connect()
        {
            IsConnected = true;
        }

        //public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission)
        //{
        //    IsConnected = true;
        //    return null;
        //}

        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            IsConnected = true;
            return null;
        }

        public override void disconnect()
        {
            IsConnected = false;
        }

        public override object query(MdoQuery query, AbstractPermission permission = null)
        {
            // hardcoded datetime request
            if (String.Equals(query.RpcName, "ORWU DT") && String.Equals(((VistaQuery.Parameter)query.Parameters[0]).Value, "NOW"))
            {
                return "3010101.120101";
            }

            if (!IsConnected)
            {
                throw new NotConnectedException();
            }

            xmlSource.VerifyRpc = this.VerifyRpc;
            String reply = xmlSource.query(query);
            if (reply.Contains("M  ERROR"))
            {
                throw new MdoException(MdoExceptionCode.VISTA_FAULT, reply);
            }
            return reply;
        }

        public override object query(string request, AbstractPermission permission = null)
        {
            throw new MethodAccessException("This query method was not expected");
        }

        public override string getWelcomeMessage()
        {
            return "Hullo from MockConnection";
        }

        public override bool hasPatch(string patchId)
        {
            return true;
        }

        public override string getServerTimeout()
        {
            return null;
        }

        public override object query(SqlQuery request, Delegate functionToInvoke, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }
    }
}
