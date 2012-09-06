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
using System.Collections.Specialized;
using System.Text;

namespace gov.va.medora.mdo
{
    public class OrderDialog
    {
        string dialogId;
        string name;
        int quickLevel;
        string responseId;
        string type;
        string formId;
        string displayGrp;
        OrderResponse[] responses;
        IndexedHashtable columns;

        public OrderDialog() {}
        
        public string DialogId
        {
            get { return dialogId; }
            set { dialogId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int QuickLevel
        {
            get { return quickLevel; }
            set { quickLevel = value; }
        }

        public string ResponseId
        {
            get { return responseId; }
            set { responseId = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string FormId
        {
            get { return formId; }
            set { formId = value; }
        }

        public string DisplayGrp
        {
            get { return displayGrp; }
            set { displayGrp = value; }
        }

        public OrderResponse[] Responses
        {
            get { return responses; }
            set { responses = value; }
        }

        public IndexedHashtable Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public bool Exists(string colnum)
        {
            if (Columns == null)
            {
                return false;
            }
            return Columns.ContainsKey(colnum);
        }

        public void AddColumn(string colnum)
        {
            Columns.Add(colnum, new OrderDialogColumn(colnum));
        }

        public OrderDialogColumn GetColumn(string colnum)
        {
            return (OrderDialogColumn)Columns.GetValue(colnum);
        }
    }
}
