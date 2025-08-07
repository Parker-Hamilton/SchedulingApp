namespace ScheduleingApp
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            userNameInput = new TextBox();
            passwordMaskedInput = new MaskedTextBox();
            usernameLabel = new Label();
            passwordLabel = new Label();
            btnLogin = new Button();
            languageComboBox = new ComboBox();
            userLocationDisplay = new Label();
            userLocationLabel = new Label();
            SuspendLayout();
            // 
            // userNameInput
            // 
            userNameInput.BorderStyle = BorderStyle.FixedSingle;
            userNameInput.Location = new Point(219, 75);
            userNameInput.Name = "userNameInput";
            userNameInput.Size = new Size(100, 23);
            userNameInput.TabIndex = 0;
            userNameInput.TextChanged += userNameInput_TextChanged;
            // 
            // passwordMaskedInput
            // 
            passwordMaskedInput.BorderStyle = BorderStyle.FixedSingle;
            passwordMaskedInput.Location = new Point(219, 126);
            passwordMaskedInput.Name = "passwordMaskedInput";
            passwordMaskedInput.Size = new Size(100, 23);
            passwordMaskedInput.TabIndex = 1;
            passwordMaskedInput.UseSystemPasswordChar = true;
            passwordMaskedInput.TextChanged += passwordMaskedInput_TextChanged;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(138, 77);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(65, 15);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "User Name";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(138, 128);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(57, 15);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Enabled = false;
            btnLogin.Location = new Point(146, 178);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 23);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // languageComboBox
            // 
            languageComboBox.FormattingEnabled = true;
            languageComboBox.Location = new Point(268, 178);
            languageComboBox.Name = "languageComboBox";
            languageComboBox.Size = new Size(121, 23);
            languageComboBox.TabIndex = 5;
            languageComboBox.SelectedValueChanged += languageComboBox_SelectedValueChanged;
            // 
            // userLocationDisplay
            // 
            userLocationDisplay.AutoSize = true;
            userLocationDisplay.Location = new Point(234, 225);
            userLocationDisplay.Name = "userLocationDisplay";
            userLocationDisplay.Size = new Size(0, 15);
            userLocationDisplay.TabIndex = 6;
            userLocationDisplay.Text = this.currentRegion.ToString();
            // 
            // userLocationLabel
            // 
            userLocationLabel.AutoSize = true;
            userLocationLabel.Location = new Point(101, 225);
            userLocationLabel.Name = "userLocationLabel";
            userLocationLabel.Size = new Size(82, 15);
            userLocationLabel.TabIndex = 7;
            userLocationLabel.Text = "User Location:";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(535, 395);
            Controls.Add(userLocationLabel);
            Controls.Add(userLocationDisplay);
            Controls.Add(languageComboBox);
            Controls.Add(btnLogin);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(passwordMaskedInput);
            Controls.Add(userNameInput);
            Name = "Login";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox userNameInput;
        private MaskedTextBox passwordMaskedInput;
        private Label usernameLabel;
        private Label passwordLabel;
        private Button btnLogin;
        private ComboBox languageComboBox;
        private Label userLocationDisplay;
        private Label userLocationLabel;
    }
}
