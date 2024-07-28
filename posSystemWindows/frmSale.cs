using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using posSystemWindows.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using VenomHubLibrary;
using VenomHubLibrary.Queries;

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
            dgvCart.AutoGenerateColumns = false;
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            //loadData();
            LoadCategories();
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

        public void clear()
        {
            txtItemName.Text = string.Empty;
            txtItemPrice.Text = string.Empty;
            txtWholeSalePrice.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtSubCategory.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtSubBrand.Text = string.Empty;
            txtQuantity.Text = "1";
            txtByName.Text = string.Empty;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void SearchItem()
        {
            string itemName = txtByName.Text;
            string itemBarcode = txtBarcode.Text;
            string categoryCode = cboCategories.SelectedValue.ToString();

            var parameters = new
            {
                itemName = string.IsNullOrEmpty(itemName) ? (string?)null : itemName,
                itemBarcode = string.IsNullOrEmpty(itemBarcode) ? (string?)null : itemBarcode,
                catCode = string.IsNullOrEmpty(categoryCode) ? (string?)null : categoryCode
            };

            if (!string.IsNullOrEmpty(itemName) || !string.IsNullOrEmpty(itemBarcode))
            {
                // Fetch only one row for item name or barcode search
                var dataRow = _dapperService.QueryDataRow(Queries.GetItemBy, parameters);

                if (dataRow != null)
                {
                    // Convert DataRow to a DataTable with a single row to bind to DataGridView
                    var singleRowTable = dataRow.Table.Clone(); // Create an empty DataTable with the same schema
                    singleRowTable.ImportRow(dataRow); // Import the DataRow
                    dgvItems.DataSource = singleRowTable;
                }
                else
                {
                    // Handle case when no item is found
                    MessageBox.Show("No item found with the specified criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (!string.IsNullOrEmpty(categoryCode))
            {
                // Fetch all rows for category search
                var dataTable = _dapperService.QueryDataTable(Queries.GetItemBy, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    dgvItems.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No item found with the specified criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void SearchUnkownValue()
        {
            string itemName = txtByName.Text;
            string itemBarcode = txtBarcode.Text;

            var parameters = new
            {
                itemName = string.IsNullOrEmpty(itemName) ? (string?)null : itemName,
                itemBarcode = string.IsNullOrEmpty(itemBarcode) ? (string?)null : itemBarcode
            };

            var dataTable = _dapperService.QueryDataTable(Queries.GetItemByNameOrBarcodeUnKown, parameters);

            if (dataTable.Rows.Count > 0)
            {
                dgvItems.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No item found with the specified criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearchRange_Click(object sender, EventArgs e)
        {
            SearchUnkownValue();
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvItems.Rows[e.RowIndex];

                // Retrieve itemName and itemPrice from the selected row
                string itemName = selectedRow.Cells["itemName"].Value.ToString()!;
                decimal itemPrice = Convert.ToDecimal(selectedRow.Cells["itemPrice"].Value);
                decimal itemWholeSalePrice = Convert.ToDecimal(selectedRow.Cells["itemWholeSalePrice"].Value);
                string catName = selectedRow.Cells["catName"].Value.ToString()!;
                string subCatName = selectedRow.Cells["subCatName"].Value.ToString()!;
                string brandName = selectedRow.Cells["brandName"].Value.ToString()!;
                string subBrandName = selectedRow.Cells["subBrandName"].Value.ToString()!;
                int quantity = Convert.ToInt32(txtQuantity.Text);

                txtItemName.Text = itemName;
                txtItemPrice.Text = itemPrice.ToString();
                txtWholeSalePrice.Text = itemWholeSalePrice.ToString();
                txtCategory.Text = catName;
                txtSubCategory.Text = subCatName;
                txtBrand.Text = brandName;
                txtSubBrand.Text = subBrandName;
            }
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            string itemName;
            decimal itemPrice;
            decimal itemWholeSalePrice;
            string catName;
            string subCatName;
            string brandName;
            string subBrandName;
            int quantity;
            decimal salePrice;

            itemName = txtItemName.Text;
            itemPrice = Convert.ToDecimal(txtItemPrice.Text);
            itemWholeSalePrice = Convert.ToDecimal(txtWholeSalePrice.Text);
            catName = txtCategory.Text;
            subCatName = txtSubCategory.Text;
            brandName = txtBrand.Text;
            subBrandName = txtSubBrand.Text;
            quantity = Convert.ToInt32(txtQuantity.Text);

            salePrice = chbWholeSale.Checked ? itemWholeSalePrice : itemPrice;

            decimal netAmount = salePrice * quantity;
            dgvCart.Rows.Add(itemName, quantity, salePrice, netAmount);
            clear();
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the edited cell is in the quantity column
            if (e.ColumnIndex == dgvCart.Columns["quantity"].Index)
            {
                DataGridViewRow row = dgvCart.Rows[e.RowIndex];
                decimal itemPrice = Convert.ToDecimal(row.Cells["colItemPrice"].Value);
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

        private void LoadCategories()
        {
            try
            {
                var dataTable = _dapperService.QueryDataTable(Queries.GetCategories);

                if (dataTable.Rows.Count > 0)
                {
                    cboCategories.DisplayMember = "catName";
                    cboCategories.ValueMember = "catCode";
                    cboCategories.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No categories found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
