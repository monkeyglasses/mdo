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
using gov.va.medora.mdo.domain.sm.enums;

namespace gov.va.medora.mdo.domain.sm
{
    public class Addressee : PersistentObject
    {
        public AddresseeRoleEnum Role { get; set; }
        public long FolderId { get; set; }
        public Folder Folder { get; set; }
        public DateTime ReadDate { get; set; }
        public User Owner { get; set; }
        public Message Message { get; set; }
        public DateTime ReminderDate { get; set; }

        internal static Addressee getAddresseeFromReader(System.Data.IDataReader rdr)
        {
            return getAddresseeFromReader(rdr, mdo.dao.oracle.mhv.sm.QueryUtils.getColumnExistsTable(gov.va.medora.mdo.dao.oracle.mhv.sm.TableSchemas.ADDRESSEE_COLUMNS, rdr));
        }

        internal static Addressee getAddresseeFromReader(System.Data.IDataReader rdr, Dictionary<string, bool> columnTable)
        {
            Addressee addr = new Addressee();

            if (columnTable["ADDRESSEE_ID"])
            {
                int idIndex = rdr.GetOrdinal("ADDRESSEE_ID");
                if (!rdr.IsDBNull(idIndex))
                {
                    addr.Id = Convert.ToInt32(rdr.GetDecimal(idIndex));
                }
            }
            if (columnTable["ADDRESSEE_ROLE"])
            {
                int roleIndex = rdr.GetOrdinal("ADDRESSEE_ROLE");
                if (!rdr.IsDBNull(roleIndex))
                {
                    addr.Role = (AddresseeRoleEnum)Convert.ToInt32(rdr.GetDecimal(roleIndex));
                }
            }
            if (columnTable["SECURE_MESSAGE_ID"])
            {
                int smIdIndex = rdr.GetOrdinal("SECURE_MESSAGE_ID");
                if (!rdr.IsDBNull(smIdIndex))
                {
                    addr.Message = new Message() { Id = Convert.ToInt32(rdr.GetDecimal(smIdIndex)) };
                }
            }
            if (columnTable["USER_ID"])
            {
                int userIdIndex = rdr.GetOrdinal("USER_ID");
                if (!rdr.IsDBNull(userIdIndex))
                {
                    addr.Owner = new User() { Id = Convert.ToInt32(rdr.GetDecimal(userIdIndex)) };
                }
            }
            if (columnTable["ADDROPLOCK"])
            {
                int oplockIndex = rdr.GetOrdinal("ADDROPLOCK");
                if (!rdr.IsDBNull(oplockIndex))
                {
                    addr.Oplock = Convert.ToInt32(rdr.GetDecimal(oplockIndex));
                }
            }
            if (columnTable["FOLDER_ID"])
            {
                int folderIdIndex = rdr.GetOrdinal("FOLDER_ID");
                if (!rdr.IsDBNull(folderIdIndex))
                {
                    addr.FolderId = Convert.ToInt32(rdr.GetDecimal(folderIdIndex));
                }
                addr.Folder = Folder.getFolderFromReader(rdr);
            }
            if (columnTable["READ_DATE"])
            {
                int readDateIndex = rdr.GetOrdinal("READ_DATE");
                if (!rdr.IsDBNull(readDateIndex))
                {
                    addr.ReadDate = rdr.GetDateTime(readDateIndex);
                }
            }
            if (columnTable["REMINDER_DATE"])
            {
                int reminderDateIndex = rdr.GetOrdinal("REMINDER_DATE");
                if (!rdr.IsDBNull(reminderDateIndex))
                {
                    addr.ReminderDate = rdr.GetDateTime(reminderDateIndex);
                }
            }

            return addr;
        }

    }
}
