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
using System.Collections.Specialized;
using System.Text;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.dao.oracle.adr
{
    public class AdrClaimsDao : IClaimsDao
    {
        MdoOracleConnection myCxn;

        public AdrClaimsDao(AbstractConnection cxn)
        {
            myCxn = (MdoOracleConnection)cxn;
        }

        public List<Person> getClaimants(string lastName, string firstName, string middleName, string dob, Address addr, int maxrex)
        {
            BuildGetClaimantsRequestTemplate bldTemplate = new AdrBuildGetClaimantsRequest();
            string sql = bldTemplate.buildGetClaimantsRequest(lastName, firstName, middleName, dob, addr, maxrex);
            OracleClaimsDao oracleDao = new OracleClaimsDao(myCxn);
            return oracleDao.getClaimants(sql);
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
    }

    internal class AdrBuildGetClaimantsRequest : BuildGetClaimantsRequestTemplate
    {
        StringDictionary fldNames;

        public AdrBuildGetClaimantsRequest() : base() 
        {
            fldNames = new StringDictionary();
            fldNames.Add("ID", "person_id");
            fldNames.Add("LastName", "last_name");
            fldNames.Add("FirstName", "first_name");
            fldNames.Add("MiddleName", "middle_name");
            fldNames.Add("DOB", "date_of_birth");
            fldNames.Add("Zipcode", "zip_code");
            fldNames.Add("State", "state_code");
            fldNames.Add("City", "city");
        }

        public override string Tables
        {
            get { return AdrConstants.GET_CLAIMANTS_TABLES; }
        }

        public override string Fields
        {
            get { return AdrConstants.GET_CLAIMANTS_FIELDS; }
        }

        public override string Where
        {
            get { return AdrConstants.GET_CLAIMANTS_WHERE; }
        }

        public override StringDictionary FieldNames
        {
            get { return fldNames; }
        }
    }
}
