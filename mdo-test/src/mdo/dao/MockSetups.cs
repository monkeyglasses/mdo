using System;
using System.Collections.Generic;
using System.Text;
using gov.va.medora.mdo.dao.vista;
using Spring.Context;
using Spring.Context.Support;

namespace gov.va.medora.mdo.dao
{
    public class MockSetups
    {
        public static AbstractConnection authorizedConnect(string theUser, bool isBse)
        {
            string securityPhrase = (isBse ? "" : "NON-BSE");
            MockApp theApp = getTheApp(theUser, securityPhrase);
            DataSource src = getSrc(theApp.SiteTable, theApp.LoginSitecode);
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(src.Protocol));
            AbstractConnection cxn = f.getConnection(src);
            AbstractCredentials credentials = getVisitCredentials(theApp);
            AbstractPermission permission = new MenuOption(theApp.User.PermissionString);
            theApp.User = (User)cxn.authorizedConnect(credentials, permission, null);
            return cxn;
        }

        internal static User getTestUser(string userTag)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            StringTestObject testObj = (StringTestObject)ctx.GetObject(userTag);
            User user = new User();
            user.Uid = testObj.get("userDUZ");
            user.Name = new PersonName(testObj.get("userName"));
            user.SSN = new SocSecNum(testObj.get("userSSN"));
            user.LogonSiteId = new SiteId(testObj.get("siteCode"), testObj.get("siteName"));
            user.UserName = testObj.get("accessCode");
            user.Pwd = testObj.get("verifyCode");
            user.PermissionString = testObj.get("context");
            return user;
        }

        internal static MockApp getTheApp()
        {
            return getTheApp("MockUser", "");
        }

        // Need to Spring this stuff...
        internal static MockApp getTheApp(string theUser, string securityPhrase)
        {
            MockApp app = new MockApp();
            //app.SiteTable = new SiteTable("../../resources/xml/VhaSites.xml");
            app.User = getTestUser(theUser);
            app.LoginSitecode = app.User.LogonSiteId.Id;
            app.AccessCode = app.User.UserName;
            app.VerifyCode = app.User.Pwd;
            app.Context = app.User.PermissionString;
            app.SecurityPhrase = securityPhrase;
            app.RemoteSitecodes = new List<string>(2);
            app.RemoteSitecodes.Add("515");
            app.RemoteSitecodes.Add("553");
            return app;
        }

        internal static DataSource getSrc(SiteTable t, string id)
        {
            // Need to clone a new source for test suites that might change the
            // properties.
            DataSource theSource = new DataSource();
            DataSource result = new DataSource();
            result.Modality = "HIS";
            result.Port = theSource.Port;
            result.Protocol = theSource.Protocol;
            result.SiteId = new SiteId(id);
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
