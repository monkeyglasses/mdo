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

namespace gov.va.medora.mdo
{
    public class GeographicLocation
    {
        string zipcode;
        string zipcodeType;
        string cityName;
        string cityType;
        string countyName;
        string countyFips;
        string stateName;
        string stateAbbr;
        string stateFips;
        string msaCode;
        string areaCode;
        string timeZone;
        int utc;
        bool dst;
        double latitude;
        double longitude;

        public GeographicLocation() { }

        public string Zipcode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }

        public string ZipcodeType
        {
            get { return zipcodeType; }
            set { zipcodeType = value; }
        }

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public string CityType
        {
            get { return cityType; }
            set { cityType = value; }
        }

        public string CountyName
        {
            get { return countyName; }
            set { countyName = value; }
        }

        public string CountyFips
        {
            get { return countyFips; }
            set { countyFips = value; }
        }

        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }

        public string StateAbbreviation
        {
            get { return stateAbbr; }
            set { stateAbbr = value; }
        }

        public string StateFips
        {
            get { return stateFips; }
            set { stateFips = value; }
        }

        public string MsaCode
        {
            get { return msaCode; }
            set { msaCode = value; }
        }

        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }

        public int Utc
        {
            get { return utc; }
            set { utc = value; }
        }

        public bool DaylightSavings
        {
            get { return dst; }
            set { dst = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
    }
}
