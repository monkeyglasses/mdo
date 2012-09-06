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
    public class NewMessage
    {
        private MailParticipant _from;

        internal MailParticipant From
        {
            get { return _from; }
            set { _from = value; }
        }
        private MailParticipant _to;

        internal MailParticipant To
        {
            get { return _to; }
            set { _to = value; }
        }
        private MailParticipant _cc;

        internal MailParticipant Cc
        {
            get { return _cc; }
            set { _cc = value; }
        }
        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        private string _body;

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
        private TriageGroup _triageGroup;

        public TriageGroup TriageGroup
        {
            get { return _triageGroup; }
            set { _triageGroup = value; }
        }
        private bool _draft;

        public bool Draft
        {
            get { return _draft; }
            set { _draft = value; }
        }
        private Int64 _attachmentId;

        public Int64 AttachmentId
        {
            get { return _attachmentId; }
            set { _attachmentId = value; }
        }
        private string _attachmentName;

        public string AttachmentName
        {
            get { return _attachmentName; }
            set { _attachmentName = value; }
        }
        private Int64 _messageCategoryTypeId;

        public Int64 MessageCategoryTypeId
        {
            get { return _messageCategoryTypeId; }
            set { _messageCategoryTypeId = value; }
        }
    }
}
