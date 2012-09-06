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
using System.Runtime.CompilerServices;
using System.IO;
using gov.va.medora.mdo.exceptions;
/*
 * Author: Steven Weber
 * 
 * The utils is a general class for unclassified utility methods
 */
namespace gov.va.medora.utils
{
    public class Utils
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string errmsg(string expected, string actual)
        {
            return "Expected " + expected + ", got " + actual;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string getResponseFromFile(string filePath, string fileName)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Invalid FilePath");
            }

            if (String.IsNullOrEmpty(fileName))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Invalid FileName");
            }

            return FileIOUtils.readFromFile(Path.Combine(filePath, fileName));
        }
    }
}
