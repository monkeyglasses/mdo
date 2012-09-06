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
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace gov.va.medora.mdo
{
    public class DictionaryHashList : NameObjectCollectionBase
    {
        private DictionaryEntry _de = new DictionaryEntry();

        public DictionaryHashList() {}

        public DictionaryHashList(IDictionary d, Boolean bReadOnly)
        {
            foreach (DictionaryEntry de in d)
            {
                this.BaseAdd((string)de.Key, de.Value);
            }
            this.IsReadOnly = bReadOnly;
        }

        public DictionaryEntry this[int index]
        {
            get
            {
                _de.Key = this.BaseGetKey(index);
                _de.Value = this.BaseGet(index);
                return _de;
            }
        }

        public Object this[String key]
        {
            get
            {
                return this.BaseGet(key);
            }
            set
            {
                this.BaseSet(key, value);
            }
        }

        public String[] AllKeys
        {
            get
            {
                return this.BaseGetAllKeys();
            }
        }

        public Object[] AllValues
        {
            get
            {
                return this.BaseGetAllValues();
            }
        }

        public String[] AllStringValues
        {
            get
            {
                return (String[])this.BaseGetAllValues(Type.GetType("System.String"));
            }
        }

        public Boolean HasKeys
        {
            get
            {
                return this.BaseHasKeys();
            }
        }

        public void Add(String key, Object value)
        {
            this.BaseAdd(key, value);
        }

        public void Remove(String key)
        {
            this.BaseRemove(key);
        }

        public void Remove(int index)
        {
            this.BaseRemoveAt(index);
        }

        public void Clear()
        {
            this.BaseClear();
        }
    }
}
