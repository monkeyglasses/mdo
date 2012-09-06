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
    public class MsSqlConfiguration : AbstractSqlConfiguration
    {
        public MsSqlConfiguration() : base() { }

        public MsSqlConfiguration(string connectionString) : base(connectionString) { }

        public MsSqlConfiguration(Dictionary<string, string> settings) : base(settings) { }

        public override string buildConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Data Source=");
            sb.Append(Hostname);
            sb.Append(";Initial Catalog=");
            sb.Append(Database);
            sb.Append(";User Id=");
            sb.Append(Username);
            sb.Append(";Password=");
            sb.Append(Password);
            return sb.ToString();
        }

    }
}
