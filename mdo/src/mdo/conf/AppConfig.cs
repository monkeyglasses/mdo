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
using gov.va.medora.mdo.dao.file;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.conf
{
    public class AppConfig
    {
        public Dictionary<string, Dictionary<string, string>> AllConfigs { get; set; }
        public string CdwConnectionString { get; set; }
        public string BseValidatorConnectionString { get; set; }
        public string SqlConnectionString { get; set; }
        public string AdrConnectionString { get; set; }
        public string NptConnectionString { get; set; }
        public string MhvConnectionString { get; set; }
        public string VadirConnectionString { get; set; }
        public string VbaCorpConnectionString { get; set; }
        public string MosConnectionString { get; set; }
        public AbstractSqlConfiguration SqlConfiguration { get; set; }

        /// <summary>
        /// Parameterless constructor. Will use ConfigFileConstants.CONFIG_FILE_NAME as the default file
        /// </summary>
        public AppConfig() 
        {
            readConfigFile(utils.ResourceUtils.ResourcesPath + "conf\\" + ConfigFileConstants.CONFIG_FILE_NAME);
        }

        /// <summary>
        /// Instantiate all config properties via config file
        /// </summary>
        /// <param name="configFilePath">The full path to the configuration file</param>
        public AppConfig(string configFilePath)
        {
            readConfigFile(configFilePath);
        }

        /// <summary>
        /// Use the default resources path and the specified file name
        /// </summary>
        /// <param name="useDefaultResourcesPath">Locate the default resources file path</param>
        /// <param name="fileName">The file name</param>
        public AppConfig(bool useDefaultResourcesPath, string fileName)
        {
            if (useDefaultResourcesPath && !String.IsNullOrEmpty(fileName))
            {
                readConfigFile(utils.ResourceUtils.ResourcesPath + "conf\\" + fileName);
            }
        }

        public void readConfigFile(string configFilePath)
        {
            ConfigFileDao configDao = new ConfigFileDao(configFilePath);
            try
            {
                AllConfigs = configDao.getAllValues();
            }
            catch (System.IO.FileNotFoundException) 
            { 
                return; 
            }

            if (!AllConfigs.ContainsKey(ConfigFileConstants.PRIMARY_CONFIG_SECTION))
            {
                throw new MdoException("Invalid configuration file! Unable to continue...");
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.SQL_CONFIG_SECTION))
            {
                AbstractSqlConfiguration primarySqlConfig = new MsSqlConfiguration(AllConfigs[ConfigFileConstants.SQL_CONFIG_SECTION]);
                SqlConnectionString = primarySqlConfig.buildConnectionString();
                SqlConfiguration = primarySqlConfig;
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.BSE_CONFIG_SECTION))
            {
                AbstractSqlConfiguration bseSqlConfig = new MsSqlConfiguration(AllConfigs[ConfigFileConstants.BSE_CONFIG_SECTION]);
                BseValidatorConnectionString = bseSqlConfig.buildConnectionString();
            }
            else
            {
                BseValidatorConnectionString = SqlConnectionString; // use SQL connection string if separate BSE string isn't found
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.ADR_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.ADR_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new OracleConfiguration
                        (AllConfigs[ConfigFileConstants.ADR_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    AdrConnectionString = config.ConnectionString;
                }
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.MHV_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.MHV_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new OracleConfiguration
                        (AllConfigs[ConfigFileConstants.MHV_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    MhvConnectionString = config.ConnectionString;
                }
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.VADIR_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.VADIR_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new OracleConfiguration
                        (AllConfigs[ConfigFileConstants.VADIR_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    VadirConnectionString = config.ConnectionString;
                }
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.VBA_CORP_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.VBA_CORP_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new OracleConfiguration
                        (AllConfigs[ConfigFileConstants.VBA_CORP_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    VbaCorpConnectionString = config.ConnectionString;
                }
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.NPT_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.NPT_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new MsSqlConfiguration
                        (AllConfigs[ConfigFileConstants.NPT_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    NptConnectionString = config.ConnectionString;
                }
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.CDW_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.CDW_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new MsSqlConfiguration
                        (AllConfigs[ConfigFileConstants.CDW_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    CdwConnectionString = config.ConnectionString;
                }
            }

            if (AllConfigs.ContainsKey(ConfigFileConstants.MOS_CONFIG_SECTION))
            {
                if (AllConfigs[ConfigFileConstants.MOS_CONFIG_SECTION].ContainsKey(ConfigFileConstants.CONNECTION_STRING))
                {
                    AbstractSqlConfiguration config = new OracleConfiguration
                        (AllConfigs[ConfigFileConstants.MOS_CONFIG_SECTION][ConfigFileConstants.CONNECTION_STRING]);
                    MosConnectionString = config.ConnectionString;
                }
            }
        }

    }
}
