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

namespace gov.va.medora.mdo.conf
{
    public abstract class AbstractSqlConfiguration
    {
        public AbstractSqlConfiguration() { }

        public AbstractSqlConfiguration(string connectionString) 
        {
            this.ConnectionString = connectionString;
        }

        public AbstractSqlConfiguration(Dictionary<string, string> settings)
        {
            if (settings.ContainsKey(ConfigFileConstants.CONNECTION_STRING))
            {
                ConnectionString = settings[ConfigFileConstants.CONNECTION_STRING];
            }
            if (settings.ContainsKey(ConfigFileConstants.SQL_HOSTNAME))
            {
                Hostname = settings[ConfigFileConstants.SQL_HOSTNAME];
            }
            if (settings.ContainsKey(ConfigFileConstants.SQL_DB))
            {
                Database = settings[ConfigFileConstants.SQL_DB];
            }
            if (settings.ContainsKey(ConfigFileConstants.SQL_USERNAME))
            {
                Username = settings[ConfigFileConstants.SQL_USERNAME];
            }
            if (settings.ContainsKey(ConfigFileConstants.SQL_PASSWORD))
            {
                Password = settings[ConfigFileConstants.SQL_PASSWORD];
            }
            if (settings.ContainsKey(ConfigFileConstants.SQL_PORT))
            {
                Int32.TryParse(settings[ConfigFileConstants.SQL_PORT], out _port);
            }
        }

        public abstract string buildConnectionString();
        public string ConnectionString { get; set; }
        public string Hostname { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private Int32 _port;
        public Int32 Port { get { return _port; } set { _port = value; } }
    }
}
