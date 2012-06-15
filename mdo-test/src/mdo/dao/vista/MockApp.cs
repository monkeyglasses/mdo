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
