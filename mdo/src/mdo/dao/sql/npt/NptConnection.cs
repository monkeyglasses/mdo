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
using System.Data.SqlClient;
using System.Configuration;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.sql.npt
{
    public class NptConnection : AbstractConnection
    {
        string _cxnString;
        SqlConnection _myCxn;
        SqlParameterCollection _params;

        /// <summary>
        /// Setter and Getter for SQL Parameterized queries. The query(string) function will use these, if available.
        /// </summary>
        public SqlParameterCollection SqlParameters
        {
            get { return _params; }
            set { _params = value; }
        }

        public NptConnection(DataSource src) : base(src)
        {
            _cxnString = src.ConnectionString;
        }

        public override ISystemFileHandler SystemFileHandler
        {
            get { throw new NotImplementedException(); }
        }

        public override void connect()
        {
            _myCxn = new SqlConnection(_cxnString);
            _myCxn.Open();
            IsConnected = true;
        }

        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource = null)
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

        public override string getServerTimeout()
        {
            return null;
        }

        public override object query(MdoQuery request, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }

        public override object query(string request, AbstractPermission permission = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _myCxn;
            cmd.CommandText = request;
            if (_params != null && _params.Count > 0)
            {
                int count = _params.Count;
                for (int i = 0; i < count; i++)
                {
                    SqlParameter temp = _params[0];
                    _params.RemoveAt(0);
                    cmd.Parameters.Add(temp);
                }
            }
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }

        public override void disconnect()
        {
            IsConnected = false;
            if (_myCxn != null)
            {
                _myCxn.Dispose();
            }
        }

        public override object query(SqlQuery request, Delegate functionToInvoke, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }
    }
}
