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
using System.Collections.Specialized;
using System.Text;

namespace gov.va.medora.mdo.dao.oracle.adr
{
    public class AdrConstants
    {
        public static string DEFAULT_CXN_STRING = vista.VistaConstants.CONFIG.AdrConnectionString;

        public const string GET_CLAIMANTS_TABLES = 
            "psim.rpt_psim_traits t," +
            "adr.person p," +
            "adr.address a," +
            "adr.phone h," +
            "sdsadm.std_phonecontacttype c," +
            "adr.email e";

        public const string GET_CLAIMANTS_FIELDS =
            "p.person_id as Id," +
            "t.last_name as LastName," +
            "t.first_name as FirstName," +
            "t.middle_name as MiddleName," +
            "t.prefix as NamePrefix," +
            "t.suffix as NameSuffix," +
            "t.gender_code as Gender," +
            "t.ssn," +
            "t.date_of_birth as DOB," +
            "a.address_line1 as Street1," +
            "a.address_line2 as Street2," +
            "a.address_line3 as Street3," +
            "a.city as City," +
            "a.state_code as State," +
            "a.county_code as County," +
            "a.zip_code as Zipcode," +
            "a.zip_plus_4 as ZipSuffix," +
            "a.postal_code as PostalCode," +
            "c.name as PhoneType," +
            "h.phone_number as PhoneNumber," +
            "e.email_address as Email";

        public const string GET_CLAIMANTS_WHERE =
            "p.vpid_id=t.vpid_id and " +
            "p.person_id=a.person_id (+) and " +
            "p.person_id=h.person_id (+) and " +
            "p.person_id=e.person_id (+) and " +
            "h.phone_type_id=c.id";

    }
}
