using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using gov.va.medora.utils;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaFile
    {
        string fileName;
        string fileNum;
        string global;
        string mdoName;
        DictionaryHashList fields;

        public VistaFile() { }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string FileNumber
        {
            get { return fileNum; }
            set { fileNum = value; }
        }

        public string Global
        {
            get { return global; }
            set { global = value; }
        }

        public string MdoName
        {
            get { return mdoName; }
            set { mdoName = value; }
        }

        public DictionaryHashList Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public string getFieldString()
        {
            string result = "";
            for (int i = 0; i < fields.Count; i++)
            {
                DictionaryEntry e = fields[i];
                result += ((VistaField)e.Value).VistaNumber + ';';
            }
            if (result != "")
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
