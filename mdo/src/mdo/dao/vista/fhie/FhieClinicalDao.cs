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
using System.Collections;
using System.Text;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieClinicalDao : IClinicalDao
    {
        VistaConnection cxn = null;
        VistaClinicalDao vistaDao = null;

        public FhieClinicalDao(AbstractConnection cxn)
        {
            this.cxn = (VistaConnection)cxn;
            vistaDao = new VistaClinicalDao(cxn);
        }

        public Dictionary<string, ArrayList> getNoteTitles(String target, String direction)
        {
            return null;
        }

        public Allergy[] getAllergies()
        {
            return vistaDao.getAllergies();
        }

        public Problem[] getProblemList(string type)
        {
            return vistaDao.getProblemList(type);
        }

        public MdoDocument[] getHealthSummaryList()
        {
            return null;
        }

        public String getHealthSummaryTitle(String summaryId)
        {
            return null;
        }

        public string getHealthSummaryText(String mpiPid, MdoDocument hs, String sourceSiteId)
        {
            return null;
        }

        public HealthSummary getHealthSummary(MdoDocument hs)
        {
            return null;
        }

        public SurgeryReport[] getSurgeryReports(bool fWithText)
        {
            return null;
        }

        public string getSurgeryReportText(string rptId)
        {
            return null;
        }

        public string getAdHocHealthSummaryByDisplayName(string displayName)
        {
            return null;
        }

        public string getNhinData(string types = null)
        {
            return null;
        }

        public string getNhinData(string dfn, string types = null)
        {
            return null;
        }

        public List<MentalHealthInstrumentAdministration> getMentalHealthInstrumentsForPatient()
        {
            return null;
        }

        public List<MentalHealthInstrumentAdministration> getMentalHealthInstrumentsForPatient(string dfn)
        {
            return null;
        }

        public void addMentalHealthInstrumentResultSet(MentalHealthInstrumentAdministration administration)
        {
            return;
        }

        public MentalHealthInstrumentResultSet getMentalHealthInstrumentResultSet(string administrationId)
        {
            return null;
        }



        public string getAllergiesAsXML()
        {
            throw new NotImplementedException();
        }
    }
}