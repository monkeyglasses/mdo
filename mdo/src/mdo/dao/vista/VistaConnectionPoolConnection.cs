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

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaConnectionPoolConnection : VistaConnection
    {
        public event EventHandler Changed;
        ConnectionPoolEventArgs _eventArgs = new ConnectionPoolEventArgs();

        public VistaConnectionPoolConnection(DataSource ds) : base(ds)
        {
            // class serves only to override calls for attaching events for the connection pool
        }

        public virtual void OnChanged(EventArgs e)
        {
            if (Changed != null) // ensures subscriptions exist
            {
                Changed(this, e);
            }
        }

        public override void connect()
        {
            base.connect();
            //_eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            //OnChanged(_eventArgs);
        }

        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            object result = base.authorizedConnect(credentials, permission, validationDataSource);
            //_eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            //OnChanged(_eventArgs);
            return result;
        }

        public override void disconnect()
        {
            base.disconnect();
            _eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.Disconnected;
            OnChanged(_eventArgs);
        }

        public override object query(MdoQuery vq, AbstractPermission context = null)
        {
            object result = base.query(vq, context);
            _eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            OnChanged(_eventArgs); // query complete event
            return result;
        }

        public override object query(string request, AbstractPermission context = null)
        {
            object result = base.query(request, context);
            _eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            OnChanged(_eventArgs); // query complete event
            return result;
        }

        public override string getServerTimeout()
        {
            string result = base.getServerTimeout();
            _eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            OnChanged(_eventArgs); // query complete event
            return result;
        }

        public override bool hasPatch(string patchId)
        {
            bool result = base.hasPatch(patchId);
            _eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            OnChanged(_eventArgs); // query complete event
            return result;
        }

        public override string getWelcomeMessage()
        {
            string result = base.getWelcomeMessage();
            _eventArgs.ConnectionEventType = ConnectionPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
            OnChanged(_eventArgs); // query complete event
            return result;
        }
    }
}
