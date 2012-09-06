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
using Spring.Context;
using Spring.Context.Support;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaSetups
    {
        public static AbstractConnection authorizedConnect(string theUser, bool isBse)
        {
            string securityPhrase = (isBse ? "" : "NON-BSE");
            MockApp theApp = getTheApp(securityPhrase);
            DataSource src = VistaSetups.getSrc(theApp.SiteTable, theApp.LoginSitecode);
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(src.Protocol));
            AbstractConnection cxn = f.getConnection(src);
            AbstractCredentials credentials = getVisitCredentials(theApp);
            AbstractPermission permission = new MenuOption(theApp.User.PermissionString);
            theApp.User = (User)cxn.authorizedConnect(credentials, permission, null);
            return cxn;
        }

        internal static User getTestUser()
        {
            User user = new User();
            user.Uid = "1";
            user.Name = new PersonName("USER,VISTA");
            user.SSN = new SocSecNum("999999907");
            user.LogonSiteId = new SiteId("700", "Local Mock Vista");
            user.UserName = "TestUserAccessCode";
            user.Pwd = "TestUserVerifyCode";
            user.PermissionString = "OR CPRS GUI CHART";
            return user;
        }

        // Need to Spring this stuff...
        internal static MockApp getTheApp(string securityPhrase)
        {
            MockApp app = new MockApp();
            app.SiteTable = new SiteTable("../../resources/xml/VhaSites.xml");
            app.User = getTestUser();
            app.LoginSitecode = app.User.LogonSiteId.Id;
            app.AccessCode = app.User.UserName;
            app.VerifyCode = app.User.Pwd;
            app.Context = app.User.PermissionString;
            app.SecurityPhrase = securityPhrase;
            app.RemoteSitecodes = new List<string>(4);
            app.RemoteSitecodes.Add("701");
            app.RemoteSitecodes.Add("702");
            app.RemoteSitecodes.Add("703");
            app.RemoteSitecodes.Add("800");
            return app;
        }

        internal static DataSource getSrc(SiteTable t, string id)
        {
            // Need to clone a new source for test suites that might change the
            // properties.
            DataSource theSource = t.getSite(id).getDataSourceByModality("HIS");
            DataSource result = new DataSource();
            result.Modality = "HIS";
            result.Port = theSource.Port;
            result.Protocol = theSource.Protocol;
            result.SiteId = theSource.SiteId;
            result.Provider = theSource.Provider;
            return result;
        }

        public static AbstractCredentials getVisitCredentials(MockApp theApp)
        {
            // Everything but the security phrase.
            AbstractCredentials result = new VistaCredentials();
            result.AuthenticationSource = new DataSource();
            result.AuthenticationSource.SiteId = new SiteId(theApp.User.LogonSiteId.Id, theApp.User.LogonSiteId.Name);
            result.LocalUid = theApp.User.Uid;
            result.FederatedUid = theApp.User.SSN.toString();
            result.SubjectName = theApp.User.Name.getLastNameFirst();
            result.SubjectPhone = theApp.User.Phone;
            return result;
        }

    }
}
