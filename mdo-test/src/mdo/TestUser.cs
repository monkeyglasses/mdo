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
