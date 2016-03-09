using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.IS_API
{
    class IntraServiceSqlDatabase : IDisposable
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _dataReader;

        public SqlDataReader ExecuteCommandAndGetDataReader(string sqlCommandText)
        {
            OpenSqlConnection();
            CreateAndExecuteDataReader(sqlCommandText);

            return _dataReader;
        }

        private void OpenSqlConnection()
        {
            string connectionString = ConfigurationManager.AppSettings[Constants.IS_DATABASE_KEY];

            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }

        private void CreateAndExecuteDataReader(string sqlCommand)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlCommand;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _sqlConnection;

            _dataReader = cmd.ExecuteReader();
        }

        public void Dispose()
        {
            _sqlConnection.Close();
        }
    }
}
