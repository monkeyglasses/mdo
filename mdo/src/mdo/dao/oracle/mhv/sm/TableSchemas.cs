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

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    public static class TableSchemas
    {
        public static IList<string> SECURE_MESSAGE_COLUMNS = new List<string>()
        {
            "SECURE_MESSAGE_ID",
            "CLINICIAN_STATUS",
            "COMPLETED_DATE",
            "ASSIGNED_TO",
            "CHECKSUM",
            "THREAD_ID",
            "STATUS_SET_BY",
            "SMOPLOCK", // remember - all shared column names should be prefixed with the alias used in our SQL statments
            "SMACTIVE",
            "SMCREATED_DATE",
            "SMMODIFIED_DATE",
            "ESCALATED",
            "BODY",
            "SENT_DATE",
            "SENDER_TYPE",
            "SENDER_ID",
            "SENDER_NAME",
            "RECIPIENT_TYPE",
            "RECIPIENT_ID",
            "RECIPIENT_NAME",
            "SENT_DATE_LOCAL",
            "ESCALATION_NOTIFICATION_DATE",
            "ESCALATION_NOTIFICATION_TRIES",
            "READ_RECEIPT",
            "HAS_ATTACHMENT",
            "ATTACHMENT_ID"
        };

        public static IList<string> MESSAGE_THREAD_COLUMNS = new List<string>()
        {
            "THREAD_ID",
            "SUBJECT",
            "TRIAGE_GROUP_ID",
            "MTOPLOCK", // remember - all shared column names should be prefixed with the alias used in our SQL statments
            "MTACTIVE",
            "MTCREATED_DATE",
            "MTMODIFIED_DATE",
            "CATEGORY_TYPE"
        };

        public static IList<string> ADDRESSEE_COLUMNS = new List<string>()
        {
            "ADDRESSEE_ID",
            "ADDRESSEE_ROLE",
            "SECURE_MESSAGE_ID",
            "USER_ID",
            "ADDROPLOCK",
            "ADDRACTIVE",
            "ADDRCREATED_DATE",
            "ADDRMODIFIED_DATE",
            "FOLDER_ID",
            "READ_DATE",
            "REMINDER_DATE"
        };

        public static IList<string> FOLDER_COLUMNS = new List<string>()
        {
            "FOLDER_ID",
            "USER_ID",
            "FOLDER_NAME",
            "FOLDOPLOCK",
            "FOLDACTIVE",
            "FOLDCREATED_DATE",
            "FOLDMODIFIED_DATE"
        };

        public static IList<string> MESSAGE_ATTACHMENT_COLUMNS = new List<string>()
        {
            "ATTACHMENT_ID",
            "ATTACHMENT_NAME",
            "ATTACHMENT",
            "MIME_TYPE",
            "ATTOPLOCK"
        };
    }
}
