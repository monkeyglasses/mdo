using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gov.va.medora.mdo.dao.sql.cdw
{
    public class CdwConnection : AbstractConnection, IDisposable
    {
        SqlConnection _cxn;

        public CdwConnection(DataSource ds) : base(ds)
        {
            this.Account = new CdwAccount();
        }

        public override ISystemFileHandler SystemFileHandler
        {
            get { throw new NotImplementedException(); }
        }

        public override void connect()
        {
            if (DataSource == null || String.IsNullOrEmpty(DataSource.ConnectionString))
            {
                throw new mdo.exceptions.MdoException(exceptions.MdoExceptionCode.ARGUMENT_NULL, "No CDW connection string!");
            }
            _cxn = new SqlConnection(DataSource.ConnectionString);
            _cxn.Open();
            IsConnected = true;
        }

        public override object authorizedConnect(AbstractCredentials credentials, AbstractPermission permission, DataSource validationDataSource)
        {
            throw new NotImplementedException();
        }

        public override string getWelcomeMessage()
        {
            if (IsConnected)
            {
                return "OK";
            }
            throw new mdo.exceptions.MdoException(mdo.exceptions.MdoExceptionCode.USAGE_NO_CONNECTION);
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
            if (!IsConnected)
            {
                connect();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _cxn;
            cmd.CommandText = request;
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }

        public virtual object query(SqlDataAdapter adapter, AbstractPermission permission = null)
        {
            if (!IsConnected)
            {
                connect();
            }
            if (adapter.SelectCommand != null)
            {
                adapter.SelectCommand.Connection = _cxn;
                //DataSet results = new DataSet();
                //adapter.Fill(results);
                //return results;
                return adapter.SelectCommand.ExecuteReader();
            }
            else if (adapter.DeleteCommand != null)
            {
                adapter.DeleteCommand.Connection = _cxn;
                return adapter.DeleteCommand.ExecuteNonQuery();
            }
            else if (adapter.UpdateCommand != null)
            {
                adapter.UpdateCommand.Connection = _cxn;
                return adapter.UpdateCommand.ExecuteNonQuery();
            }
            else if (adapter.InsertCommand != null)
            {
                adapter.InsertCommand.Connection = _cxn;
                return adapter.InsertCommand.ExecuteNonQuery();
            }
            throw new ArgumentException("Must supply a SQL command");
        }

        public override string getServerTimeout()
        {
            throw new NotImplementedException();
        }

        public override void disconnect()
        {
            try
            {
                if (IsConnected)
                {
                    _cxn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsConnected = false;
            }
        }

        public void Dispose()
        {
            disconnect();
        }

        public override object query(SqlQuery request, Delegate functionToInvoke, AbstractPermission permission = null)
        {
            throw new NotImplementedException();
        }
    }
}
