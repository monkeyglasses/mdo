using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.src.mdo.utils
{
    [TestFixture]
    public class ResourceUtilsTest
    {
        [SetUp]
        public void setup()
        {
        }

        [TearDown]
        public void teardown()
        {
        }

        [Test]
        public void testGetResources() 
        {
            string path = ResourceUtils.ResourcesPath;
            Assert.IsTrue(path.EndsWith(@"\mdo-test\resources\"));
        }

        [Test]
        public void testGetXmlResources()
        {
            string path = ResourceUtils.XmlResourcesPath;
            Assert.IsTrue(path.EndsWith(@"\mdo-test\resources\xml"));
        }

        [Test]
        public void testGetDataResources()
        {
            string path = ResourceUtils.DataResourcesPath;
            Assert.IsTrue(path.EndsWith(@"\mdo-test\resources\data"));
        }
    }
}
