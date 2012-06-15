using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwAccount : AbstractAccount
    {
        // TBD - once we get some service accounts, should we verify them somehow?
        
        public override string authenticate(AbstractCredentials credentials, DataSource validationDataSource = null)
        {
            this.isAuthorized = true;
            this.isAuthenticated = true;
            return "OK";
        }

        public override User authorize(AbstractCredentials credentials, AbstractPermission permission)
        {
            this.isAuthorized = true;
            this.isAuthenticated = true;
            return new User();
        }

        public override User authenticateAndAuthorize(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource = null)
        {
            this.isAuthorized = true;
            this.isAuthenticated = true;
            return new User();
        }
    }
}
