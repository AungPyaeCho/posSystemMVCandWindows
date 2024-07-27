using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
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
            dgvItems.AutoGenerateColumns = false;
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            try
            {
                // Fetch data using Dapper and bind it to the DataGridView
                var dataTable = _dapperService.QueryDataTable(VenomHubLibrary.Queries.Queries.GetItems);

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
            }
        }
    }
}
