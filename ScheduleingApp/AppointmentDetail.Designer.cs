namespace ScheduleingApp
{
    partial class AppointmentDetail
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
            titleInput = new TextBox();
            locationInput = new TextBox();
            contactInput = new TextBox();
            typeInput = new TextBox();
            descInput = new RichTextBox();
            urlInput = new TextBox();
            titleLabel = new Label();
            descriptionLabel = new Label();
            locationLabel = new Label();
            contactLabel = new Label();
            typeLabel = new Label();
            urlLabel = new Label();
            customerComboBox = new ComboBox();
            customerLabel = new Label();
            btnCancel = new Button();
            startTimePicker = new DateTimePicker();
            endTimePicker = new DateTimePicker();
            startLabel = new Label();
            endLabel = new Label();
            btnSave = new Button();
            startTimeAlert = new Label();
            endTimeAlert = new Label();
            addAppointmentFalseAlert = new Label();
            SuspendLayout();
            // 
            // titleInput
            // 
            titleInput.BorderStyle = BorderStyle.FixedSingle;
            titleInput.Location = new Point(283, 36);
            titleInput.Name = "titleInput";
            titleInput.Size = new Size(246, 23);
            titleInput.TabIndex = 0;
            titleInput.TextChanged += titleInput_TextChanged;
            // 
            // locationInput
            // 
            locationInput.BorderStyle = BorderStyle.FixedSingle;
            locationInput.Location = new Point(650, 36);
            locationInput.Name = "locationInput";
            locationInput.Size = new Size(246, 23);
            locationInput.TabIndex = 2;
            locationInput.TextChanged += locationInput_TextChanged;
            // 
            // contactInput
            // 
            contactInput.BorderStyle = BorderStyle.FixedSingle;
            contactInput.Location = new Point(650, 77);
            contactInput.Name = "contactInput";
            contactInput.Size = new Size(246, 23);
            contactInput.TabIndex = 3;
            contactInput.TextChanged += contactInput_TextChanged;
            // 
            // typeInput
            // 
            typeInput.BorderStyle = BorderStyle.FixedSingle;
            typeInput.Location = new Point(650, 118);
            typeInput.Name = "typeInput";
            typeInput.Size = new Size(246, 23);
            typeInput.TabIndex = 4;
            typeInput.TextChanged += typeInput_TextChanged;
            // 
            // descInput
            // 
            descInput.BorderStyle = BorderStyle.FixedSingle;
            descInput.Cursor = Cursors.IBeam;
            descInput.Location = new Point(283, 122);
            descInput.Name = "descInput";
            descInput.Size = new Size(246, 68);
            descInput.TabIndex = 5;
            descInput.Text = "";
            descInput.TextChanged += descInput_TextChanged;
            // 
            // urlInput
            // 
            urlInput.BorderStyle = BorderStyle.FixedSingle;
            urlInput.Location = new Point(650, 162);
            urlInput.Name = "urlInput";
            urlInput.Size = new Size(246, 23);
            urlInput.TabIndex = 6;
            urlInput.TextChanged += urlInput_TextChanged;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(189, 38);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(30, 15);
            titleLabel.TabIndex = 7;
            titleLabel.Text = "Title";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(189, 124);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(67, 15);
            descriptionLabel.TabIndex = 8;
            descriptionLabel.Text = "Description";
            // 
            // locationLabel
            // 
            locationLabel.AutoSize = true;
            locationLabel.Location = new Point(556, 38);
            locationLabel.Name = "locationLabel";
            locationLabel.Size = new Size(53, 15);
            locationLabel.TabIndex = 9;
            locationLabel.Text = "Location";
            // 
            // contactLabel
            // 
            contactLabel.AutoSize = true;
            contactLabel.Location = new Point(556, 79);
            contactLabel.Name = "contactLabel";
            contactLabel.Size = new Size(49, 15);
            contactLabel.TabIndex = 10;
            contactLabel.Text = "Contact";
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(556, 120);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(32, 15);
            typeLabel.TabIndex = 11;
            typeLabel.Text = "Type";
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(556, 164);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(28, 15);
            urlLabel.TabIndex = 12;
            urlLabel.Text = "URL";
            // 
            // customerComboBox
            // 
            customerComboBox.FormattingEnabled = true;
            customerComboBox.Location = new Point(283, 79);
            customerComboBox.Name = "customerComboBox";
            customerComboBox.Size = new Size(246, 23);
            customerComboBox.TabIndex = 13;
            customerComboBox.SelectedIndexChanged += customerComboBox_SelectedIndexChanged;
            // 
            // customerLabel
            // 
            customerLabel.AutoSize = true;
            customerLabel.Location = new Point(189, 82);
            customerLabel.Name = "customerLabel";
            customerLabel.Size = new Size(59, 15);
            customerLabel.TabIndex = 14;
            customerLabel.Text = "Customer";
            // 
            // btnCancel
            // 
            btnCancel.BackgroundImageLayout = ImageLayout.Center;
            btnCancel.Location = new Point(821, 290);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 36);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // startTimePicker
            // 
            startTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            startTimePicker.Format = DateTimePickerFormat.Custom;
            startTimePicker.Location = new Point(283, 217);
            startTimePicker.Name = "startTimePicker";
            startTimePicker.ShowUpDown = true;
            startTimePicker.Size = new Size(200, 23);
            startTimePicker.TabIndex = 16;
            startTimePicker.ValueChanged += startTimePicker_ValueChanged;
            // 
            // endTimePicker
            // 
            endTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            endTimePicker.Format = DateTimePickerFormat.Custom;
            endTimePicker.Location = new Point(650, 217);
            endTimePicker.Name = "endTimePicker";
            endTimePicker.ShowUpDown = true;
            endTimePicker.Size = new Size(200, 23);
            endTimePicker.TabIndex = 17;
            endTimePicker.ValueChanged += endTimePicker_ValueChanged;
            // 
            // startLabel
            // 
            startLabel.AutoSize = true;
            startLabel.Location = new Point(189, 223);
            startLabel.Name = "startLabel";
            startLabel.Size = new Size(31, 15);
            startLabel.TabIndex = 18;
            startLabel.Text = "Start";
            // 
            // endLabel
            // 
            endLabel.AutoSize = true;
            endLabel.Location = new Point(556, 223);
            endLabel.Name = "endLabel";
            endLabel.Size = new Size(27, 15);
            endLabel.TabIndex = 19;
            endLabel.Text = "End";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(690, 290);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 36);
            btnSave.TabIndex = 20;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // startTimeAlert
            // 
            startTimeAlert.AutoSize = true;
            startTimeAlert.ForeColor = Color.Red;
            startTimeAlert.Location = new Point(244, 254);
            startTimeAlert.Name = "startTimeAlert";
            startTimeAlert.Size = new Size(82, 15);
            startTimeAlert.TabIndex = 21;
            startTimeAlert.Text = "startTimeAlert";
            startTimeAlert.Visible = false;
            // 
            // endTimeAlert
            // 
            endTimeAlert.AutoSize = true;
            endTimeAlert.ForeColor = Color.Red;
            endTimeAlert.Location = new Point(611, 254);
            endTimeAlert.Name = "endTimeAlert";
            endTimeAlert.Size = new Size(79, 15);
            endTimeAlert.TabIndex = 22;
            endTimeAlert.Text = "endTimeAlert";
            endTimeAlert.Visible = false;
            // 
            // addAppointmentFalseAlert
            // 
            addAppointmentFalseAlert.AutoSize = true;
            addAppointmentFalseAlert.ForeColor = Color.Red;
            addAppointmentFalseAlert.Location = new Point(611, 355);
            addAppointmentFalseAlert.Name = "addAppointmentFalseAlert";
            addAppointmentFalseAlert.Size = new Size(149, 15);
            addAppointmentFalseAlert.TabIndex = 23;
            addAppointmentFalseAlert.Text = "addAppointmentFalseAlert";
            addAppointmentFalseAlert.Visible = false;
            // 
            // AppointmentDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1116, 603);
            Controls.Add(addAppointmentFalseAlert);
            Controls.Add(endTimeAlert);
            Controls.Add(startTimeAlert);
            Controls.Add(btnSave);
            Controls.Add(endLabel);
            Controls.Add(startLabel);
            Controls.Add(endTimePicker);
            Controls.Add(startTimePicker);
            Controls.Add(btnCancel);
            Controls.Add(customerLabel);
            Controls.Add(customerComboBox);
            Controls.Add(urlLabel);
            Controls.Add(typeLabel);
            Controls.Add(contactLabel);
            Controls.Add(locationLabel);
            Controls.Add(descriptionLabel);
            Controls.Add(titleLabel);
            Controls.Add(urlInput);
            Controls.Add(descInput);
            Controls.Add(typeInput);
            Controls.Add(contactInput);
            Controls.Add(locationInput);
            Controls.Add(titleInput);
            Name = "AppointmentDetail";
            Text = "Form1";
            Load += AppointmentDetail_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox titleInput;
        private TextBox locationInput;
        private TextBox contactInput;
        private TextBox typeInput;
        private RichTextBox descInput;
        private TextBox urlInput;
        private Label titleLabel;
        private Label descriptionLabel;
        private Label locationLabel;
        private Label contactLabel;
        private Label typeLabel;
        private Label urlLabel;
        private ComboBox customerComboBox;
        private Label customerLabel;
        private Button btnCancel;
        private DateTimePicker startTimePicker;
        private DateTimePicker endTimePicker;
        private Label startLabel;
        private Label endLabel;
        private Button btnSave;
        private Label startTimeAlert;
        private Label endTimeAlert;
        private Label addAppointmentFalseAlert;
    }
}