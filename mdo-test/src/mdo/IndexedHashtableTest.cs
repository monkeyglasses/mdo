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
using NUnit.Framework;

namespace gov.va.medora.mdo
{
    [TestFixture]
    public class IndexedHashtableTest
    {
        IndexedHashtable tbl;

        #region ForEachTest
        [SetUp]
        public void setup()
        {
            tbl = new IndexedHashtable();
            for (int i = 0; i < 5; i++)
            {
                tbl.Add("Key" + i, "Value" + i);
            }
        }

        [TearDown]
        public void tearDown()
        {
            tbl = null;
        }

        #endregion

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAddNullKey()
        {
            tbl.Add(null, null);
        }

        [Test]
        public void testAddNullValue()
        {
            tbl.Add("AddedKey", null);
            Assert.IsTrue(tbl.Count == 6);
        }

        [Test]
        public void testAddValue()
        {
            tbl.Add("AddedKey", new object());
            Assert.IsTrue(tbl.Count == 6);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testRemoveNullKey()
        {
            tbl.Remove(null);
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void testRemoveNonexistentItem()
        {
            tbl.Remove("NonexistentKey");
        }

        [Test]
        public void testRemove()
        {
            tbl.Remove("Key0");
            Assert.IsTrue(tbl.Count == 4);
            Assert.AreEqual("Key4", tbl.GetKey(tbl.Count-1), "Expected \"Key4\", got \"" + tbl.GetKey(tbl.Count-1) + "\"");
        }

        [Test]
        public void testGetValueWithKey()
        {
            Assert.AreEqual("Value0", tbl.GetValue("Key0"));
        }

        [Test]
        public void testGetValueByIndex()
        {
            Assert.AreEqual("Value0", tbl.GetValue(0));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testGetValueByIndexOutOfRangeAfterRemove()
        {
            int lth = tbl.Count;
            tbl.Remove("Key0");
            Assert.AreEqual("Value4", tbl.GetValue(lth - 1));
        }

        [Test]
        public void testGetValueByIndexAfterRemove()
        {
            tbl.Remove("Key0");
            Assert.AreEqual("Value4", tbl.GetValue(tbl.Count - 1));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testGetKeyByIndexOutOfRange()
        {
            object k = tbl.GetKey(tbl.Count);
        }

        [Test]
        public void testGetKey()
        {
            Assert.AreEqual("Key0", tbl.GetKey(0));
            Assert.AreEqual("Key4", tbl.GetKey(4));
        }

        [Test]
        public void testContainsKey()
        {
            Assert.IsTrue(tbl.ContainsKey("Key3"));
            Assert.IsFalse(tbl.ContainsKey("NonexistentKey"));
        }
    }
}
