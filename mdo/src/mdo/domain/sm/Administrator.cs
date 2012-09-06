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
    public class Administrator : gov.va.medora.mdo.domain.sm.User
    {
        // national administrator?
        public bool National { get; set; }

        // list of facilities that this administrator can manipulate
        public List<Facility> Facilities { get; set; }

        // list of VISNs that this administrator can manipulate
        private List<Facility> Visns { get; set; }

        public Administrator()
        {
            //super();
            UserType = UserTypeEnum.ADMINISTRATOR;
            //ParticipantType = null;
        }

    }
}
