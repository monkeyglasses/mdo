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
using Oracle.DataAccess.Client;
using gov.va.medora.mdo.exceptions;
using System.Data;
using gov.va.medora.mdo.domain.sm;

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    public class UserDao
    {
        MdoOracleConnection _cxn;
        delegate OracleDataReader reader();
        delegate Int32 nonQuery();

        public UserDao(AbstractConnection cxn)
        {
            _cxn = (MdoOracleConnection)cxn;
        }

        public domain.sm.User getUserDetail(Int32 userId)
        {
            domain.sm.User user = getUserById(userId);
            user.Groups = getValidRecipients(user).ToList<TriageGroup>();
            user.Mailbox = new Mailbox() { UserFolders = new FolderDao(_cxn).getUserFolders(userId).ToList<domain.sm.Folder>() };
            return user;
        }

        public domain.sm.User getUserById(Int32 userId)
        {
            OracleQuery query = buildGetUserByIdQuery(userId);
            reader executeReader = delegate() { return query.Command.ExecuteReader(); };
            OracleDataReader reader = (OracleDataReader)_cxn.query(query, executeReader);
            return toUserFromDataReader(reader);
        }

        internal OracleQuery buildGetUserByIdQuery(Int32 userId)
        {
            string sql = "SELECT USER_ID, FIRST_NAME, LAST_NAME, USER_TYPE, STATUS, EMAIL_ADDRESS, OPLOCK, ACTIVE, " +
                "DOB, ICN, SSN, STATION_NO, DUZ, EMAIL_NOTIFICATION, DEFAULT_MESSAGE_FILTER, LAST_EMAIL_NOTIFICATION, " +
                "NSSN, PROVIDER, EXTERNAL_USER_NAME FROM SMS.sms_user WHERE USER_ID = :userId";

            OracleQuery query = new OracleQuery();
            query.Command = new OracleCommand(sql);
            //query.Command.Parameters = new OracleParameterCollection();
            OracleParameter userIdParam = new OracleParameter("userId", OracleDbType.Decimal);
            userIdParam.Value = userId;
            query.Command.Parameters.Add(userIdParam);

            return query;
        }

        public domain.sm.User getUserByIcn(string icn)
        {
            OracleQuery query = buildGetUserByIcnQuery(icn);
            reader executeReader = delegate() { return query.Command.ExecuteReader(); };
            OracleDataReader reader = (OracleDataReader)_cxn.query(query, executeReader);
            return toUserFromDataReader(reader);
        }

        internal OracleQuery buildGetUserByIcnQuery(string icn)
        {
            string sql = "SELECT USER_ID, FIRST_NAME, LAST_NAME, USER_TYPE, STATUS, EMAIL_ADDRESS, OPLOCK, ACTIVE, " +
                "DOB, ICN, SSN, STATION_NO, DUZ, EMAIL_NOTIFICATION, DEFAULT_MESSAGE_FILTER, LAST_EMAIL_NOTIFICATION, " +
                "NSSN, PROVIDER, EXTERNAL_USER_NAME FROM SMS.sms_user WHERE ICN = :icn";

            OracleQuery query = new OracleQuery();
            query.Command = new OracleCommand(sql);
            //query.Command.Parameters = new OracleParameterCollection();
            OracleParameter icnParam = new OracleParameter("icn", OracleDbType.Varchar2, 50);
            icnParam.Value = icn;
            query.Command.Parameters.Add(icnParam);

            return query;
        }

        // TODO - make sure we're setting the correct defaults for the User object
        internal domain.sm.User toUserFromDataReader(IDataReader rdr)
        {
            domain.sm.User user = new domain.sm.User();

            if (!rdr.Read())
            {
                throw new MdoException(MdoExceptionCode.DATA_NO_RECORD_FOR_ID);
            }

            user.Id = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("USER_ID")));
            user.FirstName = rdr.GetString(rdr.GetOrdinal("FIRST_NAME"));
            user.LastName = rdr.GetString(rdr.GetOrdinal("LAST_NAME"));
            int userType = Convert.ToInt32(rdr.GetString(rdr.GetOrdinal("USER_TYPE")));
            if (Enum.IsDefined(typeof(domain.sm.enums.UserTypeEnum), userType))
            {
                user.UserType = (domain.sm.enums.UserTypeEnum)userType;
            }
            int statusCode = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("STATUS")));
            if (Enum.IsDefined(typeof(domain.sm.enums.UserStatusEnum), statusCode))
            {
                user.Status = (domain.sm.enums.UserStatusEnum)statusCode;
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("EMAIL_ADDRESS")))
            {
                user.Email = rdr.GetString(rdr.GetOrdinal("EMAIL_ADDRESS"));
            }

            user.Oplock = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("OPLOCK")));
            user.Active = (rdr.GetDecimal(rdr.GetOrdinal("ACTIVE")) == 1);

            if (!rdr.IsDBNull(rdr.GetOrdinal("SSN")))
            {
                user.Ssn = rdr.GetString(rdr.GetOrdinal("SSN"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("STATION_NO")))
            {
                // TBD - should we map this?
                rdr.GetString(rdr.GetOrdinal("STATION_NO"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("DUZ")))
            {
                // TBD - should we map this
                rdr.GetString(rdr.GetOrdinal("DUZ"));
            }

            int emailNoticeCode = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("EMAIL_NOTIFICATION")));
            if (Enum.IsDefined(typeof(domain.sm.enums.EmailNotificationEnum), emailNoticeCode))
            {
                user.EmailNotification = (domain.sm.enums.EmailNotificationEnum)emailNoticeCode;
            }
            int messageFilterCode = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("DEFAULT_MESSAGE_FILTER")));
            if (Enum.IsDefined(typeof(domain.sm.enums.MessageFilterEnum), emailNoticeCode))
            {
                user.MessageFilter = (domain.sm.enums.MessageFilterEnum)messageFilterCode;
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("LAST_EMAIL_NOTIFICATION")))
            {
                user.LastNotification = rdr.GetDateTime(rdr.GetOrdinal("LAST_EMAIL_NOTIFICATION"));
            }
            return user;
        }

        public IList<TriageGroup> getValidRecipients(domain.sm.User user)
        {
            if (user == null || user.Id <= 0)
            {
                throw new MdoException("Invalid user");
            }

            if (user.UserType == domain.sm.enums.UserTypeEnum.PATIENT)
            {
                return getValidRecipientsForPatient(user.Id);
            }
            else if (user.UserType == domain.sm.enums.UserTypeEnum.CLINICIAN)
            {
                return getValidRecipientsForProvider(user.Id);
            }
            else
            {
                throw new MdoException("Unexpected user type: " + Enum.GetName(typeof(domain.sm.enums.UserTypeEnum), user.UserType));
            }
        }

        public IList<TriageGroup> getValidRecipients(Int32 userId)
        {
            domain.sm.User user = getUserById(userId);
            if (user == null)
            {
                throw new MdoException(MdoExceptionCode.DATA_NO_RECORD_FOR_ID);
            }
            return getValidRecipients(user);
        }

        internal IList<TriageGroup> getValidRecipientsForProvider(Int32 userId)
        {
            OracleQuery query = buildGetValidRecipientsForProviderQuery(userId);
            reader executeReader = delegate() { return query.Command.ExecuteReader(); };
            OracleDataReader reader = (OracleDataReader)_cxn.query(query, executeReader);
            return toTriageGroupsFromReader(reader);
        }

        // need to finish
        internal OracleQuery buildGetValidRecipientsForProviderQuery(Int32 userId)
        {
            string sql = "SELECT TG.TRIAGE_GROUP_ID, TG.TRIAGE_GROUP_NAME, TG.DESCRIPTION " +
                "FROM SMS.CLINICIAN_TRIAGE_MAP CTM JOIN SMS.TRIAGE_GROUP TG " +
                "ON CTM.TRIAGE_GROUP_ID=TG.TRIAGE_GROUP_ID  " +
                "JOIN SMS.PATIENT_TRIAGE_MAP USR ON TRIAGE_GROUP.TRIAGE_GROUP_ID = USR. " +
                "WHERE CTM.USER_ID = :userId AND TG.ACTIVE=1";

            OracleQuery query = new OracleQuery();
            query.Command = new OracleCommand(sql);

            OracleParameter userIdParam = new OracleParameter("userId", OracleDbType.Decimal);
            userIdParam.Value = userId;
            query.Command.Parameters.Add(userIdParam);

            return query;
        }

        internal IList<TriageGroup> getValidRecipientsForPatient(Int32 userId)
        {
            OracleQuery query = buildGetValidRecipientsForPatientQuery(userId);
            reader executeReader = delegate() { return query.Command.ExecuteReader(); };
            OracleDataReader reader = (OracleDataReader)_cxn.query(query, executeReader);
            return toTriageGroupsFromReader(reader);
        }

        // GOOD TO GO
        internal OracleQuery buildGetValidRecipientsForPatientQuery(Int32 userId)
        {
            string sql = "SELECT TG.TRIAGE_GROUP_ID, TG.TRIAGE_GROUP_NAME, TG.DESCRIPTION " +
                "FROM SMS.PATIENT_TRIAGE_MAP PTM JOIN SMS.TRIAGE_RELATION TR  " +
                "ON PTM.RELATION_ID=TR.RELATION_ID  " +
                "JOIN SMS.TRIAGE_GROUP TG ON TG.TRIAGE_GROUP_ID=TR.TRIAGE_GROUP_ID " +
                "WHERE PTM.USER_ID = :userId AND PTM.ACTIVE=1";

            OracleQuery query = new OracleQuery();
            query.Command = new OracleCommand(sql);

            OracleParameter userIdParam = new OracleParameter("userId", OracleDbType.Decimal);
            userIdParam.Value = userId;
            query.Command.Parameters.Add(userIdParam);

            return query;
        }


        internal IList<TriageGroup> toTriageGroupsFromReader(IDataReader rdr)
        {
            IList<TriageGroup> groups = new List<TriageGroup>();

            while (rdr.Read())
            {
                TriageGroup newGroup = new TriageGroup();

                newGroup.Id = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("TRIAGE_GROUP_ID")));
                newGroup.Name = rdr.GetString(rdr.GetOrdinal("TRIAGE_GROUP_NAME"));
                if (!rdr.IsDBNull(rdr.GetOrdinal("DESCRIPTION")))
                {
                    newGroup.Description = rdr.GetString(rdr.GetOrdinal("DESCRIPTION"));
                }
                //int relationTypeCode = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("RELATION_TYPE")));
                //if (Enum.IsDefined(typeof(domain.sm.enums.RelationTypeEnum), relationTypeCode))
                //{
                //    newGroup.Relations = new List<TriageRelation>();
                //    newGroup.Relations.Add(new TriageRelation() { RelationType = (domain.sm.enums.RelationTypeEnum)relationTypeCode });
                //}

                groups.Add(newGroup);
            }

            return groups;
        }

        internal IList<domain.sm.User> getTriageGroupMembers(Int32 groupId)
        {
            OracleQuery query = buildGetTriageGroupMembersQuery(groupId);
            reader executeReader = delegate() { return query.Command.ExecuteReader(); };
            OracleDataReader reader = (OracleDataReader)_cxn.query(query, executeReader);
            return toTriageGroupMembers(reader);
        }

        internal OracleQuery buildGetTriageGroupMembersQuery(Int32 groupId)
        {
            //string sql = "SELECT USER_ID, EMAIL_ADDRESS, OPLOCK, EMAIL_NOTIFICATION, LAST_EMAIL_NOTIFICATION FROM SMS.CLINICIAN_TRIAGE_MAP WHERE TRIAGE_GROUP_ID = :groupId and ACTIVE = 1";
            string sql = "SELECT CTM.USER_ID, USR.EMAIL_ADDRESS, USR.OPLOCK, USR.EMAIL_NOTIFICATION, USR.LAST_EMAIL_NOTIFICATION " +
                "FROM SMS.CLINICIAN_TRIAGE_MAP CTM JOIN SMS.SMS_USER USR ON CTM.USER_ID=USR.USER_ID " +
                "WHERE CTM.TRIAGE_GROUP_ID=:groupId AND CTM.ACTIVE=1";
            OracleQuery query = new OracleQuery();
            query.Command = new OracleCommand(sql);

            OracleParameter groupIdParam = new OracleParameter("groupId", OracleDbType.Decimal);
            groupIdParam.Value = Convert.ToDecimal(groupId);
            query.Command.Parameters.Add(groupIdParam);

            return query;
        }

        internal IList<domain.sm.User> toTriageGroupMembers(IDataReader rdr)
        {
            IList<domain.sm.User> users = new List<domain.sm.User>();

            while (rdr.Read())
            {
                domain.sm.User user = new domain.sm.User();
                user.Id = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("USER_ID")));

                if (!rdr.IsDBNull(rdr.GetOrdinal("EMAIL_ADDRESS")))
                {
                    user.Email = rdr.GetString(rdr.GetOrdinal("EMAIL_ADDRESS"));
                }

                user.Oplock = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("OPLOCK")));

                int emailNoticeCode = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("EMAIL_NOTIFICATION")));
                if (Enum.IsDefined(typeof(domain.sm.enums.EmailNotificationEnum), emailNoticeCode))
                {
                    user.EmailNotification = (domain.sm.enums.EmailNotificationEnum)emailNoticeCode;
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LAST_EMAIL_NOTIFICATION")))
                {
                    user.LastNotification = rdr.GetDateTime(rdr.GetOrdinal("LAST_EMAIL_NOTIFICATION"));
                }

                users.Add(user);
            }

            if (users.Count <= 0)
            {
                throw new MdoException("That triage group does not appear to have any members!");
            }

            return users;
        }

        internal bool updateLastEmailNotification(domain.sm.User user)
        {
            // this function should attempt to update the user's last notification date and return true if successful
            // be sure to use the OPLOCK!!!
            try
            {
                OracleQuery query = buildUpdateLastEmailNotificationQuery(user);
                nonQuery update = delegate() { return query.Command.ExecuteNonQuery(); };
                Int32 rowsAffected = (Int32)_cxn.query(query, update);

                if (rowsAffected != 1)
                {
                    return false;
                }
                user.Oplock++;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal OracleQuery buildUpdateLastEmailNotificationQuery(domain.sm.User user)
        {
            string sql = "UPDATE SMS.SMS_USER SET LAST_EMAIL_NOTIFICATION=:lastEmailNotification, OPLOCK=:oplockPlusOne WHERE USER_ID=:userId and OPLOCK=:oplock";

            OracleQuery query = new OracleQuery();
            query.Command = new OracleCommand(sql);

            OracleParameter lastEmailNotificationParam = new OracleParameter("lastEmailNotification", OracleDbType.Date);
            lastEmailNotificationParam.Value = (Oracle.DataAccess.Types.OracleDate)DateTime.Now;
            query.Command.Parameters.Add(lastEmailNotificationParam);

            OracleParameter oplockPlusOneParam = new OracleParameter("oplockPlusOne", OracleDbType.Decimal);
            oplockPlusOneParam.Value = user.Oplock + 1;
            query.Command.Parameters.Add(oplockPlusOneParam);

            OracleParameter userIdParam = new OracleParameter("userId", OracleDbType.Decimal);
            userIdParam.Value = Convert.ToDecimal(user.Id);
            query.Command.Parameters.Add(userIdParam);

            OracleParameter oplockParam = new OracleParameter("oplock", OracleDbType.Decimal);
            oplockParam.Value = Convert.ToDecimal(user.Oplock);
            query.Command.Parameters.Add(oplockParam);

            return query;
        }

    }
}
