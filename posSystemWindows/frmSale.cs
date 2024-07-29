using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.IdentityModel.Tokens;
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
        DataTable DataTable = new DataTable();

        public frmSale()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            dgvItems.AutoGenerateColumns = false;
            dgvCart.AutoGenerateColumns = false;
            txtBarcode.Focus();
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            //loadData();
            LoadCboData();
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
            chbWholeSale.Checked = false;
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
            string categoryCode = cboCategories.SelectedValue?.ToString();
            string subCatCode = cboSubCategories.SelectedValue?.ToString();
            string brandCode = cboBrands.SelectedValue?.ToString();
            string subBrandCode = cboSubBrands.SelectedValue?.ToString();

            var parameters = new
            {
                itemName = string.IsNullOrEmpty(itemName) ? (string?)null : itemName,
                itemBarcode = string.IsNullOrEmpty(itemBarcode) ? (string?)null : itemBarcode,
                catCode = string.IsNullOrEmpty(categoryCode) ? (string?)null : categoryCode,
                subCatCode = string.IsNullOrEmpty(subCatCode) ? (string?)null : subCatCode,
                brandCode = string.IsNullOrEmpty(brandCode) ? (string?)null : brandCode,
                subBrandCode = string.IsNullOrEmpty(subBrandCode) ? (string?)null : subBrandCode
            };

            DataTable dataTable = null;

            if (!string.IsNullOrEmpty(itemName) || !string.IsNullOrEmpty(itemBarcode))
            {
                // Fetch only one row for item name or barcode search
                var dataRow = _dapperService.QueryDataRow(Queries.GetItemBy, parameters);

                if (dataRow != null)
                {
                    // Convert DataRow to a DataTable with a single row to bind to DataGridView
                    dataTable = dataRow.Table.Clone(); // Create an empty DataTable with the same schema
                    dataTable.ImportRow(dataRow); // Import the DataRow
                }
            }
            else
            {
                // Fetch all rows for category, subcategory, brand, or subbrand search
                dataTable = _dapperService.QueryDataTable(Queries.GetItemBy, parameters);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                dgvItems.DataSource = dataTable;
            }
            else
            {
                // Handle case when no item is found
                MessageBox.Show("No item found with the specified criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void LoadCboData()
        {
            try
            {
                LoadComboBox(cboCategories, Queries.GetCategories, "catCode", "catName", "All Categories");
                LoadComboBox(cboSubCategories, Queries.GetSubCategories, "subCatCode", "subCatName", "All Sub-Categories");
                LoadComboBox(cboBrands, Queries.GetBrands, "brandCode", "brandName", "All Brands");
                LoadComboBox(cboSubBrands, Queries.GetSubBrands, "subBrandCode", "subBrandName", "All Sub-Brands");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading combo box data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBox(ComboBox comboBox, string query, string valueMember, string displayMember, string defaultDisplayText)
        {
            var dataTable = _dapperService.QueryDataTable(query);

            // Add a default "All" row to the DataTable
            DataRow defaultRow = dataTable.NewRow();
            defaultRow[valueMember] = DBNull.Value; // or an appropriate value like -1 or "All"
            defaultRow[displayMember] = defaultDisplayText;
            dataTable.Rows.InsertAt(defaultRow, 0);

            if (dataTable.Rows.Count > 0)
            {
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
                comboBox.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show($"No data found for {comboBox.Name}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvCart.SelectedRows.Count > 0)
            {
                // Remove the selected row
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Please select a row to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            string itemBarcode = txtBarcode.Text;
            LoadItemsByBarcode(itemBarcode);
        }

        private void LoadItemsByBarcode(string itemBarcode)
        {
            var parameters = new
            {
                itemBarcode = string.IsNullOrEmpty(itemBarcode) ? (string?)null : itemBarcode
            };

            var dataTable = _dapperService.QueryDataTable(Queries.GetItemsByBarcode, parameters);

            if (dataTable.Rows.Count > 0)
            {
                dgvItems.DataSource = dataTable;
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> cartData = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                // Skip the new row placeholder if it's there
                if (!row.IsNewRow)
                {
                    cartData.Add(row);
                }
            }

            frmCheckOut frmCheckOut = new frmCheckOut();
            frmCheckOut.LoadData(cartData);
            frmCheckOut.Show();
            this.Hide();
            
        }
    }
}

