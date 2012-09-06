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

namespace gov.va.medora.mdo.domain.sm
{
    [Serializable]
    public class Thread : BaseModel
    {
        private domain.sm.enums.MessageCategoryTypeEnum _messageCategoryType;

        public domain.sm.enums.MessageCategoryTypeEnum MessageCategoryType
        {
            get { return _messageCategoryType; }
            set { _messageCategoryType = value; }
        }

        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        private List<Message> _messages;

        public List<Message> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }
        private List<Annotation> _annotations;

        public List<Annotation> Annotations
        {
            get { return _annotations; }
            set { _annotations = value; }
        }
        private TriageGroup _mailGroup;

        public TriageGroup MailGroup
        {
            get { return _mailGroup; }
            set { _mailGroup = value; }
        }
        //private MessageCategoryTypeEnum _messageCategoryType;

        //public MessageCategoryTypeEnum MessageCategoryType
        //{
        //    get { return _messageCategoryType; }
        //    set { _messageCategoryType = value; }
        //}

        internal static Thread getThreadFromReader(System.Data.IDataReader rdr)
        {
            return getThreadFromReader(rdr, mdo.dao.oracle.mhv.sm.QueryUtils.getColumnExistsTable(gov.va.medora.mdo.dao.oracle.mhv.sm.TableSchemas.MESSAGE_THREAD_COLUMNS, rdr));
        }

        internal static Thread getThreadFromReader(System.Data.IDataReader rdr, Dictionary<string, bool> columnTable)
        {
            Thread thread = new Thread();

            if (columnTable["THREAD_ID"])
            {
                thread.Id = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("THREAD_ID")));
            }
            if (columnTable["SUBJECT"])
            {
                thread.Subject = rdr.GetString(rdr.GetOrdinal("SUBJECT"));
            }
            if (columnTable["TRIAGE_GROUP_ID"])
            {
                if (!rdr.IsDBNull(rdr.GetOrdinal("TRIAGE_GROUP_ID")))
                {
                    thread.MailGroup = new TriageGroup() { Id = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("TRIAGE_GROUP_ID"))) };
                }
            }
            if (columnTable["MTOPLOCK"])
            {
                thread.Oplock = Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("MTOPLOCK")));
            }
            if (columnTable["CATEGORY_TYPE"])
            {
                thread.MessageCategoryType = (enums.MessageCategoryTypeEnum)Convert.ToInt32(rdr.GetDecimal(rdr.GetOrdinal("CATEGORY_TYPE")));
            }

            return thread;
        }
    }
}
