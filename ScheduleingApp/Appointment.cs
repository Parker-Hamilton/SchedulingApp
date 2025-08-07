using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleingApp
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Customer Customer { get; set; }
        public string CustomerName => Customer?.Name;
        public User User { get; set; }
        public string UserName => User?.Username;
        public string StartTime => Start.ToString("h:mm tt");
        public string EndTime => End.ToString("h:mm tt");
        public List<StoredProcedureParameters> Parameters;
        public void SetDetails(int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, Customer customer, User user)
        {
            this.CustomerID = customerId;
            this.UserID = userId;
            this.Title = title;
            this.Description = description;
            this.Location = location;
            this.Contact = contact;
            this.Type = type;
            this.URL = url;
            this.Start = start;
            this.End = end;
            this.Customer = customer;
            this.User = user;
        }
        public void SetDetailsFromDatabase(int id, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, Customer customer, User user)
        {
            this.Id = id;
            this.CustomerID = customerId;
            this.UserID = userId;
            this.Title = title;
            this.Description = description;
            this.Location = location;
            this.Contact = contact;
            this.Type = type;
            this.URL = url;
            this.Start = start;
            this.End = end;
            this.Customer = customer;
            this.User = user;
        }
        private void InitParameters(string type)
        {
            if (type == "add")
            {
                Parameters = new List<StoredProcedureParameters>()
                {

                    new StoredProcedureParameters("@customerId", this.CustomerID, SqlDbType.Int),
                    new StoredProcedureParameters("@userId", this.UserID, SqlDbType.Int),
                    new StoredProcedureParameters("@title", this.Title?.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@description", this.Description?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@location", this.Location?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@contact", this.Contact?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@type", this.Type?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@url", this.URL?.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@start", this.Start, SqlDbType.DateTime),
                    new StoredProcedureParameters("@end", this.End, SqlDbType.DateTime),
                    new StoredProcedureParameters("@createdBy", Credentials.Instance.Username.Trim(), SqlDbType.VarChar),

                };
            }
            else if (type == "modify")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@appointmentId", this.Id, SqlDbType.Int),
                    new StoredProcedureParameters("@customerId", this.CustomerID, SqlDbType.Int),
                    new StoredProcedureParameters("@userId", this.UserID, SqlDbType.Int),
                    new StoredProcedureParameters("@title", this.Title?.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@description", this.Description?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@location", this.Location?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@contact", this.Contact?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@type", this.Type?.Trim(), SqlDbType.Text),
                    new StoredProcedureParameters("@url", this.URL?.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@start", this.Start, SqlDbType.DateTime),
                    new StoredProcedureParameters("@end", this.End, SqlDbType.DateTime),
                    new StoredProcedureParameters("@lastUpdateBy", Credentials.Instance.Username.Trim(), SqlDbType.VarChar)
                };
            }
            else if (type == "delete")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@appointmentId", this.Id, SqlDbType.Int),
                };
            }
        }

        public bool AddAppointmentInDatabase()
        {
            this.InitParameters("add");
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    string storedProcedureName = ConfigurationManager.AppSettings["SP_AddAppointment"];

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    returnValue.SqlDbType = SqlDbType.Int;
                    this.Id = connection.ExecuteStoredProc(storedProcedureName, this.Parameters, returnValue);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }
        public bool ModifyAppointmentInDatabase()
        {
            this.InitParameters("modify");
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    string storedProcedureName = ConfigurationManager.AppSettings["SP_UpdateAppointment"];
                    connection.ExecuteStoredProc(storedProcedureName, this.Parameters);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }
        public bool DeleteAppointmentInDatabase()
        {
            this.InitParameters("delete");
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    string storedProcedureName = ConfigurationManager.AppSettings["SP_DeleteAppointment"];
                    connection.ExecuteStoredProc(storedProcedureName, this.Parameters);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }
    }
}
