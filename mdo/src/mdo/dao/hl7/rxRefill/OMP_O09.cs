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
using NHapi.Model.V24.Segment;
using NHapi.Base.Model;

namespace gov.va.medora.mdo.dao.hl7.rxRefill
{
    public class OMP_O09_PID : NHapi.Model.V24.Message.OMP_O09
    {
        public OMP_O09_PID()
            : base()
        {
            this.add(typeof(PID), true, false);
            this.add(typeof(ORC), true, true);
            this.add(typeof(RXE), true, true);
            //this.add(typeof(OMP_O09_ORCRXE), true, true);
        }

        //public OMP_O09_ORCRXE getOrcrxe(int rep)
        //{
        //    return (OMP_O09_ORCRXE)this.GetStructure("OMP_O09_ORCRXE", rep);
        //}

        public ORC getOrc(int rep)
        {
            return (ORC)this.GetStructure("ORC", rep);
        }

        public RXE getRxe(int rep)
        {
            return (RXE)this.GetStructure("RXE", rep);
        }

        public PID getPid()
        {
            return (PID)this.GetStructure("PID");
        }

        public string encode()
        {
            return "";
        }
    }

    //public class OMP_O09_ORCRXE : AbstractGroup
    //{
    //    public OMP_O09_ORCRXE() : base(new NHapi.Base.Parser.DefaultModelClassFactory())
    //    {
    //        this.add(typeof(ORC), true, false);
    //        this.add(typeof(RXE), true, false);
    //    }

    //    public ORC getOrc()
    //    {
    //        return (ORC)this.GetStructure("ORC");
    //    }

    //    public RXE getRxe()
    //    {
    //        return (RXE)this.GetStructure("RXE");
    //    }
    //}
}
