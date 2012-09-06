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
    public class VistaField
    {
        int pos;
        string vistaName;
        string vistaNum;
        string vistaNode;
        string vistaPiece;
        string vistaValue;
        string mdoName;
        string mdoType;
        VistaFieldMapping mapping;

        public VistaField() { }

        public int Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public string VistaName
        {
            get { return vistaName; }
            set { vistaName = value; }
        }

        public string VistaNumber
        {
            get { return vistaNum; }
            set { vistaNum = value; }
        }

        public string VistaNode
        {
            get { return vistaNode; }
            set { vistaNode = value; }
        }

        public string VistaPiece
        {
            get { return vistaPiece; }
            set { vistaPiece = value; }
        }

        public string VistaValue
        {
            get { return vistaValue; }
            set { vistaValue = value; }
        }

        public string MdoName
        {
            get { return mdoName; }
            set { mdoName = value; }
        }

        public string MdoType
        {
            get { return mdoType; }
            set { mdoType = value; }
        }

        public VistaFieldMapping Mapping
        {
            get { return mapping; }
            set { mapping = value; }
        }
    }
}
