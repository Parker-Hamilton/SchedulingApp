using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleingApp
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public List<StoredProcedureParameters> Parameters;

        public Customer() { }

        public void SetDetails(string firstName, string lastName, string address, string address2, string city, string postalCode, string country, string phone)
        {
            this.Name = $"{firstName.Trim()} {lastName.Trim()}";
            this.Address = address;
            this.Address2 = address2;
            this.City = city;
            this.PostalCode = postalCode;
            this.Country = country;
            this.Phone = phone;
        }
        public void SetDetailsFromDatabase(int id, string customerName, string address, string address2, string city, string postalCode, string country, string phone)
        {
            this.CustomerId = id;
            this.Name = customerName;
            this.Address = address;
            this.Address2 = address2;
            this.City = city;
            this.PostalCode = postalCode;
            this.Country = country;
            this.Phone = phone;
        }
        private void InitParameters(string type)
        {
            object address2 = string.Empty;
            if (this.Address2 == string.Empty)
            {
                address2 = DBNull.Value;
            }
            else
            {
                address2 = this.Address2.Trim();
            }
            if (type == "add")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@customerName", this.Name.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@address", this.Address.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@address2", address2, MySqlDbType.VarChar),
                    new StoredProcedureParameters("@postalCode", this.PostalCode.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@phone", this.Phone.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@city", this.City.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@country", this.Country.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@createdBy", Credentials.Instance.Username.Trim(), MySqlDbType.VarChar)
                };
            }
            else if (type == "modify")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@customerId", this.CustomerId, MySqlDbType.Int32),
                    new StoredProcedureParameters("@customerName", this.Name.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@address", this.Address.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@address2", address2, MySqlDbType.VarChar),
                    new StoredProcedureParameters("@postalCode", this.PostalCode.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@phone", this.Phone.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@city", this.City.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@country", this.Country.Trim(), MySqlDbType.VarChar),
                    new StoredProcedureParameters("@updatedBy", Credentials.Instance.Username.Trim(), MySqlDbType.VarChar)
                };
            }
            else if (type == "delete")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@customerId", this.CustomerId, MySqlDbType.Int32),
                };
            }
        }
        public bool AddCustomerInDatabase()
        {
            string sqlCommand = MySqlCommands.CustomerAction(this, "add");
            string returnCommand = MySqlCommands.GetAddedCustomerID;
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    this.CustomerId = connection.ExecuteMySqlCommand_PUT(sqlCommand, returnCommand);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }
        public bool DeleteCustomerInDatabase()
        {
            string sqlCommand = MySqlCommands.DeleteCustomer(this);
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
        public bool ModifyCustomerInDatabase()
        {
            string sqlCommand = MySqlCommands.CustomerAction(this, "modify");
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
