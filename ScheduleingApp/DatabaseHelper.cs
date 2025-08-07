
using System;
using System.Data;
using System.Data.SqlClient;

namespace ScheduleingApp
{
    public class DatabaseHelper : IDisposable
    {
        private readonly Credentials credentials = Credentials.Instance;
        private readonly SqlConnection connection;
        private readonly SqlDataReader _reader;
        private readonly string connectionString;

        private bool _disposed = false;

        public DatabaseHelper()
        {
            connectionString = "server=localhost\\SQLEXPRESS;" + $"User Id={credentials.Username};Password={credentials.Password}; database=Schedules";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        public void ExecuteStoredProc(string name, List<StoredProcedureParameters> parameters = null)
        {
            using (var command = new SqlCommand(name, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param.Name, param.SqlDbType).Value = param.Value;
                }
                command.ExecuteNonQuery();
            }
        }
        public int ExecuteStoredProc(string name, List<StoredProcedureParameters> parameters = null, SqlParameter returnValue = null)
        {
            using (var command = new SqlCommand(name, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param.Name, param.SqlDbType).Value = param.Value;
                }
                command.Parameters.Add(returnValue);
                command.ExecuteNonQuery();

                return (int)returnValue.Value;
            }
        }
        public DataTable ExecuteStoredProc(string query)
        {
            using (var command = new SqlCommand(query, connection))
            using (var dbAdapter = new SqlDataAdapter(command))
            {
                var dataTable = new DataTable();
                dbAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        ~DatabaseHelper()
        {
            Dispose(false);
        }
    }
}
