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
using gov.va.medora.mdo.dao.sql.cdw;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using gov.va.medora.utils.mock;

namespace gov.va.medora.mdo.dao.mock
{
    public class XCdwConnection : CdwConnection
    {
        public bool SaveToFile { get; set; }
        public string FileName { get; set; }

        public XCdwConnection(DataSource ds) : base(ds) 
        {
            SaveToFile = false;

            MockXmlSource xml = new MockXmlSource(ds.SiteId.Id);
        }

        public override object query(SqlDataAdapter adapter, AbstractPermission permission = null)
        {
            string request = adapter.SelectCommand.CommandText;
            IDataReader rdr = (IDataReader)base.query(request);

            // copy to datatable
            DataTable table = new DataTable();

            for (int i = 0; i < rdr.FieldCount; i++)
            {
                table.Columns.Add(rdr.GetName(i), rdr.GetFieldType(i));
            }
            while (rdr.Read())
            {
                object[] destination = new object[rdr.FieldCount];
                rdr.GetValues(destination);
                table.Rows.Add(destination);
            }

            if (!String.Equals(System.Reflection.Assembly.GetExecutingAssembly().FullName, "gov.va.medora.mdo-x"))
            {
                throw new ApplicationException("You should only use XCdwConnection from mdo-x dummy");    
            }
            // save to file is set
            if (SaveToFile && !String.IsNullOrEmpty(FileName))
            {
                Stream stream = new FileStream("./../../../mdo/resources/data/" + FileName, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, table);
            }

            MockDataReader newRdr = new MockDataReader();
            newRdr.Table = table;

            return newRdr;
        }
    }
}
