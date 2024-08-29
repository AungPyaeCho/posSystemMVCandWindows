using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VenomHubLibrary.Queries;
using VenomHubLibrary;
using posSystemWindows.Models;

namespace posSystemWindows
{

    public class CheckMiddle
    {
        private readonly DapperService _dapperService;

        public CheckMiddle(DapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public bool GetAdmin(string username, string password)
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

        //public bool GetUser(string UserName, string Password)
        //{
        //    string encryptedPassword = SimpleEncryptionHelper.Encrypt(Password);

        //    var parameters = new
        //    {
        //        staffName = UserName,
        //        staffPassword = encryptedPassword
        //    };

        //    try
        //    {
        //        var user = _dapperService.QueryFOD<StaffModel>(Queries.GetUser, parameters);

        //        if (user != null)
        //        {
        //            if (user.staffName == UserName && user.staffPassword == encryptedPassword)
        //            {
        //                //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                LoginRecord(user);
        //                frmMain mainForm = new frmMain(user);
        //                mainForm.ShowDialog();
        //                //this.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                //MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        //private void LoginRecord(StaffModel user)
        //{
        //    var LoginStaff = new LoginStaffDetailModel
        //    {
        //        staffCode = user.staffCode,
        //        staffName = user.staffName,
        //        staffRole = user.staffRole,
        //        loginAt = System.DateTime.Now.ToLocalTime()
        //    };
        //    _dapperService.Execute(Queries.CreateLoginRecord, LoginStaff);
        //}
    }

    
}
