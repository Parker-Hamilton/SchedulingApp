using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                    new StoredProcedureParameters("@customerName", this.Name.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@address", this.Address.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@address2", address2, SqlDbType.VarChar),
                    new StoredProcedureParameters("@postalCode", this.PostalCode.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@phone", this.Phone.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@city", this.City.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@country", this.Country.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@createdBy", Credentials.Instance.Username.Trim(), SqlDbType.VarChar)
                };
            }
            else if (type == "modify")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@customerId", this.CustomerId, SqlDbType.Int),
                    new StoredProcedureParameters("@customerName", this.Name.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@address", this.Address.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@address2", address2, SqlDbType.VarChar),
                    new StoredProcedureParameters("@postalCode", this.PostalCode.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@phone", this.Phone.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@city", this.City.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@country", this.Country.Trim(), SqlDbType.VarChar),
                    new StoredProcedureParameters("@updatedBy", Credentials.Instance.Username.Trim(), SqlDbType.VarChar)
                };
            }
            else if (type == "delete")
            {
                Parameters = new List<StoredProcedureParameters>()
                {
                    new StoredProcedureParameters("@customerId", this.CustomerId, SqlDbType.Int),
                };
            }
        }
        public bool AddCustomerInDatabase()
        {
            this.InitParameters("add");
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    string storedProcedureName = ConfigurationManager.AppSettings["SP_AddCustomer"];

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    returnValue.SqlDbType = SqlDbType.Int;
                    this.CustomerId = connection.ExecuteStoredProc(storedProcedureName, this.Parameters, returnValue);
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
            this.InitParameters("delete");
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    string storedProcedureName = ConfigurationManager.AppSettings["SP_DeleteCustomer"];
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
        public bool ModifyCustomerInDatabase()
        {
            this.InitParameters("modify");
            try
            {
                using (var connection = new DatabaseHelper())
                {
                    string storedProcedureName = ConfigurationManager.AppSettings["SP_ModifyCustomer"];
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
