using System;
using System.Collections.Generic;
using System.Text;

namespace gov.va.medora.mdo
{
    public class LabTest : ObservationType
    {
        string units;
        string lowRef;
        string hiRef;
        string refRange;
        string loinc;

        public LabTest() { }

        public string Units
        {
            get { return units; }
            set { units = value; }
        }

        public string LowRef
        {
            get { return lowRef; }
            set { lowRef = value; }
        }

        public string HiRef
        {
            get { return hiRef; }
            set { hiRef = value; }
        }

        public string RefRange
        {
            get { return refRange; }
            set { refRange = value; }
        }

        public string Loinc
        {
            get { return loinc; }
            set { loinc = value; }
        }
    }
}
