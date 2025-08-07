using ScheduleingApp;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace ScheduleingApp
{
    public partial class Login : Form
    {
        Dictionary<string, string> lang;
        public CultureInfo english;
        public CultureInfo espanol;
        private Credentials credentials;
        string currentRegion = RegionInfo.CurrentRegion.DisplayName;
        string currentLangCode = CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName;
        //Establish db connection before allowing user into the application
        public Login()
        {
            InitializeComponent();

            Credentials.Init();
            credentials = Credentials.Instance;

            lang = new Dictionary<string, string>()
            {
                { "English", "ENU" },
                { "Español", "ESN" }
            };

            english = new CultureInfo("en");
            espanol = new CultureInfo("es");

            languageComboBox.DataSource = new BindingSource(lang, null);
            languageComboBox.DisplayMember = "Key";
            languageComboBox.ValueMember = "Value";
            languageComboBox.SelectedValue = currentLangCode;
        }
        private bool LoginAttempt(string username, string password)
        {
            try
            {
                credentials.Set(username, password);
                using (var connection = new DatabaseHelper())
                {
                    this.LogLoginAttempt(this.credentials.Username, this.credentials.Password, "success");
                    return true;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(
                    Resources.loginErrorMessage,
                    Resources.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine($"Error: {ex.Message}");
                this.LogLoginAttempt(credentials.Username, credentials.Password, "failure", ex);
                credentials.Clear();
                return false;
            }
        }
        private void LogLoginAttempt(string username, string password, string status, Exception ex = null)
        {
            string folderPath = @"..\..\..\Logs";
            string fileName = "Login_History.txt";
            string fullPath = Path.Combine(folderPath, fileName);
            Directory.CreateDirectory(folderPath);
            using (FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write)) {
                using (StreamWriter fileWriter = new StreamWriter(fs))
                {
                    DateTime timestamp = DateTime.Now;
                    if (ex == null)
                    {
                        
                        fileWriter.WriteLine($"{timestamp} - username: {username} - credentials status: {status}");
                    }
                    else
                    {
                        fileWriter.WriteLine($"{timestamp} - username: {username} - credentials status: {status}");
                    }
                }
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = userNameInput.Text;
            string password = passwordMaskedInput.Text;
            bool loginStatus = this.LoginAttempt(userName, password);
            if (loginStatus)
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }
        }
        private void languageComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedLanguage = languageComboBox.SelectedValue.ToString();
            if (selectedLanguage == this.lang["English"])
            {
                Thread.CurrentThread.CurrentUICulture = this.english;


            }
            if (selectedLanguage == this.lang["Español"])
            {
                Thread.CurrentThread.CurrentUICulture = this.espanol;

            }
            usernameLabel.Text = Resources.usernameLabel;
            passwordLabel.Text = Resources.passwordLabel;
            btnLogin.Text = Resources.btnLoginLabel;
            userLocationLabel.Text = Resources.userLocationlabel;
        }
        private bool EnableLogin() 
        {
            if (userNameInput.Text == string.Empty || passwordMaskedInput.Text == string.Empty) {  return false; } else { return true; }
        }
        private void userNameInput_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = this.EnableLogin();
        }
        private void passwordMaskedInput_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = this.EnableLogin();
        }
    }
}