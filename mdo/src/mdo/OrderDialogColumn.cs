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
using System.Collections.Specialized;
using System.Text;

namespace gov.va.medora.mdo
{
    public class OrderDialogColumn
    {
        string colNum;
        IndexedHashtable rows;

        public OrderDialogColumn() { }

        public OrderDialogColumn(string colNum)
        {
            ColNum = colNum;
            Rows = new IndexedHashtable();
        }

        public string ColNum
        {
            get { return colNum; }
            set { colNum = value; }
        }

        public IndexedHashtable Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public bool Exists(string rownum)
        {
            if (Rows == null)
            {
                return false;
            }
            return Rows.ContainsKey(rownum);
        }

        public void AddRow(string rownum)
        {
            Rows.Add(rownum, new OrderDialogRow(rownum));
        }

        public OrderDialogRow GetRow(string rownum)
        {
            return (OrderDialogRow)Rows.GetValue(rownum);
        }
    }
}
