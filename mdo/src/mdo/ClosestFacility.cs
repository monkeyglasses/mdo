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
    public class ClosestFacility
    {
        string visn;
        string city;
        string state;
        string county;
        string zip;
        string longitude;
        string latitude;
        string fips;
        string msa;
        string urb;
        Site nearestFacility;
        string nearestFacilityDistance;
        string nearestFacilityMsa;
        string nearestFacilityUrb;
        Site nearestMedicalCenter;
        string nearestMedicalCenterDistance;
        string nearestMedicalCenterMsa;
        string nearestMedicalCenterUrb;
        Site nearestFacilityInRegion;
        string nearestFacilityInRegionDistance;
        string nearestFacilityInRegionMsa;
        string nearestFacilityInRegionUrb;
        Site nearestMedicalCenterInRegion;
        string nearestMedicalCenterInRegionDistance;
        string nearestMedicalCenterInRegionMsa;
        string nearestMedicalCenterInRegionUrb;

        public ClosestFacility() { }

        public string RegionId
        {
            get { return visn; }
            set { visn = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string County
        {
            get { return county; }
            set { county = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Zipcode
        {
            get { return zip; }
            set { zip = value; }
        }

        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public string Fips
        {
            get { return fips; }
            set { fips = value; }
        }

        public string Msa
        {
            get { return msa; }
            set { msa = value; }
        }

        public string Urb
        {
            get { return urb; }
            set { urb = value; }
        }

        public Site NearestFacility
        {
            get { return nearestFacility; }
            set { nearestFacility = value; }
        }

        public string NearestFacilityDistance
        {
            get { return nearestFacilityDistance; }
            set { nearestFacilityDistance = value; }
        }

        public string NearestFacilityMsa
        {
            get { return nearestFacilityMsa; }
            set { nearestFacilityMsa = value; }
        }

        public string NearestFacilityUrb
        {
            get { return nearestFacilityUrb; }
            set { nearestFacilityUrb = value; }
        }

        public Site NearestMedicalCenter
        {
            get { return nearestMedicalCenter; }
            set { nearestMedicalCenter = value; }
        }

        public string NearestMedicalCenterDistance
        {
            get { return nearestMedicalCenterDistance; }
            set { nearestMedicalCenterDistance = value; }
        }

        public string NearestMedicalCenterMsa
        {
            get { return nearestMedicalCenterMsa; }
            set { nearestMedicalCenterMsa = value; }
        }

        public string NearestMedicalCenterUrb
        {
            get { return nearestMedicalCenterUrb; }
            set { nearestMedicalCenterUrb = value; }
        }

        public Site NearestFacilityInRegion
        {
            get { return nearestFacilityInRegion; }
            set { nearestFacilityInRegion = value; }
        }

        public string NearestFacilityInRegionDistance
        {
            get { return nearestFacilityInRegionDistance; }
            set { nearestFacilityInRegionDistance = value; }
        }

        public string NearestFacilityInRegionMsa
        {
            get { return nearestFacilityInRegionMsa; }
            set { nearestFacilityInRegionMsa = value; }
        }

        public string NearestFacilityInRegionUrb
        {
            get { return nearestFacilityInRegionUrb; }
            set { nearestFacilityInRegionUrb = value; }
        }

        public Site NearestMedicalCenterInRegion
        {
            get { return nearestMedicalCenterInRegion; }
            set { nearestMedicalCenterInRegion = value; }
        }

        public string NearestMedicalCenterInRegionDistance
        {
            get { return nearestMedicalCenterInRegionDistance; }
            set { nearestMedicalCenterInRegionDistance = value; }
        }

        public string NearestMedicalCenterInRegionMsa
        {
            get { return nearestMedicalCenterInRegionMsa; }
            set { nearestMedicalCenterInRegionMsa = value; }
        }

        public string NearestMedicalCenterInRegionUrb
        {
            get { return nearestMedicalCenterInRegionUrb; }
            set { nearestMedicalCenterInRegionUrb = value; }
        }

    }
}
