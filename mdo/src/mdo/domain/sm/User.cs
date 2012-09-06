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
using gov.va.medora.mdo.domain.sm.enums;

namespace gov.va.medora.mdo.domain.sm
{
    public class User : BaseModel
    {
        public ParticipantTypeEnum ParticipantType { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public List<TriageGroup> UserAssociatedGroups { get; set; }

        public string Email { get; set; }
        //list of groups that the actor belongs 
        public List<TriageGroup> Groups { get; set; }
        public UserStatusEnum Status { get; set; }
        public EmailNotificationEnum EmailNotification { get; set; }
        public MessageFilterEnum MessageFilter { get; set; }
        public DateTime LastNotification { get; set; }
        public string Ssn { get; set; }
        public string Nssn { get; set; }

        public Mailbox Mailbox { get; set; }

        public List<AdminRole> AdminRoles { get; set; }


        public User()
        {
            // set some acceptable defaults - these defaults came from MHV code
            EmailNotification = EmailNotificationEnum.NONE;
            Status = UserStatusEnum.OPT_OUT;
            MessageFilter = MessageFilterEnum.ALL;
        }

        public string getName()
        {
            return new StringBuilder().Append(LastName).Append(", ").Append(FirstName).ToString();
        }
    }
}
