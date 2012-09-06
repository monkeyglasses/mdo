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
    public class MhvAuthenticationSubject
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _icn;

        public string Icn
        {
            get { return _icn; }
            set { _icn = value; }
        }
        private string _ssn;

        public string Ssn
        {
            get { return _ssn; }
            set { _ssn = value; }
        }
        private DateTime _dob;

        public DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        private bool _authenticated;

        public bool Authenticated
        {
            get { return _authenticated; }
            set { _authenticated = value; }
        }
        private bool _national;

        public bool National
        {
            get { return _national; }
            set { _national = value; }
        }
        private string _checksum;

        public string Checksum
        {
            get { return _checksum; }
            set { _checksum = value; }
        }
        private Int64 _timestamp;

        public Int64 Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        private string[] _facilities;

        public string[] Facilities
        {
            get { return _facilities; }
            set { _facilities = value; }
        }
        private string[] _visns;

        public string[] Visns
        {
            get { return _visns; }
            set { _visns = value; }
        }
        private bool _requiresCredentials;

        public bool RequiresCredentials
        {
            get { return _requiresCredentials; }
            set { _requiresCredentials = value; }
        }
    }
}
