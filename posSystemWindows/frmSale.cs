using posSystemWindows.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using VenomHubLibrary;

namespace posSystemWindows
{
    public partial class frmSale : Form
    {
        private readonly DapperService _dapperService;
        public frmSale()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            //    dgvItems.AutoGenerateColumns = false;
            string query = @"
        SELECT 
            I.itemCode,
            I.itemName,
            I.itemStock,
            C.catName,
            B.brandName,
            SB.subBrandName,
            SC.subCatName
        FROM tblItem I
        INNER JOIN tblBrand B ON I.brandCode = B.brandCode
        INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
        INNER JOIN tblCategory C ON I.catCode = C.catCode
        INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode";

            try
            {
                // Fetch data using Dapper and bind it to the DataGridView
                var dataTable = _dapperService.QueryDataTable(query);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvItems.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, possibly log the error and show a message to the user
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log exception (optional)
                // LogError(ex);
            }
        }
    }
}
