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

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    public static class QueryUtils
    {
        internal static bool columnExists(string columnName, System.Data.IDataReader rdr)
        {
            if (String.IsNullOrEmpty(columnName) || rdr == null || rdr.FieldCount == 0)
            {
                return false;
            }
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                if (String.Equals(rdr.GetName(i), columnName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        internal static Dictionary<string, bool> getColumnExistsTable(IList<string> columnNames, System.Data.IDataReader rdr)
        {
            Dictionary<string, bool> columnExistenceTable = new Dictionary<string, bool>(columnNames.Count);
            foreach (string columnName in columnNames)
            {
                columnExistenceTable.Add(columnName, columnExists(columnName, rdr));
            }
            return columnExistenceTable;
        }
    }
}
