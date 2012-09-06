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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace gov.va.medora.mdo.dao
{
    public interface IPatientDao
    {
        Patient[] match(string target);
        Patient[] getPatientsByWard(string wardId);
        Patient[] getPatientsByClinic(string clinicId);
        Patient[] getPatientsByClinic(string clinicId, string fromDate, string toDate);
        Patient[] getPatientsBySpecialty(string specialtyId);
        Patient[] getPatientsByTeam(string teamId);
        Patient[] getPatientsByProvider(string providerId);
        Patient[] matchByNameCityState(string name, string city, string stateAbbr);
        Patient select(string pid);
        Patient select();
        Patient selectBySSN(string ssn);
        string getLocalPid(string mpiPID);
        bool isTestPatient();
        KeyValuePair<int, string> getConfidentiality();
        string issueConfidentialityBulletin();
        StringDictionary getRemoteSiteIds(string pid);
        Site[] getRemoteSites(string pid);
        OEF_OIF[] getOefOif();
        void addHomeData(Patient patient);
        PatientAssociate[] getPatientAssociates(string pid);
        StringDictionary getPatientTypes();
        string patientInquiry(string pid);
        RatedDisability[] getRatedDisabilities();
        RatedDisability[] getRatedDisabilities(string pid);
        KeyValuePair<string, string> getPcpForPatient(string dfn);
        TextReport getMOSReport(Patient patient);
        Dictionary<string, string> getTreatingFacilityIds(string pid);
        DemographicSet getDemographics();
    }
}
