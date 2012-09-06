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
