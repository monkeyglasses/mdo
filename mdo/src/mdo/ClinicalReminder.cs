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
using gov.va.medora.mdo.dao;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo
{
    public class ClinicalReminder
    {
        string id;
        string name;

        const string DAO_NAME = "IRemindersDao";

        public ClinicalReminder() { }

        public ClinicalReminder(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        internal static IRemindersDao getDao(AbstractConnection cxn)
        {
            if (!cxn.IsConnected)
            {
                throw new MdoException(MdoExceptionCode.USAGE_NO_CONNECTION, "Unable to instantiate DAO: unconnected");
            }
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(cxn.DataSource.Protocol));
            return f.getRemindersDao(cxn);
        }

        public static string[] getReminderReportTemplates(AbstractConnection cxn)
        {
            return getDao(cxn).getReminderReportTemplates();
        }

        public static OrderedDictionary getActiveReminderReports(AbstractConnection cxn)
        {
            return getDao(cxn).getActiveReminderReports();
        }

        public static ReminderReportPatientList getPatientListForReminderReport(AbstractConnection cxn, string rptId)
        {
            return getDao(cxn).getPatientListForReminderReport(rptId);
        }
    }
}
