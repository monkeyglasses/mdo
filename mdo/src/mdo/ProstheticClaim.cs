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
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdo
{
    public class ProstheticClaim : Claim
    {
        const string DAO_NAME = "IClaimsDao";

        string itemId;
        string name;

        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public string ItemName
        {
            get { return name; }
            set { name = value; }
        }

        internal static IClaimsDao getDao(AbstractConnection cxn)
        {
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(cxn.DataSource.Protocol));
            return f.getClaimsDao(cxn);
        }

        public static ProstheticClaim[] getProstheticClaimsForPatient(AbstractConnection cxn)
        {
            return getDao(cxn).getProstheticClaimsForClaimant();
        }

        public static ProstheticClaim[] getProstheticClaimsForPatient(AbstractConnection cxn, string pid)
        {
            return getDao(cxn).getProstheticClaimsForClaimant(pid);
        }

        public static IndexedHashtable getProstheticClaimsForPatient(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getProstheticClaimsForPatient", new object[] { });
        }

        public static List<ProstheticClaim> getProstheticClaims(AbstractConnection cxn, string pid, List<string> episodeDates)
        {
            return getDao(cxn).getProstheticClaims(pid, episodeDates);
        }
    }
}
