using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace gov.va.medora.utils
{
    [TestFixture]
    public class FileIOUtilsTest
    {
        const string FILE_NAME = "testWriteTo.txt";

        [SetUp]
        public void setup()
        {
            Assert.IsFalse(File.Exists(FILE_NAME));
        }

        [TearDown]
        public void teardown() {
            File.Delete(FILE_NAME);
        }

        [Test]
        public void testWriteAndReadToFileString()
        {
            string data = "This is some yummy data to test write";

            FileIOUtils.writeToFile(FILE_NAME, data, false);
            Assert.IsTrue(File.Exists(FILE_NAME));

            string expected = data; 
            Assert.AreEqual(expected, FileIOUtils.readFromFile(FILE_NAME));

            FileIOUtils.writeToFile(FILE_NAME, data, true);
            Assert.AreEqual(expected + expected, FileIOUtils.readFromFile(FILE_NAME));
        }

        [Test]
        public void testWriteAndReadToFileArray()
        {
            string[] data = {
                                "This is some yummy data to test write...",
                                "And this is even more scrumptiousness...",
                                "Then to top it all off, here's a tasty little morsel."
                            };

            FileIOUtils.writeToFile(FILE_NAME, data, false);
            Assert.IsTrue(File.Exists(FILE_NAME));

            string expected = data[0] + StringUtils.CRLF + data[1] + StringUtils.CRLF + data[2] + StringUtils.CRLF;
            Assert.AreEqual(expected, FileIOUtils.readFromFile(FILE_NAME));

            FileIOUtils.writeToFile(FILE_NAME, data, true);
            Assert.AreEqual(expected + expected, FileIOUtils.readFromFile(FILE_NAME));
        }
    }
}
