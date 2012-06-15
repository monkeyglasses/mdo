using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.utils
{
    [TestFixture]
    public class SSTCryptographerTest
    {
        const string KEY = "yourmommadressesyoufunny";
        const string SECRET = "mysupersecretdata";
        const string ENCRYPTED = "tjGlzYRiVpYg4dPgCoQusMHR3TfE8edQ";

        [SetUp]
        public void setup()
        {
            SSTCryptographer.Key = KEY;
        }

        [TearDown]
        public void teardown()
        {
        }

        [Test]
        public void testEncrypt()
        {
            Assert.AreEqual(ENCRYPTED, SSTCryptographer.Encrypt(SECRET));
        }

        [Test]
        public void testDecrypt()
        {
            Assert.AreEqual(SECRET, SSTCryptographer.Decrypt(ENCRYPTED));
        }
    }
}
