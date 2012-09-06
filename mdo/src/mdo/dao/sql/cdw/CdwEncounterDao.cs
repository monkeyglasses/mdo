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
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwEncounterDao : IEncounterDao
    {
        CdwConnection _cxn;

        public CdwEncounterDao(AbstractConnection cxn)
        {
            _cxn = (CdwConnection)cxn;
        }

        public Dictionary<string, HashSet<string>> getUpdatedFutureAppointments(DateTime updatedSince)
        {
            if (DateTime.Now.Subtract(updatedSince).TotalDays > 30)
            {
                throw new ArgumentException("Can not ask for more than 30 days of updates");
            }

            string commandText = "SELECT DISTINCT appt.Sta3n, patient.PatientICN FROM Appt.Appointment as appt " +
                "RIGHT OUTER JOIN SPatient.SPatient as patient " +
                "ON appt.PatientSID=patient.PatientSID " +
                "WHERE AppointmentDateTime>GETDATE() " +
                "AND appt.VistaEditDate>@updatedSince;";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(commandText);

            SqlParameter startParam = new SqlParameter("@updatedSince", SqlDbType.DateTime);
            startParam.Value = updatedSince;
            adapter.SelectCommand.Parameters.Add(startParam);
            adapter.SelectCommand.CommandTimeout = 600; // allow query to run for up to 10 minutes

            using (_cxn)
            {
                SqlDataReader reader = (SqlDataReader)_cxn.query(adapter);

                Dictionary<string, HashSet<string>> results = new Dictionary<string, HashSet<string>>();

                while (reader.Read())
                {
                    if (reader.IsDBNull(1))
                    {
                        continue;
                    }
                    string sitecode = reader.GetSqlInt16(0).ToString();
                    string patientICN = reader.GetString(1);

                    if (!results.ContainsKey(sitecode))
                    {
                        results.Add(sitecode, new HashSet<string>());
                    }
                    if (!results[sitecode].Contains(patientICN))
                    {
                        results[sitecode].Add(patientICN);
                    }
                }
                return results;
            }
        }

        #region Not implemented members
        public Appointment[] getAppointments()
        {
            return getAppointments(_cxn.Pid);
        }

        public Appointment[] getAppointments(string pid)
        {
            SqlDataAdapter adapter = buildGetAppointmentsRequest(pid);
            IDataReader rdr = (IDataReader)_cxn.query(adapter);
            return toAppointmentsFromDataReader(rdr);
        }

        internal SqlDataAdapter buildGetAppointmentsRequest(string pid)
        {
            string commandText = "SELECT APPT.Sta3n, APPT.PatientIEN, APPT.AppointmentDateTime, " +
                "APPT.AppointmentType, APPT.PurposeOfVisit, APPT.AppointmentStatus, " +
                "LOC.LocationName, LOC.LocationType " +
                "FROM Appt.Appointment AS APPT " +
                "JOIN Dim.Location AS LOC ON APPT.LocationSID=LOC.LocationSID " +
                "JOIN SPatient.SPatient AS PAT ON APPT.PatientSID=PAT.PatientSID " +
                "WHERE PAT.PatientICN=@patientId ORDER BY APPT.AppointmentDateTime DESC;";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(commandText);

            SqlParameter idParam = new SqlParameter("@patientId", SqlDbType.VarChar, 50);
            idParam.Value = pid;
            adapter.SelectCommand.Parameters.Add(idParam);
            adapter.SelectCommand.CommandTimeout = 600; // allow query to run for up to 10 minutes
            
            return adapter;
        }

        internal Appointment[] toAppointmentsFromDataReader(IDataReader reader)
        {
            IList<Appointment> appts = new List<Appointment>();

            while (reader.Read())
            {
                Appointment appt = new Appointment();

                appt.Facility = new SiteId(((Int16)reader[reader.GetOrdinal("Sta3n")]).ToString(), ""); // we don't know site names here - should plug in manually later
                appt.Timestamp = reader.IsDBNull(reader.GetOrdinal("AppointmentDateTime")) ? "" : reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")).ToString();
                appt.Type = reader.IsDBNull(reader.GetOrdinal("AppointmentType")) ? "" : reader.GetString(reader.GetOrdinal("AppointmentType"));
                appt.Status = reader.IsDBNull(reader.GetOrdinal("AppointmentStatus")) ? "" : reader.GetString(reader.GetOrdinal("AppointmentStatus"));
                appt.Purpose = reader.IsDBNull(reader.GetOrdinal("PurposeOfVisit")) ? "" : reader.GetString(reader.GetOrdinal("PurposeOfVisit"));
                appt.Clinic = new HospitalLocation();
                appt.Clinic.Name = reader.IsDBNull(reader.GetOrdinal("LocationName")) ? "" : reader.GetString(reader.GetOrdinal("LocationName"));
                appt.Clinic.Type = reader.IsDBNull(reader.GetOrdinal("LocationType")) ? "" : reader.GetString(reader.GetOrdinal("LocationType"));
                appts.Add(appt);
            }

            return appts.ToArray<Appointment>();
        }

        public Appointment[] getFutureAppointments()
        {
            throw new NotImplementedException();
        }

        public Appointment[] getFutureAppointments(string pid)
        {
            throw new NotImplementedException();
        }

        public Appointment[] getAppointments(int pastDays, int futureDays)
        {
            throw new NotImplementedException();
        }

        public Appointment[] getAppointments(string pid, int pastDays, int futureDays)
        {
            throw new NotImplementedException();
        }

        public string getAppointmentText(string apptId)
        {
            throw new NotImplementedException();
        }

        public string getAppointmentText(string pid, string apptId)
        {
            throw new NotImplementedException();
        }

        public Adt[] getInpatientMoves(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }

        public Adt[] getInpatientMovesByCheckinId(string checkinId)
        {
            throw new NotImplementedException();
        }

        public Adt[] getInpatientMoves()
        {
            throw new NotImplementedException();
        }

        public Adt[] getInpatientMoves(string pid)
        {
            throw new NotImplementedException();
        }

        public Adt[] getInpatientMoves(string fromDate, string toDate, string iterLength)
        {
            throw new NotImplementedException();
        }

        public HospitalLocation[] lookupLocations(string target, string direction)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Specialized.StringDictionary lookupHospitalLocations(string target)
        {
            throw new NotImplementedException();
        }

        public string getLocationId(string locationName)
        {
            throw new NotImplementedException();
        }

        public HospitalLocation[] getWards()
        {
            throw new NotImplementedException();
        }

        public HospitalLocation[] getClinics(string target, string direction)
        {
            throw new NotImplementedException();
        }

        public InpatientStay[] getStaysForWard(string wardId)
        {
            throw new NotImplementedException();
        }

        public Drg[] getDRGRecords()
        {
            throw new NotImplementedException();
        }

        public Visit[] getOutpatientVisits()
        {
            throw new NotImplementedException();
        }

        public Visit[] getOutpatientVisits(string pid)
        {
            throw new NotImplementedException();
        }

        public Visit[] getVisits(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }

        public Visit[] getVisits(string pid, string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }

        public Visit[] getVisitsForDay(string theDate)
        {
            throw new NotImplementedException();
        }

        public InpatientStay[] getAdmissions()
        {
            throw new NotImplementedException();
        }

        public InpatientStay[] getAdmissions(string pid)
        {
            throw new NotImplementedException();
        }

        public string getServiceConnectedCategory(string initialCategory, string locationIen, bool outpatient)
        {
            throw new NotImplementedException();
        }

        public string getOutpatientEncounterReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getOutpatientEncounterReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getAdmissionsReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getAdmissionsReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getExpandedAdtReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getExpandedAdtReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getDischargesReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getDischargesReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getTransfersReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getTransfersReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getFutureClinicVisitsReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getFutureClinicVisitsReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getPastClinicVisitsReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getPastClinicVisitsReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getTreatingSpecialtyReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getTreatingSpecialtyReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getCareTeamReport()
        {
            throw new NotImplementedException();
        }

        public string getCareTeamReport(string pid)
        {
            throw new NotImplementedException();
        }

        public string getDischargeDiagnosisReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getDischargeDiagnosisReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public IcdReport[] getIcdProceduresReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public IcdReport[] getIcdProceduresReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public IcdReport[] getIcdSurgeryReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public IcdReport[] getIcdSurgeryReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getCompAndPenReport(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getCompAndPenReport(string pid, string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public DictionaryHashList getSpecialties()
        {
            throw new NotImplementedException();
        }

        public DictionaryHashList getTeams()
        {
            throw new NotImplementedException();
        }

        public Adt[] getInpatientDischarges(string pid)
        {
            throw new NotImplementedException();
        }

        public InpatientStay[] getStayMovementsByDateRange(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }

        public InpatientStay getStayMovements(string checkinId)
        {
            throw new NotImplementedException();
        }

        public Site[] getSiteDivisions(string siteId)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
