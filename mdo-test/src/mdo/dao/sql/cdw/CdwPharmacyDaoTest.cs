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
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    [TestFixture]
    public class CdwPharmacyDaoTest
    {
        CdwPharmacyDao _dao = new CdwPharmacyDao(new CdwConnection(new DataSource() { ConnectionString = "FakeCxnString" }));

        [Test]
        public void testToMeds()
        {
            DataTable medTable = new DataTable();
            medTable.Columns.Add("RxOutpatSid", typeof(Int64));
            medTable.Columns.Add("RxOutpatIEN");
            medTable.Columns.Add("Sta3n", typeof(Int16));
            medTable.Columns.Add("RxNumber");
            medTable.Columns.Add("LocalDrugIEN");
            medTable.Columns.Add("IssueDate", typeof(DateTime));
            medTable.Columns.Add("MaxRefills", typeof(Int16));
            medTable.Columns.Add("RxStatus");
            medTable.Columns.Add("LocalDrugNameWithDose");
            medTable.Columns.Add("DrugNameWithoutDose");
            medTable.Columns.Add("PatientIEN");
            medTable.Columns.Add("NationalDrugIEN");
            medTable.Rows.Add(new object[] { 800062880992, "7900386", 506, "4238177", "1234509er", DateTime.Now, 5, "ACTIVE", "AMOXICILLIN 500mg", "AMOXICILLIN", "MyIEN", "NatlDrugIEN" });

            MockDataReader rdr = new MockDataReader();
            rdr.Table = medTable;

            CdwPharmacyDao dao = new CdwPharmacyDao(new CdwConnection(new DataSource() { ConnectionString = "myCxnString" }));

            Medication[] results = dao.toMeds(rdr);

            Assert.AreEqual(results.Length, 1);
            Assert.IsTrue(String.Equals(results[0].RxNumber, "4238177"));
            Assert.IsTrue(String.Equals(results[0].Status, "ACTIVE"));
        }

    }
}
