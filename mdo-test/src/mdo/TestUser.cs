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
using gov.va.medora;

namespace gov.va.medora.mdo
{
    /// <summary>Encapsulation of creating a user from a StringTestObject so that this can be sprung.
    /// </summary>
    class TestUser : User
    {
        public TestUser(StringTestObject testUser, bool useCaseInsensitive)
        {
            Uid = testUser.get("userDUZ", useCaseInsensitive);
            Name = new PersonName(testUser.get("userName", useCaseInsensitive));
            SSN = new SocSecNum(testUser.get("userSSN", useCaseInsensitive));
            PermissionString = testUser.get("context", useCaseInsensitive);
        }
    }
}
