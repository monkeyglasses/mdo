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

namespace gov.va.medora.mdo
{
    public class Address
    {
        string street1;
        string street2;
        string street3;
        string city;
        string county;
        string state;
        string zip;

        public string Street1
        {
            get { return street1; }
            set { street1 = value; }
        }


        public string Street2
        {
            get { return street2; }
            set { street2 = value; }
        }

        public string Street3
        {
            get { return street3; }
            set { street3 = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string County
        {
            get { return county; }
            set { county = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Zipcode
        {
            get { return zip; }
            set { zip = value; }
        }

        /// <summary>
        /// A CRLF delimited string of address fields
        /// </summary>
        /// <returns>
        /// Street1
        /// Street2
        /// Street3
        /// City
        /// County
        /// State
        /// Zipcode
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(street1);
            sb.Append(Environment.NewLine);
            sb.Append(street2);
            sb.Append(Environment.NewLine);
            sb.Append(street3);
            sb.Append(Environment.NewLine);
            sb.Append(city);
            sb.Append(Environment.NewLine);
            sb.Append(county);
            sb.Append(Environment.NewLine);
            sb.Append(state);
            sb.Append(Environment.NewLine);
            sb.Append(zip);
            return sb.ToString();
        }

        /// <summary>
        /// Call ToString() on both objects and verify equality
        /// </summary>
        /// <param name="obj">Address</param>
        /// <returns>True if address strings are equal</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Address))
            {
                return false;
            }
            Address temp = obj as Address;
            if (String.Equals(temp.ToString(), this.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calls ToString().GetHashCode() to retrieve the hashcode of the string representation of an Address object
        /// </summary>
        /// <returns>Int32 HashCode</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
