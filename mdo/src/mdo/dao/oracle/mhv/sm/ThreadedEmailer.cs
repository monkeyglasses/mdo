using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gov.va.medora.mdo.domain.sm;

namespace gov.va.medora.mdo.dao.oracle.mhv.sm
{
    public class ThreadedEmailer
    {
        public IList<gov.va.medora.mdo.domain.sm.User> Users { get; set; }
        public IList<gov.va.medora.mdo.domain.sm.Addressee> Addressees { get; set; }

        MdoOracleConnection _cxn;

        public ThreadedEmailer(AbstractConnection cxn) 
        {
            setConnection(cxn);
        }

        public ThreadedEmailer(AbstractConnection cxn, IList<gov.va.medora.mdo.domain.sm.User> users)
        {
            setConnection(cxn);
            this.Users = users;
        }

        public ThreadedEmailer(AbstractConnection cxn, IList<gov.va.medora.mdo.domain.sm.Addressee> addressees)
        {
            setConnection(cxn);
            this.Addressees = addressees;
        }

        // this class should use it's own DB connection since the cxn passed in may be disposed when wrapped in a 'using(AbstractConnection)' block
        void setConnection(AbstractConnection cxn)
        {
            _cxn = new MdoOracleConnection(new DataSource() { ConnectionString = cxn.DataSource.ConnectionString });
        }

        public void emailAllAsync()
        {
            System.Threading.Thread emailer = new System.Threading.Thread(new System.Threading.ThreadStart(emailAll));
            emailer.Start();
        }

        internal void emailAll()
        {
            try
            {
                if (this.Users != null && this.Users.Count > 0)
                {
                    foreach (gov.va.medora.mdo.domain.sm.User user in this.Users)
                    {
                        if (shouldSend(user))
                        {
                            sendEmail(user.Email);
                        }
                    }
                }
                else if (this.Addressees != null && this.Addressees.Count > 0)
                {
                    foreach (Addressee addressee in this.Addressees)
                    {
                        if (shouldSend(addressee.Owner))
                        {
                            sendEmail(addressee.Owner.Email);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //swallow for now - TBD: should we log failures?
            }
        }

        internal bool shouldSend(domain.sm.User user)
        {
            if (String.IsNullOrEmpty(user.Email))
            {
                return false;
            }
            if (user.EmailNotification == domain.sm.enums.EmailNotificationEnum.NONE)
            {
                return false;
            }
            if (user.EmailNotification == domain.sm.enums.EmailNotificationEnum.ONE_DAILY && DateTime.Now.Subtract(user.LastNotification).TotalDays > 1)
            {
                return new UserDao(_cxn).updateLastEmailNotification(user);
            }
            if (user.EmailNotification == domain.sm.enums.EmailNotificationEnum.EACH_MESSAGE || user.EmailNotification == domain.sm.enums.EmailNotificationEnum.ON_ASSIGNMENT)
            {
                return new UserDao(_cxn).updateLastEmailNotification(user);
            }
            else
            {
                return false;
            }
        }

        // TODO - need to read these settings from the config file
        public void sendEmail(string to)
        {
            System.Diagnostics.Debug.WriteLine("Sending an email to " + to + " on behalf of SM...");
            try
            {
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.va.gov", 25);
                smtp.Send("MHVSecureMessaging@va.gov", to, "You have a new Secure Message", "Please login to MHV to view your message");
            }
            catch (Exception) { }
        }
    }
}
