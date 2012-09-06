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
using gov.va.medora.mdo.dao.vista;

namespace gov.va.medora.mdo.dao
{
    public class MockDaoFactory : AbstractDaoFactory
    {
        public override AbstractConnection getConnection(DataSource dataSource)
        {
            if (dataSource == null || dataSource.SiteId == null || String.IsNullOrEmpty(dataSource.SiteId.Id))
            {
                throw new ArgumentNullException("MockConnection must have a data source");
            }
            MockConnection cxn = new MockConnection(dataSource.SiteId.Id, dataSource.Protocol);
            cxn.OverrideMockFile = "MDWS";
            cxn.VerifyRpc = false;
            cxn.ConnectStrategy = new MockConnectStrategy(cxn);
            return cxn;
        }

        public override IUserDao getUserDao(AbstractConnection cxn)
        {
            return new VistaUserDao(cxn);
        }

        public override IPatientDao getPatientDao(AbstractConnection cxn)
        {
            return new VistaPatientDao(cxn);
        }

        public override IChemHemDao getChemHemDao(AbstractConnection cxn)
        {
            return new VistaChemHemDao(cxn);
        }

        public override IClinicalDao getClinicalDao(AbstractConnection cxn)
        {
            return new VistaClinicalDao(cxn);
        }

        public override IEncounterDao getEncounterDao(AbstractConnection cxn)
        {
            return new VistaEncounterDao(cxn);
        }

        public override IPharmacyDao getPharmacyDao(AbstractConnection cxn)
        {
            return new VistaPharmacyDao(cxn);
        }

        public override ILabsDao getLabsDao(AbstractConnection cxn)
        {
            return new VistaLabsDao(cxn);
        }

        public override IToolsDao getToolsDao(AbstractConnection cxn)
        {
            return new VistaToolsDao(cxn);
        }

        public override INoteDao getNoteDao(AbstractConnection cxn)
        {
            return new VistaNoteDao(cxn);
        }

        public override IVitalsDao getVitalsDao(AbstractConnection cxn)
        {
            return new VistaVitalsDao(cxn);
        }

        public override IClaimsDao getClaimsDao(AbstractConnection cxn)
        {
            return new VistaClaimsDao(cxn);
        }

        public override IConsultDao getConsultDao(AbstractConnection cxn)
        {
            return new VistaConsultDao(cxn);
        }

        public override IOrdersDao getOrdersDao(AbstractConnection cxn)
        {
            return new VistaOrdersDao(cxn);
        }

        public override ILocationDao getLocationDao(AbstractConnection cxn)
        {
            return new VistaLocationDao(cxn);
        }

        public override IRemindersDao getRemindersDao(AbstractConnection cxn)
        {
            return new VistaRemindersDao(cxn);
        }

        public static new AbstractDaoFactory getDaoFactory(int protocol)
        {
            return new MockDaoFactory();
        }

        public override IRadiologyDao getRadiologyDao(AbstractConnection cxn)
        {
            return new VistaRadiologyDao(cxn);
        }

    }
}
