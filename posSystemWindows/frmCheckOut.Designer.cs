namespace posSystemWindows
{
    partial class frmCheckOut
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            dgvCart = new DataGridView();
            itemCode = new DataGridViewTextBoxColumn();
            colItemName = new DataGridViewTextBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            itemPrice = new DataGridViewTextBoxColumn();
            netAmount = new DataGridViewTextBoxColumn();
            itemRemainStock = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            txtDiscountAmount = new TextBox();
            cboDicount = new ComboBox();
            label16 = new Label();
            btnExit = new Button();
            btnPrint = new Button();
            label5 = new Label();
            txtRefundCash = new TextBox();
            label6 = new Label();
            txtReceiveCash = new TextBox();
            cboPayment = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            cboMember = new ComboBox();
            label2 = new Label();
            txtMemberCode = new TextBox();
            label1 = new Label();
            txtTotalAmount = new TextBox();
            label7 = new Label();
            txtTotalQuantity = new TextBox();
            lblUserName = new Label();
            lblStaffCode = new Label();
            lblStaffRole = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvCart);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(799, 614);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Shopping Cart";
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.BackgroundColor = SystemColors.Control;
            dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgvCart.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dgvCart.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Columns.AddRange(new DataGridViewColumn[] { itemCode, colItemName, quantity, itemPrice, netAmount, itemRemainStock });
            dgvCart.Location = new Point(12, 37);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgvCart.Size = new Size(770, 575);
            dgvCart.TabIndex = 45;
            // 
            // itemCode
            // 
            itemCode.DataPropertyName = "itemCode";
            itemCode.HeaderText = "Item Code";
            itemCode.MinimumWidth = 9;
            itemCode.Name = "itemCode";
            itemCode.ReadOnly = true;
            // 
            // colItemName
            // 
            colItemName.DataPropertyName = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.MinimumWidth = 9;
            colItemName.Name = "colItemName";
            // 
            // quantity
            // 
            quantity.DataPropertyName = "quantity";
            quantity.HeaderText = "Quantity";
            quantity.MinimumWidth = 9;
            quantity.Name = "quantity";
            // 
            // itemPrice
            // 
            itemPrice.DataPropertyName = "itemPrice";
            itemPrice.HeaderText = "Item Price";
            itemPrice.MinimumWidth = 9;
            itemPrice.Name = "itemPrice";
            // 
            // netAmount
            // 
            netAmount.DataPropertyName = "netAmount";
            netAmount.HeaderText = "Amount";
            netAmount.MinimumWidth = 9;
            netAmount.Name = "netAmount";
            // 
            // itemRemainStock
            // 
            itemRemainStock.DataPropertyName = "itemRemainStock";
            itemRemainStock.HeaderText = "itemRemainStock";
            itemRemainStock.MinimumWidth = 9;
            itemRemainStock.Name = "itemRemainStock";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDiscountAmount);
            groupBox2.Controls.Add(cboDicount);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(btnExit);
            groupBox2.Controls.Add(btnPrint);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtRefundCash);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(txtReceiveCash);
            groupBox2.Controls.Add(cboPayment);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cboMember);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtMemberCode);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(txtTotalAmount);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(txtTotalQuantity);
            groupBox2.Dock = DockStyle.Bottom;
            groupBox2.Location = new Point(0, 643);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(799, 317);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // txtDiscountAmount
            // 
            txtDiscountAmount.Location = new Point(198, 208);
            txtDiscountAmount.Name = "txtDiscountAmount";
            txtDiscountAmount.ReadOnly = true;
            txtDiscountAmount.Size = new Size(196, 38);
            txtDiscountAmount.TabIndex = 51;
            // 
            // cboDicount
            // 
            cboDicount.FormattingEnabled = true;
            cboDicount.Location = new Point(198, 163);
            cboDicount.Name = "cboDicount";
            cboDicount.Size = new Size(196, 39);
            cboDicount.TabIndex = 48;
            cboDicount.SelectedIndexChanged += cboDicount_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(12, 166);
            label16.Name = "label16";
            label16.Size = new Size(120, 32);
            label16.TabIndex = 49;
            label16.Text = "Discount :";
            // 
            // btnExit
            // 
            btnExit.BackColor = SystemColors.Control;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.ForeColor = Color.Black;
            btnExit.Location = new Point(405, 252);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(382, 50);
            btnExit.TabIndex = 47;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = SystemColors.Control;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.ForeColor = Color.Black;
            btnPrint.Location = new Point(12, 252);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(382, 50);
            btnPrint.TabIndex = 46;
            btnPrint.Text = "&Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(405, 78);
            label5.Name = "label5";
            label5.Size = new Size(112, 32);
            label5.TabIndex = 26;
            label5.Text = "Refunds :";
            // 
            // txtRefundCash
            // 
            txtRefundCash.Location = new Point(591, 75);
            txtRefundCash.Name = "txtRefundCash";
            txtRefundCash.Size = new Size(196, 38);
            txtRefundCash.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(405, 34);
            label6.Name = "label6";
            label6.Size = new Size(121, 32);
            label6.TabIndex = 24;
            label6.Text = "Received :";
            // 
            // txtReceiveCash
            // 
            txtReceiveCash.Location = new Point(591, 31);
            txtReceiveCash.Name = "txtReceiveCash";
            txtReceiveCash.Size = new Size(196, 38);
            txtReceiveCash.TabIndex = 25;
            txtReceiveCash.TextChanged += txtReceiveCash_TextChanged;
            // 
            // cboPayment
            // 
            cboPayment.FormattingEnabled = true;
            cboPayment.Location = new Point(198, 75);
            cboPayment.Name = "cboPayment";
            cboPayment.Size = new Size(196, 39);
            cboPayment.TabIndex = 23;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 78);
            label4.Name = "label4";
            label4.Size = new Size(118, 32);
            label4.TabIndex = 22;
            label4.Text = "Payment :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(405, 167);
            label3.Name = "label3";
            label3.Size = new Size(180, 32);
            label3.TabIndex = 21;
            label3.Text = "Member Code :";
            // 
            // cboMember
            // 
            cboMember.FormattingEnabled = true;
            cboMember.Location = new Point(591, 119);
            cboMember.Name = "cboMember";
            cboMember.Size = new Size(196, 39);
            cboMember.TabIndex = 20;
            cboMember.SelectedIndexChanged += cboMember_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(405, 122);
            label2.Name = "label2";
            label2.Size = new Size(117, 32);
            label2.TabIndex = 18;
            label2.Text = "Member :";
            // 
            // txtMemberCode
            // 
            txtMemberCode.Location = new Point(591, 164);
            txtMemberCode.Name = "txtMemberCode";
            txtMemberCode.Size = new Size(196, 38);
            txtMemberCode.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 122);
            label1.Name = "label1";
            label1.Size = new Size(170, 32);
            label1.TabIndex = 16;
            label1.Text = "Total Amount :";
            // 
            // txtTotalAmount
            // 
            txtTotalAmount.Location = new Point(198, 119);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.Size = new Size(196, 38);
            txtTotalAmount.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 34);
            label7.Name = "label7";
            label7.Size = new Size(176, 32);
            label7.TabIndex = 14;
            label7.Text = "Total Quantity :";
            // 
            // txtTotalQuantity
            // 
            txtTotalQuantity.Location = new Point(198, 31);
            txtTotalQuantity.Name = "txtTotalQuantity";
            txtTotalQuantity.Size = new Size(196, 38);
            txtTotalQuantity.TabIndex = 15;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 9F);
            lblUserName.Location = new Point(333, 435);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(132, 30);
            lblUserName.TabIndex = 11;
            lblUserName.Text = "lblUserName";
            // 
            // lblStaffCode
            // 
            lblStaffCode.AutoSize = true;
            lblStaffCode.Font = new Font("Segoe UI", 9F);
            lblStaffCode.Location = new Point(333, 495);
            lblStaffCode.Name = "lblStaffCode";
            lblStaffCode.Size = new Size(125, 30);
            lblStaffCode.TabIndex = 12;
            lblStaffCode.Text = "lblStaffCode";
            // 
            // lblStaffRole
            // 
            lblStaffRole.AutoSize = true;
            lblStaffRole.Font = new Font("Segoe UI", 9F);
            lblStaffRole.Location = new Point(333, 465);
            lblStaffRole.Name = "lblStaffRole";
            lblStaffRole.Size = new Size(117, 30);
            lblStaffRole.TabIndex = 13;
            lblStaffRole.Text = "lblStaffRole";
            // 
            // frmCheckOut
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 960);
            Controls.Add(lblUserName);
            Controls.Add(lblStaffCode);
            Controls.Add(lblStaffRole);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmCheckOut";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Check Out";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView dgvCart;
        private Label label7;
        private TextBox txtTotalQuantity;
        private Label label1;
        private TextBox txtTotalAmount;
        private Label label2;
        private TextBox txtMemberCode;
        private ComboBox cboPayment;
        private Label label4;
        private Label label3;
        private ComboBox cboMember;
        private Label label5;
        private TextBox txtRefundCash;
        private Label label6;
        private TextBox txtReceiveCash;
        private Button btnPrint;
        private Button btnExit;
        private DataGridViewTextBoxColumn itemCode;
        private DataGridViewTextBoxColumn colItemName;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn itemPrice;
        private DataGridViewTextBoxColumn netAmount;
        private DataGridViewTextBoxColumn itemRemainStock;
        private ComboBox cboDicount;
        private Label label16;
        private TextBox txtDiscountAmount;
        private Label lblUserName;
        private Label lblStaffCode;
        private Label lblStaffRole;
    }
}