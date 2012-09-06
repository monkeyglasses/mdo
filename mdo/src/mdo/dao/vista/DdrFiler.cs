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
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.vista
{
    public class DdrFiler : DdrQuery
    {
        string operation;
        string[] args;

        public DdrFiler(AbstractConnection cxn) : base(cxn) { }

        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        public string[] Args
        {
            get { return args; }
            set { args = value; }
        }

        internal MdoQuery buildRequest()
        {
            if (Operation == null || Operation == "")
            {
                throw new ArgumentNullException("Must have an operation");
            }
            VistaQuery vq = new VistaQuery("DDR FILER");
            vq.addParameter(vq.LITERAL, Operation);
            DictionaryHashList lst = new DictionaryHashList();
            for (int i = 0; i < Args.Length; i++)
            {
                lst.Add(Convert.ToString(i+1), Args[i]);
            }
            vq.addParameter(vq.LIST, lst);
            return vq;
        }

        public string execute()
        {
            MdoQuery request = buildRequest();
            return this.execute(request);
        }
    }
}
