using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace posSystemWindows
{
    public partial class frmCheckOut : Form
    {
        public frmCheckOut()
        {
            InitializeComponent();
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

            decimal sumAmount = 0;
            decimal sumTotalQty = 0;
            for (int i = 0; i < dgvCart.Rows.Count; ++i)
            {
                sumAmount += Convert.ToDecimal(dgvCart.Rows[i].Cells[3].Value);
                sumTotalQty += Convert.ToDecimal(dgvCart.Rows[i].Cells[1].Value);
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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmSale frmSale = new frmSale();
            this.Close();
            frmSale.ShowDialog();
        }
    }
}
