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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is not on the header row
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvItems.Rows[e.RowIndex];

                // Retrieve itemName and itemPrice from the selected row
                string itemName = selectedRow.Cells["itemName"].Value.ToString();
                decimal itemPrice = Convert.ToDecimal(selectedRow.Cells["itemPrice"].Value);

                // Check if item is already in dgvCheckout
                //foreach (DataGridViewRow row in dgvCart.Rows)
                //{
                //    if (row.Cells["Column3"].Value.ToString() == itemName)
                //    {
                //        MessageBox.Show("Item already added to checkout.");
                //        return;
                //    }
                //}

                // Add the item to dgvCheckout with a default quantity of 1
                dgvCart.Rows.Add(itemName,1,itemPrice);
            }
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the edited cell is in the quantity column
            if (e.ColumnIndex == dgvCart.Columns["quantity"].Index)
            {
                DataGridViewRow row = dgvCart.Rows[e.RowIndex];
                decimal itemPrice = Convert.ToDecimal(row.Cells["Column8"].Value);
                int quantity;

                // Try to parse the quantity value
                if (int.TryParse(row.Cells["quantity"].Value.ToString(), out quantity))
                {
                    // Calculate the total price and update the total price cell
                    row.Cells["netAmount"].Value = itemPrice * quantity;
                }
                else
                {
                    // If parsing fails, reset the quantity to 1 and update the total price
                    row.Cells["quantity"].Value = 1;
                    row.Cells["netAmount"].Value = itemPrice;
                }
            }
        }
    }
}
