using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Baidyanath.Models.DataAccess
{
    public enum DataBaseType
    {
        SqlClient,
        Oracle,
        OleDb,
        Odbc
    }

    public class DBHelper : IDisposable
    {
        #region ///Variable Declaration
        private bool _disposed = false;
        public string connectionString = string.Empty;
        private DataBaseType dataBasetype = DataBaseType.SqlClient;

        private DataSet dataSet = null;
        private DbConnection connection;
        private DbCommand dbCommand;
        private DbProviderFactory factory = null;
        private DbParameterCollection parameterCollection;
        private Hashtable dbOutParameterHashtable = null;

        #endregion

        #region ///Property
        private string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }
        public DataBaseType DBType
        {
            get { return this.dataBasetype; }
            set { this.dataBasetype = value; }
        }

        public DbCommand CurrentCommand
        {
            get { return this.dbCommand; }
            set { this.dbCommand = value; }
        }
        #endregion

        #region ///Constructor
        public DBHelper()
        {
            this.Constructor();
            this.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public DBHelper(String ConnectionStringName)
        {
            this.Constructor(ConnectionStringName);
            this.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }
        private void Constructor(String ConnectionStringName = "ConnectionString")
        {
            string strProviderName = ConfigurationManager.ConnectionStrings[ConnectionStringName].ProviderName;
            if (!string.IsNullOrEmpty(strProviderName))
            {
                switch (strProviderName.Trim())
                {
                    case "System.Data.SqlClient":
                        this.factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                        DBType = DataBaseType.SqlClient;
                        break;

                    case "System.Data.OleDb":
                        this.factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                        DBType = DataBaseType.OleDb;
                        break;

                    case "System.Data.Odbc":
                        this.factory = DbProviderFactories.GetFactory("System.Data.Odbc");
                        DBType = DataBaseType.Odbc;
                        break;

                    case "System.Data.OracleClient":
                        this.factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
                        DBType = DataBaseType.Oracle;
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }
        #endregion

        #region ///IUtilities Members
        public DbConnection GetConnection
        {
            get
            {
                return this.connection;
            }
        }

        #region ///DataSet
        public DataSet GetDataSet(string Query)
        {
            try
            {
                return this.GetDataSet(Query, CommandType.Text);
            }
            catch (Exception objEx)
            {
                throw objEx;
            }
        }
        public DataSet GetDataSet(string StoredProcedureName, CommandType CommandType)
        {
            if (this.dbCommand == null)
                this.dbCommand = factory.CreateCommand();

            using (this.dbCommand)
            {
                this.dbCommand.CommandText = StoredProcedureName;
                this.dbCommand.CommandType = CommandType;
                this.dbCommand.Connection = factory.CreateConnection();
                this.dbCommand.Connection.ConnectionString = this.ConnectionString;
                this.dbCommand.Connection.Open();

                using (DbDataAdapter dataAdapter = factory.CreateDataAdapter())
                {
                    dataAdapter.SelectCommand = this.dbCommand;
                    this.dataSet = new DataSet();
                    dataAdapter.Fill(this.dataSet);
                    this.parameterCollection = this.dbCommand.Parameters;
                }

                this.dbCommand.Connection.Close();
            }

            return this.dataSet;
        }
        public DataSet GetDataSet(string StoredProcedureName, CommandType CommandType, ref object ObjectName)
        {
            this.dataSet = this.GetDataSet(StoredProcedureName, CommandType);
            foreach (DbParameter dbParameter in this.parameterCollection)
            {
                if (dbParameter.Value != null && dbParameter.Direction == ParameterDirection.Output)
                {
                    ObjectName = (object)dbParameter.Value;
                    break;
                }
            }

            return this.dataSet;

        }
        #endregion

        #region ///IDataReader
        public IDataReader GetDataReader(string Query)
        {
            return this.GetDataReader(Query, CommandType.Text);
        }
        public IDataReader GetDataReader(string StoredProcedureName, CommandType CommandType)
        {
            IDataReader dataReader;

            if (this.dbCommand == null)
                this.dbCommand = factory.CreateCommand();

            using (this.dbCommand)
            {
                this.dbCommand.CommandText = StoredProcedureName;
                this.dbCommand.CommandType = CommandType;
                this.dbCommand.Connection = factory.CreateConnection();
                this.dbCommand.Connection.ConnectionString = this.ConnectionString;
                this.dbCommand.Connection.Open();

                dataReader = this.dbCommand.ExecuteReader();
                this.parameterCollection = this.dbCommand.Parameters;
            }

            return dataReader;
        }
        #endregion

        #region ///GetScalar
        public object GetScalar(string Query)
        {
            return this.GetScalar(Query, CommandType.Text);
        }
        public object GetScalar(string StoredProcedureName, CommandType CommandType)
        {
            object objReturnValue = null;

            if (this.dbCommand == null)
                this.dbCommand = factory.CreateCommand();

            using (this.dbCommand)
            {
                this.dbCommand.CommandText = StoredProcedureName;
                this.dbCommand.CommandType = CommandType;
                this.dbCommand.Connection = factory.CreateConnection();
                this.dbCommand.Connection.ConnectionString = this.ConnectionString;
                this.dbCommand.Connection.Open();

                objReturnValue = this.dbCommand.ExecuteScalar();
                this.parameterCollection = this.dbCommand.Parameters;
            }
            this.dbCommand.Connection.Close();
            return objReturnValue;
        }
        #endregion

        #region ///ExecuteNonQuery
        public int ExecuteQuery(string Query)
        {
            return this.ExecuteQuery(Query, CommandType.Text);
        }
        public int ExecuteQuery(string StoredProcedureName, CommandType CommandType)
        {
            int numberOfExcutedRow;

            if (this.dbCommand == null)
                this.dbCommand = factory.CreateCommand();

            using (this.dbCommand)
            {
                this.dbCommand.CommandText = StoredProcedureName;
                this.dbCommand.CommandType = CommandType;
                this.dbCommand.Connection = factory.CreateConnection();
                this.dbCommand.Connection.ConnectionString = this.ConnectionString;
                this.dbCommand.Connection.Open();

                numberOfExcutedRow = this.dbCommand.ExecuteNonQuery();
                this.parameterCollection = this.dbCommand.Parameters;
            }
            this.dbCommand.Connection.Close();
            return numberOfExcutedRow;
        }

        public int ExecuteQueryDataTable(string StoredProcedureName, CommandType CommandType)
        {
            if (this.dbCommand == null)
                this.dbCommand = factory.CreateCommand();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {

                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcedureName, connection))
                {
                    command.CommandType = CommandType;
                    foreach (DbParameter parameter in this.dbCommand.Parameters)
                    {
                        command.Parameters.Add(new SqlParameter(parameter.ParameterName, parameter.Value));
                    }

                    this.parameterCollection = command.Parameters;

                    return command.ExecuteNonQuery();
                    
                }               
            }

        }

        #endregion

        #region ///Transaction
        public void BeginTransaction()
        {
            this.connection = this.factory.CreateConnection();
            this.dbCommand = this.factory.CreateCommand();
            this.connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            this.dbCommand.Connection = this.connection;

            if (this.connection == null)
                throw new NotImplementedException("Connection Not Initialized.");

            if (this.connection.State == ConnectionState.Closed || this.connection.State == ConnectionState.Broken)
                this.connection.Open();

            //this.OpenConnection();
            this.dbCommand.Transaction = this.connection.BeginTransaction();
        }
        public void BeginTransaction(IsolationLevel IsoLevel)
        {
            this.connection = this.factory.CreateConnection();
            this.dbCommand = this.factory.CreateCommand();
            this.connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            this.dbCommand.Connection = this.connection;

            if (this.connection == null)
                throw new NotImplementedException("Connection Not Initialized.");

            if (this.connection.State == ConnectionState.Closed || this.connection.State == ConnectionState.Broken)
                this.connection.Open();

            this.dbCommand.Transaction = this.connection.BeginTransaction(IsoLevel);

        }
        public void CommitTransaction()
        {
            try
            {
                if (this.dbCommand.Transaction != null)
                {
                    this.dbCommand.Transaction.Commit();
                }

                if (this.connection.State == System.Data.ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
            catch (Exception objEx)
            {
                throw new Exception("Error while CommitTransaction ", objEx);
            }
            finally
            {
                this.connection = null;
                this.dbCommand = null;
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                this.dbCommand.Transaction.Rollback();

                if (this.connection.State == System.Data.ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
            catch (Exception objEx)
            {
                throw new Exception("Error while performing RollbackTransaction operation. ", objEx);
            }
            finally
            {
                this.connection = null;
                this.dbCommand = null;
            }
        }
        #endregion

        #region ///Parameters
        public int AddInParameter(string strParameterName, object objParameterValue)
        {
            return this.AddInParameter(strParameterName, objParameterValue, DbType.String);
        }
        public int AddInParameter(string strParameterName, object objParameterValue, DbType objDbType)
        {
            if (objDbType == DbType.DateTime)
            {
                objDbType = DbType.String;
                objParameterValue = (object)Convert.ToDateTime(objParameterValue).ToShortDateString();
            }

            DbParameter dbpDbParameter = this.factory.CreateParameter();
            dbpDbParameter.Direction = ParameterDirection.Input;
            dbpDbParameter.ParameterName = strParameterName.Trim();
            dbpDbParameter.DbType = objDbType;
            dbpDbParameter.Value = objParameterValue;

            if (this.dbCommand == null)
                this.dbCommand = this.factory.CreateCommand();

            return this.dbCommand.Parameters.Add(dbpDbParameter);
        }
        public int AddOutParameter(string strParameterName, DbType objDBType)
        {
            return this.AddOutParameter(strParameterName, objDBType, 500);
        }
        public int AddOutParameter(string strParameterName, DbType objDBType, int intParameterSize)
        {
            if (this.dbOutParameterHashtable == null)
            {
                this.dbOutParameterHashtable = new Hashtable();
            }
            else
            {
                this.dbOutParameterHashtable.Clear();
            }

            this.dbOutParameterHashtable.Add(strParameterName.Trim(), string.Empty);

            DbParameter pdpOutDbParameter = this.factory.CreateParameter();
            pdpOutDbParameter.ParameterName = strParameterName;
            pdpOutDbParameter.DbType = objDBType;
            pdpOutDbParameter.Size = intParameterSize;
            pdpOutDbParameter.Direction = ParameterDirection.Output;

            if (this.dbCommand == null)
                this.dbCommand = this.factory.CreateCommand();

            return this.dbCommand.Parameters.Add(pdpOutDbParameter);
        }

        public Hashtable GetOutParameterValue()
        {
            string strParameterName = string.Empty;

            if (this.dbOutParameterHashtable != null && this.dbOutParameterHashtable.Count > 0)
            {
                foreach (DbParameter dbp in this.parameterCollection)
                {
                    if (dbp.Value != null && (dbp.Direction == ParameterDirection.Output || dbp.Direction == ParameterDirection.ReturnValue))
                    {
                        if (this.dbOutParameterHashtable.ContainsKey(dbp.ParameterName))
                        {
                            this.dbOutParameterHashtable.Remove(dbp.ParameterName);
                            this.dbOutParameterHashtable.Add(dbp.ParameterName, dbp.Value);
                        }
                    }
                }

                return this.dbOutParameterHashtable;
            }

            return null;
        }
        public object GetOutParameterValue(string strParameterName)
        {
            object objValue = null;
            foreach (DbParameter dbpParameter in this.parameterCollection)
            {
                if (dbpParameter.Value != null && (dbpParameter.Direction == ParameterDirection.Output || dbpParameter.Direction == ParameterDirection.ReturnValue))
                {
                    if (string.Equals(dbpParameter.ParameterName, strParameterName))
                    {
                        objValue = (object)dbpParameter.Value;
                    }
                }
            }

            return objValue;
        }

        public void ClearAllParameters()
        {
            dbCommand.Parameters.Clear();
        }
        #endregion

        #endregion

        #region ///Memory Management

        ~DBHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {

                }

                // shared cleanup logic
                this._disposed = true;
            }
        }

        #endregion
    }
}