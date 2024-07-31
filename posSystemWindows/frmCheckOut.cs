using posSystemWindows.Models;
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
using VenomHubLibrary.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace posSystemWindows
{
    public partial class frmCheckOut : Form
    {
        private readonly DapperService _dapperService;
        string _staffCode;
        string _staffName;
        public frmCheckOut()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings._sqlConnectionStringBuilder.ConnectionString);
            LoadDataCbo();
            txtTotalAmount.ReadOnly = true;
            txtTotalQuantity.ReadOnly = true;
        }

        public void LoadData(List<DataGridViewRow> rows)
        {
            foreach (var row in rows)
            {
                int rowIndex = dgvCart.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dgvCart.Rows[rowIndex].Cells[i].Value = row.Cells[i].Value;
                }
            }

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

        public void LoadDataCbo()
        {
            var items = new Dictionary<int, string>
            {
                { 1, "Yes" },
                { 2, "No" }
            };

            cboMember.DataSource = new BindingSource(items, null);
            cboMember.DisplayMember = "Value";
            cboMember.ValueMember = "Key";
            cboMember.SelectedValue = 2;

            var payment = new Dictionary<int, string>
            {
                { 1, "Cash" },
                { 2, "Credit" },
                { 3, "Banking" },
                { 4, "Pay" }
            };

            cboPayment.DataSource = new BindingSource(payment, null);
            cboPayment.DisplayMember = "Value";
            cboPayment.ValueMember = "Key";
            cboPayment.SelectedValue = 1;

            var dataTable = _dapperService.QueryDataTable(Queries.GetDiscounts);

            // Add a default "All" row to the DataTable
            DataRow defaultRow = dataTable.NewRow();
            defaultRow["disValue"] = DBNull.Value; // or an appropriate value like -1 or "All"
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
                MessageBox.Show($"No data found for {cboDicount.Name}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmSale frmSale = new frmSale();
            this.Close();
            frmSale.ShowDialog();
        }

        private void cboMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMember.SelectedValue is 2) txtMemberCode.ReadOnly = true; else txtMemberCode.ReadOnly = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string mb;
            if (cboMember.SelectedValue is 2)
            {
                mb = "No Member";
            }
            else
            {
                mb = txtMemberCode.Text;
            }

            string invoiceNo = "DL" + DateTime.Now.ToString();
            SaleModel saleModel = new SaleModel()
            {
                invoiceNo = invoiceNo,
                staffCode = "Test",
                staffName = "Test",
                memberCode = mb,
                saleQty = Convert.ToInt32(txtTotalQuantity.Text),
                totalAmount = Convert.ToInt32(txtTotalAmount.Text),
                saleDate = DateTime.Now,
                receiveCash = Convert.ToInt32(txtReceiveCash.Text),
                refundCash = Convert.ToInt32(txtRefundCash.Text),
                paymentMethod = cboPayment.SelectedText
            };

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;

                SaleDetailModel saleDetailModel = new SaleDetailModel
                {
                    invoiceNo = invoiceNo,
                    itemCode = row.Cells["itemCode"].Value.ToString()!,
                    itemPrice = string.IsNullOrEmpty(row.Cells["itemPrice"].Value?.ToString()) ? (int?)null : Convert.ToInt32(row.Cells["itemPrice"].Value),
                    saleQuantity = string.IsNullOrEmpty(row.Cells["quantity"].Value?.ToString()) ? (int?)null : Convert.ToInt32(row.Cells["quantity"].Value),
                    totalAmount = Convert.ToInt32(row.Cells["netAmount"].Value?.ToString()),
                    saleDate = DateTime.Now
                    // Add other properties if required
                };
                ItemModel itemModel = new ItemModel
                {
                    itemCode = row.Cells["itemCode"].Value.ToString()!,
                    itemSold = Convert.ToInt32(row.Cells["quantity"].Value),
                    itemRemainStock = Convert.ToInt32(row.Cells["quantity"].Value),
                };
                _dapperService.Execute(Queries.UpdateItem, itemModel);
                _dapperService.Execute(Queries.CreateSaleDetail, saleDetailModel);
            }

            int result = _dapperService.Execute(Queries.CreateSale, saleModel);
            string message = result > 0 ? "Sale Complete" : "Sale Failed";
            MessageBox.Show(message);
        }

        private void txtReceiveCash_TextChanged(object sender, EventArgs e)
        {
            int receive = Convert.ToInt32(txtReceiveCash.Text);
            int total = Convert.ToInt32(txtTotalAmount.Text);
            int refund = receive - total;
            txtRefundCash.Text = refund.ToString();
        }
    }
}
