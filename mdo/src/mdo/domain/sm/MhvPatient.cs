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
    public class MhvPatient : BaseModel
    {
        private string _userName;

        protected string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _lastName;

        protected string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _firstName;

        protected string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _middleName;

        protected string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        private string _email;

        protected string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _ssn;

        protected string Ssn
        {
            get { return _ssn; }
            set { _ssn = value; }
        }
        private string _nssn;

        protected string Nssn
        {
            get { return _nssn; }
            set { _nssn = value; }
        }

        private DateTime _dob;

        protected DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        private string _icn;

        protected string Icn
        {
            get { return _icn; }
            set { _icn = value; }
        }

        private string _facility;

        protected string Facility
        {
            get { return _facility; }
            set { _facility = value; }
        }
    }
}
