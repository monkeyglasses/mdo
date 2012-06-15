using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using gov.va.medora.utils;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.vista
{
    public class DdrLister : DdrQuery
    {
        string file = "";
        string iens = "";
        string[] requestedFields;       // Holds user's requested order
        Hashtable requestedFieldsTbl;   // Needed to fetch by FileMan field #, and to hold if External or not
        ArrayList ienLst;               // Needed to hold record IENs across methods.
        string flags = "";
        string max = "";
        string from = "";
        string part = "";
        string xref = "";
        string screen = "";
        string id = "";
        string options = "";
        string moreFrom = "";
        string moreIens = "";

        public bool more = false;

        public DdrLister(AbstractConnection cxn) : base(cxn) { }

        internal MdoQuery buildRequest()
        {
            if (File == "")
            {
                throw new ArgumentNullException("Must have a file!");
            }
            VistaQuery vq = new VistaQuery("DDR LISTER");
            DictionaryHashList paramLst = new DictionaryHashList();
            paramLst.Add("\"FILE\"", File);
            if (Iens != "")
            {
                paramLst.Add("\"IENS\"", Iens);
            }
            if (requestedFields.Length > 0)
            {
                paramLst.Add("\"FIELDS\"", getFieldsArg());
            }
            if (Flags != "")
            {
                paramLst.Add("\"FLAGS\"", Flags);
            }
            if (Max != "")
            {
                paramLst.Add("\"MAX\"", Max);
            }
            if (From != "")
            {
                paramLst.Add("\"FROM\"", From);
            }
            if (Part != "")
            {
                paramLst.Add("\"PART\"", Part);
            }
            if (Xref != "")
            {
                paramLst.Add("\"XREF\"", Xref);
            }
            if (Screen != "")
            {
                paramLst.Add("\"SCREEN\"", Screen);
            }
            if (Id != "")
            {
                paramLst.Add("\"ID\"", Id);
            }
            if (Options != "")
            {
                paramLst.Add("\"OPTIONS\"", Options);
            }
            if (moreFrom != "")
            {
                paramLst.Add("\"FROM\"", moreFrom);
            }
            if (moreIens != "")
            {
                paramLst.Add("\"IENS\"", moreIens);
            }
            vq.addParameter(vq.LIST, paramLst);
            return vq;
        }

        public string[] execute()
        {
            MdoQuery request = buildRequest();
            string response = this.execute(request);
            return buildResult(response.Replace('|','^'));
        }

        private String getFieldsArg()
        {
            String result = "@";
            for (int i = 0; i < requestedFields.Length; i++)
            {
                if (requestedFields[i] == "@")
                {
                    continue;
                }
                result += ';' + requestedFields[i];
            }
            return result;
        }

        private void setMoreParams(string line)
        {
            string[] flds = StringUtils.split(line, StringUtils.CARET);
            if (flds[0] != "MORE")
            {
                throw new UnexpectedDataException("Invalid return data: expected 'MORE', got " + flds[0]);
            }
            moreFrom = flds[1];
            moreIens = flds[2];
            more = true;
        }

        public string[] buildResult(string rtn)
        {
            String[] lines = StringUtils.split(rtn, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            int i = 0;

            if (lines[i] == VistaConstants.MISC)
            {
                if (!lines[++i].StartsWith("MORE"))
                {
                    throw new UnexpectedDataException("Error packing LISTER return; expected 'MORE...', got " + lines[i]);
                }
                setMoreParams(lines[i]);
            }

            if (this.flags.IndexOf("P") != -1)
            {
                return parsePackedResult(lines);
            }
            else
            {
                return packResult(lines);
            }
        }

        private string[] parsePackedResult(string[] lines)
        {
            int idx = StringUtils.getIdx(lines, VistaConstants.BEGIN_ERRS, 0);
            if (idx != -1)
            {
                throw new ConnectionException(getErrMsg(lines,idx));
            }

            idx = StringUtils.getIdx(lines, VistaConstants.BEGIN_DATA, 0);
            if (idx == -1)
            {
                throw new UnexpectedDataException("Error parsing packed result: expected " + VistaConstants.BEGIN_DATA + ", found none.");
            }

            ArrayList lst = new ArrayList();
            idx++;
            while (idx < lines.Length && lines[idx] != VistaConstants.END_DATA)
            {
                lst.Add(lines[idx++]);
            }
            return (string[])lst.ToArray(typeof(string));
        }

        private string[] packResult(string[] lines)
        {
            int idx = 0;

            if (lines[idx] == VistaConstants.UNPACKED_NO_RESULTS)
            {
                return new string[] { };
            }

            Hashtable rs = new Hashtable();

            if ((idx = StringUtils.getIdx(lines, VistaConstants.BEGIN_ERRS, 0)) != -1)
            {
                throw new ConnectionException(getErrMsg(lines,idx));
            }

            if ((idx = StringUtils.getIdx(lines, VistaConstants.BEGIN_DATA, 0)) != -1)
            {
                ienLst = new ArrayList();
                if (lines[++idx] != VistaConstants.BEGIN_IENS)
                {
                    throw new UnexpectedDataException("Incorrectly formatted return data");
                }
                idx++;
                while (lines[idx] != VistaConstants.END_IENS)
                {
                    ienLst.Add(lines[idx++]);
                }

                idx++;
                if (lines[idx] != VistaConstants.BEGIN_IDVALS)
                {
                    throw new UnexpectedDataException("Incorrectly formatted return data");
                }
                String[] flds = StringUtils.split(lines[++idx], StringUtils.SEMICOLON);

                int recIdx = 0;
                idx++;
                while (lines[idx] != VistaConstants.END_IDVALS)
                {
                    // the last field in flds is the field count, not a field
                    Hashtable rec = new Hashtable();
                    for (int fldIdx = 0; fldIdx < flds.Length-1; fldIdx++)
                    {
                        DdrField f = new DdrField();
                        f.FmNumber = flds[fldIdx];
                        String requestedOptions = (String)requestedFieldsTbl[f.FmNumber];
                        f.HasExternal = requestedOptions.IndexOf('E') != -1;
                        if (f.HasExternal)
                        {
                            f.ExternalValue = lines[idx++];
                        }
                        if (requestedOptions.IndexOf('I') != -1)
                        {
                            f.Value = lines[idx++];
                        }
                        rec.Add(f.FmNumber, f);
                    }
                    rs.Add((String)ienLst[recIdx++], rec);
                }
                // at this point line should be VistaConstants.END_DATA
                // unless more functionality is added.
            }
            return toStringArray(rs);
        }

        private string getErrMsg(string[] lines, int idx)
        {
            string msg = lines[idx + 3];
            int endIdx = StringUtils.getIdx(lines, VistaConstants.END_ERRS, 0);
            for (int i = idx + 4; i < endIdx; i++)
            {
                if (msg[msg.Length - 1] != '.')
                {
                    msg += ". ";
                }
                msg += lines[i];
            }
            return msg;
        }

        private String[] toStringArray(Hashtable hashedRex)
        {
            ArrayList lst = new ArrayList();
            for (int recnum = 0; recnum < ienLst.Count; recnum++)
            {
                String s = (String)ienLst[recnum];
                Hashtable hashedFlds = (Hashtable)hashedRex[ienLst[recnum]];
                for (int fldnum = 0; fldnum < requestedFields.Length; fldnum++)
                {
                    String fmNum = requestedFields[fldnum];
                    bool external = false;
                    if (fmNum.IndexOf('E') != -1)
                    {
                        fmNum = fmNum.Substring(0, fmNum.Length - 1);
                        external = true;
                    }
                    DdrField fld = (DdrField)hashedFlds[fmNum];
                    if (external)
                    {
                        s += '^' + fld.ExternalValue;
                    }
                    else
                    {
                        s += '^' + fld.Value;
                    }
                }
                lst.Add(s);
            }
            return (String[])lst.ToArray(typeof(String));
        }

        public String File
        {
            get { return file; }
            set { file = value; }
        }

        public String Iens
        {
            get { return iens; }
            set { iens = value; }
        }

        public String Fields
        {
            set
            {
                String s = value;
                requestedFields = StringUtils.split(s, StringUtils.SEMICOLON);
                requestedFieldsTbl = new Hashtable(requestedFields.Length);
                for (int i = 0; i < requestedFields.Length; i++)
                {
                    if (requestedFields[i] == "")
                    {
                        continue;
                    }
                    String fldnum = requestedFields[i];
                    String option = "I";
                    if (fldnum.IndexOf('E') != -1)
                    {
                        fldnum = fldnum.Substring(0, fldnum.Length - 1);
                        option = "E";
                    }
                    if (!requestedFieldsTbl.ContainsKey(fldnum))
                    {
                        requestedFieldsTbl.Add(fldnum, option);
                    }
                    else
                    {
                        requestedFieldsTbl[fldnum] += option;
                    }
                }
            }
        }

        public String Flags
        {
            get { return flags; }
            set { flags = value; }
        }

        public string Max
        {
            get { return max; }
            set { max = value; }
        }

        public String From
        {
            get { return from; }
            set { from = value; }
        }

        public String Part
        {
            get { return part; }
            set { part = value; }
        }

        public String Xref
        {
            get { return xref; }
            set { xref = value; }
        }

        public String Screen
        {
            get { return screen; }
            set { screen = value; }
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Options
        {
            get { return options; }
            set { options = value; }
        }

        public string MoreFrom
        {
            get { return moreFrom; }
            set { moreFrom = value; }
        }

        public string MoreIens
        {
            get { return moreIens; }
            set { moreIens = value; }
        }
    }
}
