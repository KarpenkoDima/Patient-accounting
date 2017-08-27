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

        public static string DbProviderName
        {
            get { return ConfigurationManager.ConnectionStrings["SOPB.PatientAcounting"].ProviderName; }
        }

        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        public static SqlCommand SqlCommand
        {
            get { return _sqlConnection.CreateCommand(); }
        }

        public static SqlConnection GetConnection(string login, string password)
        {
            SetConnection(login, password);
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException("Class Connection Manager","Незадана строка подключения к БД. Вызовите для начала метод SetConnectionString.");
            }
            return _sqlConnection;
        }

        private static void SetConnection(string login, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = _dbServerName;
            builder.InitialCatalog = _dbDatabaseName;
            builder.UserID = login;
            builder.Password = password;
            builder.MultipleActiveResultSets = true;
            _connectionString = builder.ConnectionString;
            if (string.IsNullOrEmpty(_sqlConnection.ConnectionString))
            {
                _sqlConnection.ConnectionString = _connectionString;
            }
            else
            {
                _sqlConnection = new SqlConnection(_connectionString);
            }
        }

        public static bool TestConnection(string login, string password)
        {
            try
            {
                using (SqlConnection connection = GetConnection(login, password))
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
