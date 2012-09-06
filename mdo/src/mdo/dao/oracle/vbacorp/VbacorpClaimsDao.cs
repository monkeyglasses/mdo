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

namespace gov.va.medora.mdo.dao.oracle.vbacorp
{
    public class VbacorpClaimsDao : IClaimsDao
    {
        MdoOracleConnection myCxn;

        public VbacorpClaimsDao(AbstractConnection cxn)
        {
            myCxn = (MdoOracleConnection)cxn;
        }

        public List<Person> getClaimants(string lastName, string firstName, string middleName, string dob, Address addr, int maxrex)
        {
            BuildGetClaimantsRequestTemplate bldTemplate = new VbacorpBuildGetClaimantsRequest();
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

    class VbacorpBuildGetClaimantsRequest : BuildGetClaimantsRequestTemplate
    {
        StringDictionary fldNames;

        public VbacorpBuildGetClaimantsRequest() : base()
        {
            fldNames = new StringDictionary();
            fldNames.Add("ID", "ptcpnt_id");
            fldNames.Add("LastName", "last_nm");
            fldNames.Add("FirstName", "first_nm");
            fldNames.Add("MiddleName", "middle_nm");
            fldNames.Add("DOB", "@brthdy_dt");
            fldNames.Add("Zipcode", "zip_prefix_nbr");
            fldNames.Add("State", "postal_cd");
            fldNames.Add("City", "city_nm");
        }

        public override string Tables
        {
            get { return VbacorpConstants.GET_CLAIMANTS_TABLES; }
        }

        public override string Fields
        {
            get { return VbacorpConstants.GET_CLAIMANTS_FIELDS; }
        }

        public override string Where
        {
            get { return VbacorpConstants.GET_CLAIMANTS_WHERE; }
        }

        public override StringDictionary FieldNames
        {
            get { return fldNames; }
        }
    }
}
