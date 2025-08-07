using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleingApp
{
    public partial class CustomerDetail : Form
    {

        private bool IsAddMode;
        private bool IsEditMode;
        private readonly Credentials credentials = Credentials.Instance;
        public Dashboard dashboard;
        public Customer customer;

        public void SetIsAddMode()
        {
            this.IsAddMode = true;
            this.IsEditMode = false;
        }
        public void SetIsEditMode()
        {
            this.IsEditMode = true;
            this.IsAddMode = false;
        }
        private bool EnableSave()
        {
            if (firstNameInput.Text == string.Empty
                || lastNameInput.Text == string.Empty
                || addressInput.Text == string.Empty
                || cityInput.Text == string.Empty
                || postalCodeInput.Text == string.Empty
                || phoneInput.Text == string.Empty
                || countryInput.Text == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void SetModifyDetails()
        {
            string[] name = customer.Name.Split(' ');
            firstNameInput.Text = name[0];
            lastNameInput.Text = name[1];
            addressInput.Text = customer.Address.ToString();
            address2Input.Text = customer.Address2.ToString();
            cityInput.Text = customer.City.ToString();
            postalCodeInput.Text = customer.PostalCode.ToString();
            countryInput.Text = customer.Country.ToString();
            phoneInput.Text = customer.Phone.ToString();
        }

        public CustomerDetail(Dashboard dashboard)
        {
            InitializeComponent();
            this.dashboard = dashboard;

        }
        public CustomerDetail(Dashboard dashboard, Customer customer)
        {
            InitializeComponent();
            this.dashboard = dashboard;
            this.customer = customer;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsAddMode)
            {
                this.AddCustomer();
            }
            if (IsEditMode)
            {
                this.ModifyCustomer();
            }
        }
        private void AddCustomer()
        {
            Customer customer = new Customer();
            customer.SetDetails(firstNameInput.Text, lastNameInput.Text, addressInput.Text, address2Input.Text, cityInput.Text, postalCodeInput.Text, countryInput.Text, phoneInput.Text);
            bool complete = customer.AddCustomerInDatabase();
            if (complete)
            {
                dashboard.customers.Add(customer);
                dashboard.UpdateDataSource();
                this.Close();
                dashboard.Show();
            }
        }
        private void ModifyCustomer()
        {
            this.customer.SetDetails(firstNameInput.Text, lastNameInput.Text, addressInput.Text, address2Input.Text, cityInput.Text, postalCodeInput.Text, countryInput.Text, phoneInput.Text);
            bool complete = customer.ModifyCustomerInDatabase();
            if (complete)
            {
                dashboard.UpdateDataSource();
                this.Close();
                dashboard.Show();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dashboard.UpdateDataSource();
            this.Close();
            dashboard.Show();
        }
        private void FormClose(object sender, FormClosingEventArgs e)
        {
            dashboard.Show();
        }
        private void firstNameInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }
        private void lastNameInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }
        private void addressInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }
        private void cityInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }
        private void postalCodeInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }
        private void countryInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }
        private void phoneInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void phoneInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
