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

namespace gov.va.medora.mdo.dao
{
    public interface IPharmacyDao
    {
        //Medication[] getOutpatientMeds(string fromDate, string toDate, int nRex);
        Medication[] getOutpatientMeds();
        Medication[] getIvMeds();
        Medication[] getIvMeds(string pid);
        Medication[] getUnitDoseMeds();
        Medication[] getUnitDoseMeds(string pid);
        Medication[] getOtherMeds();
        Medication[] getOtherMeds(string pid);
        Medication[] getAllMeds();
        Medication[] getAllMeds(string dfn);
        Medication[] getVaMeds(string dfn);
        Medication[] getVaMeds();
        Medication[] getInpatientForOutpatientMeds();
        Medication[] getInpatientForOutpatientMeds(string pid);
        string getMedicationDetail(string medId);
        string getOutpatientRxProfile();
        string getMedsAdminHx(string fromDate, string toDate, int nrpts);
        string getMedsAdminLog(string fromDate, string toDate, int nrpts);
        string getImmunizations(string fromDate, string toDate, int nrpts);
        Medication refillPrescription(string rxId);
    }
}
