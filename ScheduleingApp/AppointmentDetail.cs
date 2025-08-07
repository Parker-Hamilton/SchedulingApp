using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleingApp
{
    public partial class AppointmentDetail : Form
    {
        public bool IsAddMode;
        public bool IsEditMode;
        public Appointment appointment;
        public Dashboard dashboard;
        public BindingList<Customer> customers;
        public User user;

        public AppointmentDetail()
        {
            InitializeComponent();
        }
        public AppointmentDetail(Dashboard dashboard)
        {
            InitializeComponent();
            this.dashboard = dashboard;
            this.customers = dashboard.customers;
            this.user = dashboard.users.FirstOrDefault(u => u.Username == Credentials.Instance.Username);
            customerComboBox.DataSource = customers;
            customerComboBox.DisplayMember = "Name";
            customerComboBox.ValueMember = "CustomerId";
            DefaultStartAndEndDateTime();
        }
        public AppointmentDetail(Dashboard dashboard, Appointment appointment)
        {
            InitializeComponent();
            this.dashboard = dashboard;
            this.customers = dashboard.customers;
            this.user = dashboard.users.FirstOrDefault(u => u.Username == Credentials.Instance.Username);
            this.appointment = appointment;
            customerComboBox.DataSource = customers;
            customerComboBox.DisplayMember = "Name";
            customerComboBox.ValueMember = "CustomerId";
            DefaultStartAndEndDateTime();
        }
        private void DefaultStartAndEndDateTime()
        {
            {
                TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
                DateTime estStart = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0).AddDays(1);

                DateTime defaultStart = TimeZoneInfo.ConvertTime(estStart, est, TimeZoneInfo.Local);
                DateTime defaultEnd = defaultStart.AddHours(1);

                startTimePicker.Value = defaultStart;
                endTimePicker.Value = defaultEnd;
            }
        }
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
        public void SetModifyDetails()
        {
            titleInput.Text = this.appointment.Title;
            descInput.Text = this.appointment.Description;
            customerComboBox.SelectedValue = this.appointment.CustomerID;
            startTimePicker.Value = this.appointment.Start;
            endTimePicker.Value = this.appointment.End;
            urlInput.Text = this.appointment.URL;
            typeInput.Text = this.appointment.Type;
            locationInput.Text = this.appointment.Location;
            contactInput.Text = this.appointment.Contact;
        }
        private bool EnableSave()
        {
            if ((titleInput.Text == string.Empty
                || customerComboBox.Text == string.Empty
                || descInput.Text == string.Empty
                || startTimePicker.Text == string.Empty
                || endTimePicker.Text == string.Empty
                || urlInput.Text == string.Empty
                || typeInput.Text == string.Empty
                || locationInput.Text == string.Empty
                || contactInput.Text == string.Empty)
                || (startTimePicker.Value > endTimePicker.Value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dashboard.Show();
            this.Close();

        }

        private void AppointmentDetail_Load(object sender, EventArgs e)
        {

        }

        private void titleInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();

        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void descInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void startTimePicker_ValueChanged(object sender, EventArgs e)
        {
            addAppointmentFalseAlert.Visible = false;
            Control control = sender as Control;
            string name = control.Name;
            ValidateTimeInput(name);
        }

        private void endTimePicker_ValueChanged(object sender, EventArgs e)
        {
            addAppointmentFalseAlert.Visible = false;
            Control control = sender as Control;
            string name = control.Name;
            ValidateTimeInput(name);
        }
        private void ValidateTimeInput(string controlName)
        {
            DateTimePicker timePicker;
            Label alertLabel;
            if (controlName == endTimePicker.Name)
            {
                startTimeAlert.Visible = false;
                timePicker = endTimePicker;
                alertLabel = endTimeAlert;
            }
            else if (controlName == startTimePicker.Name)
            {
                endTimeAlert.Visible = false;
                timePicker = startTimePicker;
                alertLabel = startTimeAlert;
            }
            else
            {
                return;
            }
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime selectedTime = timePicker.Value;
            DateTime startTime = startTimePicker.Value;
            DateTime endTime = endTimePicker.Value;
            DateTime estTime = TimeZoneInfo.ConvertTime(selectedTime, est);

            DateTime minTime = estTime.Date.AddHours(9); // 9:00 AM EST
            DateTime maxTime = estTime.Date.AddHours(17); // 5:00 PM EST

            if (estTime < minTime || estTime > maxTime)
            {
                alertLabel.Visible = true;
                alertLabel.Text = $"Please select a time between 9:00 AM EST and 5:00 PM EST\n" +
                    $"Time is displayed in your local time zone ({TimeZoneInfo.Local.StandardName})";
            }
            else if (startTime > endTime)
            {
                alertLabel.Visible = true;
                alertLabel.Text = $"Please select a start time prior to your end time";
            }
            else
            {
                alertLabel.Visible = false;
            }
            btnSave.Enabled = this.EnableSave();
        }

        private void urlInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void typeInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void contactInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void locationInput_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = this.EnableSave();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsAddMode)
            {
                this.AddAppointment();
            }
            if (IsEditMode)
            {
                this.ModifyAppointment();
            }
        }
        private bool TryAddAppointment()
        {
            foreach (Appointment app in dashboard.appointments)
            {
                if (startTimePicker.Value < app.End && endTimePicker.Value > app.Start)
                {
                    return false;
                }
            }
            return true;
        }
        private void AddAppointment()
        {
            if (TryAddAppointment())
            {
                addAppointmentFalseAlert.Visible = true;
                Appointment appointment = new Appointment();
                Customer selectedCustomer = customers.FirstOrDefault(c => c.CustomerId == Convert.ToInt32(customerComboBox.SelectedValue));
                appointment.SetDetails(Convert.ToInt32(customerComboBox.SelectedValue), 1, titleInput.Text, descInput.Text, locationInput.Text, contactInput.Text, typeInput.Text, urlInput.Text, startTimePicker.Value, endTimePicker.Value, selectedCustomer, this.user);
                bool complete = appointment.AddAppointmentInDatabase();
                if (complete)
                {
                    dashboard.appointments.Add(appointment);
                    dashboard.UpdateDataSource();
                    this.Close();
                    dashboard.Show();
                }
            }
            else
            {
                addAppointmentFalseAlert.Text = $"This time is not available please select another time";
                addAppointmentFalseAlert.Visible = true;
            }
        }
        private void ModifyAppointment()
        {
            Customer selectedCustomer = customers.FirstOrDefault(c => c.CustomerId == Convert.ToInt32(customerComboBox.SelectedValue));
            this.appointment.SetDetails(Convert.ToInt32(customerComboBox.SelectedValue), 1, titleInput.Text, descInput.Text, locationInput.Text, contactInput.Text, typeInput.Text, urlInput.Text, startTimePicker.Value, endTimePicker.Value, selectedCustomer, this.user);
            bool complete = appointment.ModifyAppointmentInDatabase();
            if (complete)
            {
                dashboard.UpdateDataSource();
                this.Close();
                dashboard.Show();
            }
        }
    }
}
