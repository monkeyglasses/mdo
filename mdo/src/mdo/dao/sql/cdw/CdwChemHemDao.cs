using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwChemHemDao : IChemHemDao
    {
        CdwConnection _cxn;

        public CdwChemHemDao(AbstractConnection cxn)
        {
            _cxn = cxn as CdwConnection;
        }

        public Dictionary<string, HashSet<string>> getNewChemHemReports(DateTime start)
        {
            if (DateTime.Now.Subtract(start).TotalDays > 30)
            {
                throw new ArgumentException("Only the last 30 days can be retrieved");
            }
            string commandText = "SELECT DISTINCT chem.Sta3n, patient.PatientICN " +
                "FROM Chem.LabChem AS chem " +
                "RIGHT OUTER JOIN SPatient.SPatient AS patient " +
                "ON chem.PatientSID=patient.PatientSID " +
                "WHERE " +
                "( " +
                "	( " +
                "		chem.LabChemCompleteDateTime>=DATEADD(DAY, -30, GETDATE()) AND  " +
                "		chem.VistaEditDate>=@start " +
                "	) " +
                "	OR " +
                "	( " +
                "		chem.LabChemCompleteDateTime IS NULL AND  " +
                "		chem.VistaEditDate>=@start " +
                "	) " +
                ") " +
                ";";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(commandText);

            SqlParameter startParam = new SqlParameter("@start", SqlDbType.DateTime);
            startParam.Value = start;
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

        public ChemHemReport[] getChemHemReports(string dfn, string fromDate, string toDate)
        {
            string commandText = "SELECT TOP(1) * " +
                "FROM Chem.LabChem;";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(commandText);

            adapter.SelectCommand.CommandTimeout = 600; // allow query to run for up to 10 minutes

            using (_cxn)
            {
                SqlDataReader reader = (SqlDataReader)_cxn.query(adapter);

                IList<ChemHemReport> results = new List<ChemHemReport>();

                while (reader.Read())
                {
                    if (reader.IsDBNull(1))
                    {
                        continue;
                    }
                    string labSid = ((Int64)reader[reader.GetOrdinal("LabChemSID")]).ToString();
                    string specimen = (string)reader[reader.GetOrdinal("Specimen")];
                    string siteId = ((Int16)reader[reader.GetOrdinal("Sta3n")]).ToString();
                    string testIen = (string)reader[reader.GetOrdinal("LabChemTestIEN")];
                    //DateTime completedTimestamp = (DateTime)reader[reader.GetOrdinal("LabChemCompleteDateTime")];

                    ChemHemReport result = new ChemHemReport()
                    {
                        Id = testIen,
                        Specimen = new LabSpecimen("", specimen, "", "")
                    };

                    results.Add(result);

                }
                return results.ToArray<ChemHemReport>();
            }
        }

        public ChemHemReport[] getChemHemReports(string fromDate, string toDate)
        {
            return getChemHemReports(_cxn.Pid, fromDate, toDate);
        }

        #region Not implemented members
        public ChemHemReport getChemHemReport(string dfn, ref string nextDate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
