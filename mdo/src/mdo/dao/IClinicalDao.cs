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

namespace gov.va.medora.mdo.dao
{
    public interface IClinicalDao
    {
        Problem[] getProblemList(string type);
        string getAllergiesAsXML();
        Allergy[] getAllergies();
        MdoDocument[] getHealthSummaryList();
        string getHealthSummaryTitle(string summaryId);
        string getHealthSummaryText(string mpiPid, MdoDocument hs, string sourceSiteId);
        // string getHealthSummary(string dfn, MdoDocument hs);
        HealthSummary getHealthSummary(MdoDocument hs);
        string getAdHocHealthSummaryByDisplayName(string displayName);
        SurgeryReport[] getSurgeryReports(bool fWithText);
        string getSurgeryReportText(string rptId);
        string getNhinData(string types = null);
        string getNhinData(string dfn, string types = null);
        List<MentalHealthInstrumentAdministration> getMentalHealthInstrumentsForPatient();
        List<MentalHealthInstrumentAdministration> getMentalHealthInstrumentsForPatient(string pid);
        void addMentalHealthInstrumentResultSet(MentalHealthInstrumentAdministration administration);
        MentalHealthInstrumentResultSet getMentalHealthInstrumentResultSet(string administrationId);
    }
}
