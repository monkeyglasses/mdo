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
