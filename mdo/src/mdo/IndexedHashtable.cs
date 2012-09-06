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
using System.Collections.Generic;
using System.Text;

namespace gov.va.medora.mdo
{
    public class IndexedHashtable
    {
        ArrayList keyArray;
        Hashtable hashTable;

        public IndexedHashtable() 
        {
            keyArray = new ArrayList();
            hashTable = new Hashtable();
        }

        public IndexedHashtable(int capacity)
        {
            keyArray = new ArrayList(capacity);
            hashTable = new Hashtable(capacity);
        }

        public void Add(Object key, Object value)
        {
            keyArray.Add(key);
            hashTable.Add(key, value);
        }

        public void Remove(Object key)
        {
            if (!hashTable.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }
            keyArray.Remove(key);
            hashTable.Remove(key);
        }

        public void Clear()
        {
            hashTable = new Hashtable();
            keyArray = new ArrayList();
        }

        public Object GetValue(String key)
        {
            return hashTable[key];
        }

        public Object GetValue(int index)
        {
            return hashTable[keyArray[index]];
        }

        public int Count
        {
            get { return keyArray.Count; }
        }

        public Object GetKey(int index)
        {
            return keyArray[index];
        }

        public bool ContainsKey(String target)
        {
            return hashTable.ContainsKey(target);
        }

    }
}
