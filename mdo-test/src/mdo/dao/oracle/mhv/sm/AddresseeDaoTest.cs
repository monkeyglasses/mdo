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
    public class AddresseeDaoTest
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
        public void testGetAddresseesForMessageSqlStatement()
        {
            OracleQuery request = new AddresseeDao(_cxn).buildGetAddresseesForMessageQuery(1);
            Assert.IsTrue(String.Equals(request.Command.CommandText, "SELECT ADDRESSEE_ID, ADDRESSEE_ROLE, SECURE_MESSAGE_ID, USER_ID, OPLOCK AS ADDROPLOCK, FOLDER_ID, READ_DATE, REMINDER_DATE FROM SMS.ADDRESSEE WHERE SECURE_MESSAGE_ID=:messageId AND ACTIVE=1"));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void testBuildCreateAddresseeSqlStatementExpectedException()
        {
            AddresseeDao dao = new AddresseeDao(_cxn);
            OracleQuery query = dao.buildCreateAddresseeQuery(new domain.sm.Addressee(), 1);
            Assert.Fail("Previous line should have thrown exception");
        }

        [Test]
        public void testBuildCreateAddresseeSqlStatement()
        {
            AddresseeDao dao = new AddresseeDao(_cxn);
            OracleQuery query = dao.buildCreateAddresseeQuery(new domain.sm.Addressee() { Folder = new domain.sm.Folder(), Owner = new domain.sm.User() }, 1);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "INSERT INTO SMS.ADDRESSEE (ADDRESSEE_ROLE, SECURE_MESSAGE_ID, USER_ID, FOLDER_ID) VALUES (:addresseeRole, :smId, :userId, :folderId) RETURNING ADDRESSEE_ID INTO :outId"));
        }

        [Test]
        public void testBuildReadMessageSqlStatementWithDate()
        {
            AddresseeDao dao = new AddresseeDao(_cxn);
            OracleQuery query = dao.buildReadMessageRequest(new domain.sm.Addressee() { ReadDate = DateTime.Now });
            Assert.IsTrue(String.Equals(query.Command.CommandText, "UPDATE SMS.ADDRESSEE SET READ_DATE=:readDate, OPLOCK=:oplockPlusOne, MODIFIED_DATE=:modifiedDate WHERE ADDRESSEE_ID=:addresseeId AND OPLOCK=:oplock RETURNING SECURE_MESSAGE_ID INTO :outId"));
            Assert.IsTrue(((Oracle.DataAccess.Types.OracleDate)query.Command.Parameters["readDate"].Value).Year > 1900, "The read date should be set");
        }


    }
}
