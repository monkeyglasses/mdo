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
    public class Consult : Order
    {
        KeyValuePair<string, string> service;
        string title;
        string requestedProcedure;

        public Consult() { }

        public KeyValuePair<string, string> Service
        {
            get { return service; }
            set { service = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string RequestedProcedure
        {
            get { return requestedProcedure; }
            set { requestedProcedure = value; }
        }

        const string DAO_NAME = "IConsultDao";

        internal static new IConsultDao getDao(AbstractConnection cxn)
        {
            AbstractDaoFactory f = AbstractDaoFactory.getDaoFactory(AbstractDaoFactory.getConstant(cxn.DataSource.Protocol));
            return f.getConsultDao(cxn);
        }

        public static string getConsultNote(AbstractConnection cxn, string consultId)
        {
            return getDao(cxn).getConsultNote(consultId);
        }

        public static IndexedHashtable getConsultsForPatient(ConnectionSet cxns)
        {
            return cxns.query(DAO_NAME, "getConsultsForPatient", new object[] { });
        }

        public Consult[] getConsultsForPatient(AbstractConnection cxn)
        {
            return getDao(cxn).getConsultsForPatient();
        }

        public Consult[] getConsultsForPatient(AbstractConnection cxn, string pid)
        {
            return getDao(cxn).getConsultsForPatient(pid);
        }
    }
}
