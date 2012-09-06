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
using System.Data.SqlClient;
using System.Configuration;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.dao.vista;

namespace gov.va.medora.mdo.dao.sql.npt
{
    public class NptClaimsDao : IClaimsDao
    {
        NptConnection myCxn;

        public NptClaimsDao(AbstractConnection cxn)
        {
            myCxn = (NptConnection)cxn;
        }

        public ProstheticClaim[] getProstheticClaimsForClaimant()
        {
            throw new NotImplementedException();
        }

        public ProstheticClaim[] getProstheticClaimsForClaimant(string dfn)
        {
            throw new NotImplementedException();
        }

        public List<ProstheticClaim> getProstheticClaims(string dfn, List<string> episodeDates)
        {
            throw new NotImplementedException();
        }

        public List<Person> getClaimants(string lastName, string firstName, string middleName, string dob, Address addr, int maxrex)
        {
            throw new NotImplementedException();
        }
    }
}
