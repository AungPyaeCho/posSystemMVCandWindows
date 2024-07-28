namespace posSystemWindows
{
    partial class frmSale
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox2 = new GroupBox();
            comboBox3 = new ComboBox();
            label5 = new Label();
            comboBox4 = new ComboBox();
            label6 = new Label();
            comboBox2 = new ComboBox();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label3 = new Label();
            txtBarcode = new TextBox();
            label2 = new Label();
            txtByName = new TextBox();
            label1 = new Label();
            btnExit = new Button();
            btnSearch = new Button();
            btnShowAll = new Button();
            dgvItems = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            itemName = new DataGridViewTextBoxColumn();
            catName = new DataGridViewTextBoxColumn();
            brandName = new DataGridViewTextBoxColumn();
            subCatName = new DataGridViewTextBoxColumn();
            subBrandName = new DataGridViewTextBoxColumn();
            itemPrice = new DataGridViewTextBoxColumn();
            itemWholeSalePrice = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            dgvCart = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            netAmount = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            btnSearchRange = new Button();
            txtItemName = new TextBox();
            label7 = new Label();
            groupBox3 = new GroupBox();
            chbWholeSale = new CheckBox();
            label15 = new Label();
            txtSubCategory = new TextBox();
            label14 = new Label();
            txtCategory = new TextBox();
            txtQuantity = new TextBox();
            label13 = new Label();
            label8 = new Label();
            txtSubBrand = new TextBox();
            label12 = new Label();
            txtBrand = new TextBox();
            label11 = new Label();
            txtWholeSalePrice = new TextBox();
            label10 = new Label();
            txtItemPrice = new TextBox();
            btnAddCart = new Button();
            groupBox4 = new GroupBox();
            btnCheckOut = new Button();
            btnRemove = new Button();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(comboBox3);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(comboBox4);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(comboBox2);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtBarcode);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtByName);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(11, 124);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(208, 491);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Search";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(6, 438);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(196, 39);
            comboBox3.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 404);
            label5.Name = "label5";
            label5.Size = new Size(194, 32);
            label5.TabIndex = 10;
            label5.Text = "by Sub-Category";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(6, 363);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(196, 39);
            comboBox4.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 329);
            label6.Name = "label6";
            label6.Size = new Size(143, 32);
            label6.TabIndex = 8;
            label6.Text = "by Category";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(6, 288);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(196, 39);
            comboBox2.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 254);
            label4.Name = "label4";
            label4.Size = new Size(160, 32);
            label4.TabIndex = 6;
            label4.Text = "by Sub-Brand";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 214);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(196, 39);
            comboBox1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 179);
            label3.Name = "label3";
            label3.Size = new Size(109, 32);
            label3.TabIndex = 4;
            label3.Text = "by Brand";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(6, 140);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(196, 38);
            txtBarcode.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 106);
            label2.Name = "label2";
            label2.Size = new Size(133, 32);
            label2.TabIndex = 2;
            label2.Text = "by Barcode";
            // 
            // txtByName
            // 
            txtByName.Location = new Point(6, 68);
            txtByName.Name = "txtByName";
            txtByName.Size = new Size(196, 38);
            txtByName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 33);
            label1.Name = "label1";
            label1.Size = new Size(111, 32);
            label1.TabIndex = 0;
            label1.Text = "by Name";
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Bottom;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(12, 997);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(207, 50);
            btnExit.TabIndex = 5;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Location = new Point(12, 68);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(208, 50);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(12, 12);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(208, 50);
            btnShowAll.TabIndex = 0;
            btnShowAll.Text = "Show All Items";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.Anchor = AnchorStyles.Top;
            dgvItems.BackgroundColor = SystemColors.Window;
            dgvItems.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { Column1, itemName, catName, brandName, subCatName, subBrandName, itemPrice, itemWholeSalePrice });
            dgvItems.Location = new Point(232, 0);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersWidth = 72;
            dgvItems.Size = new Size(749, 659);
            dgvItems.TabIndex = 1;
            dgvItems.CellContentClick += dgvItems_CellContentClick;
            // 
            // Column1
            // 
            Column1.DataPropertyName = "itemCode";
            Column1.HeaderText = "Item Code";
            Column1.MinimumWidth = 9;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 175;
            // 
            // itemName
            // 
            itemName.DataPropertyName = "itemName";
            itemName.HeaderText = "Item Name";
            itemName.MinimumWidth = 9;
            itemName.Name = "itemName";
            itemName.ReadOnly = true;
            itemName.Width = 200;
            // 
            // catName
            // 
            catName.DataPropertyName = "catName";
            catName.HeaderText = "Category";
            catName.MinimumWidth = 9;
            catName.Name = "catName";
            catName.ReadOnly = true;
            catName.Width = 120;
            // 
            // brandName
            // 
            brandName.DataPropertyName = "brandName";
            brandName.HeaderText = "Brand";
            brandName.MinimumWidth = 9;
            brandName.Name = "brandName";
            brandName.ReadOnly = true;
            brandName.Width = 120;
            // 
            // subCatName
            // 
            subCatName.DataPropertyName = "subCatName";
            subCatName.HeaderText = "Sub Category";
            subCatName.MinimumWidth = 9;
            subCatName.Name = "subCatName";
            subCatName.ReadOnly = true;
            subCatName.Width = 120;
            // 
            // subBrandName
            // 
            subBrandName.DataPropertyName = "subBrandName";
            subBrandName.HeaderText = "Sub Brand";
            subBrandName.MinimumWidth = 9;
            subBrandName.Name = "subBrandName";
            subBrandName.ReadOnly = true;
            subBrandName.Width = 120;
            // 
            // itemPrice
            // 
            itemPrice.DataPropertyName = "itemSalePrice";
            itemPrice.HeaderText = "itemPrice";
            itemPrice.MinimumWidth = 9;
            itemPrice.Name = "itemPrice";
            itemPrice.ReadOnly = true;
            itemPrice.Visible = false;
            itemPrice.Width = 175;
            // 
            // itemWholeSalePrice
            // 
            itemWholeSalePrice.DataPropertyName = "itemWholeSalePrice";
            itemWholeSalePrice.HeaderText = "itemWholeSalePrice";
            itemWholeSalePrice.MinimumWidth = 9;
            itemWholeSalePrice.Name = "itemWholeSalePrice";
            itemWholeSalePrice.ReadOnly = true;
            itemWholeSalePrice.Visible = false;
            itemWholeSalePrice.Width = 175;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvCart);
            groupBox1.Location = new Point(987, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(751, 796);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Shopping Cart";
            // 
            // dgvCart
            // 
            dgvCart.BackgroundColor = Color.White;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Columns.AddRange(new DataGridViewColumn[] { Column3, quantity, Column8, netAmount });
            dgvCart.Location = new Point(6, 38);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 72;
            dgvCart.Size = new Size(736, 752);
            dgvCart.TabIndex = 0;
            dgvCart.CellEndEdit += dgvCart_CellEndEdit;
            // 
            // Column3
            // 
            Column3.DataPropertyName = "itemName";
            Column3.HeaderText = "Item Name";
            Column3.MinimumWidth = 9;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 200;
            // 
            // quantity
            // 
            quantity.HeaderText = "Quantity";
            quantity.MinimumWidth = 9;
            quantity.Name = "quantity";
            quantity.Width = 175;
            // 
            // Column8
            // 
            Column8.DataPropertyName = "itemPrice";
            Column8.HeaderText = "Price";
            Column8.MinimumWidth = 9;
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 150;
            // 
            // netAmount
            // 
            netAmount.HeaderText = "Amount";
            netAmount.MinimumWidth = 9;
            netAmount.Name = "netAmount";
            netAmount.Width = 175;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearchRange);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnShowAll);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(226, 1059);
            panel1.TabIndex = 0;
            // 
            // btnSearchRange
            // 
            btnSearchRange.FlatStyle = FlatStyle.Flat;
            btnSearchRange.Location = new Point(12, 621);
            btnSearchRange.Name = "btnSearchRange";
            btnSearchRange.Size = new Size(208, 50);
            btnSearchRange.TabIndex = 7;
            btnSearchRange.Text = "Search Range";
            btnSearchRange.UseVisualStyleBackColor = true;
            btnSearchRange.Click += btnSearchRange_Click;
            // 
            // txtItemName
            // 
            txtItemName.Location = new Point(6, 69);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(196, 38);
            txtItemName.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 34);
            label7.Name = "label7";
            label7.Size = new Size(133, 32);
            label7.TabIndex = 12;
            label7.Text = "Item Name";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Bottom;
            groupBox3.Controls.Add(chbWholeSale);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(txtSubCategory);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(txtCategory);
            groupBox3.Controls.Add(txtQuantity);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(txtSubBrand);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(txtBrand);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(txtWholeSalePrice);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(txtItemPrice);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(txtItemName);
            groupBox3.Location = new Point(232, 697);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(749, 350);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Details";
            // 
            // chbWholeSale
            // 
            chbWholeSale.AutoSize = true;
            chbWholeSale.Location = new Point(612, 69);
            chbWholeSale.Name = "chbWholeSale";
            chbWholeSale.Size = new Size(74, 36);
            chbWholeSale.TabIndex = 30;
            chbWholeSale.Text = "Yes";
            chbWholeSale.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(208, 186);
            label15.Name = "label15";
            label15.Size = new Size(158, 32);
            label15.TabIndex = 24;
            label15.Text = "Sub Category";
            // 
            // txtSubCategory
            // 
            txtSubCategory.Enabled = false;
            txtSubCategory.Location = new Point(208, 221);
            txtSubCategory.Name = "txtSubCategory";
            txtSubCategory.Size = new Size(196, 38);
            txtSubCategory.TabIndex = 25;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 186);
            label14.Name = "label14";
            label14.Size = new Size(110, 32);
            label14.TabIndex = 22;
            label14.Text = "Category";
            // 
            // txtCategory
            // 
            txtCategory.Enabled = false;
            txtCategory.Location = new Point(6, 221);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(196, 38);
            txtCategory.TabIndex = 23;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(6, 300);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(196, 38);
            txtQuantity.TabIndex = 24;
            txtQuantity.Text = "1";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(208, 110);
            label13.Name = "label13";
            label13.Size = new Size(124, 32);
            label13.TabIndex = 20;
            label13.Text = "Sub Brand";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 265);
            label8.Name = "label8";
            label8.Size = new Size(106, 32);
            label8.TabIndex = 23;
            label8.Text = "Quantity";
            // 
            // txtSubBrand
            // 
            txtSubBrand.Enabled = false;
            txtSubBrand.Location = new Point(208, 145);
            txtSubBrand.Name = "txtSubBrand";
            txtSubBrand.Size = new Size(196, 38);
            txtSubBrand.TabIndex = 21;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 110);
            label12.Name = "label12";
            label12.Size = new Size(76, 32);
            label12.TabIndex = 18;
            label12.Text = "Brand";
            // 
            // txtBrand
            // 
            txtBrand.Enabled = false;
            txtBrand.Location = new Point(6, 145);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(196, 38);
            txtBrand.TabIndex = 19;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(410, 34);
            label11.Name = "label11";
            label11.Size = new Size(192, 32);
            label11.TabIndex = 16;
            label11.Text = "Whole Sale Price";
            // 
            // txtWholeSalePrice
            // 
            txtWholeSalePrice.Location = new Point(410, 69);
            txtWholeSalePrice.Name = "txtWholeSalePrice";
            txtWholeSalePrice.Size = new Size(196, 38);
            txtWholeSalePrice.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(208, 34);
            label10.Name = "label10";
            label10.Size = new Size(65, 32);
            label10.TabIndex = 14;
            label10.Text = "Price";
            // 
            // txtItemPrice
            // 
            txtItemPrice.Location = new Point(208, 69);
            txtItemPrice.Name = "txtItemPrice";
            txtItemPrice.Size = new Size(196, 38);
            txtItemPrice.TabIndex = 15;
            // 
            // btnAddCart
            // 
            btnAddCart.BackColor = Color.SeaGreen;
            btnAddCart.FlatStyle = FlatStyle.Flat;
            btnAddCart.ForeColor = Color.White;
            btnAddCart.Location = new Point(6, 32);
            btnAddCart.Name = "btnAddCart";
            btnAddCart.Size = new Size(180, 180);
            btnAddCart.TabIndex = 19;
            btnAddCart.Text = "&Add Cart";
            btnAddCart.UseVisualStyleBackColor = false;
            btnAddCart.Click += btnAddCart_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Bottom;
            groupBox4.Controls.Add(btnCheckOut);
            groupBox4.Controls.Add(btnAddCart);
            groupBox4.Controls.Add(btnRemove);
            groupBox4.Location = new Point(987, 814);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(751, 233);
            groupBox4.TabIndex = 19;
            groupBox4.TabStop = false;
            // 
            // btnCheckOut
            // 
            btnCheckOut.BackColor = Color.SteelBlue;
            btnCheckOut.FlatStyle = FlatStyle.Flat;
            btnCheckOut.ForeColor = Color.White;
            btnCheckOut.Location = new Point(562, 32);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(180, 180);
            btnCheckOut.TabIndex = 29;
            btnCheckOut.Text = "Check Out";
            btnCheckOut.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Brown;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.ForeColor = Color.White;
            btnRemove.Location = new Point(290, 32);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(180, 180);
            btnRemove.TabIndex = 22;
            btnRemove.Text = "Remove Item";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // frmSale
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1750, 1059);
            Controls.Add(dgvItems);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmSale";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sale";
            Load += frmSale_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvItems;
        private Button btnShowAll;
        private Button btnExit;
        private Button btnSearch;
        private GroupBox groupBox1;
        private DataGridView dgvCart;
        private GroupBox groupBox2;
        private ComboBox comboBox3;
        private Label label5;
        private ComboBox comboBox4;
        private Label label6;
        private ComboBox comboBox2;
        private Label label4;
        private ComboBox comboBox1;
        private Label label3;
        private TextBox txtBarcode;
        private Label label2;
        private TextBox txtByName;
        private Label label1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn netAmount;
        private Panel panel1;
        private TextBox txtItemName;
        private Label label7;
        private GroupBox groupBox3;
        private Label label15;
        private TextBox txtSubCategory;
        private Label label14;
        private TextBox txtCategory;
        private Label label13;
        private TextBox txtSubBrand;
        private Label label12;
        private TextBox txtBrand;
        private Label label11;
        private TextBox txtWholeSalePrice;
        private Label label10;
        private TextBox txtItemPrice;
        private Button btnAddCart;
        private GroupBox groupBox4;
        private TextBox txtQuantity;
        private Label label8;
        private Button btnCheckOut;
        private Button btnRemove;
        private CheckBox chbWholeSale;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn itemName;
        private DataGridViewTextBoxColumn catName;
        private DataGridViewTextBoxColumn brandName;
        private DataGridViewTextBoxColumn subCatName;
        private DataGridViewTextBoxColumn subBrandName;
        private DataGridViewTextBoxColumn itemPrice;
        private DataGridViewTextBoxColumn itemWholeSalePrice;
        private Button btnSearchRange;
    }
}
