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

namespace gov.va.medora.mdo.dao.oracle.vbacorp
{
    public class VbacorpConstants
    {
        public static string DEFAULT_CXN_STRING = vista.VistaConstants.CONFIG.VbaCorpConnectionString;

        public const string GET_CLAIMANTS_TABLES =
            "corpprod.person p," +
            "corpprod.ptcpnt_addrs a," +
            "corpprod.ptcpnt_phone h";

        public const string GET_CLAIMANTS_FIELDS =
            "p.ptcpnt_id as Id," +                       
            "p.last_nm as LastName," +
            "p.first_nm as FirstName," +
            "p.middle_nm as MiddleName," +
            "to_char(p.brthdy_dt,'YYYYMMDD') as DOB," +
            "p.gender_cd as Gender," +
            "p.ssn_nbr as SSN," +
            "a.addrs_one_txt as Street1," +
            "a.addrs_two_txt as Street2," +
            "a.addrs_three_txt as Street3," +
            "a.city_nm as City," +
            "a.county_nm as County," +
            "a.zip_prefix_nbr as Zipcode," +
            "a.zip_first_suffix_nbr as ZipSuffix1," +
            "a.zip_second_suffix_nbr as ZipSuffix2," +
            "a.postal_cd as State," +
            "a.email_addrs_txt as Email," +
            "h.phone_type_nm as PhoneType," +
            "h.phone_nbr as PhoneNumber," +
            "h.area_nbr as AreaCode," +
            "h.extnsn_nbr as Extension";

        public const string GET_CLAIMANTS_WHERE =
            "p.ptcpnt_id=a.ptcpnt_id (+) and " +
            "p.ptcpnt_id=h.ptcpnt_id (+)"; 

    }
}
