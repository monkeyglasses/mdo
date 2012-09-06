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

namespace gov.va.medora.mdo.dao
{
    public interface IOrdersDao
    {
        Order[] getOrdersForPatient();
        Order[] getOrdersForPatient(string pid);
        OrderedDictionary getOrderableItemsByName(string name);
        string getOrderStatusForPatient(string dfn, string orderableItemId);
        OrderedDictionary getOrderDialogsForDisplayGroup(string displayGroupId);
        List<OrderDialogItem> getOrderDialogItems(string dialogId);
        Order writeSimpleOrderByPolicy(
            Patient patient,
            string duz,
            string esig,
            string locationIen,
            string orderIen,
            DateTime startDate);
    }
}
