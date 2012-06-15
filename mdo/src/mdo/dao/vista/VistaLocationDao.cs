using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.utils;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaLocationDao : ILocationDao
    {
        AbstractConnection cxn = null;

        public VistaLocationDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
        }

        public List<SiteId> getSitesForStation()
        {
            MdoQuery request = buildGetSitesForStationRequest();
            string response = (string)cxn.query(request, new MenuOption("AMOJ VL APPTFL"));
            return siteIdsToMdo(response);
        }

        internal MdoQuery buildGetSitesForStationRequest()
        {
            // This first RPC is from the AO project and the M code 
            // behind it doesn't have a bug fix that the same function
            // in the SS project does have.  The second RPC is the one
            // with the bug fix.
            //VistaQuery vq = new VistaQuery("AMOJVL CPGPI GETSITES");
            VistaQuery vq = new VistaQuery("AMOJ VL APPTFL GET SITES");
            return vq;
        }

        internal List<SiteId> siteIdsToMdo(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            List<SiteId> result = new List<SiteId>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (String.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                result.Add(new SiteId(flds[1], flds[0]));
            }
            return result;
        }

        public OrderedDictionary getClinicsByName(string name)
        {
            MdoQuery request = buildGetClinicsByNameRequest(name);
            string response = (string)cxn.query(request, new MenuOption("AMOJ VL CPGPI"));
            return clinicNamesToMdo(response);
        }

        internal MdoQuery buildGetClinicsByNameRequest(string name)
        {
            VistaQuery vq = new VistaQuery("AMOJVL CPGPI CLLOOK");
            vq.addParameter(vq.LITERAL, name);
            return vq;
        }

        internal OrderedDictionary clinicNamesToMdo(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            OrderedDictionary result = new OrderedDictionary();
            for (int i = 0; i < lines.Length; i++)
            {
                if (String.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                result.Add(flds[1], flds[0]);
            }
            return result;
        }
    }
}
