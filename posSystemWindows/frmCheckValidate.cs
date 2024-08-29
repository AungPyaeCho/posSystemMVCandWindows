using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VenomHubLibrary;

namespace posSystemWindows
{
    public partial class frmCheckValidate : Form
    {
        private readonly DapperService _dapperService;
        private readonly CheckMiddle _ch;
        public frmCheckValidate()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            _ch = new CheckMiddle(_dapperService);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //bool isv = _ch.GetAdmin(txtUserName.Text,txtPassword.Text);
                bool isAdminValid = _ch.GetAdmin(txtUserName.Text, txtPassword.Text);
                if (isAdminValid)
                {
                    // Admin login is successful
                    this.DialogResult = DialogResult.OK; // Approve the action
                    return; // Stop further execution
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
