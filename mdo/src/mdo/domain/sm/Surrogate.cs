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
    public class Surrogate : BaseModel
    {
        private Int64 _surrogateId;

        public Int64 SurrogateId
        {
            get { return _surrogateId; }
            set { _surrogateId = value; }
        }
        private User _smsUser;

        public User SmsUser
        {
            get { return _smsUser; }
            set { _smsUser = value; }
        }
        //private ParticipantTypeEnum _surrogateType;

        //public ParticipantTypeEnum SurrogateType
        //{
        //    get { return _surrogateType; }
        //    set { _surrogateType = value; }
        //}
        private DateTime _surrogateStartDate;

        public DateTime SurrogateStartDate
        {
            get { return _surrogateStartDate; }
            set { _surrogateStartDate = value; }
        }
        private DateTime _surrogateEndDate;

        public DateTime SurrogateEndDate
        {
            get { return _surrogateEndDate; }
            set { _surrogateEndDate = value; }
        }
        private bool _surrogateAllDay;

        public bool SurrogateAllDay
        {
            get { return _surrogateAllDay; }
            set { _surrogateAllDay = value; }
        }
        private Int64 _timeZone;

        public Int64 TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }
    }
}
