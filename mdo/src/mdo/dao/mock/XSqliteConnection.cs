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
using System.Data.SQLite;
using gov.va.medora.utils;
using System.IO;

namespace gov.va.medora.mdo.dao.mock
{
    public class XSqliteConnection : AbstractConnection, IDisposable
    {
        public bool SaveResults { get; set; }
        public bool UpdateResults { get; set; }

        SQLiteConnection _cxn;
        DataSource _src;

        public XSqliteConnection(DataSource src)
            : base(src)
        {
            if (src == null || String.IsNullOrEmpty(src.ConnectionString))
            {
                throw new ArgumentNullException("Must supply a connection string");
            }
            _src = src;
            _cxn = new SQLiteConnection(src.ConnectionString);
        }

        public override string getWelcomeMessage()
        {
            return "SQlite says hello";
        }

        public override void connect()
        {
            if (!IsConnected)
            {
                _cxn.Open();
            }
        }

        public override object query(string request, AbstractPermission permission = null)
        {
            string sql = "SELECT OBJECT_TYPE, DOMAIN_OBJECT_SIZE, DOMAIN_OBJECT, QUERY_STRING FROM " +
                _src.SiteId.Id + " WHERE QUERY_STRING_HASH = '" + StringUtils.getMD5Hash(request) + "';";

            SQLiteCommand cmd = new SQLiteCommand(sql, _cxn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                string fullAssemblyName = rdr.GetString(0); // gives us the object type
                Int32 objectSize = rdr.GetInt32(1); // gives us the object size in bytes - should have saved this info to database when mocking data
                byte[] buffer = new byte[objectSize];
                rdr.GetBytes(2, 0, buffer, 0, objectSize);

                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return deserializer.Deserialize(new MemoryStream(buffer));
            }
            else
            {
                throw new exceptions.MdoException(exceptions.MdoExceptionCode.DATA_NO_RECORD_FOR_ID);
            }
        }

        public override object query(SqlQuery request, Delegate functionToInvoke, AbstractPermission permission = null)
        {
            string requestString = "";
            if (request is OracleQuery)
            {
                requestString = ((OracleQuery)request).Command.CommandText + buildParametersString(((OracleQuery)request).Command.Parameters);
            }
            else
            {
                throw new ArgumentException("Only supporting OracleQuery request types. Need to implement others...");
            }

            string sql = "SELECT OBJECT_TYPE, DOMAIN_OBJECT_SIZE, DOMAIN_OBJECT, QUERY_STRING FROM " +
                _src.SiteId.Id + " WHERE QUERY_STRING_HASH = '" + StringUtils.getMD5Hash(requestString) + "';";

            SQLiteCommand cmd = new SQLiteCommand(sql, _cxn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                string fullAssemblyName = rdr.GetString(0); // gives us the object type
                Int32 objectSize = rdr.GetInt32(1); // gives us the object size in bytes - should have saved this info to database when mocking data
                byte[] buffer = new byte[objectSize];
                rdr.GetBytes(2, 0, buffer, 0, objectSize);

                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return deserializer.Deserialize(new MemoryStream(buffer));
            }
            else
            {
                throw new exceptions.MdoException(exceptions.MdoExceptionCode.DATA_NO_RECORD_FOR_ID);
            }
        }

        public override void disconnect()
        {
            IsConnected = false;

            if (IsConnected)
            {
                _cxn.Close();
            }
        }

        public void Dispose()
        {
            if (IsConnected)
            {
                disconnect();
            }
        }

        internal string buildParametersString(Oracle.DataAccess.Client.OracleParameterCollection oracleParams)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Oracle.DataAccess.Client.OracleParameter param in oracleParams)
            {
                if (param.DbType == System.Data.DbType.Binary || param.DbType == System.Data.DbType.Object)
                {
                    sb.Append("BINARY DATA");
                    continue;
                }
                sb.Append(param.Value.ToString());
            }
            return sb.ToString();
        }

        #region Not Implemented Members
        public override ISystemFileHandler SystemFileHandler
        {
            get { throw new NotImplementedException(); }
        }
        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            throw new NotImplementedException();
        }


        public override bool hasPatch(string patchId)
        {
            throw new NotImplementedException();
        }

        public override object query(MdoQuery request, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }

        public override string getServerTimeout()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
