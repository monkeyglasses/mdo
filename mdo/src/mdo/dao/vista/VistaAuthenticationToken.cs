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

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaAuthenticationToken : AbstractAuthenticationToken
    {
        //string sitecode;
        //string sitename;
        //string duz;
        //SocSecNum ssn;
        //PersonName username;
        //string phone;
        //const char delimiter = '_';

        public static string NATIONAL_ID = "NationalId";
        public static string LOCAL_ID = "LocalId";
        public static string SITE_ID = "SiteId";
        public static string SITE_NAME = "SiteName";
        public static string USER_NAME = "UserName";
        public static string PHONE = "Phone";

        //public string Sitecode
        //{
        //    get { return Items["sitecode"]; }
        //    set { sitecode = value; }
        //}

        //public string Sitename
        //{
        //    get { return sitename; }
        //    set { sitename = value; }
        //}

        //public string LocalId
        //{
        //    get { return duz; }
        //    set { duz = value; }
        //}

        //public string NationalId
        //{
        //    get { return ssn.toString(); }
        //    set { ssn = new SocSecNum(value); }
        //}

        //public string Username
        //{
        //    get { return username.getLastNameFirst(); }
        //    set { username = new PersonName(value); }
        //}

        //public string Phone
        //{
        //    get { return phone; }
        //    set { phone = value; }
        //}

        //public string SecurityToken
        //{
        //    get{return sitecode + delimiter + duz;}
        //}

    }
}
