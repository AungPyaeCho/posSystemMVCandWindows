using posSystemWindows.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VenomHubLibrary;
using VenomHubLibrary.Queries;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace posSystemWindows
{
    public partial class frmLogin : Form
    {
        private readonly DapperService _dapperService;

        string? _password;

        public frmLogin()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isAdminValid = GetAdmin(txtUserName.Text, txtPassword.Text);
            if (isAdminValid)
            {
                // Admin login is successful
                this.DialogResult = DialogResult.OK; // Approve the action
                return; // Stop further execution
            }

            // If not an admin, try logging in as a regular user.
            bool isUserValid = GetUser(txtUserName.Text, txtPassword.Text);
            if (isUserValid)
            {
                // User login is successful
                frmLogin frmLogin = new frmLogin();
                frmLogin.Close();
                return; // Stop further execution
            }

            // If neither admin nor user login is successful, show an error message.
            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoginRecord(StaffModel user)
        {
            var LoginStaff = new LoginStaffDetailModel
            {
                staffCode = user.staffCode,
                staffName = user.staffName,
                staffRole = user.staffRole,
                loginAt = System.DateTime.Now
            };
            _dapperService.Execute(Queries.CreateLoginRecord, LoginStaff);
        }

        private bool GetAdmin(string username, string password)
        {
            try
            {
                string encryptedPassword = SimpleEncryptionHelper.Encrypt(password);

                var parameters = new
                {
                    adminName = username,
                    adminPassword = encryptedPassword
                };

                var admin = _dapperService.QueryFOD<AdminModel>(Queries.GetAdmin, parameters);
                if (admin != null)
                {
                    if (admin.adminName == username && admin.adminPassword == encryptedPassword) return true;
                    else 
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool GetUser(string UserName, string Password)
        {
            string encryptedPassword = SimpleEncryptionHelper.Encrypt(Password);

            var parameters = new
            {
                staffName = UserName,
                staffPassword = encryptedPassword
            };

            try
            {
                var user = _dapperService.QueryFOD<StaffModel>(Queries.GetUser, parameters);

                if (user != null)
                {
                    if (user.staffName == UserName && user.staffPassword == encryptedPassword)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginRecord(user);
                        frmMain mainForm = new frmMain(user);
                        mainForm.Show();
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblSetting_Click(object sender, EventArgs e)
        {
            frmSettingConnection frmSettingConnection = new frmSettingConnection();
            frmSettingConnection.Show();
        }
    }
}
