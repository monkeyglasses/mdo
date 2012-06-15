using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using gov.va.medora.utils;
using gov.va.medora.mdo.src.mdo;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.dao.vista
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// for more information on underlying calls, see http://www.hardhats.org/fileman/pm/dba_frm.htm
    /// </remarks>
    public class VistaToolsDao : IToolsDao
    {
        AbstractConnection cxn = null;

        public VistaToolsDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
        }

        public string[] ddrLister(
            string file,
            string iens,
            string flds,
            string flags,
            string maxRex,
            string from,
            string part,
            string xref,
            string screen,
            string identifier
            )
        {
            DdrLister query = new DdrLister(cxn);
            query.File = file;
            query.Iens = iens;
            query.Fields = flds;
            query.Flags = flags;
            query.Max = maxRex;
            query.From = from;
            query.Part = part;
            query.Xref = xref;
            query.Screen = screen;
            query.Id = identifier;
            String[] rtn = query.execute();
            return rtn;
        }

        public string getFieldAttribute(string file, string fld, string attribute)
        {
            string arg = "$P($$GET1^DID(\"" + file + "\",\"" + fld + "\",\"\",\"" + attribute + "\"),U,1)";
            string rtn = VistaUtils.getVariableValue(cxn,arg);
            return rtn;
        }

        public string ddiol(string file, string fld, string attribute)
        {
            string arg = "$P(D EN^DDIOL($$GET1^DID(\"9000010.23\",\".02\",\"\",\"LABEL\")),U,1)";
            string rtn = VistaUtils.getVariableValue(cxn,arg);
            return rtn;
        }

        public string[] ddrGetsEntry(
            string file,
            string iens,
            string flds,
            string flags)
        {
            DdrGetsEntry query = new DdrGetsEntry(cxn);
            query.File = file;
            query.Iens = iens;
            query.Fields = flds;
            query.Flags = flags;
            string[] result = query.execute();
            return result;
        }

        public string getVariableValue(string arg)
        {
            return VistaUtils.getVariableValue(cxn,arg);
        }

        public KeyValuePair<string,string>[] getRpcList(string target)
        {
            MdoQuery request = buildGetRpcListRequest(target);
            string response = (string)cxn.query(request);
            return toRpcArray(response);
        }

        internal MdoQuery buildGetRpcListRequest(string target)
        {
            VistaQuery vq = new VistaQuery("XWB RPC LIST");
            vq.addParameter(vq.LITERAL, target);
            return vq;
        }

        internal KeyValuePair<string,string>[] toRpcArray(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            KeyValuePair<string, string>[] result = new KeyValuePair<string,string>[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                int p = lines[i].IndexOf(' ');
                string IEN = lines[i].Substring(0, p);
                string name = getRpcName(IEN);
                result[i] = new KeyValuePair<string, string>(IEN, name);
            }
            return result;
        }

        public string getRpcName(string rpcIEN)
        {
            string arg = "$P($G(^XWB(8994," + rpcIEN + ",0)),U,1)";
            string response = VistaUtils.getVariableValue(cxn,arg);
            return response;
        }

        public bool isRpcAvailableAtSite(string target, string localRemote, string version)
        {
            MdoQuery request = buildIsRpcAvailableAtSiteRequest(target, localRemote, version);
            string response = (string)cxn.query(request);
            return response == "1";
        }

        internal MdoQuery buildIsRpcAvailableAtSiteRequest(string target, string localRemote, string version)
        {
            localRemote = localRemote.ToUpper();
            if (localRemote != "R" && localRemote != "L" && localRemote != "")
            {
                throw new Exception("Invalid localRemote param, must be empty, R or L");
            }
            VistaQuery vq = new VistaQuery("XWB IS RPC AVAILABLE");
            vq.addParameter(vq.LITERAL, target);
            vq.addParameter(vq.LITERAL, localRemote);
            vq.addParameter(vq.LITERAL, (version == "" ? "0" : version));
            return vq;
        }

        public string isRpcAvailable(string target, string context)
        {
            return isRpcAvailable(target, context, "L", "");
        }

        public string isRpcAvailable(string target, string context, string localRemote, string version)
        {
            if (!isRpcAvailableAtSite(target, localRemote, version))
            {
                return "Not installed at site";
            }
            KeyValuePair<string,string>[] rpcList = getRpcList(target);
            string rpcIEN = rpcList[0].Key;
            VistaUserDao userDao = new VistaUserDao(cxn);
            string optIEN = userDao.getOptionIen(context);
            if (!StringUtils.isNumeric(optIEN))
            {
                return "Error getting context IEN: " + optIEN;
            }
            DdrLister query = buildGetOptionRpcsQuery(optIEN);
            string[] optRpcs = query.execute();
            if (!isRpcIenPresent(optRpcs, rpcIEN))
            {
                return "RPC not in context";
            }
            return "YES";
        }

        internal DdrLister buildGetOptionRpcsQuery(string optIEN)
        {
            DdrLister query = new DdrLister(cxn);
            query.File = "19.05";
            query.Iens = "," + optIEN + ",";
            query.Fields = ".01";
            query.Flags = "IP";
            return query;
        }

        internal bool isRpcIenPresent(string[] optRpcs, string rpcIEN)
        {
            if (optRpcs == null)
            {
                return false;
            }
            for (int i = 0; i < optRpcs.Length; i++)
            {
                string[] flds = StringUtils.split(optRpcs[i], StringUtils.CARET);
                if (flds[1] == rpcIEN)
                {
                    return true;
                }
            }
            return false;
        }

        public string xusHash(string s)
        {
            DdrLister query = buildXusHashQuery(s);
            string[] response = query.execute();
            return StringUtils.piece(response[0],StringUtils.CARET,2);
        }

        internal DdrLister buildXusHashQuery(string s)
        {
            DdrLister query = new DdrLister(cxn);
            query.File = "200";
            query.Flags = "IP";
            query.Fields = "";
            query.Max = "1";
            query.Id = "S X=$$EN^XUSHSH(\"" + s + "\") D EN^DDIOL(X)";
            return query;
        }

        public FileHeader getFileHeader(string globalName)
        {
            string arg = "$G(" + globalName + "0))";
            string response = VistaUtils.getVariableValue(cxn, arg);
            return toFileHeader(response);
        }

        internal FileHeader toFileHeader(string response)
        {
            if (response == "")
            {
                return null;
            }
            FileHeader result = new FileHeader();
            string[] flds = StringUtils.split(response, StringUtils.CARET);
            result.Name = flds[0];
            result.AlternateName = "";
            int i = 0;
            while (StringUtils.isNumericChar(flds[1][i]))
            {
                result.AlternateName += flds[1][i];
                i++;
            }
            if (i < flds[1].Length)
            {
                result.Characteristics = new ArrayList();
                do
                {
                    result.Characteristics.Add(flds[1][i]);
                    i++;
                } while (i < flds[1].Length);
            }
            result.LatestId = flds[2];
            result.NumberOfRecords = Convert.ToInt64(flds[3]);
            return result;
        }

        public string getTimestamp()
        {
            MdoQuery request = buildGetTimestampRequest();
            string response = (string)cxn.query(request);
            return VistaTimestamp.toUtcString(response);
        }

        internal MdoQuery buildGetTimestampRequest()
        {
            VistaQuery vq = new VistaQuery("ORWU DT");
            vq.addParameter(vq.LITERAL, "NOW");
            return vq;
        }

        public string runRpc(string rpcName, string[] paramValues, int[] paramTypes, bool[] paramEncrypted)
        {

            MdoQuery request = buildRpcRequest(rpcName, paramValues, paramTypes, paramEncrypted);
            string response = (string)cxn.query(request);
            return response;
        }

        public MdoQuery buildRpcRequest(string rpcName, string[] paramValues, int[] paramTypes, bool[] paramEncrypted)
        {
            if (String.IsNullOrEmpty(rpcName))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "rpcName must be specified");

            }
            if (paramValues.Length != paramTypes.Length || paramValues.Length != paramEncrypted.Length)
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "paramValues, paramTypes and paramEncrpted must be the same size");
            }

            VistaQuery vq = new VistaQuery(rpcName);
            for (int n = 0; n < paramValues.Length; n++)
            {
                if (paramEncrypted[n])
                    vq.addEncryptedParameter(paramTypes[n], paramValues[n]);
                else
                    vq.addParameter(paramTypes[n], paramValues[n]);
            }
            return vq;
        }

        public byte runQueryThread()
        {
            // this function serves to do nothing more than serve as a tool to initialize a query thread. 
            // Afaik, a byte is the smallest object available in the .NET runtime 
            return new byte();
        }

    }
}
