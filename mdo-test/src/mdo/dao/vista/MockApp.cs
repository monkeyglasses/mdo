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
    public class MockApp
    {
        SiteTable siteTable;
        string loginSitecode;
        string lookupSitecode;
        List<string> remoteSitecodes;
        string accessCode;
        string verifyCode;
        string context;
        string phrase;
        User user;

        public MockApp() { }

        public SiteTable SiteTable
        {
            get { return siteTable; }
            set { siteTable = value; }
        }

        public string LoginSitecode
        {
            get { return loginSitecode; }
            set { loginSitecode = value; }
        }

        public string LookupSitecode
        {
            get { return lookupSitecode; }
            set { lookupSitecode = value; }
        }

        public List<string> RemoteSitecodes
        {
            get { return remoteSitecodes; }
            set { remoteSitecodes = value; }
        }

        public string AccessCode
        {
            get { return accessCode; }
            set { accessCode = value; }
        }

        public string VerifyCode
        {
            get { return verifyCode; }
            set { verifyCode = value; }
        }

        public string Context
        {
            get { return context; }
            set { context = value; }
        }

        public string SecurityPhrase
        {
            get { return phrase; }
            set { phrase = value; }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }
    }
}
