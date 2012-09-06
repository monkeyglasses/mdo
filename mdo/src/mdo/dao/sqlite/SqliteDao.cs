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
using System.IO;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.sqlite
{
    public class SqliteDao
    {
        const string TABLE_NAME_PREFIX = "SITE_";
        const string SQLITE_FILE_NAME = "MockMdoData.sqlite";

        bool _cxnIsOpen;
        SQLiteConnection _cxn;

        public SqliteDao()
        {
            _cxn = new SQLiteConnection(getResourcesPath());
        }

        public object getObject(string dataSourceId, string objectHashString)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT ASSEMBLY_NAME, DOMAIN_OBJECT_SIZE, DOMAIN_OBJECT, QUERY_STRING FROM " + TABLE_NAME_PREFIX + dataSourceId +
                " WHERE QUERY_STRING_HASH = '" + objectHashString + "';", _cxn);

            try
            {
                openConnection();

                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    string assemblyName = rdr.GetString(0); // probably don't need this since we know how to cast the object type already 
                    Int32 domainObjectSize = rdr.GetInt32(1);
                    byte[] domainObject = new byte[domainObjectSize];
                    rdr.GetBytes(2, 0, domainObject, 0, domainObjectSize);
                    string dbQueryString = rdr.GetString(3);

                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return bf.Deserialize(new MemoryStream(domainObject));
                }
                else
                {
                    throw new exceptions.MdoException("No record found for that hashcode");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConnection();
            }
        }

        public void updateObject(string dataSourceId, string objectHashString, object queryResults)
        {
            string fullAssemblyName = queryResults.GetType().ToString();
            MemoryStream ms = new MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(ms, queryResults);
            byte[] buffer = new byte[ms.Length];
            buffer = ms.ToArray();
            ms.Dispose();

            SQLiteCommand command = new SQLiteCommand("UPDATE " + TABLE_NAME_PREFIX + dataSourceId + " SET DOMAIN_OBJECT_SIZE = " + buffer.Length +
                ", DOMAIN_OBJECT = @domainObject WHERE QUERY_STRING_HASH = '" + objectHashString + "';", _cxn);

            SQLiteParameter objParam = new SQLiteParameter("@domainObject", System.Data.DbType.Binary);
            objParam.Value = buffer;
            command.Parameters.Add(objParam);

            SQLiteParameter queryStringParam = new SQLiteParameter("@queryString", System.Data.DbType.String);
            queryStringParam.Value = queryResults;
            command.Parameters.Add(queryStringParam);

            try
            {
                openConnection();

                if (command.ExecuteNonQuery() != 1)
                {
                    throw new exceptions.MdoException("Failed to update record: " + objectHashString + " to table " + dataSourceId);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConnection();
            }
        }

        public void saveObject(string dataSourceId, string queryString, object queryStringResults)
        {
            string fullAssemblyName = queryStringResults.GetType().ToString();
            MemoryStream ms = new MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(ms, queryStringResults);
            byte[] buffer = new byte[ms.Length];
            buffer = ms.ToArray();
            ms.Dispose();
            string md5QueryStringHash = StringUtils.getMD5Hash(queryString);

            if (!dataSourceId.StartsWith(TABLE_NAME_PREFIX))
            {
                dataSourceId = TABLE_NAME_PREFIX + dataSourceId;
            }
            SQLiteCommand command = new SQLiteCommand("INSERT INTO " + dataSourceId + " (ASSEMBLY_NAME, DOMAIN_OBJECT_SIZE, DOMAIN_OBJECT, QUERY_STRING, QUERY_STRING_HASH) " +
                "VALUES ('" + fullAssemblyName + "', " + buffer.Length + ", @domainObject, @queryString, '" + md5QueryStringHash + "');", _cxn);

            SQLiteParameter objParam = new SQLiteParameter("@domainObject", System.Data.DbType.Binary);
            objParam.Value = buffer;
            command.Parameters.Add(objParam);

            SQLiteParameter queryStringParam = new SQLiteParameter("@queryString", System.Data.DbType.String);
            queryStringParam.Value = queryString;
            command.Parameters.Add(queryStringParam);

            try
            {
                openConnection();

                if (command.ExecuteNonQuery() != 1)
                {
                    throw new exceptions.MdoException("Failed to save query: " + queryString + " to table " + dataSourceId);
                }
            }
            catch (Exception) 
            {
                throw;
            }
            finally
            {
                closeConnection();
            }
        }

        void openConnection()
        {
            if (!_cxnIsOpen)
            {
                _cxn.Open();
            }
            _cxnIsOpen = true;
        }

        void closeConnection()
        {
            _cxnIsOpen = false;
            _cxn.Close();
        }

        // needs a bit of work... very ugly but it seems to serve it's purpose for running tests froim mdo-x and mdo-test
        string getResourcesPath()
        {
            string executingAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string executingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            UriBuilder uri = new UriBuilder(executingAssemblyPath);
            executingAssemblyPath = Uri.UnescapeDataString(uri.Path); // removes file:\\ at beginning of string
            executingAssemblyPath = Path.GetDirectoryName(executingAssemblyPath);
            if (!executingAssemblyPath.EndsWith("\\"))
            {
                executingAssemblyPath += "\\";
            }
            if (executingAssemblyPath.EndsWith("Debug\\"))
            {
                executingAssemblyPath = executingAssemblyPath.Replace("Debug\\", "");
            }
            if (executingAssemblyPath.EndsWith("Release\\"))
            {
                executingAssemblyPath = executingAssemblyPath.Replace("Release\\", "");
            }

            string replacementString = "\\mdo-test\\resources\\data\\MockMdoData.sqlite";
            string adjustedPath = "";

            adjustedPath = executingAssemblyPath.Replace("\\mdo\\bin\\", replacementString);
            adjustedPath = executingAssemblyPath.Replace("\\mdo-test\\bin\\", replacementString);
            adjustedPath = executingAssemblyPath.Replace("\\mdo-x\\bin\\", replacementString);

            if (!adjustedPath.Contains(replacementString))
            {
                throw new Exception("Unrecognized calling assembly: " + executingAssemblyName);
            }

            return "Data Source=" + adjustedPath;
        }

        public void createTableForSite(string siteId)
        {
            string sqlCreateTable = "CREATE TABLE \"" + TABLE_NAME_PREFIX + siteId + "\" (" +
                "\"ID\" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                "\"ASSEMBLY_NAME\" TEXT NOT NULL, " +
                "\"DOMAIN_OBJECT_SIZE\" INTEGER NOT NULL, " +
                "\"DOMAIN_OBJECT\" BLOB NOT NULL, " +
                "\"QUERY_STRING\" TEXT NOT NULL, " +
                "\"QUERY_STRING_HASH\" TEXT NOT NULL, " +
                "\"LAST_UPDATED\" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP);";

            // index name must share unique site ID name because sqlite doesn't like duplicate index names across tables
            string sqlCreateIndex = "CREATE UNIQUE INDEX IDX_QUERY_STRING_HASH_" + siteId + " ON " + TABLE_NAME_PREFIX + siteId + " (QUERY_STRING_HASH);";

            SQLiteCommand createTableCmd = new SQLiteCommand(sqlCreateTable, _cxn);
            SQLiteCommand createIndexCmd = new SQLiteCommand(sqlCreateIndex, _cxn);

            try
            {
                openConnection();
                createTableCmd.ExecuteNonQuery();
                createIndexCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConnection();
            }
        }
    }
}
