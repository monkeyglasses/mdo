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
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using Oracle.DataAccess.Client;

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    [TestFixture]
    public class UserDaoTest
    {
        DataSource _src;
        MdoOracleConnection _cxn;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _src = new DataSource()
            {
                ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=mhv.va.gov)(PORT=1))(CONNECT_DATA=(SERVICE_NAME=sid)));User ID=user;Password=password;"
            };
            _cxn = new MdoOracleConnection(_src);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _cxn.disconnect();
        }

        [Test]
        public void testUpdateLastNotificationSqlStatement()
        {
            UserDao dao = new UserDao(_cxn);
            OracleQuery query = dao.buildUpdateLastEmailNotificationQuery(
                new domain.sm.User()
                {
                    Email = "me@a.b",
                    EmailNotification = domain.sm.enums.EmailNotificationEnum.EACH_MESSAGE,
                    Id = 1,
                    LastNotification = DateTime.Now
                });
            Assert.IsTrue(String.Equals(query.Command.CommandText, "UPDATE SMS.SMS_USER SET LAST_EMAIL_NOTIFICATION=:lastEmailNotification, OPLOCK=:oplockPlusOne WHERE USER_ID=:userId and OPLOCK=:oplock"));
        }

        [Test]
        public void testGetValidRecipientsForPatientSqlStatement()
        {
            UserDao dao = new UserDao(_cxn);
            OracleQuery query = dao.buildGetValidRecipientsForPatientQuery(1);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT TG.TRIAGE_GROUP_ID, TG.TRIAGE_GROUP_NAME, TG.DESCRIPTION FROM SMS.PATIENT_TRIAGE_MAP PTM JOIN SMS.TRIAGE_RELATION TR  ON PTM.RELATION_ID=TR.RELATION_ID  JOIN SMS.TRIAGE_GROUP TG ON TG.TRIAGE_GROUP_ID=TR.TRIAGE_GROUP_ID WHERE PTM.USER_ID = :userId AND PTM.ACTIVE=1"));
        }

        [Test]
        public void testGetUserByIdSqlStatement()
        {
            UserDao dao = new UserDao(_cxn);
            OracleQuery query = dao.buildGetUserByIdQuery(1);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT USER_ID, FIRST_NAME, LAST_NAME, USER_TYPE, STATUS, EMAIL_ADDRESS, OPLOCK, ACTIVE, DOB, ICN, SSN, STATION_NO, DUZ, EMAIL_NOTIFICATION, DEFAULT_MESSAGE_FILTER, LAST_EMAIL_NOTIFICATION, NSSN, PROVIDER, EXTERNAL_USER_NAME FROM SMS.sms_user WHERE USER_ID = :userId"));
        }

        [Test]
        public void testGetUserByIcnSqlStatement()
        {
            UserDao dao = new UserDao(_cxn);
            OracleQuery query = dao.buildGetUserByIcnQuery("1");
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT USER_ID, FIRST_NAME, LAST_NAME, USER_TYPE, STATUS, EMAIL_ADDRESS, OPLOCK, ACTIVE, DOB, ICN, SSN, STATION_NO, DUZ, EMAIL_NOTIFICATION, DEFAULT_MESSAGE_FILTER, LAST_EMAIL_NOTIFICATION, NSSN, PROVIDER, EXTERNAL_USER_NAME FROM SMS.sms_user WHERE ICN = :icn"));
        }

        [Test]
        public void testGetTriageGroupMemberIdsSqlStatement()
        {
            UserDao dao = new UserDao(_cxn);
            OracleQuery query = dao.buildGetTriageGroupMembersQuery(1);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT CTM.USER_ID, USR.EMAIL_ADDRESS, USR.OPLOCK, USR.EMAIL_NOTIFICATION, USR.LAST_EMAIL_NOTIFICATION FROM SMS.CLINICIAN_TRIAGE_MAP CTM JOIN SMS.SMS_USER USR ON CTM.USER_ID=USR.USER_ID WHERE CTM.TRIAGE_GROUP_ID=:groupId AND CTM.ACTIVE=1"));
        }

    }
}
