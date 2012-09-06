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

namespace gov.va.medora.mdo.domain.sm
{
    public class MhvIntegrationSettings
    {
        private string _encryptionPassword;

        public string EncryptionPassword
        {
            get { return _encryptionPassword; }
            set { _encryptionPassword = value; }
        }
        private string _seed;

        public string Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }
        private bool _expiration = false;

        public bool Expiration
        {
            get { return _expiration; }
            set { _expiration = value; }
        }
        private bool _productionMode = true;

        public bool ProductionMode
        {
            get { return _productionMode; }
            set { _productionMode = value; }
        }
        private string _patientSource;

        public string PatientSource
        {
            get { return _patientSource; }
            set { _patientSource = value; }
        }
        private string _administratorSource;

        public string AdministratorSource
        {
            get { return _administratorSource; }
            set { _administratorSource = value; }
        }
        private string _clinicianSource;

        public string ClinicianSource
        {
            get { return _clinicianSource; }
            set { _clinicianSource = value; }
        }
        private int _credentialsExpirationPeriod = 120;

        public int CredentialsExpirationPeriod
        {
            get { return _credentialsExpirationPeriod; }
            set { _credentialsExpirationPeriod = value; }
        }
        private string _authenticationKey;

        public string AuthenticationKey
        {
            get { return _authenticationKey; }
            set { _authenticationKey = value; }
        }


    }
}
