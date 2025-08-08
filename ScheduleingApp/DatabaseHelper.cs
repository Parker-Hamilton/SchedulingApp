
using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace ScheduleingApp
{
    public class DatabaseHelper : IDisposable
    {
        private readonly Credentials credentials = Credentials.Instance;
        private readonly MySqlConnection connection;
        private readonly MySqlDataReader _reader;
        private readonly string connectionString;

        private bool _disposed = false;

        public DatabaseHelper()
        {
            connectionString = "Server=localhost;" + $"Uid=sqlUser;Pwd=Passw0rd!; Database=client_schedule; Port=3306; Allow User Variables=True;AllowBatch=True;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }
        public void ExecuteStoredProc(string name, List<StoredProcedureParameters> parameters = null)
        {
            using (var command = new MySqlCommand(name, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param.Name, param.MySqlDbType).Value = param.Value;
                }
                command.ExecuteNonQuery();
            }
        }
        public int ExecuteStoredProc(string name, List<StoredProcedureParameters> parameters = null, MySqlParameter returnValue = null)
        {
            using (var command = new MySqlCommand(name, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param.Name, param.MySqlDbType).Value = param.Value;
                }
                command.Parameters.Add(returnValue);
                command.ExecuteNonQuery();

                return (int)returnValue.Value;
            }
        }
        public DataTable MySqlCommand_GET(string sqlCommand)
        {

            using (var command = new MySqlCommand(sqlCommand, connection))
            using (var dbAdapter = new MySqlDataAdapter(command))
            {
                var dataTable = new DataTable();
                dbAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public int ExecuteMySqlCommand_PUT(string sqlCommand, string returnCommand)
        {

            using (var command = new MySqlCommand(sqlCommand, connection))
            {
                command.ExecuteNonQuery();
            }
            using (var command = new MySqlCommand(returnCommand, connection))
            {

                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
        public void ExecuteMySqlCommand_VOID(string sqlCommand)
        {

            using (var command = new MySqlCommand(sqlCommand, connection))
            {
                command.ExecuteNonQuery();
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
