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
using System.Linq;
using System.Text;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwDaoFactory : AbstractDaoFactory
    {
        public CdwDaoFactory()
        {

        }
        public override AbstractConnection getConnection(DataSource dataSource)
        {
            return new CdwConnection(dataSource);
        }

        public override IUserDao getUserDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IPatientDao getPatientDao(AbstractConnection cxn)
        {
            return new CdwPatientDao(cxn);
        }

        public override IClinicalDao getClinicalDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IEncounterDao getEncounterDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IPharmacyDao getPharmacyDao(AbstractConnection cxn)
        {
            return new CdwPharmacyDao(cxn);
        }

        public override ILabsDao getLabsDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IToolsDao getToolsDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override INoteDao getNoteDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IVitalsDao getVitalsDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IChemHemDao getChemHemDao(AbstractConnection cxn)
        {
            return new CdwChemHemDao(cxn);
        }

        public override IClaimsDao getClaimsDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IConsultDao getConsultDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IRemindersDao getRemindersDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override ILocationDao getLocationDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IOrdersDao getOrdersDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }

        public override IRadiologyDao getRadiologyDao(AbstractConnection cxn)
        {
            throw new NotImplementedException();
        }
    }
}
