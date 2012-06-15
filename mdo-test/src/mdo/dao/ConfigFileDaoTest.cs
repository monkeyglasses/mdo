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
