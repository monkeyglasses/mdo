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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaFile
    {
        string fileName;
        string fileNum;
        string global;
        string mdoName;
        DictionaryHashList fields;

        public VistaFile() { }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string FileNumber
        {
            get { return fileNum; }
            set { fileNum = value; }
        }

        public string Global
        {
            get { return global; }
            set { global = value; }
        }

        public string MdoName
        {
            get { return mdoName; }
            set { mdoName = value; }
        }

        public DictionaryHashList Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public string getFieldString()
        {
            string result = "";
            for (int i = 0; i < fields.Count; i++)
            {
                DictionaryEntry e = fields[i];
                result += ((VistaField)e.Value).VistaNumber + ';';
            }
            if (result != "")
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
