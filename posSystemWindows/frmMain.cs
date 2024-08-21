using posSystemWindows.Models;
using VenomHubLibrary;
using VenomHubLibrary.Queries;

namespace posSystemWindows
{
    public partial class frmMain : Form
    {
        private readonly DapperService _dapperService;
        private readonly StaffModel _staffModel;

        public frmMain(StaffModel staffModel)
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            _staffModel = staffModel;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lblUserName.Text = _staffModel.staffName;
            lblStaffCode.Text = _staffModel.staffCode;
            lblStaffRole.Text = _staffModel.staffRole;
        }

        private void LogoutRecord()
        {
            var LoginStaff = new LoginStaffDetailModel
            {
                staffCode = _staffModel.staffCode,
                logOutAt = System.DateTime.Now,
            };
            _dapperService.Execute(Queries.UpdateLoginRecord, LoginStaff);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LogoutRecord();
            this.Close();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmSale frmSale = new frmSale(_staffModel);
            frmSale.ShowDialog();
            this.Close();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSettingConnection frmSettingConnection = new frmSettingConnection();
            frmSettingConnection.ShowDialog();
        }
    }
}
