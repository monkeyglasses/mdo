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
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.vista
{
    public class DdrValidator
    {
        VistaConnection cxn;

        string file = "";
        string iens = "";
        string field = "";
        string valu = "";

        public DdrValidator(VistaConnection cxn) 
        {
            this.cxn = cxn;
        }

        public string execute()
        {
            if (File == "")
            {
                throw new Exception("Must have a file!");
            }
            if (Iens == "")
            {
                throw new Exception("Must have IENS!");
            }
            if (Field == "")
            {
                throw new Exception("Must have a field!");
            }
            if (Value == "")
            {
                throw new Exception("Must have a value!");
            }
            VistaQuery vq = new VistaQuery("DDR VALIDATOR");
            DictionaryHashList paramLst = new DictionaryHashList();
            paramLst.Add("\"FILE\"", File);
            paramLst.Add("\"IENS\"", Iens);
            paramLst.Add("\"FIELD\"", Field);
            paramLst.Add("\"VALUE\"", Value);
            vq.addParameter(vq.LIST, paramLst);
            DdrQuery query = new DdrQuery(cxn);
            string response = query.execute(vq);
            return buildResult(response);
        }

        internal string buildResult(string rtn)
        {
            string[] lines = StringUtils.split(rtn, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            int idx = StringUtils.getIdx(lines, VistaConstants.BEGIN_ERRS, 0);
            if (idx != -1)
            {
                idx = StringUtils.getIdx(lines, "The value", idx+1);
                if (idx != -1)
                {
                    return lines[idx];
                }
                throw new Exception("Unexpected return from VistA: " + rtn);
            }
            return "OK";
        }

        public string File
        {
            get { return file; }
            set { file = value; }
        }

        public string Iens
        {
            get { return iens; }
            set { iens = value; }
        }

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        public string Value
        {
            get { return valu; }
            set { valu = value; }
        }

    }
}
