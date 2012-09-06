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
using System.Text;

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieEncounterDao
    {
        public FhieEncounterDao(AbstractConnection cxn)
        {
        }

        public Appointment[] getAppointments() {return null;}
        public Appointment[] getFutureAppointments() { return null; }
        public string getAppointmentText(string apptId) { return null; }
        public Adt[] getInpatientMoves(String fromDate, string toDate) { return null; }
        public Adt[] getInpatientMoves() { return null; }
        public IndexedHashtable lookupLocations(string target, string direction) { return null; }
        public HospitalLocation[] getWards() { return null; }
        public InpatientStay[] getStaysForWard(string wardId) { return null; }
        public Drg[] getDRGRecords() { return null; }
    }
}
