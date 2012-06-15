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
