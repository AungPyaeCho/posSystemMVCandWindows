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
            colItemName = new DataGridViewTextBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            colItemPrice = new DataGridViewTextBoxColumn();
            netAmount = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            btnExit = new Button();
            btnPrint = new Button();
            label5 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            textBox4 = new TextBox();
            cboPayment = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            cboMember = new ComboBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            txtTotalAmount = new TextBox();
            label7 = new Label();
            txtTotalQuantity = new TextBox();
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
            dgvCart.Columns.AddRange(new DataGridViewColumn[] { colItemName, quantity, colItemPrice, netAmount });
            dgvCart.Location = new Point(12, 37);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgvCart.Size = new Size(770, 575);
            dgvCart.TabIndex = 45;
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
            // colItemPrice
            // 
            colItemPrice.DataPropertyName = "itemPrice";
            colItemPrice.HeaderText = "Item Price";
            colItemPrice.MinimumWidth = 9;
            colItemPrice.Name = "colItemPrice";
            // 
            // netAmount
            // 
            netAmount.DataPropertyName = "netAmount";
            netAmount.HeaderText = "Amount";
            netAmount.MinimumWidth = 9;
            netAmount.Name = "netAmount";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnExit);
            groupBox2.Controls.Add(btnPrint);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(textBox4);
            groupBox2.Controls.Add(cboPayment);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cboMember);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(txtTotalAmount);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(txtTotalQuantity);
            groupBox2.Dock = DockStyle.Bottom;
            groupBox2.Location = new Point(0, 640);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(799, 260);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = SystemColors.Control;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.ForeColor = Color.Black;
            btnExit.Location = new Point(405, 192);
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
            btnPrint.Location = new Point(405, 120);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(382, 50);
            btnPrint.TabIndex = 46;
            btnPrint.Text = "&Print";
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 210);
            label5.Name = "label5";
            label5.Size = new Size(112, 32);
            label5.TabIndex = 26;
            label5.Text = "Refunds :";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(198, 207);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(196, 38);
            textBox3.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 166);
            label6.Name = "label6";
            label6.Size = new Size(121, 32);
            label6.TabIndex = 24;
            label6.Text = "Received :";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(198, 163);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(196, 38);
            textBox4.TabIndex = 25;
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
            label3.Location = new Point(400, 79);
            label3.Name = "label3";
            label3.Size = new Size(180, 32);
            label3.TabIndex = 21;
            label3.Text = "Member Code :";
            // 
            // cboMember
            // 
            cboMember.FormattingEnabled = true;
            cboMember.Location = new Point(586, 31);
            cboMember.Name = "cboMember";
            cboMember.Size = new Size(196, 39);
            cboMember.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(400, 34);
            label2.Name = "label2";
            label2.Size = new Size(117, 32);
            label2.TabIndex = 18;
            label2.Text = "Member :";
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(586, 76);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(196, 38);
            textBox2.TabIndex = 19;
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
            // frmCheckOut
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 900);
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
        private TextBox textBox2;
        private ComboBox cboPayment;
        private Label label4;
        private Label label3;
        private ComboBox cboMember;
        private Label label5;
        private TextBox textBox3;
        private Label label6;
        private TextBox textBox4;
        private Button btnPrint;
        private Button btnExit;
        private DataGridViewTextBoxColumn colItemName;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn colItemPrice;
        private DataGridViewTextBoxColumn netAmount;
    }
}