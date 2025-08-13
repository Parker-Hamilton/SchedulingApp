using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
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
        public string StartTimeLocal { get; set; }
        public string EndTimeLocal { get; set; }

        public List<StoredProcedureParameters> Parameters;
        public void SetDetails(int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, Customer customer, User user)
        {
            this.CustomerID = customerId;
            this.UserID = userId;
            this.Title = title.Trim();
            this.Description = description.Trim();
            this.Location = location.Trim();
            this.Contact = contact.Trim();
            this.Type = type.Trim();
            this.URL = url.Trim();
            this.Start = TimeZoneInfo.ConvertTimeToUtc(start, TimeZoneInfo.Local);
            this.End = TimeZoneInfo.ConvertTimeToUtc(end, TimeZoneInfo.Local);
            this.Customer = customer;
            this.User = user;
            this.StartTimeLocal = TimeZoneInfo.ConvertTime(this.Start, TimeZoneInfo.Utc, TimeZoneInfo.Local).ToString("MMM dd hh:mm tt");
            this.EndTimeLocal = TimeZoneInfo.ConvertTime(this.End, TimeZoneInfo.Utc, TimeZoneInfo.Local).ToString("MMM dd hh:mm tt");
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
            this.StartTimeLocal = TimeZoneInfo.ConvertTime(this.Start, TimeZoneInfo.Utc, TimeZoneInfo.Local).ToString("MMM dd hh:mm tt");
            this.EndTimeLocal = TimeZoneInfo.ConvertTime(this.End, TimeZoneInfo.Utc, TimeZoneInfo.Local).ToString("MMM dd hh:mm tt");
        }
        public bool AddAppointmentInDatabase()
        {
            string sqlCommand = MySqlCommands.AddAppointment(this);
            string returnCommand = MySqlCommands.GetAddedAppointmentID; 
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    this.Id = connection.ExecuteMySqlCommand_PUT(sqlCommand, returnCommand);
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
            string sqlCommand = MySqlCommands.UpdateAppointment(this);
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    connection.ExecuteMySqlCommand_VOID(sqlCommand);
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
            string sqlCommand = MySqlCommands.DeleteAppointment(this);
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    connection.ExecuteMySqlCommand_VOID(sqlCommand);
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
