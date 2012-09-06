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
using gov.va.medora.mdo.dao.sql.pssg;
using gov.va.medora.mdo.dao.sql.zipcodeDB;

namespace gov.va.medora.mdo.api
{
    public class SitesApi
    {
        public SitesApi() { }

        public string[] matchCityAndState(string city, string stateAbbr, string connectionString)
        {
            ZipcodeDao dao = new ZipcodeDao(connectionString);
            return dao.matchCityAndState(city, stateAbbr);
        }

        public Site[] getClosestFacilities(string fips, string connectionString)
        {
            PssgDao dao = new PssgDao(connectionString);
            return dao.getClosestFacilities(fips);
        }

        public ClosestFacility getNearestFacility(string zipcode, string connectionString)
        {
            PssgDao dao = new PssgDao(connectionString);
            return dao.getNearestFacility(zipcode);
        }
    }
}
