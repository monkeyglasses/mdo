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

namespace gov.va.medora.mdo.dao.file
{
    [TestFixture]
    public class ConfigFileDaoTest
    {
        [Test]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void testReadConfigInvalidFile()
        {
            string foo = "C:\\NotARealFile.conf";
            ConfigFileDao dao = new ConfigFileDao(foo);
            dao.getAllValues();
        }

        [Test]
        public void testReadConfigValidFile()
        {
            string projectResourcePath = gov.va.medora.utils.ResourceUtils.ResourcesPath;
            string confFilePath = projectResourcePath + "conf\\app.conf";

            ConfigFileDao dao = new ConfigFileDao(confFilePath);
            Dictionary<string, Dictionary<string, string>> result = dao.getAllValues();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result[result.Keys.First()].Count > 0);
        }

    }
}
