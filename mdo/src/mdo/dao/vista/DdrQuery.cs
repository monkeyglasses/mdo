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
using gov.va.medora.utils;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.vista
{
    public class DdrQuery
    {
        AbstractConnection cxn;

        public DdrQuery(AbstractConnection cxn)
        {
            this.cxn = cxn;
        }

        public string execute(MdoQuery vq)
        {
            if (cxn.Account.PrimaryPermission == null ||
                String.IsNullOrEmpty(cxn.Account.PrimaryPermission.Name))
            {
                throw new UnauthorizedAccessException("Current context is empty");
            }
            AbstractPermission currentContext = cxn.Account.PrimaryPermission;

            if (currentContext.Name != VistaConstants.MDWS_CONTEXT && currentContext.Name != VistaConstants.DDR_CONTEXT)
            {
                changeContext(cxn);
            }

            string response = (string)cxn.query(vq);

            if (currentContext.Name != VistaConstants.MDWS_CONTEXT && currentContext.Name != VistaConstants.DDR_CONTEXT)
            {
                ((VistaAccount)cxn.Account).setContext(currentContext);
            }
            return response;
        }

        internal void changeContext(AbstractConnection cxn)
        {
            VistaAccount acct = (VistaAccount)cxn.Account;

            //if (acct.Permissions.ContainsKey(VistaConstants.MDWS_CONTEXT))
            //{
            //    acct.setContext(acct.Permissions[VistaConstants.MDWS_CONTEXT]);
            //}
            //else
            //{
            //    acct.addContextInVista(cxn.Uid, new MenuOption(VistaConstants.MDWS_CONTEXT));
            //}
            if (acct.Permissions.ContainsKey(VistaConstants.DDR_CONTEXT))
            {
                acct.setContext(acct.Permissions[VistaConstants.DDR_CONTEXT]);
            }
            else
            {
                acct.addContextInVista(cxn.Uid, new MenuOption(VistaConstants.DDR_CONTEXT));
            }
        }
    }
}
