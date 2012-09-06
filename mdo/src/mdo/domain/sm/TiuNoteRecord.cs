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
    public class TiuNoteRecord : BaseModel
    {
        private string _vistaDiv;

        public string VistaDiv
        {
            get { return _vistaDiv; }
            set { _vistaDiv = value; }
        }
        private Int64 _threadId;

        public Int64 ThreadId
        {
            get { return _threadId; }
            set { _threadId = value; }
        }
        private Int64 _lastMessageId;

        public Int64 LastMessageId
        {
            get { return _lastMessageId; }
            set { _lastMessageId = value; }
        }
        private DateTime _lockedDate;

        public DateTime LockedDate
        {
            get { return _lockedDate; }
            set { _lockedDate = value; }
        }
        private string _conversationId;

        public string ConversationId
        {
            get { return _conversationId; }
            set { _conversationId = value; }
        }
        private DateTime _noteCreationDate;

        public DateTime NoteCreationDate
        {
            get { return _noteCreationDate; }
            set { _noteCreationDate = value; }
        }
        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}
