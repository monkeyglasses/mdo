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
using System.Collections.Specialized;

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieChemHemDao : IChemHemDao
    {
        AbstractConnection cxn;

        VistaChemHemDao vistaDao = null;

        public FhieChemHemDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
            vistaDao = new VistaChemHemDao(cxn);
        }

        public ChemHemReport[] getChemHemReports(string fromDate, string toDate)
        {
            return getChemHemReports(cxn.Pid, fromDate, toDate);
        }

        public ChemHemReport[] getChemHemReports(string dfn, string fromDate, string toDate)
        {
            return vistaDao.getChemHemReports(dfn, fromDate, toDate, 1000);
        }

        public ChemHemReport getChemHemReport(string dfn, ref string nextDate)
        {
            throw new NotImplementedException();
        }


        public Dictionary<string, HashSet<string>> getNewChemHemReports(DateTime start)
        {
            throw new NotImplementedException();
        }
    }
}
