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
using System.IO;
using System.Runtime.CompilerServices;

namespace gov.va.medora.utils
{
    public class FileIOUtils
    {
        /// <summary>
        /// Saves string to the specified file, appending if the file exists
        /// </summary>
        /// <param name="path">Full path to file</param>
        /// <param name="response">Response to write to file</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void writeToFile(string path, string data, Boolean append = false)
        {
            FileMode mode = append ? FileMode.Append : FileMode.OpenOrCreate;
            FileStream fileStream = new FileStream(path, mode, FileAccess.Write, FileShare.None);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(data);
            }
            fileStream.Close();
        }

        /// <summary>
        /// Saves and array of strings to the specified file, appending if the file exists
        /// </summary>
        /// <param name="path">Full path to file</param>
        /// <param name="response">Array of strings to write to file</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void writeToFile(string path, string[] data, Boolean append = false)
        {
            FileMode mode = append ? FileMode.Append : FileMode.OpenOrCreate;
            FileStream fileStream = new FileStream(path, mode, FileAccess.Write, FileShare.None);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    streamWriter.WriteLine(data[i]);
                }
            }
            fileStream.Close();
        }

        /// <summary>
        /// Reads the specified file returning the content
        /// </summary>
        /// <param name="path">Full path of file</param>
        /// <returns>string representation of the file</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string readFromFile(string path)
        {
            string content = "";

           FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);

            using (StreamReader rdr = new StreamReader(fileStream))
            {
                content = rdr.ReadToEnd();
            }
            fileStream.Close();
            return content;
        }
    }
}
