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
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieVitalsDao : IVitalsDao
    {
        AbstractConnection cxn = null;
        VistaVitalsDao vistaDao = null;

        public FhieVitalsDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
            vistaDao = new VistaVitalsDao(cxn);
        }

        public VitalSignSet[] getVitalSigns()
        {
            return getVitalSigns(cxn.Pid);
        }

        public VitalSignSet[] getVitalSigns(string dfn)
        {
            MdoQuery mq = buildGetVitalSignsRequest(dfn);
            string response = (string)cxn.query(mq);
            return vistaDao.toVitalSignsFromRdv(response);
        }

        internal MdoQuery buildGetVitalSignsRequest(string dfn)
        {
            return VistaUtils.buildReportTextRequest_AllResults(dfn, "OR_DODVS:VITAL SIGNS~VS;ORDV04;47;");
        }

        public VitalSignSet[] getVitalSigns(string fromDate, string toDate, int maxRex)
        {
            return getVitalSigns(cxn.Pid, fromDate,toDate, maxRex);
        }

        public VitalSignSet[] getVitalSigns(string dfn, string fromDate, string toDate, int maxRex)
        {
            string request = buildGetVitalSignsRequest(dfn, fromDate, toDate, maxRex);
            string response = (string)cxn.query(request);
            return vistaDao.toVitalSignsFromRdv(response);
        }

        internal string buildGetVitalSignsRequest(string dfn, string fromDate, string toDate, int maxRex)
        {
            return VistaUtils.buildReportTextRequest(dfn, fromDate, toDate, maxRex, "OR_DODVS:VITAL SIGNS~VS;ORDV04;47;").buildMessage();
        }

        public VitalSign[] getLatestVitalSigns()
        {
            return getLatestVitalSigns(cxn.Pid);
        }

        // TBD: Build latest from getVitalSigns?
        public VitalSign[] getLatestVitalSigns(string pid)
        {
            if (!VistaUtils.isWellFormedIen(pid))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Invalid DFN: ");
            }

            return null;
        }
    }
}
