using posSystemWindows.Models;
using System.Data;
using VenomHubLibrary;
using VenomHubLibrary.Queries;
using NLog;
using posSystem.Models;

namespace posSystemWindows
{
    public partial class frmCheckOut : Form
    {
        private readonly DapperService _dapperService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); 
        private readonly StaffModel _staffModel;

        // Constructor initializes the form and its controls
        public frmCheckOut(StaffModel staffModel)
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            _staffModel = staffModel;
            InitializeControls();
        }

        // Initialize form controls
        private void InitializeControls()
        {
            LoadComboBoxData();
            txtTotalAmount.ReadOnly = true;
            txtTotalQuantity.ReadOnly = true;

            lblUserName.Text = _staffModel.staffName;
            lblStaffCode.Text = _staffModel.staffCode;
            lblStaffRole.Text = _staffModel.staffRole;
        }

        // Load data into DataGridView and update summary
        public void LoadData(List<DataGridViewRow> rows)
        {
            try
            {
                PopulateDataGrid(rows);
                UpdateSummary();
            }
            catch (Exception ex)
            {
                LogError("Error loading data into DataGridView", ex);
                ShowErrorMessage("An error occurred while loading data.");
            }
        }

        // Populate DataGridView with rows
        private void PopulateDataGrid(List<DataGridViewRow> rows)
        {
            foreach (var row in rows)
            {
                int rowIndex = dgvCart.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dgvCart.Rows[rowIndex].Cells[i].Value = row.Cells[i].Value;
                }
            }
        }

        // Update summary fields
        private void UpdateSummary()
        {
            int sumAmount = 0;
            int sumTotalQty = 0;
            for (int i = 0; i < dgvCart.Rows.Count; ++i)
            {
                sumAmount += Convert.ToInt32(dgvCart.Rows[i].Cells[4].Value);
                sumTotalQty += Convert.ToInt32(dgvCart.Rows[i].Cells[2].Value);
            }

            txtTotalQuantity.Text = sumTotalQty.ToString();
            txtTotalAmount.Text = sumAmount.ToString();
        }

        // Load data into combo boxes
        public void LoadComboBoxData()
        {
            try
            {
                LoadMemberComboBox();
                LoadPaymentComboBox();
                LoadDiscountComboBox();
            }
            catch (Exception ex)
            {
                LogError("Error loading data into combo boxes", ex);
                ShowErrorMessage("An error occurred while loading data into combo boxes.");
            }
        }

        // Load member data into combo box
        private void LoadMemberComboBox()
        {
            var members = new Dictionary<int, string>
            {
                { 1, "Yes" },
                { 2, "No" }
            };

            cboMember.DataSource = new BindingSource(members, null);
            cboMember.DisplayMember = "Value";
            cboMember.ValueMember = "Key";
            cboMember.SelectedValue = 2;
        }

        // Load payment method data into combo box
        private void LoadPaymentComboBox()
        {
            var payments = new Dictionary<int, string>
            {
                { 1, "Cash" },
                { 2, "Credit" },
                { 3, "Banking" },
                { 4, "Pay" }
            };

            cboPayment.DataSource = new BindingSource(payments, null);
            cboPayment.DisplayMember = "Value";
            cboPayment.ValueMember = "Key";
            cboPayment.SelectedValue = 1;
        }

        // Load discount data into combo box
        private void LoadDiscountComboBox()
        {
            var dataTable = _dapperService.QueryDataTable(Queries.GetDiscounts);
            DataRow defaultRow = dataTable.NewRow();
            defaultRow["disValue"] = 0;
            defaultRow["disName"] = "No Discount";
            dataTable.Rows.InsertAt(defaultRow, 0);

            if (dataTable.Rows.Count > 0)
            {
                cboDicount.DisplayMember = "disName";
                cboDicount.ValueMember = "disValue";
                cboDicount.DataSource = dataTable;
            }
            else
            {
                ShowInformationMessage($"No data found for {cboDicount.Name}.");
            }
        }

        // Handle exit button click
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {

                frmSale frmSale = new frmSale(_staffModel);
                frmSale.ShowDialog();
                this.Close();

            }
            catch (Exception ex)
            {
                LogError("Error exiting form", ex);
                ShowErrorMessage("An error occurred while exiting.");
            }
        }

        // Handle member combo box selection change
        private void cboMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMemberCode.ReadOnly = cboMember.SelectedValue is 2;
            }
            catch (Exception ex)
            {
                LogError("Error in cboMember_SelectedIndexChanged", ex);
                ShowErrorMessage("An error occurred while changing member selection.");
            }
        }

        // Handle print button click
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessSale();
            }
            catch (Exception ex)
            {
                LogError("Error in btnPrint_Click method", ex);
                ShowErrorMessage("An error occurred while processing the sale.");
            }
        }

        // Process sale and update database
        private void ProcessSale()
        {
            string memberCode = cboMember.SelectedValue is 2 ? "No Member" : txtMemberCode.Text;
            int amount = string.IsNullOrEmpty(txtDiscountAmount.Text) ? Convert.ToInt32(txtTotalAmount.Text) : Convert.ToInt32(txtDiscountAmount.Text);
            //string discount = string.IsNullOrEmpty(txtDiscountAmount.Text) ? "" : cboDicount.Text;
            int receiveCash = cboPayment.SelectedValue is 1 ? Convert.ToInt32(txtReceiveCash.Text) : 0;
            int refundCash = cboPayment.SelectedValue is 1 ? Convert.ToInt32(txtRefundCash.Text) : 0;
            int memberPoint = amount > 50000 ? 5 : 0;
            string invoiceNo = "DL" + DateTime.Now.ToString("ddMMyyHHmm");

            // Create sale model
            var saleModel = new SaleModel
            {
                invoiceNo = invoiceNo,
                staffCode = _staffModel.staffCode!,
                staffName = _staffModel.staffName,
                memberCode = memberCode,
                saleQty = Convert.ToInt32(txtTotalQuantity.Text),
                totalAmount = amount,
                saleDate = DateTime.Now,
                receiveCash = receiveCash,
                refundCash = refundCash,
                discount = cboDicount.Text,
                paymentMethod = cboPayment.Text
            };

            // Create member model
            var memberModel = new MemberModel
            {
                memberCode = memberCode,
                memberPoints = memberPoint
            };

            // Process each item in the cart
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;

                var saleDetailModel = new SaleDetailModel
                {
                    invoiceNo = invoiceNo,
                    itemCode = row.Cells["itemCode"].Value?.ToString() ?? string.Empty,
                    itemPrice = string.IsNullOrEmpty(row.Cells["itemPrice"].Value?.ToString()) ? (int?)null : Convert.ToInt32(row.Cells["itemPrice"].Value),
                    saleQuantity = string.IsNullOrEmpty(row.Cells["quantity"].Value?.ToString()) ? (int?)null : Convert.ToInt32(row.Cells["quantity"].Value),
                    totalAmount = Convert.ToInt32(row.Cells["netAmount"].Value?.ToString()),
                    saleDate = DateTime.Now
                };

                var itemModel = new ItemModel
                {
                    itemCode = row.Cells["itemCode"].Value?.ToString() ?? string.Empty,
                    itemSold = Convert.ToInt32(row.Cells["quantity"].Value),
                    itemRemainStock = Convert.ToInt32(row.Cells["quantity"].Value)
                };

                _dapperService.Execute(Queries.UpdateItem, itemModel);
                _dapperService.Execute(Queries.CreateSaleDetail, saleDetailModel);
            }

            // Update member points if applicable
            if (!string.IsNullOrEmpty(txtMemberCode.Text))
            {
                _dapperService.Execute(Queries.UpdateMember, memberModel);
            }

            // Create sale record
            int result = _dapperService.Execute(Queries.CreateSale, saleModel);
            ShowInformationMessage(result > 0 ? "Sale Complete" : "Sale Failed");

            Clear();
        }

        // Handle text change event for receive cash input
        private void txtReceiveCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateRefund();
            }
            catch (FormatException)
            {
                ShowWarningMessage("Please enter valid numbers for the amounts.");
            }
            catch (OverflowException)
            {
                ShowWarningMessage("The amounts entered are too large.");
            }
            catch (InvalidOperationException ex)
            {
                ShowWarningMessage(ex.Message);
            }
            catch (Exception ex)
            {
                LogError("Error in txtReceiveCash_TextChanged method", ex);
                ShowErrorMessage("An unexpected error occurred.");
            }
        }

        // Calculate refund amount based on receive cash and total amount
        private void CalculateRefund()
        {
            if (!string.IsNullOrEmpty(txtReceiveCash.Text) && !string.IsNullOrEmpty(txtTotalAmount.Text))
            {
                int receive = Convert.ToInt32(txtReceiveCash.Text);
                int totalAmount = Convert.ToInt32(txtTotalAmount.Text);
                int refund = receive - totalAmount;
                txtRefundCash.Text = refund.ToString();
            }
            else
            {
                txtRefundCash.Text = "0";
            }

        }

        // Handle discount combo box selection change
        private void cboDicount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateDiscountAmount();
            }
            catch (Exception ex)
            {
                LogError("Error in cboDicount_SelectedIndexChanged method", ex);
                ShowErrorMessage("An error occurred while updating the discount.");
            }
        }

        // Update discount amount based on selected discount percentage
        private void UpdateDiscountAmount()
        {
            if (cboDicount.SelectedValue is 0)
            {

            }
            else
            {
                int totalAmount = Convert.ToInt32(txtTotalAmount.Text);
                int discountPercentage = 0;
                if (cboDicount.SelectedValue != DBNull.Value && cboDicount.SelectedValue != null)
                {
                    discountPercentage = Convert.ToInt32(cboDicount.SelectedValue);
                }

                // Calculate the discount amount
                int discountAmount = (totalAmount * discountPercentage) / 100;// Assuming cboDicount has the discount percentage value
                txtDiscountAmount.Text = (totalAmount - discountAmount).ToString();
            }
        }

        // Display an error message
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Display an information message
        private void ShowInformationMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Display a warning message
        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Log an error message with exception details
        private void LogError(string message, Exception ex)
        {
            Logger.Error(ex, message);
        }

        // Clear the form fields and reset to default state
        public void Clear()
        {
            try
            {
                // Clear all TextBox controls
                ClearTextBoxes(this.Controls);

                // Clear DataGridView
                dgvCart.Rows.Clear();
                // dgvCart.Refresh(); // Removed unless needed for specific redraw behavior
            }
            catch (Exception ex)
            {
                // Log error and show a user-friendly message
                Logger.Error(ex, "Error in Clear method");
                MessageBox.Show("An error occurred while clearing the form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearTextBoxes(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control.HasChildren)
                {
                    ClearTextBoxes(control.Controls); // Recursively clear TextBoxes in nested containers
                }
            }
        }
    }
}
