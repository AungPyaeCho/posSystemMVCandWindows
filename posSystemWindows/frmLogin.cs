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

        string _password;

        public frmLogin()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GetUser(txtUserName.Text, txtPassword.Text);
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

        private void GetUser(string UserName, string Password)
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
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
