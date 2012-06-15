using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gov.va.medora.mdo.dao
{
    public interface IRadiologyDao
    {
        RadiologyReport[] getRadiologyReports(string fromDate, string toDate, int nrpts);
        RadiologyReport getImagingReport(string dfn, string accessionNumber);
    }
}
