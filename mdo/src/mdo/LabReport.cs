using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace gov.va.medora.mdo
{
    public class LabReport : Report
    {
        LabSpecimen specimen;
        string comment;

        public LabReport() { }

        public LabSpecimen Specimen
        {
            get { return specimen; }
            set { specimen = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

    }
}
