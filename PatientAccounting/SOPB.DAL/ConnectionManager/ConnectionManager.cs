using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SOPB.Accounting.DAL.ConnectionManager
{
    /// <summary>
    /// Repository for PatientAccounting connection settings
    /// </summary>
    public static class ConnectionManager
    {
        private static bool _isInit = true;

        // cache data for connection settings.
        private static string _dbProviderName;
        private static string _dbDatabaseName;
        private static string _dbServerName;

        private static string _connectionString;
        private static SqlConnection _sqlConnection;

        static ConnectionManager()
        {
            _dbDatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            _dbServerName = ConfigurationManager.AppSettings["ServerName"];
            _dbProviderName = ConfigurationManager.ConnectionStrings["SOPB.PatientAccounting"].ProviderName;
            _connectionString = String.Empty;
          _sqlConnection = new SqlConnection();
        }

        public static SqlConnection Connection
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new ArgumentNullException("Class Connection Manager",
                       "Незадана строка подключения к БД. Вызовите для начала метод SetConnectionString.");
                }
               else  if (_sqlConnection.State == ConnectionState.Closed && string.IsNullOrEmpty(_sqlConnection.ConnectionString))
                    _sqlConnection.ConnectionString = _connectionString;
                return _sqlConnection;
            }
        }
        public static void SetConnection(string login, string password)
        {
            if (_isInit)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _dbServerName;
                builder.InitialCatalog = _dbDatabaseName;
                builder.UserID = login;
                builder.Password = password;
                builder.MultipleActiveResultSets = true;
                _connectionString = builder.ConnectionString;
                _sqlConnection.ConnectionString = _connectionString;
                _isInit = false;
            }
        }

        public static bool TestConnection(string login, string password)
        {
            try
            {
                SetConnection(login, password);
                using (SqlConnection connection = Connection)
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
