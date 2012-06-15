using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.utils;
using gov.va.medora.mdo.src.mdo;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.dao.vista
{
    public class DdrGetsEntry : DdrQuery
    {
        string file;
        string iens;
        string flds;
        string flags;

        /// <summary>
        /// DdrGetsDataEntry query constructor. Executes GET^VEJDDDR0
        /// </summary>
        /// <param name="cxn"></param>
        public DdrGetsEntry(AbstractConnection cxn) : base(cxn) { }

        internal MdoQuery buildRequest()
        {
            if (String.IsNullOrEmpty(File))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_NULL, "Must have a file!");
            }
            if (String.IsNullOrEmpty(Iens))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_NULL, "Must have an IENS!");
            }
            if (String.IsNullOrEmpty(Fields))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_NULL, "Must have a field!");
            }
            VistaQuery vq = new VistaQuery("DDR GETS ENTRY DATA");
            DictionaryHashList paramLst = new DictionaryHashList();
            paramLst.Add("\"FILE\"", File);
            paramLst.Add("\"IENS\"", Iens);
            paramLst.Add("\"FIELDS\"", Fields);
            if (!String.IsNullOrEmpty(Flags))
            {
                paramLst.Add("\"FLAGS\"", Flags);
            }
            vq.addParameter(vq.LIST, paramLst);

            return vq;
        }

        public string[] execute()
        {
            MdoQuery request = buildRequest();
            string response = this.execute(request);
            return StringUtils.split(response,StringUtils.CRLF);
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

        public string Fields
        {
            get { return flds; }
            set { flds = value; }
        }

        public string Flags
        {
            get { return flags; }
            set { flags = value; }
        }
    }
}
