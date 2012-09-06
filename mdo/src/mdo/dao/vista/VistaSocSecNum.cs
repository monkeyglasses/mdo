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

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaSocSecNum : SocSecNum
    {
        bool isPseudo = false;

        static string[] VALID_AREA_NUMBER_EXCEPTIONS = 
            {
                "819"   // Manila VA
            };

        public VistaSocSecNum() : base() { }

        public VistaSocSecNum(string value) : base(value) 
        {
            char c = value[value.Length - 1];
            if (c == 'p' || c == 'P')
            {
                isPseudo = true;
                value = value.Substring(0, value.Length - 1);
            }
        }

        public bool IsPseudo
        {
            get { return isPseudo; }
        }

        public override bool IsValidAreaNumber
        {
            get
            {
                if (base.IsValidAreaNumber)
                {
                    return true;
                }
                if (isValidAreaNumberException(myAreaNumber))
                {
                    return true;
                }
                return false;
            }
        }

        internal static bool isValidAreaNumberException(string value)
        {
            for (int i = 0; i < VALID_AREA_NUMBER_EXCEPTIONS.Length; i++)
            {
                if (value == VALID_AREA_NUMBER_EXCEPTIONS[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool isValid(SocSecNum ssn)
        {
            if (ssn.IsValid)
            {
                return true;
            }
            if (!isValidAreaNumberException(ssn.AreaNumber))
            {
                return false;
            }
            if (ssn.IsValidGroupNumber && ssn.IsValidSerialNumber)
            {
                return true;
            }
            return false;
        }

        public override string  toString()
        {
 	         return base.toString() + (isPseudo ? "p" : "");
        }

        public override string toHyphenatedString()
        {
            return base.toHyphenatedString() + (isPseudo ? "p" : "");
        }
    }
}
