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
using System.Text;

namespace gov.va.medora.mdo.dao
{
    public enum PermissionType
    {
        MenuOption,
        DelegatedOption,
        SecurityKey
    }

    public abstract class AbstractPermission
    {
        string permissionId;
        string name;
        string recordId;
        bool primary;

        public AbstractPermission() { }

        public AbstractPermission(string name)
        {
            Name = name;
        }

        public AbstractPermission(string id, string name)
        {
            RecordId = id;
            Name = name;
        }

        public AbstractPermission(string permissionId, string name, string recordId)
        {
            PermissionId = permissionId;
            Name = name;
            RecordId = recordId;
        }

        public string PermissionId
        {
            get { return permissionId; }
            set { permissionId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string RecordId
        {
            get { return recordId; }
            set { recordId = value; }
        }

        public abstract PermissionType Type
        {
            get;
        }

        public bool IsPrimary
        {
            get { return primary; }
            set { primary = value; }
        }

        //public static AbstractPermission getPermissionForSource(DataSource source, string permissionName)
        //{
        //    if (source.Protocol == "VISTA")
        //    {
        //        return new gov.va.medora.mdo.dao.vista.MenuOption(permissionName);
        //    }
        //    return null;
        //}

        //public static string getDefaultPermissionStringForSource(DataSource source)
        //{
        //    if (source.Protocol == "VISTA")
        //    {
        //        return gov.va.medora.mdo.dao.vista.VistaConstants.CPRS_CONTEXT;
        //    }
        //    return null;
        //}
    }
}
