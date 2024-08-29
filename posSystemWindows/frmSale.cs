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
        private readonly StaffModel _staffModel;
        string _itemCode;

        public frmSale(StaffModel staffModel)
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            _staffModel = staffModel;

            dgvItems.AutoGenerateColumns = false;
            dgvCart.AutoGenerateColumns = false;
            txtBarcode.Focus();
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            LoadCboData();
            lblUserName.Text = _staffModel.staffName;
            lblStaffCode.Text = _staffModel.staffCode;
            lblStaffRole.Text = _staffModel.staffRole;
        }

        private void loadData()
        {
            try
            {
                // Fetch data using Dapper and bind it to the DataGridView
                var dataTable = _dapperService.QueryDataTable(Queries.GetItems);

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

        private void LoadCboData()
        {
            try
            {
                LoadComboBox(cboCategories, Queries.GetCategories, "catCode", "catName", "All Categories");
                LoadComboBox(cboSubCategories, Queries.GetSubCategories, "subCatCode", "subCatName", "All Sub-Categories");
                LoadComboBox(cboBrands, Queries.GetBrands, "brandCode", "brandName", "All Brands");
                LoadComboBox(cboSubBrands, Queries.GetSubBrands, "subBrandCode", "subBrandName", "All Sub-Brands");
                LoadComboBox(cboPromotion, Queries.GetPromotions, "proCode", "proName", "All Promotions");
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

        private void SearchItem()
        {
            try
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
                    categoryCode = string.IsNullOrEmpty(categoryCode) ? (string?)null : categoryCode,
                    subCatCode = string.IsNullOrEmpty(subCatCode) ? (string?)null : subCatCode,
                    brandCode = string.IsNullOrEmpty(brandCode) ? (string?)null : brandCode,
                    subBrandCode = string.IsNullOrEmpty(subBrandCode) ? (string?)null : subBrandCode
                };

                string query = GetQuery(parameters);

                DataTable dataTable = _dapperService.QueryDataTable(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dgvItems.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No item found with the specified criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private string GetQuery(dynamic parameters)
        {
            try
            {
                bool hasItemName = !string.IsNullOrEmpty(parameters.itemName);
                bool hasItemBarcode = !string.IsNullOrEmpty(parameters.itemBarcode);
                bool hasCategory = !string.IsNullOrEmpty(parameters.categoryCode);
                bool hasSubCategory = !string.IsNullOrEmpty(parameters.subCatCode);
                bool hasBrand = !string.IsNullOrEmpty(parameters.brandCode);
                bool hasSubBrand = !string.IsNullOrEmpty(parameters.subBrandCode);

                if (hasItemName || hasItemBarcode)
                {
                    if (hasCategory || hasSubCategory || hasBrand || hasSubBrand)
                    {
                        return Queries.GetItemByNameOrBarcodeWithFilter;
                    }
                    else
                    {
                        return Queries.GetItemByNameOrBarcode;
                    }
                }

                if (hasCategory || hasSubCategory || hasBrand || hasSubBrand)
                {
                    return Queries.GetItemByFilters;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return Queries.GetItemByAllSearch;
        }

        private void SearchUnkownValue()
        {
            try
            {

                string itemName = txtByName.Text;
                string itemBarcode = txtBarcode.Text;
                string catCode = cboCategories.SelectedValue?.ToString();
                string subCatCode = cboSubCategories.SelectedValue?.ToString();
                string brandCode = cboBrands.SelectedValue?.ToString();
                string subBrandCode = cboSubBrands.SelectedValue?.ToString();

                var parameters = new ItemModel
                {
                    itemName = string.IsNullOrEmpty(itemName) ? null : itemName,
                    itemBarcode = string.IsNullOrEmpty(itemBarcode) ? null : itemBarcode,
                    catCode = string.IsNullOrEmpty(catCode) ? null : catCode,
                    subCatCode = string.IsNullOrEmpty(subCatCode) ? null : subCatCode,
                    brandCode = string.IsNullOrEmpty(brandCode) ? null : brandCode,
                    subBrandCode = string.IsNullOrEmpty(subBrandCode) ? null : subBrandCode
                };

                string query = Queries.BuildDynamicLikeQuery(parameters);

                var dataTable = _dapperService.QueryDataTable(query, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dgvItems.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No item found with the specified criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void addToCart()
        {
            try
            {
                string itemName;
                int itemPrice;
                int itemWholeSalePrice;
                string catName;
                string subCatName;
                string brandName;
                string subBrandName;
                int quantity;
                int salePrice;
                int itemRemainStock;

                itemName = txtItemName.Text;
                itemPrice = Convert.ToInt32(txtItemPrice.Text);
                itemWholeSalePrice = Convert.ToInt32(txtWholeSalePrice.Text);
                catName = txtCategory.Text;
                subCatName = txtSubCategory.Text;
                brandName = txtBrand.Text;
                subBrandName = txtSubBrand.Text;
                quantity = Convert.ToInt32(txtQuantity.Text);
                itemRemainStock = Convert.ToInt32(txtRemainStock.Text);


                salePrice = chbWholeSale.Checked ? itemWholeSalePrice : itemPrice;

                int netAmount = salePrice * quantity;
                dgvCart.Rows.Add(_itemCode, itemName, quantity, salePrice, netAmount, itemRemainStock);
                Clear();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter valid numeric values for prices and quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getFromItem(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dgvItems.Rows[e.RowIndex];

                    int quantity = Convert.ToInt32(txtQuantity.Text);
                    int itemRemainStock = Convert.ToInt32(selectedRow.Cells["Column3"].Value);

                    _itemCode = selectedRow.Cells["itemCode"].Value.ToString()!;
                    txtItemName.Text = selectedRow.Cells["itemName"].Value.ToString()!;
                    txtItemPrice.Text = selectedRow.Cells["itemPrice"].Value.ToString()!;
                    txtWholeSalePrice.Text = selectedRow.Cells["itemWholeSalePrice"].Value.ToString()!;
                    txtCategory.Text = selectedRow.Cells["catName"].Value.ToString()!;
                    txtSubCategory.Text = selectedRow.Cells["subCatName"].Value.ToString()!;
                    txtBrand.Text = selectedRow.Cells["brandName"].Value.ToString()!;
                    txtSubBrand.Text = selectedRow.Cells["subBrandName"].Value.ToString()!;
                    txtRemainStock.Text = itemRemainStock.ToString();
                    lblStock.ForeColor = itemRemainStock <= 2 ? Color.Red : DefaultForeColor;
                    lblStock.Text = itemRemainStock <= 2 ? "Stock (Low)" : "Stock";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cellQtyEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ensure the edited cell is in the quantity column
                if (e.ColumnIndex == dgvCart.Columns["quantity"].Index)
                {
                    DataGridViewRow row = dgvCart.Rows[e.RowIndex];
                    int itemPrice = Convert.ToInt32(row.Cells["colItemPrice"].Value);
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

                if (e.ColumnIndex == dgvCart.Columns["colItemPrice"].Index)
                {

                    using (frmCheckValidate checkValidate = new frmCheckValidate())
                    {
                        if (checkValidate.ShowDialog() == DialogResult.OK)
                        {
                            DataGridViewRow selectedRow = dgvCart.Rows[e.RowIndex];
                            int itemPrice = Convert.ToInt32(selectedRow.Cells["colItemPrice"].Value);
                            int quantity;

                            // Try to parse the quantity value
                            if (int.TryParse(selectedRow.Cells["quantity"].Value.ToString(), out quantity))
                            {
                                // Calculate the total price and update the total price cell
                                selectedRow.Cells["netAmount"].Value = itemPrice * quantity;
                            }
                            else
                            {
                                return;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //SearchItem();
            SearchUnkownValue();
        }
        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            string itemBarcode = txtBarcode.Text;
            LoadItemsByBarcode(itemBarcode);
        }

        private void btnSearchRange_Click(object sender, EventArgs e)
        {

        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            getFromItem(sender, e);
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            addToCart();
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cellQtyEdit(sender, e);
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

            frmCheckOut frmCheckOut = new frmCheckOut(_staffModel);
            frmCheckOut.LoadData(cartData);
            frmCheckOut.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //frmCheckOut.Close();
            frmMain frmMain = new frmMain(_staffModel);
            frmMain.Show();
            this.Close();
        }

        public void Clear()
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
            txtRemainStock.Text = string.Empty;
            _itemCode = "";
        }
    }
}


