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
using System.Reflection;
using gov.va.medora.mdo.exceptions;

namespace gov.va.medora.mdo.dao
{
    public class QueryThread
    {
        public DateTime QueueTimestamp { get; set; }
        public DateTime DequeueTimestamp { get; set; }
        public DateTime CompleteTimestamp { get; set; }

        string id;
	    Object dao;
	    string methodName;
	    Object[] args;
	    Object result;

        public event EventHandler Changed;
        // not crazy about this from an OOP perspective but is the solution for now for enabling
        // connection availablity events in the ConnectionPool
        public AbstractConnection Connection { get; set;  }

	    public QueryThread(string id, Object dao, string methodName, Object[] args)
	    {
            this.id = id;
		    this.dao = dao;
		    this.methodName = methodName;
		    this.args = args;
	    }

        public virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public void setConnection(AbstractConnection cxn)
        {
            Type daoClass = dao.GetType();
            ConstructorInfo daoConstructor = null;
            ConstructorInfo[] constructors = daoClass.GetConstructors();

            foreach (ConstructorInfo ci in constructors)
            {
                ParameterInfo[] constructorParams = ci.GetParameters();
                if (constructorParams == null || constructorParams.Length != 1)
                {
                    continue;
                }
                daoConstructor = ci;
                break;
            }
            this.dao = daoConstructor.Invoke(new object[] { cxn });
            // again, not crazy about this OOP-wise but doing for pooling
            this.Connection = cxn;
        }

        public Object Result
        {
            get { return result; }
        }

        public string Id
        {
            get { return id; }
        }

        public bool isExceptionResult()
        {
    	    return (result != null && result.GetType().Name.EndsWith("Exception"));
        }

        public void execute()
        {
            if (dao == null)    // This data source has no DAO for this data domain
            {
                return;
            }
            Type theClass = dao.GetType();
            Type[] theParamTypes = new Type[this.args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    throw new MdoException(MdoExceptionCode.ARGUMENT_NULL, "Cannot have null args in QueryThread");
                }
                else
                {
                    theParamTypes[i] = args[i].GetType();
                }
            }
            MethodInfo theMethod = theClass.GetMethod(methodName, theParamTypes);
            try
            {
                result = theMethod.Invoke(dao, BindingFlags.InvokeMethod, null, args, null);
            }
            catch (TargetInvocationException tie)
            {
                result = tie.InnerException;
            }
            catch (Exception e)
            {
                result = e;
            }
            finally
            {
                QueryThreadPoolEventArgs e = new QueryThreadPoolEventArgs();
                e.ConnectionEventType = QueryThreadPoolEventArgs.ConnectionChangeEventType.ConnectionAvailable;
                OnChanged(e);
            }
        }
    }
}
