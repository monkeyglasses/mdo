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
    public interface IChemHemDao
    {
        ChemHemReport[] getChemHemReports(string dfn, string fromDate, string toDate);
        ChemHemReport[] getChemHemReports(string fromDate, string toDate);
        ChemHemReport getChemHemReport(string dfn, ref string nextDate);
        //ChemHemReport[] getChemHemReports(string fromDate, string toDate, int maxRpts);
        Dictionary<string, HashSet<string>> getNewChemHemReports(DateTime start);
    }
}
