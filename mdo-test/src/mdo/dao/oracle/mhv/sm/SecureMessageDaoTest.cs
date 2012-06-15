using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using gov.va.medora.utils;
using Oracle.DataAccess.Client;

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    [TestFixture]
    public class SecureMessageDaoTest
    {
        delegate OracleDataReader _delegateReader();
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

        #region OracleQuery tests
        [Test]
        public void testBuildGetMessagesInFolderSqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            OracleQuery query = dao.buildGetMessagesInFolderQuery(1, 1, 0, 25);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT ADDR.ADDRESSEE_ID, ADDR.ADDRESSEE_ROLE, ADDR.OPLOCK AS ADDROPLOCK, ADDR.ACTIVE AS ADDRACTIVE, ADDR.USER_ID, ADDR.CREATED_DATE AS ADDRCREATEDDATE, ADDR.MODIFIED_DATE AS ADDRMODIFIEDDATE, ADDR.FOLDER_ID, ADDR.READ_DATE, ADDR.REMINDER_DATE, FOLD.FOLDER_NAME, FOLD.ACTIVE AS FOLDACTIVE, FOLD.OPLOCK AS FOLDOPLOCK, SM.SECURE_MESSAGE_ID, SM.CLINICIAN_STATUS, SM.COMPLETED_DATE, SM.ASSIGNED_TO, SM.CHECKSUM, SM.THREAD_ID, SM.STATUS_SET_BY, SM.OPLOCK AS SMOPLOCK, SM.ACTIVE, SM.CREATED_DATE, SM.MODIFIED_DATE, SM.ESCALATED, SM.SENT_DATE, SM.SENDER_TYPE, SM.SENDER_ID, SM.SENDER_NAME, SM.RECIPIENT_TYPE, SM.RECIPIENT_ID, SM.RECIPIENT_NAME, SM.ESCALATION_NOTIFICATION_DATE, SM.ESCALATION_NOTIFICATION_TRIES, SM.READ_RECEIPT, SM.HAS_ATTACHMENT, SM.ATTACHMENT_ID, MT.SUBJECT, MT.TRIAGE_GROUP_ID, MT.CATEGORY_TYPE, MT.OPLOCK AS MTOPLOCK FROM SMS.ADDRESSEE ADDR JOIN SMS.SECURE_MESSAGE SM ON ADDR.SECURE_MESSAGE_ID=SM.SECURE_MESSAGE_ID JOIN SMS.MESSAGE_THREAD MT ON SM.THREAD_ID=MT.THREAD_ID LEFT JOIN SMS.FOLDER FOLD ON ADDR.FOLDER_ID=FOLD.FOLDER_ID WHERE ADDR.USER_ID = :userId AND ADDR.FOLDER_ID = :folderId AND ADDR.ACTIVE = 1 AND ROWNUM >= :pageStart AND ROWNUM <= :pageSize ORDER BY SM.SENT_DATE DESC"));
        }

        [Test]
        public void testBuildGetMessagesFromThreadSqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            OracleQuery query = dao.buildGetMessagesFromThreadQuery(1);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT SM.SECURE_MESSAGE_ID, SM.CLINICIAN_STATUS, SM.COMPLETED_DATE, SM.ASSIGNED_TO, SM.OPLOCK AS SMOPLOCK, SM.SENT_DATE, SM.SENDER_ID, SM.RECIPIENT_ID, MT.SUBJECT, MT.TRIAGE_GROUP_ID, MT.OPLOCK AS MTOPLOCK, MT.CATEGORY_TYPE FROM SMS.SECURE_MESSAGE SM JOIN SMS.MESSAGE_THREAD MT ON SM.THREAD_ID=MT.THREAD_ID WHERE SM.THREAD_ID=:threadId AND SM.ACTIVE=1"));
        }

        [Test]
        public void testBuildMessagesSqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            OracleQuery query = dao.buildGetSecureMessagesQuery(1, 0, 0);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT ADDR.ADDRESSEE_ID, ADDR.ADDRESSEE_ROLE, ADDR.OPLOCK AS ADDROPLOCK, ADDR.ACTIVE AS ADDRACTIVE, ADDR.USER_ID, ADDR.CREATED_DATE AS ADDRCREATEDDATE, ADDR.MODIFIED_DATE AS ADDRMODIFIEDDATE, ADDR.FOLDER_ID, ADDR.READ_DATE, ADDR.REMINDER_DATE, FOLD.FOLDER_NAME, FOLD.ACTIVE AS FOLDACTIVE, FOLD.OPLOCK AS FOLDOPLOCK, SM.SECURE_MESSAGE_ID, SM.CLINICIAN_STATUS, SM.COMPLETED_DATE, SM.ASSIGNED_TO, SM.CHECKSUM, SM.THREAD_ID, SM.STATUS_SET_BY, SM.OPLOCK AS SMOPLOCK, SM.ACTIVE, SM.CREATED_DATE, SM.MODIFIED_DATE, SM.ESCALATED, SM.SENT_DATE, SM.SENDER_TYPE, SM.SENDER_ID, SM.SENDER_NAME, SM.RECIPIENT_TYPE, SM.RECIPIENT_ID, SM.RECIPIENT_NAME, SM.ESCALATION_NOTIFICATION_DATE, SM.ESCALATION_NOTIFICATION_TRIES, SM.READ_RECEIPT, SM.HAS_ATTACHMENT, SM.ATTACHMENT_ID, MT.SUBJECT, MT.TRIAGE_GROUP_ID, MT.CATEGORY_TYPE, MT.OPLOCK AS MTOPLOCK FROM SMS.ADDRESSEE ADDR JOIN SMS.SECURE_MESSAGE SM ON ADDR.SECURE_MESSAGE_ID=SM.SECURE_MESSAGE_ID JOIN SMS.MESSAGE_THREAD MT ON SM.THREAD_ID=MT.THREAD_ID LEFT JOIN SMS.FOLDER FOLD ON ADDR.FOLDER_ID=FOLD.FOLDER_ID WHERE ADDR.USER_ID = :userId AND ADDR.ACTIVE = 1 AND ROWNUM >= :pageStart AND ROWNUM <= :pageSize ORDER BY SM.SENT_DATE DESC"));
        }

        [Test]
        public void testBuildCreateThreadQuerySqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            domain.sm.Thread thread = new domain.sm.Thread();
            thread.MailGroup = new domain.sm.TriageGroup();
            OracleQuery query = dao.buildCreateThreadQuery(thread);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "INSERT INTO SMS.MESSAGE_THREAD (SUBJECT, TRIAGE_GROUP_ID, CREATED_DATE, MODIFIED_DATE, CATEGORY_TYPE) VALUES (:subject, :triageGroupId, :createdDate, :modifiedDate, :categoryType) RETURNING THREAD_ID INTO :outId"));
        }

        [Test]
        public void testBuildGetSecureMessageQuerySqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            OracleQuery query = dao.buildGetSecureMessageBodyQuery(1);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "SELECT SM.CHECKSUM, SM.BODY FROM SMS.SECURE_MESSAGE SM WHERE SM.SECURE_MESSAGE_ID = :secureMessageId AND SM.ACTIVE = 1"));
        }

        [Test]
        public void testSendMessageSqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            domain.sm.Message message = new domain.sm.Message();
            message.MessageThread = new domain.sm.Thread() { Id = 1, Subject = "Test" }; // this must be set otherwise function will try to create it inline

            OracleQuery query = dao.buildSendMessageCommand(message);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "INSERT INTO SMS.SECURE_MESSAGE (CLINICIAN_STATUS, COMPLETED_DATE, ASSIGNED_TO, CHECKSUM, THREAD_ID, STATUS_SET_BY, MODIFIED_DATE, ESCALATED, BODY, SENT_DATE, SENDER_TYPE, SENDER_ID, SENDER_NAME, RECIPIENT_TYPE, RECIPIENT_ID, RECIPIENT_NAME, SENT_DATE_LOCAL, ESCALATION_NOTIFICATION_DATE, ESCALATION_NOTIFICATION_TRIES, READ_RECEIPT, HAS_ATTACHMENT, ATTACHMENT_ID) VALUES (:clinicianStatus, :completedDate, :assignedTo, :checksum, :threadId, :statusSetBy, :modifiedDate, :escalated, :body, :sentDate, :senderType, :senderId, :senderName, :recipientType, :recipientId, :recipientName, :sentDateLocal, :escalationNotificationDate, :escalationNotificationTries, :readReceipt, :hasAttachment, :attachmentId) RETURNING SECURE_MESSAGE_ID INTO :outId"));
        }

        [Test]
        public void testUpdateMessageSqlStatement()
        {
            SecureMessageDao dao = new SecureMessageDao(_cxn);
            domain.sm.Message message = new domain.sm.Message();
            message.MessageThread = new domain.sm.Thread() { Id = 1, Subject = "Test" }; // this must be set otherwise function will try to create it inline
            OracleQuery query = dao.buildUpdateMessageQuery(message);
            Assert.IsTrue(String.Equals(query.Command.CommandText, "UPDATE SMS.SECURE_MESSAGE SET OPLOCK = :oplockPlusOne, CLINICIAN_STATUS = :clinicianStatus, COMPLETED_DATE = :completedDate, ASSIGNED_TO = :assignedTo, CHECKSUM = :checksum, THREAD_ID = :threadId, STATUS_SET_BY = :statusSetBy, MODIFIED_DATE = :modifiedDate, ESCALATED = :escalated, BODY = :body, SENT_DATE = :sentDate, SENDER_TYPE = :senderType, SENDER_ID = :senderId, SENDER_NAME = :senderName, RECIPIENT_TYPE = :recipientType, RECIPIENT_ID = :recipientId, RECIPIENT_NAME = :recipientName, SENT_DATE_LOCAL = :sentDateLocal, ESCALATION_NOTIFICATION_DATE = :escalationNotificationDate, ESCALATION_NOTIFICATION_TRIES = :escalationNotificationTries, READ_RECEIPT = :readReceipt, HAS_ATTACHMENT = :hasAttachment, ATTACHMENT_ID = :attachmentId WHERE SECURE_MESSAGE_ID = :secureMessageId AND OPLOCK = :oplock"));
        }

        #endregion

        [Test]
        public void testToMessageFromReaderComplete()
        {
            MockDataReader rdr = new MockDataReader();

            DataTable table = new DataTable();
            foreach (string columnName in TableSchemas.SECURE_MESSAGE_COLUMNS)
            {
                table.Columns.Add(columnName);
            }

            rdr.Table = table;

            object[] fakeValues = new object[] { 1, 2, DateTime.Now, 4, "ABCD1234", 6, 7, 8, 9, DateTime.Now, DateTime.Now,
                DateTime.Now, DBNull.Value, DateTime.Now, 15, 16, "BUTTS,SEYMOUR", 18, 19, "HUMPALOT,YVONNA", DateTime.Now, DateTime.Now, DBNull.Value, 0, 0 };
            rdr.Table.Rows.Add(fakeValues);
            rdr.Read();
            domain.sm.Message result = domain.sm.Message.getMessageFromReader(rdr);
            Assert.IsNotNull(result);
        }

        [Test]
        public void testToMessageFromReaderSparse()
        {
            MockDataReader rdr = new MockDataReader();

            DataTable table = new DataTable();
            for(int i = 0; i < TableSchemas.SECURE_MESSAGE_COLUMNS.Count; i++) 
            {
                if (i % 2 == 0) // just add the even indices to simulate randomly chosen columns
                {
                    table.Columns.Add(TableSchemas.SECURE_MESSAGE_COLUMNS[i]);
                }
            }

            rdr.Table = table;

            object[] fakeValues = new object[] { 1, DateTime.Now, "ABCD1234", 7, 9, DateTime.Now,
                 DBNull.Value, 15, "BUTTS,SEYMOUR", 19, DateTime.Now, DBNull.Value, 0 };
            rdr.Table.Rows.Add(fakeValues);
            rdr.Read();
            domain.sm.Message result = domain.sm.Message.getMessageFromReader(rdr);
            Assert.IsNotNull(result);
        }

    }
}
