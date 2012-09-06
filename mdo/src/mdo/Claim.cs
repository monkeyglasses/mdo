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
    public class Claim
    {
        const string DAO_NAME = "IClaimsDao";

        string id;
        string patientId;
        string patientName;
        string patientSsn;
        string episodeDate;
        string timestamp;
        string lastEditTimestamp;
        string insuranceName;
        string cost;
        string billableStatus;
        string condition;
        string serviceConnectedPercent;
        string consultId;
        string comment;

        public Claim() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        public string PatientSSN
        {
            get { return patientSsn; }
            set { patientSsn = value; }
        }

        public string EpisodeDate
        {
            get { return episodeDate; }
            set { episodeDate = value; }
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string LastEditTimestamp
        {
            get { return lastEditTimestamp; }
            set { lastEditTimestamp = value; }
        }

        public string InsuranceName
        {
            get { return insuranceName; }
            set { insuranceName = value; }
        }

        public string Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public string BillableStatus
        {
            get { return billableStatus; }
            set { billableStatus = value; }
        }

        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public string ServiceConnectedPercent
        {
            get { return serviceConnectedPercent; }
            set { serviceConnectedPercent = value; }
        }

        public string ConsultId
        {
            get { return consultId; }
            set { consultId = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        internal static IClaimsDao getDao(AbstractConnection cxn)
        {
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(cxn.DataSource.Protocol));
            return f.getClaimsDao(cxn);
        }

        public static IndexedHashtable getClaimants(
            ConnectionSet cxns, 
            string lastName,
            string firstName,
            string middleName,
            string dob,
            Address addr,
            int maxrex)
        {
            return cxns.query(DAO_NAME, "getClaimants", new object[] 
                { lastName, 
                  firstName,
                  middleName,
                  dob,
                  addr,
                  maxrex
                });
        }
    }
}
