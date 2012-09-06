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
using System.Data;

namespace gov.va.medora.mdo
{
    public class MockDataReader : IDataReader
    {
        public void Close()
        {
            return;
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            if (this.Table == null)
            {
                this.Table = new DataTable();
            }
            return this.Table;
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            return ++_rowIndex < _results.Rows.Count;
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get { return this._columnNameMap.Count; }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            return Convert.ToDateTime(_results.Rows[_rowIndex][i]);
        }

        public decimal GetDecimal(int i)
        {
            return Convert.ToDecimal(_results.Rows[_rowIndex][i]);
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            return Convert.ToInt32(_results.Rows[_rowIndex][i]);
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            return _columnNameMap.Keys.ToArray<string>()[i];
        }

        public int GetOrdinal(string name)
        {
            return _columnNameMap[name];
        }

        public string GetString(int i)
        {
            return Convert.ToString(_results.Rows[_rowIndex][i]);
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            return _results.Rows[_rowIndex][i] == DBNull.Value;
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get 
            {
                return _results.Rows[_rowIndex][i];
            }
        }

        private DataTable _results;

        public DataTable Table 
        { 
            get 
            { 
                return _results; 
            } 
            set 
            {
                _results = value;
                for (int i = 0; i < _results.Columns.Count; i++)
                {
                    if (!_columnNameMap.ContainsKey(_results.Columns[i].ColumnName))
                    {
                        _columnNameMap.Add(_results.Columns[i].ColumnName, i);
                    }
                }
            } 
        }

        Dictionary<string, int> _columnNameMap = new Dictionary<string, int>();
        int _rowIndex = -1;
    }
}
