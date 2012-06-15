using System;

namespace gov.va.medora.mdo.conf
{
    public class ConfigFileConstants
    {
        public const string CONFIG_FILE_NAME = "app.conf"; // DO NOT hardcode this anywhere else!!! This will be the one location

        // don't like having this MDWS constant down in MDO - consider refactoring
        public static string PRIMARY_CONFIG_SECTION = "MDWS";
        public static string BSE_CONFIG_SECTION = "BSE";
        public static string SQL_CONFIG_SECTION = "SQL";
        public static string VBA_CORP_CONFIG_SECTION = "VBACORP SQL";
        public static string NPT_CONFIG_SECTION = "NPT SQL";
        public static string VADIR_CONFIG_SECTION = "VADIR SQL";
        public static string MHV_CONFIG_SECTION = "MHV SQL";
        public static string ADR_CONFIG_SECTION = "ADR SQL";
        public static string CDW_CONFIG_SECTION = "CDW SQL";
        public static string MOS_CONFIG_SECTION = "MOS SQL";
        public static string SERVICE_ACCOUNT_CONFIG_SECTION = "Service Account IDs";
        /// <summary>
        /// The SQL connection string
        /// </summary>
        public static string CONNECTION_STRING = "ConnectionString";
        /// <summary>
        /// The SQL database name
        /// </summary>
        public static string SQL_DB = "SqlDatabase";
        /// <summary>
        /// SQL database password
        /// </summary>
        public static string SQL_PASSWORD = "SqlPassword";
        /// <summary>
        /// SQL database path
        /// </summary>
        public static string SQL_HOSTNAME = "SqlHostname";
        /// <summary>
        /// SQL database username
        /// </summary>
        public static string SQL_USERNAME = "SqlUsername";
        /// <summary>
        /// SQL port
        /// </summary>
        public static string SQL_PORT = "SqlPort";
    }
}