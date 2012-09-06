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

namespace gov.va.medora.mdo
{
    public class DataSource
    {
        string protocol;
        string modality;
        //int timeout;
        int port;
        string provider;
        string status;
        string description;
        string context;
        bool testSource;
        string vendor;
        string version;
        SiteId siteId;
        string connectionString;

        public DataSource() { }

        // Added for DB Connectivity -- originally for UM synchronizer support
        string dbProvider; // formerly "source"
        public string DbProvider
        {
            get { return dbProvider; }
            set { dbProvider = value; }
        }

        string uid;
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }

        public string Modality
        {
            get { return modality; }
            set { modality = value; }
        }

        //public int Timeout
        //{
        //    get { return timeout; }
        //    set { timeout = value; }
        //}

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Context
        {
            get { return context; }
            set { context = value; }
        }

        public bool IsTestSource
        {
            get { return testSource; }
            set { testSource = value; }
        }

        public string Vendor
        {
            get { return vendor; }
            set { vendor = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public SiteId SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
    }
}
