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
using gov.va.medora;

namespace gov.va.medora
{
    /// <summary>
    /// A test object that can be set via Spring and fed to tests
    /// </summary>
    public class TestObject
    {
        private String siteCode;
        private String userName; // access code
        private String password; // e.g. VistA verify code
        private String context; // like CPRS context
        private String userDuz;
        private String patientDfn;

        /// <summary>
        /// 
        /// </summary>
        public String SiteCode
        {
            get {return siteCode;}
            set {siteCode = value;}
        }

        public String UserName
        {
            get {return userName;}
            set {userName = value;}
        }

        public String Password
        {
            get {return password;}
            set {password = value;}
        }

        public String Context
        {
            get {return context;}
            set {context = value;}
        }

        public String UserDuz
        {
            get {return userDuz;}
            set {userDuz = value;}
        }

        public String PatientDfn
        {
            get {return patientDfn;}
            set {patientDfn = value;}
        }
    }

    /// <summary>
    /// Very simple test object to wrap the generic dictionary class so that can
    /// more idiomatically set and get String properties...
    /// </summary>
    /// <remarks>
    /// * If the key/value hasn't been set, return null.
    /// * If trying to set a value for a key that has already been set, set over it.
    /// </remarks>
    public class StringTestObject
    {
        // break encapsulation by declaring public so that someone can deal 
        // directly if they really need to it's a test object!
        public System.Collections.Generic.IDictionary<string, string> properties;

        public void set(string key, string value)
        {
            properties[key] = value;
        }

        public string get(string key)
        {
            try
            {
                return (string)properties[key];
            }
            catch
            {
                // fail silently
            }
            return null;
        }

        public string get(string key, bool useCaseInsensitive)
        {
            if (!useCaseInsensitive)
            {
                return get(key);
            }
            else
            {
                foreach (string property in properties.Keys)
                {
                    if (key.ToLowerInvariant() == property.ToLowerInvariant())
                    {
                        return properties[property];
                    }  
                }
            }
            return null;
        }
    }
}
