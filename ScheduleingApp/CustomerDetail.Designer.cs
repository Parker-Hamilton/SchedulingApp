namespace ScheduleingApp
{
    partial class CustomerDetail
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
            firstNameInput = new TextBox();
            lastNameInput = new TextBox();
            addressInput = new TextBox();
            address2Input = new TextBox();
            cityInput = new TextBox();
            postalCodeInput = new TextBox();
            countryInput = new TextBox();
            phoneInput = new TextBox();
            firstNameLabel = new Label();
            lastNameLabel = new Label();
            addressLabel = new Label();
            address2Label = new Label();
            cityLabel = new Label();
            postalCodeLabel = new Label();
            countryLabel = new Label();
            phoneLabel = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // firstNameInput
            // 
            firstNameInput.BorderStyle = BorderStyle.FixedSingle;
            firstNameInput.Location = new Point(271, 53);
            firstNameInput.Name = "firstNameInput";
            firstNameInput.Size = new Size(254, 23);
            firstNameInput.TabIndex = 0;
            firstNameInput.TextChanged += firstNameInput_TextChanged;
            // 
            // lastNameInput
            // 
            lastNameInput.BorderStyle = BorderStyle.FixedSingle;
            lastNameInput.Location = new Point(271, 92);
            lastNameInput.Name = "lastNameInput";
            lastNameInput.Size = new Size(254, 23);
            lastNameInput.TabIndex = 1;
            lastNameInput.TextChanged += lastNameInput_TextChanged;
            // 
            // addressInput
            // 
            addressInput.BorderStyle = BorderStyle.FixedSingle;
            addressInput.Location = new Point(271, 132);
            addressInput.Name = "addressInput";
            addressInput.Size = new Size(254, 23);
            addressInput.TabIndex = 2;
            addressInput.TextChanged += addressInput_TextChanged;
            // 
            // address2Input
            // 
            address2Input.BorderStyle = BorderStyle.FixedSingle;
            address2Input.Location = new Point(271, 170);
            address2Input.Name = "address2Input";
            address2Input.Size = new Size(254, 23);
            address2Input.TabIndex = 5;
            // 
            // cityInput
            // 
            cityInput.BorderStyle = BorderStyle.FixedSingle;
            cityInput.Location = new Point(271, 209);
            cityInput.Name = "cityInput";
            cityInput.Size = new Size(254, 23);
            cityInput.TabIndex = 4;
            cityInput.TextChanged += cityInput_TextChanged;
            // 
            // postalCodeInput
            // 
            postalCodeInput.BorderStyle = BorderStyle.FixedSingle;
            postalCodeInput.Location = new Point(271, 251);
            postalCodeInput.Name = "postalCodeInput";
            postalCodeInput.Size = new Size(254, 23);
            postalCodeInput.TabIndex = 3;
            postalCodeInput.TextChanged += postalCodeInput_TextChanged;
            // 
            // countryInput
            // 
            countryInput.BorderStyle = BorderStyle.FixedSingle;
            countryInput.Location = new Point(271, 293);
            countryInput.Name = "countryInput";
            countryInput.Size = new Size(254, 23);
            countryInput.TabIndex = 8;
            countryInput.TextChanged += countryInput_TextChanged;
            // 
            // phoneInput
            // 
            phoneInput.BorderStyle = BorderStyle.FixedSingle;
            phoneInput.Location = new Point(271, 334);
            phoneInput.Name = "phoneInput";
            phoneInput.Size = new Size(254, 23);
            phoneInput.TabIndex = 7;
            phoneInput.TextChanged += phoneInput_TextChanged;
            phoneInput.KeyPress += phoneInput_KeyPress;
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Location = new Point(179, 55);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new Size(67, 15);
            firstNameLabel.TabIndex = 9;
            firstNameLabel.Text = "First Name:";
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Location = new Point(180, 94);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new Size(66, 15);
            lastNameLabel.TabIndex = 10;
            lastNameLabel.Text = "Last Name:";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(194, 134);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(52, 15);
            addressLabel.TabIndex = 12;
            addressLabel.Text = "Address:";
            // 
            // address2Label
            // 
            address2Label.AutoSize = true;
            address2Label.Location = new Point(185, 172);
            address2Label.Name = "address2Label";
            address2Label.Size = new Size(61, 15);
            address2Label.TabIndex = 11;
            address2Label.Text = "Address 2:";
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Location = new Point(215, 211);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new Size(31, 15);
            cityLabel.TabIndex = 16;
            cityLabel.Text = "City:";
            // 
            // postalCodeLabel
            // 
            postalCodeLabel.AutoSize = true;
            postalCodeLabel.Location = new Point(173, 253);
            postalCodeLabel.Name = "postalCodeLabel";
            postalCodeLabel.Size = new Size(73, 15);
            postalCodeLabel.TabIndex = 14;
            postalCodeLabel.Text = "Postal Code:";
            // 
            // countryLabel
            // 
            countryLabel.AutoSize = true;
            countryLabel.Location = new Point(193, 295);
            countryLabel.Name = "countryLabel";
            countryLabel.Size = new Size(53, 15);
            countryLabel.TabIndex = 13;
            countryLabel.Text = "Country:";
            // 
            // phoneLabel
            // 
            phoneLabel.AutoSize = true;
            phoneLabel.Location = new Point(155, 336);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(91, 15);
            phoneLabel.TabIndex = 17;
            phoneLabel.Text = "Phone Number:";
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(347, 424);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 41);
            btnSave.TabIndex = 18;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(450, 424);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 41);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // CustomerDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(721, 610);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(phoneLabel);
            Controls.Add(cityLabel);
            Controls.Add(postalCodeLabel);
            Controls.Add(countryLabel);
            Controls.Add(addressLabel);
            Controls.Add(address2Label);
            Controls.Add(lastNameLabel);
            Controls.Add(firstNameLabel);
            Controls.Add(countryInput);
            Controls.Add(phoneInput);
            Controls.Add(address2Input);
            Controls.Add(cityInput);
            Controls.Add(postalCodeInput);
            Controls.Add(addressInput);
            Controls.Add(lastNameInput);
            Controls.Add(firstNameInput);
            Name = "CustomerDetail";
            Text = "Form1";
            FormClosing += FormClose;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox firstNameInput;
        private TextBox lastNameInput;
        private TextBox addressInput;
        private TextBox address2Input;
        private TextBox cityInput;
        private TextBox postalCodeInput;
        private TextBox countryInput;
        private TextBox phoneInput;
        private Label firstNameLabel;
        private Label lastNameLabel;
        private Label addressLabel;
        private Label address2Label;
        private Label cityLabel;
        private Label postalCodeLabel;
        private Label countryLabel;
        private Label phoneLabel;
        private Button btnSave;
        private Button btnCancel;
    }
}