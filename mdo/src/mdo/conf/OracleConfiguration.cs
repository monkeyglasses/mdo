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
    public class OracleConfiguration : AbstractSqlConfiguration
    {
        public OracleConfiguration(string connectionString) : base(connectionString) { }

        public OracleConfiguration(Dictionary<string, string> settings) : base(settings) 
        {
            /* Need to add the rest of the properties after base(settings) is invoked */
        }

        public override string buildConnectionString()
        {
            throw new NotImplementedException("Not yet implemented");
            /* This isn't right - needs to be built better
             * 
            StringBuilder sb = new StringBuilder();
            sb.Append("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=");
            sb.Append(Hostname);
            sb.Append(")(PORT=");
            sb.Append(Port.ToString());
            sb.Append("))(CONNECT_DATA=SERVICE_NAME=");
            sb.Append(ServiceName);
            sb.Append(")));User ID=");
            sb.Append(Username);
            sb.Append(";Password=");
            sb.Append(Password);
            sb.Append(";");
            return sb.ToString();
             */
        }

        public string ServiceName { get; set; }
    }
}
