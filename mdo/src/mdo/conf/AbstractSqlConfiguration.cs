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
