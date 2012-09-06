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

namespace gov.va.medora.mdo.domain.sm
{
    [Serializable]
    public class MessageAttachment : BaseModel
    {
        //private Blob _attachment;
        private byte[] _smFile;
        private string _attachmentName;
        private string _mimeType;		 
		 
		public byte[] SmFile
        {
			get { return _smFile; }
            set { _smFile = value; }
		}

		public string AttachmentName 
        {
			get { return _attachmentName; }
            set { _attachmentName = value; }
		}

        public string MimeType 
        {
			get { return _mimeType; }
            set { _mimeType = value; }
		}

        internal static MessageAttachment getAttachmentFromReader(System.Data.IDataReader rdr)
        {
            return getAttachmentFromReader(rdr, mdo.dao.oracle.mhv.sm.QueryUtils.getColumnExistsTable(gov.va.medora.mdo.dao.oracle.mhv.sm.TableSchemas.MESSAGE_ATTACHMENT_COLUMNS, rdr));
        }

        internal static MessageAttachment getAttachmentFromReader(System.Data.IDataReader rdr, Dictionary<string, bool> columnTable)
        {
            MessageAttachment attachment = new MessageAttachment();

            if (columnTable["ATTACHMENT_ID"])
            {
                int idIndex = rdr.GetOrdinal("ATTACHMENT_ID");
                if (!rdr.IsDBNull(idIndex))
                {
                    attachment.Id = Convert.ToInt32(rdr.GetDecimal(idIndex));
                }
            }
            if (columnTable["ATTACHMENT_NAME"])
            {
                int nameIndex = rdr.GetOrdinal("ATTACHMENT_NAME");
                if (!rdr.IsDBNull(nameIndex))
                {
                    attachment.AttachmentName = rdr.GetString(nameIndex);
                }
            }
            if (columnTable["ATTACHMENT"])
            {
                int attIndex = rdr.GetOrdinal("ATTACHMENT");
                if (!rdr.IsDBNull(attIndex))
                {
                    // not crazy about this implementation as it appears to invoke the reader twice but the commented out code
                    // block directly below throws an exception when calling GetOracleBlob for some reason... The good thing about
                    // this solution is it should work for all IDataReader implementations and doesn't need to be cast to an OracleDataReader
                    byte[] blob = new byte[rdr.GetBytes(attIndex, 0, null, 0, Int32.MaxValue)];
                    rdr.GetBytes(attIndex, 0, blob, 0, blob.Length);
                    attachment.SmFile = blob;
                    //if (rdr is Oracle.DataAccess.Client.OracleDataReader)
                    //{
                    //    System.Console.WriteLine(rdr[attIndex].GetType().ToString());
                    //    Oracle.DataAccess.Types.OracleBlob blob = ((Oracle.DataAccess.Client.OracleDataReader)rdr).GetOracleBlob(attIndex);
                    //    byte[] buf = new byte[blob.Length];
                    //    blob.Read(buf, 0, Convert.ToInt32(blob.Length));
                    //    attachment.SmFile = buf;
                    //}
                }
            }
            if (columnTable["MIME_TYPE"])
            {
                int mimeTypeIndex = rdr.GetOrdinal("MIME_TYPE");
                if (!rdr.IsDBNull(mimeTypeIndex))
                {
                    attachment.MimeType = rdr.GetString(mimeTypeIndex);
                }
            }
            if (columnTable["ATTOPLOCK"])
            {
                int oplockIndex = rdr.GetOrdinal("ATTOPLOCK");
                if (!rdr.IsDBNull(oplockIndex))
                {
                    attachment.Oplock = Convert.ToInt32(rdr.GetDecimal(oplockIndex));
                }
            }


            return attachment;
        }
    }
}
