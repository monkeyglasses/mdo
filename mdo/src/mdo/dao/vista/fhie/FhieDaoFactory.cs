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

namespace gov.va.medora.mdo.dao.vista.fhie
{
    class FhieDaoFactory : AbstractDaoFactory
    {
        public override AbstractConnection getConnection(DataSource dataSource)
        {
            VistaConnection c = new VistaConnection(dataSource);
            c.ConnectStrategy = new VistaNatConnectStrategy(c);
            return c;
        }

        public override IToolsDao getToolsDao(AbstractConnection cxn)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IUserDao getUserDao(AbstractConnection cxn)
        {
            return new FhieUserDao(cxn);
        }

        public override IPatientDao getPatientDao(AbstractConnection cxn)
        {
            return new FhiePatientDao(cxn);
        }

        public override IClinicalDao getClinicalDao(AbstractConnection cxn)
        {
            return new FhieClinicalDao(cxn);
        }

        public override IEncounterDao getEncounterDao(AbstractConnection cxn)
        {
            return null;
        }

        public override IPharmacyDao getPharmacyDao(AbstractConnection cxn)
        {
            return new FhiePharmacyDao(cxn);
        }

        public override ILabsDao getLabsDao(AbstractConnection cxn)
        {
            return new FhieLabsDao(cxn);
        }

        public override INoteDao getNoteDao(AbstractConnection cxn)
        {
            return new FhieNoteDao(cxn);
        }

        public override IVitalsDao getVitalsDao(AbstractConnection cxn)
        {
            return new FhieVitalsDao(cxn);
        }

        public override IChemHemDao getChemHemDao(AbstractConnection cxn)
        {
            return new FhieChemHemDao(cxn);
        }

        public override IClaimsDao getClaimsDao(AbstractConnection cxn)
        {
            return null;
        }

        public override IConsultDao getConsultDao(AbstractConnection cxn)
        {
            return null;
        }

        public override IRemindersDao getRemindersDao(AbstractConnection cxn)
        {
            return null;
        }

        public override ILocationDao getLocationDao(AbstractConnection cxn)
        {
            return null;
        }

        public override IOrdersDao getOrdersDao(AbstractConnection cxn)
        {
            return null;
        }

        public override IRadiologyDao getRadiologyDao(AbstractConnection cxn)
        {
            return new VistaRadiologyDao(cxn);
        }

    }
}
