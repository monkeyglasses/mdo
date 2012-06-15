using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace gov.va.medora.mdo
{
    /// <summary>
    /// Test class for gov.va.medora.mdo.Team
    /// </summary>
    /// <remarks>
    /// Notice convention used for testing C#'s setter/getters.
    /// </remarks>

    [TestFixture]
    public class TeamTest
    {

        [Test]
        public void testEmptyConstructor()
        {
            Team theTeam = new Team();
            Assert.IsNull(theTeam.Id);
            Assert.IsNull(theTeam.Name);
        }

        [Test]
        public void testConstructor()
        {
            Team theTeam = new Team("any ID", "Joe Foo");
            Assert.AreEqual("any ID", theTeam.Id);
            Assert.AreEqual("Joe Foo", theTeam.Name);
        }

        /// <summary>
        /// Test case for the Id object used to set/get the id object
        /// </summary>
        /// <remarks>
        /// testGetId() would imply that there is a getId() method. 
        /// C# offers a shorthand which doesn't use setters/getters.
        /// I propose that we adopt the testObjectGet() convention
        /// to reflect that that is what we are testing.
        /// 
        /// 
        /// 
        /// </remarks>
        [Test]
        public void testIdGet()
        {

        }

        [Test]
        public void testIdSet()
        {

        }

        [Test]
        public void testNameGet()
        {

        }

        [Test]
        public void testNameSet()
        {

        }
    }
}
