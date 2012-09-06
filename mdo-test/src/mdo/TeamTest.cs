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
