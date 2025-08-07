namespace ScheduleingApp
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            panel1 = new Panel();
            customersDataGrid = new DataGridView();
            btnAddCustomer = new Button();
            btnExit = new Button();
            btnDeleteCustomer = new Button();
            btnModifyCustomer = new Button();
            monthCalendar1 = new MonthCalendar();
            panel2 = new Panel();
            appointmentDataGrid = new DataGridView();
            upcomingMeetingAlert = new Label();
            btnAddAppointment = new Button();
            upcomingAlertPanel = new Panel();
            upcomingMeetingImg = new PictureBox();
            modifyAppointment = new Button();
            deleteAppointment = new Button();
            generateReports = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customersDataGrid).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)appointmentDataGrid).BeginInit();
            upcomingAlertPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)upcomingMeetingImg).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(customersDataGrid);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(360, 353);
            panel1.TabIndex = 0;
            // 
            // customersDataGrid
            // 
            customersDataGrid.AllowUserToAddRows = false;
            customersDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customersDataGrid.Location = new Point(0, 1);
            customersDataGrid.Name = "customersDataGrid";
            customersDataGrid.Size = new Size(360, 352);
            customersDataGrid.TabIndex = 0;
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.Location = new Point(171, 399);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(63, 38);
            btnAddCustomer.TabIndex = 1;
            btnAddCustomer.Text = "Add";
            btnAddCustomer.UseVisualStyleBackColor = true;
            btnAddCustomer.Click += btnAddCustomer_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(1175, 539);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnDeleteCustomer
            // 
            btnDeleteCustomer.Location = new Point(309, 399);
            btnDeleteCustomer.Name = "btnDeleteCustomer";
            btnDeleteCustomer.Size = new Size(63, 38);
            btnDeleteCustomer.TabIndex = 3;
            btnDeleteCustomer.Text = "Delete";
            btnDeleteCustomer.UseVisualStyleBackColor = true;
            btnDeleteCustomer.Click += btnDeleteCustomer_Click;
            // 
            // btnModifyCustomer
            // 
            btnModifyCustomer.Location = new Point(240, 399);
            btnModifyCustomer.Name = "btnModifyCustomer";
            btnModifyCustomer.Size = new Size(63, 38);
            btnModifyCustomer.TabIndex = 4;
            btnModifyCustomer.Text = "Modify";
            btnModifyCustomer.UseVisualStyleBackColor = true;
            btnModifyCustomer.Click += btnModifyCustomer_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(738, 13);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 6;
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlDark;
            panel2.Controls.Add(appointmentDataGrid);
            panel2.Location = new Point(538, 192);
            panel2.Name = "panel2";
            panel2.Size = new Size(629, 173);
            panel2.TabIndex = 7;
            // 
            // appointmentDataGrid
            // 
            appointmentDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appointmentDataGrid.Location = new Point(0, 0);
            appointmentDataGrid.Name = "appointmentDataGrid";
            appointmentDataGrid.Size = new Size(629, 173);
            appointmentDataGrid.TabIndex = 0;
            // 
            // upcomingMeetingAlert
            // 
            upcomingMeetingAlert.AutoSize = true;
            upcomingMeetingAlert.Location = new Point(48, 40);
            upcomingMeetingAlert.Name = "upcomingMeetingAlert";
            upcomingMeetingAlert.Size = new Size(116, 15);
            upcomingMeetingAlert.TabIndex = 0;
            upcomingMeetingAlert.Text = "AMeetingDrawsNear";
            // 
            // btnAddAppointment
            // 
            btnAddAppointment.Location = new Point(858, 397);
            btnAddAppointment.Name = "btnAddAppointment";
            btnAddAppointment.Size = new Size(99, 42);
            btnAddAppointment.TabIndex = 5;
            btnAddAppointment.Text = "Add Appointment";
            btnAddAppointment.UseVisualStyleBackColor = true;
            btnAddAppointment.Click += btnAddAppointment_Click;
            // 
            // upcomingAlertPanel
            // 
            upcomingAlertPanel.Controls.Add(upcomingMeetingImg);
            upcomingAlertPanel.Controls.Add(upcomingMeetingAlert);
            upcomingAlertPanel.Location = new Point(538, 371);
            upcomingAlertPanel.Name = "upcomingAlertPanel";
            upcomingAlertPanel.Size = new Size(238, 100);
            upcomingAlertPanel.TabIndex = 8;
            upcomingAlertPanel.Visible = false;
            // 
            // upcomingMeetingImg
            // 
            upcomingMeetingImg.Image = (Image)resources.GetObject("upcomingMeetingImg.Image");
            upcomingMeetingImg.Location = new Point(0, 40);
            upcomingMeetingImg.Name = "upcomingMeetingImg";
            upcomingMeetingImg.Size = new Size(42, 38);
            upcomingMeetingImg.TabIndex = 9;
            upcomingMeetingImg.TabStop = false;
            // 
            // modifyAppointment
            // 
            modifyAppointment.Location = new Point(963, 397);
            modifyAppointment.Name = "modifyAppointment";
            modifyAppointment.Size = new Size(99, 42);
            modifyAppointment.TabIndex = 9;
            modifyAppointment.Text = "Modify Appointment";
            modifyAppointment.UseVisualStyleBackColor = true;
            modifyAppointment.Click += modifyAppointment_Click;
            // 
            // deleteAppointment
            // 
            deleteAppointment.Location = new Point(1068, 397);
            deleteAppointment.Name = "deleteAppointment";
            deleteAppointment.Size = new Size(99, 42);
            deleteAppointment.TabIndex = 10;
            deleteAppointment.Text = "Delete Appointment";
            deleteAppointment.UseVisualStyleBackColor = true;
            deleteAppointment.Click += deleteAppointment_Click;
            // 
            // generateReports
            // 
            generateReports.Location = new Point(1068, 152);
            generateReports.Name = "generateReports";
            generateReports.Size = new Size(75, 23);
            generateReports.TabIndex = 11;
            generateReports.Text = "Reports";
            generateReports.UseVisualStyleBackColor = true;
            generateReports.Click += generateReports_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1262, 574);
            Controls.Add(generateReports);
            Controls.Add(deleteAppointment);
            Controls.Add(modifyAppointment);
            Controls.Add(upcomingAlertPanel);
            Controls.Add(panel2);
            Controls.Add(monthCalendar1);
            Controls.Add(btnAddAppointment);
            Controls.Add(btnModifyCustomer);
            Controls.Add(btnDeleteCustomer);
            Controls.Add(btnExit);
            Controls.Add(btnAddCustomer);
            Controls.Add(panel1);
            Name = "Dashboard";
            Text = "Form1";
            FormClosing += Dashboard_FormClosing;
            Load += Dashboard_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customersDataGrid).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)appointmentDataGrid).EndInit();
            upcomingAlertPanel.ResumeLayout(false);
            upcomingAlertPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)upcomingMeetingImg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView customersDataGrid;
        private Button btnAddCustomer;
        private Button btnExit;
        private Button btnDeleteCustomer;
        private Button btnModifyCustomer;
        private MonthCalendar monthCalendar1;
        private Panel panel2;
        private DataGridView appointmentDataGrid;
        private Label upcomingMeetingAlert;
        private Button btnAddAppointment;
        private Panel upcomingAlertPanel;
        private PictureBox upcomingMeetingImg;
        private Button modifyAppointment;
        private Button deleteAppointment;
        private Button generateReports;
    }
}