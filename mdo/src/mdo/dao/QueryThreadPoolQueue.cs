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

namespace gov.va.medora.mdo.dao
{
    public class QueryThreadPoolQueue
    {
        Dictionary<string, Queue<QueryThread>> _connectionQueues = new Dictionary<string, Queue<QueryThread>>();
        
        public event EventHandler Changed;

        public virtual void QueueChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public QueryThread dequeue(string sitecode)
        {
            if (_connectionQueues.ContainsKey(sitecode))
            {
                lock (_connectionQueues)
                {
                    if (_connectionQueues[sitecode] != null && 
                        _connectionQueues[sitecode].Count > 0 &&
                        _connectionQueues[sitecode].Peek() != null)
                    {
                        return _connectionQueues[sitecode].Dequeue();
                    }
                }
            }
            return null;
        }

        public void queue(QueryThread qt, string siteId)
        {
            lock (_connectionQueues)
            {
                if (!_connectionQueues.ContainsKey(siteId))
                {
                    _connectionQueues.Add(siteId, new Queue<QueryThread>());
                }
                _connectionQueues[siteId].Enqueue(qt);
            }
            QueryThreadPoolEventArgs e = new QueryThreadPoolEventArgs()
            {
                QueueEventType = QueryThreadPoolEventArgs.QueueChangeEventType.QueryAdded,
                SiteId = siteId
            };
            QueueChanged(e);
        }
    }
}
