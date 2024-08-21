using posSystemWindows.Models;
using System.Data.SqlClient;
using VenomHubLibrary;
using VenomHubLibrary.Queries;

namespace posSystemWindows
{
    public partial class frmSettingConnection : Form
    {
        private readonly DapperService _dapperService;

        // Default admin credentials stored in code
        private const string DefaultAdminName = "Default Admin";
        private const string DefaultAdminPassword = "Admin@123";

        public frmSettingConnection()
        {
            InitializeComponent();

            // Check if the connection string is valid
            if (IsConnectionStringValid(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString))
            {
                _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            }
            else
            {
                MessageBox.Show("Default connection string is invalid. Please update the connection settings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the entered credentials are for the default admin
                if (IsDefaultAdmin(txtAdminName.Text, txtAdminPassword.Text) || GetUser(txtAdminName.Text, txtAdminPassword.Text))
                {
                    // Get user input from text boxes
                    string serverName = txtServerName.Text;
                    string databaseName = ttxtDatabaseName.Text;
                    string userName = txtUserName.Text;
                    string password = txtServerPassword.Text;

                    // Update the connection string dynamically and save it
                    ConnectionStrings.UpdateConnectionString(serverName, databaseName, userName, password);

                    MessageBox.Show("Connection string updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsDefaultAdmin(string userName, string password)
        {
            // Check if the provided credentials match the default admin credentials
            return userName == DefaultAdminName && password == DefaultAdminPassword;
        }

        private bool GetUser(string userName, string password)
        {
            string encryptedPassword = SimpleEncryptionHelper.Encrypt(password);

            var parameters = new
            {
                adminName = userName,
                adminPassword = encryptedPassword
            };

            try
            {
                var user = _dapperService?.QueryFOD<AdminModel>(Queries.GetAdmin, parameters);

                if (user != null && user.adminName == userName && user.adminPassword == encryptedPassword)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool IsConnectionStringValid(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void Clear()
        {
            txtServerName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtAdminName.Text = string.Empty;
            txtAdminPassword.Text = string.Empty;
            txtServerPassword.Text = string.Empty;
            ttxtDatabaseName.Text = string.Empty;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Close();
        }
    }
}
