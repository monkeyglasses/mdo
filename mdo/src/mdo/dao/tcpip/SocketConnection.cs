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
using System.Net.Sockets;
using gov.va.medora.mdo.dao.vista;
using System.Net;

namespace gov.va.medora.mdo.dao.tcpip
{
    public class SocketConnection : AbstractConnection
    {
        public string EndOfMessage { get; set; }
        Socket _socket;

        public SocketConnection(DataSource src) : base(src) { }


        public override ISystemFileHandler SystemFileHandler
        {
            get { throw new NotImplementedException(); }
        }

        public override void connect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = null;
            if (IPAddress.TryParse(DataSource.Provider, out ip))
            {
                _socket.Connect(ip, DataSource.Port);
            }
            else
            {
                _socket.Connect(DataSource.Provider, DataSource.Port);
            }
        }

        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            throw new NotImplementedException();
        }

        public override string getWelcomeMessage()
        {
            throw new NotImplementedException();
        }

        public override bool hasPatch(string patchId)
        {
            throw new NotImplementedException();
        }

        public override object query(MdoQuery request, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }

        public override object query(string request, AbstractPermission permission = null)
        {
            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(request);
            int sent = _socket.Send(requestBytes);
            _socket.ReceiveTimeout = 120;
            
            byte[] buffer = new byte[1024];
            int bytesReceived = _socket.Receive(buffer, _socket.Available, SocketFlags.None);

            string batch = System.Text.Encoding.UTF8.GetString(buffer);
            StringBuilder sb = new StringBuilder(batch);

            while (!batch.Contains(EndOfMessage))
            {
                bytesReceived = _socket.Receive(buffer, _socket.Available, SocketFlags.None);
                sb.Append(batch = System.Text.Encoding.UTF8.GetString(buffer));
            }

            return sb.ToString();
        }

        public override object query(SqlQuery request, Delegate functionToInvoke, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }

        public override string getServerTimeout()
        {
            throw new NotImplementedException();
        }

        public override void disconnect()
        {
            try
            {
                _socket.Disconnect(false);
                _socket.Close();
            }
            catch (Exception) { /* just catch */ }
        }
    }
}
