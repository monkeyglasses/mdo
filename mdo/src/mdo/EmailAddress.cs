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
using System.Text;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo
{
    public class EmailAddress
    {
        string username;
        string hostname;
        string addr;

        public EmailAddress() { }

        public EmailAddress(string username, string hostname)
        {
            Username = username;
            Hostname = hostname;
        }

        public EmailAddress(string emailAddress)
        {
            addr = emailAddress;
        }

        public string Address
        {
            get { return addr; }
            set { addr = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Hostname
        {
            get { return hostname; }
            set { hostname = value; }
        }

        internal void split(string emailAddress)
        {
            string[] parts = emailAddress.Split(new char[] { '@' });
            Username = parts[0];
            Hostname = parts[1];
        }

        public static bool isValid(string emailAddress)
        {
            if (String.IsNullOrEmpty(emailAddress))
            {
                return false;
            }
            if (emailAddress.IndexOf('@') == -1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check whether two email addresses are equal
        /// </summary>
        /// <param name="obj">EmailAddress</param>
        /// <returns>True if emails are the same (case insensitive)</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is EmailAddress))
            {
                return false;
            }
            EmailAddress temp = (EmailAddress)obj;
            if (String.Equals(temp.Address, this.addr, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the email address string
        /// </summary>
        /// <returns>email address</returns>
        public override string ToString()
        {
            return addr ?? "";
        }

        /// <summary>
        /// Calls ToString().GetHashCode() to retrieve the hashcode of the string representation of an EmailAddress object
        /// </summary>
        /// <returns>Int32 HashCode</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
