using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    [TestFixture]
    public class FolderDaoTest
    {
        [Test]
        public void testGetFolderSql()
        {
            FolderDao dao = new FolderDao(new MdoOracleConnection(new DataSource() { ConnectionString = "MyCxnString" }));
            OracleQuery result = dao.buildGetFolderQuery(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("SELECT FOLDER_ID, USER_ID, FOLDER_NAME, OPLOCK AS FOLDOPLOCK FROM SMS.FOLDER WHERE FOLDER_ID=:folderId AND ACTIVE=1", result.Command.CommandText);
            Assert.AreEqual(1, result.Command.Parameters.Count);
            Assert.AreEqual(System.Data.DbType.Decimal, result.Command.Parameters[0].DbType);
            Assert.AreEqual("folderId", result.Command.Parameters[0].ParameterName);
            Assert.AreEqual(1, result.Command.Parameters[0].Value);
            Assert.AreEqual(1, result.Command.Parameters.Count);
        }

        [Test]
        public void testUpdateFolderSql()
        {
            FolderDao dao = new FolderDao(new MdoOracleConnection(new DataSource() { ConnectionString = "MyCxnString" }));
            OracleQuery result = dao.buildUpdateFolderQuery(new domain.sm.Folder() { Id = 1, Name = "New Folder Name" });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Command.Connection);
            Assert.AreEqual("UPDATE SMS.FOLDER SET FOLDER_NAME=:folderName, OPLOCK=:oplockPlusOne, MODIFIED_DATE=:modifiedDate WHERE FOLDER_ID=:folderId and OPLOCK=:oplock AND ACTIVE=1", result.Command.CommandText);
            Assert.AreEqual(5, result.Command.Parameters.Count);
            Assert.AreEqual(System.Data.DbType.String, result.Command.Parameters[0].DbType);
            Assert.AreEqual(Oracle.DataAccess.Client.OracleDbType.Varchar2, result.Command.Parameters[0].OracleDbType);
            Assert.AreEqual("folderName", result.Command.Parameters[0].ParameterName);
            Assert.AreEqual("New Folder Name", result.Command.Parameters[0].Value);
            Assert.AreEqual(System.Data.DbType.Decimal, result.Command.Parameters[1].DbType);
            Assert.AreEqual(Oracle.DataAccess.Client.OracleDbType.Decimal, result.Command.Parameters[1].OracleDbType);
            Assert.AreEqual("oplockPlusOne", result.Command.Parameters[1].ParameterName);
            Assert.AreEqual(1, result.Command.Parameters[1].Value);
            Assert.AreEqual(System.Data.DbType.Date, result.Command.Parameters[2].DbType);
            Assert.AreEqual(Oracle.DataAccess.Client.OracleDbType.Date, result.Command.Parameters[2].OracleDbType);
            Assert.AreEqual("modifiedDate", result.Command.Parameters[2].ParameterName);
            Assert.AreEqual(Oracle.DataAccess.Client.OracleDbType.Decimal, result.Command.Parameters[3].OracleDbType);
            Assert.AreEqual("folderId", result.Command.Parameters[3].ParameterName);
            Assert.AreEqual(1, result.Command.Parameters[3].Value);
            Assert.AreEqual(Oracle.DataAccess.Client.OracleDbType.Decimal, result.Command.Parameters[4].OracleDbType);
            Assert.AreEqual("oplock", result.Command.Parameters[4].ParameterName);
        }

        [Test]
        public void testDeleteFolderSql()
        {
            FolderDao dao = new FolderDao(new MdoOracleConnection(new DataSource() { ConnectionString = "MyCxnString" }));
            OracleQuery result = dao.buildDeleteFolderQuery(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("DELETE FROM SMS.FOLDER WHERE FOLDER_ID=:folderId", result.Command.CommandText);
        }

        [Test]
        public void testCreateFolderSql()
        {
            FolderDao dao = new FolderDao(new MdoOracleConnection(new DataSource() { ConnectionString = "MyCxnString" }));
            OracleQuery result = dao.buildCreateFolderQuery(new domain.sm.Folder() { Id = 1, Name = "New Folder Name", Owner = new domain.sm.User() { Id = 2 } });

            Assert.IsNotNull(result);
            Assert.AreEqual("INSERT INTO SMS.FOLDER (USER_ID, FOLDER_NAME) VALUES (:userId, :folderName) RETURNING FOLDER_ID INTO :outId", result.Command.CommandText);
        }
    }
}
