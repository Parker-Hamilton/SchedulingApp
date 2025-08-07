using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ScheduleingApp
{
    public partial class Dashboard : Form
    {
        public BindingList<Customer> customers;
        public List<Appointment> appointments;
        public List<User> users;
        public List<Appointment> todaysAppointments { get; private set; }
        public Reporter reporter;

        public Dashboard()
        {
            InitializeComponent();
            try
            {
                DataTable customerData;
                DataTable appointmentData;
                DataTable userData;
                customers = new BindingList<Customer>();
                appointments = new List<Appointment>();
                users = new List<User>();
                reporter = new Reporter();
                using (var connection = new DatabaseHelper())
                {
                    customerData = connection.ExecuteStoredProc(ConfigurationManager.AppSettings["SP_GetAllCustomers"]);
                    appointmentData = connection.ExecuteStoredProc(ConfigurationManager.AppSettings["SP_GetAllAppointments"]);
                    userData = connection.ExecuteStoredProc(ConfigurationManager.AppSettings["SP_GetAllUsers"]);
                }
                foreach (DataRow row in userData.Rows)
                {
                    User user = new User();
                    user.SetDetailsFromDatabase(
                        Convert.ToInt32(row["userId"]),
                        row["userName"].ToString()
                    );
                    users.Add(user);
                }
                foreach (DataRow row in customerData.Rows)
                {
                    Customer customer = new Customer();
                    customer.SetDetailsFromDatabase(
                        Convert.ToInt32(row["customerId"]),
                        row["customerName"].ToString(),
                        row["address"].ToString(),
                        row["address2"].ToString(),
                        row["city"].ToString(),
                        row["postalCode"].ToString(),
                        row["country"].ToString(),
                        row["phone"].ToString()
                    );
                    customers.Add(customer);
                }

                customersDataGrid.DataSource = customers;
                customersDataGrid.Columns["CustomerId"].Visible = false;
                customersDataGrid.Columns["PostalCode"].Visible = false;
                customersDataGrid.Columns["Country"].Visible = false;
                customersDataGrid.Columns["City"].Visible = false;
                customersDataGrid.Columns["Address2"].Visible = false;

                foreach (DataRow row in appointmentData.Rows)
                {
                    Appointment appointment = new Appointment();
                    appointment.SetDetailsFromDatabase(
                        Convert.ToInt32(row["appointmentId"]),
                        Convert.ToInt32(row["customerId"]),
                        Convert.ToInt32(row["userId"]),
                        row["title"].ToString(),
                        row["description"].ToString(),
                        row["location"].ToString(),
                        row["contact"].ToString(),
                        row["type"].ToString(),
                        row["url"].ToString(),
                        Convert.ToDateTime(row["start"]),
                        Convert.ToDateTime(row["end"]),
                        customers.FirstOrDefault(c => c.CustomerId == Convert.ToInt32(Convert.ToInt32(row["appointmentId"]))),
                        users.FirstOrDefault(u => u.Username == Credentials.Instance.Username)
                    );
                    appointments.Add(appointment);
                }
                monthCalendar1.SelectionStart = DateTime.Today;
                this.RefreshAppointments(DateTime.Today);
                this.SetMeetingAlert();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
        public void UpdateDataSource()
        {
            this.customersDataGrid.ResetBindings();
            this.RefreshAppointments(monthCalendar1.SelectionRange.Start);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerDetail customerDetail = new CustomerDetail(this);
            customerDetail.SetIsAddMode();
            customerDetail.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.customersDataGrid.DataSource = customers;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (customersDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Customer to delete.");
                return;
            }
            Customer customer = customers[customersDataGrid.SelectedRows[0].Index];
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete {customer.Name}?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                this.customers.Remove(customer);
                customer.DeleteCustomerInDatabase();
            }
        }

        private void btnModifyCustomer_Click(object sender, EventArgs e)
        {
            if (customersDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Customer to modify.");
                return;
            }
            else
            {
                Customer customer = customers[customersDataGrid.SelectedRows[0].Index];
                CustomerDetail customerDetail = new CustomerDetail(this, customer);
                customerDetail.SetIsEditMode();
                customerDetail.SetModifyDetails();
                customerDetail.Show();
                this.Hide();
            }
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            AppointmentDetail appointmentDetail = new AppointmentDetail(this);
            appointmentDetail.SetIsAddMode();
            appointmentDetail.Show();
            this.Hide();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            RefreshAppointments(e.Start);
        }
        private void RefreshAppointments(DateTime date)
        {
            this.todaysAppointments = appointments
                .Where(a => a.Start.Date == date.Date)
                .ToList();

            appointmentDataGrid.DataSource = this.todaysAppointments;
            appointmentDataGrid.Columns["Id"].Visible = false;
            appointmentDataGrid.Columns["CustomerID"].Visible = false;
            appointmentDataGrid.Columns["UserID"].Visible = false;
            appointmentDataGrid.Columns["Description"].Visible = false;
            appointmentDataGrid.Columns["Location"].Visible = false;
            appointmentDataGrid.Columns["Contact"].Visible = false;
            appointmentDataGrid.Columns["Type"].Visible = false;
            appointmentDataGrid.Columns["URL"].Visible = false;
            appointmentDataGrid.Columns["Start"].Visible = false;
            appointmentDataGrid.Columns["End"].Visible = false;
            appointmentDataGrid.Columns["Customer"].Visible = false;
            appointmentDataGrid.Columns["User"].Visible = false;


        }
        private void SetMeetingAlert()
        {
            foreach (Appointment app in appointments)
            {
                if (app.User.Username == Credentials.Instance.Username)
                {
                    TimeSpan timeUntilMeeting = (app.Start - DateTime.Now);
                    if (timeUntilMeeting.TotalMinutes <= 15 && timeUntilMeeting.TotalMinutes >= 0)
                    {
                        upcomingAlertPanel.Visible = true;
                        upcomingMeetingAlert.Text = $"You have a meeting at {app.Start.TimeOfDay} {TimeZoneInfo.Local.DisplayName}";
                    }
                }
            }
        }

        private void modifyAppointment_Click(object sender, EventArgs e)
        {
            if (appointmentDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an Appointment to modify.");
                return;
            }
            else
            {
                Appointment appointment = todaysAppointments[appointmentDataGrid.SelectedRows[0].Index];
                AppointmentDetail appointmentDetail = new AppointmentDetail(this, appointment);
                appointmentDetail.SetIsEditMode();
                appointmentDetail.SetModifyDetails();
                appointmentDetail.Show();
                this.Hide();
            }
        }

        private void deleteAppointment_Click(object sender, EventArgs e)
        {
            if (appointmentDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an Appointment to delete.");
                return;
            }
            Appointment appointment = todaysAppointments[appointmentDataGrid.SelectedRows[0].Index];
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete Appointment: {appointment.Title}?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                this.appointments.Remove(appointment);
                this.todaysAppointments.Remove(appointment);
                appointment.DeleteAppointmentInDatabase();
                appointmentDataGrid.ClearSelection();
                this.RefreshAppointments(monthCalendar1.SelectionStart);
            }
        }

        private void generateReports_Click(object sender, EventArgs e)
        {
            string report = reporter.GenerateReportText(this.appointments);
            try
            {
                string folderPath = @"..\..\..\Reports";
                string fileName = "Appointment_Reports.txt";
                string fullPath = Path.Combine(folderPath, fileName);
                Directory.CreateDirectory(folderPath);
                using (FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter fileWriter = new StreamWriter(fs))
                    {
                        fileWriter.WriteLine(report);
                    }
                }
                MessageBox.Show($"Reports have been sent to {Path.GetFullPath(fullPath)}");
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}
