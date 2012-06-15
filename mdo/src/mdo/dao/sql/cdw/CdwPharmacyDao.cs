using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwPharmacyDao : IPharmacyDao
    {
        CdwConnection _cxn;

        public CdwPharmacyDao(AbstractConnection cxn)
        {
            _cxn = (CdwConnection)cxn;
        }

        public Medication[] getOutpatientMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getIvMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getIvMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public Medication[] getUnitDoseMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getUnitDoseMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public Medication[] getOtherMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getOtherMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public Medication[] getAllMeds()
        {
            return getAllMeds(_cxn.Pid);
        }

        internal SqlDataAdapter buildGetAllMedsRequest(string icn)
        {
            string commandText = "SELECT MED.RxOutpatIEN, MED.Sta3n, MED.PatientIEN, " +
                "MED.RxNumber, MED.LocalDrugIEN, DRUG.LocalDrugNameWithDose, " +
                "DRUG.DrugNameWithoutDose, MED.RxStatus, MED.MaxRefills, MED.IssueDate, MED.NationalDrugIEN " +
                "FROM RxOut.RxOutpat AS MED RIGHT OUTER JOIN Dim.LocalDrug " +
                "AS DRUG ON MED.LocalDrugSID=DRUG.LocalDrugSID JOIN SPatient.Spatient AS PAT ON Med.PatientSID=PAT.PatientSID WHERE " +
                "PAT.PatientICN=@patientIcn;";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(commandText);
            SqlParameter patientIdParam = new SqlParameter("@patientIcn", System.Data.SqlDbType.VarChar, 50);
            patientIdParam.Value = icn;
            adapter.SelectCommand.Parameters.Add(patientIdParam);

            adapter.SelectCommand.CommandTimeout = 600; // allow query to run for up to 10 minutes
            return adapter;
        }

        internal Medication[] toMeds(IDataReader reader)
        {
            IList<Medication> results = new List<Medication>();
            while (reader.Read())
            {
                string rxIen = getValue(reader, reader.GetOrdinal("RxOutpatIEN"));
                string siteId = isDbNull(reader, reader.GetOrdinal("Sta3n")) ? "" : ((Int16)reader[reader.GetOrdinal("Sta3n")]).ToString();
                string localPid = getValue(reader, reader.GetOrdinal("PatientIEN"));
                string rxNum = getValue(reader, reader.GetOrdinal("RxNumber"));
                string localDrugIen = getValue(reader, reader.GetOrdinal("LocalDrugIEN"));
                string drugNameWithDose = getValue(reader, reader.GetOrdinal("LocalDrugNameWithDose"));
                string drugNameWithoutDose = getValue(reader, reader.GetOrdinal("DrugNameWithoutDose"));
                string natlDrugIen = getValue(reader, reader.GetOrdinal("NationalDrugIEN"));
                string status = getValue(reader, reader.GetOrdinal("RxStatus"));
                string refills = isDbNull(reader, reader.GetOrdinal("MaxRefills")) ? "" : ((Int16)reader[reader.GetOrdinal("MaxRefills")]).ToString();
                string issueDate = isDbNull(reader, reader.GetOrdinal("IssueDate")) ? "" : ((DateTime)reader[reader.GetOrdinal("IssueDate")]).ToShortDateString();

                Medication med = new Medication()
                {
                    IssueDate = issueDate,
                    Refills = refills,
                    Status = status,
                    Name = drugNameWithDose,
                    Id = localDrugIen,
                    RxNumber = rxNum,
                    Drug = new KeyValuePair<string, string>(localDrugIen, drugNameWithoutDose)
                };

                results.Add(med);
            }
            return results.ToArray<Medication>();
        }

        public Medication[] getAllMeds(string icn)
        {
            SqlDataAdapter adapter = buildGetAllMedsRequest(icn);
            SqlDataReader reader = (SqlDataReader)_cxn.query(adapter);
            return toMeds(reader);
        }

        bool isDbNull(IDataReader reader, int index)
        {
            return reader[index] == DBNull.Value;
        }
        string getValue(IDataReader reader, int index)
        {
            return (reader[index] == DBNull.Value) ? "" : (string)reader[index];
        }

        public Medication[] getVaMeds(string dfn)
        {
            throw new NotImplementedException();
        }

        public Medication[] getVaMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getInpatientForOutpatientMeds()
        {
            throw new NotImplementedException();
        }

        public Medication[] getInpatientForOutpatientMeds(string pid)
        {
            throw new NotImplementedException();
        }

        public string getMedicationDetail(string medId)
        {
            throw new NotImplementedException();
        }

        public string getOutpatientRxProfile()
        {
            throw new NotImplementedException();
        }

        public string getMedsAdminHx(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getMedsAdminLog(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }

        public string getImmunizations(string fromDate, string toDate, int nrpts)
        {
            throw new NotImplementedException();
        }


        public Medication refillPrescription(string rxId)
        {
            throw new NotImplementedException();
        }
    }
}
