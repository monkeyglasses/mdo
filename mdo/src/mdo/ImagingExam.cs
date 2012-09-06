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
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdo
{
    public class ImagingExam
    {
        string name;
        string timestamp;
        string modality;
        string casenum;
        string accessionNum;
        string cpt;
        string status;
        string reportId;

        public ImagingExam() { }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string Modality
        {
            get { return modality; }
            set { modality = value; }
        }

        public string CaseNumber
        {
            get { return casenum; }
            set { casenum = value; }
        }

        public string AccessionNumber
        {
            get { return accessionNum; }
            set { accessionNum = value; }
        }

        public string CptCode
        {
            get { return cpt; }
            set { cpt = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string ReportId
        {
            get { return reportId; }
            set { reportId = value; }
        }

        const string DAO_NAME = "IRadiologyDao";

        internal static IRadiologyDao getDao(AbstractConnection cxn)
        {
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(cxn.DataSource.Protocol));
            return f.getRadiologyDao(cxn);
        }

        public static RadiologyReport getReportText(AbstractConnection cxn, string dfn, string accessionNumber)
        {
            return getDao(cxn).getImagingReport(dfn,accessionNumber);
        }
    }
}
