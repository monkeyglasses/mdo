using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace gov.va.medora.mdo.dao
{
    public interface ILocationDao
    {
        List<SiteId> getSitesForStation();
        OrderedDictionary getClinicsByName(string name);
    }
}
