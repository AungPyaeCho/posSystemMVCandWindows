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
            cboSubCategories = new ComboBox();
            label5 = new Label();
            cboCategories = new ComboBox();
            label6 = new Label();
            cboSubBrands = new ComboBox();
            label4 = new Label();
            cboBrands = new ComboBox();
            label3 = new Label();
            txtBarcode = new TextBox();
            label2 = new Label();
            txtByName = new TextBox();
            label1 = new Label();
            btnExit = new Button();
            btnSearch = new Button();
            btnShowAll = new Button();
            panel1 = new Panel();
            lblUserName = new Label();
            lblStaffCode = new Label();
            lblStaffRole = new Label();
            txtItemName = new TextBox();
            label7 = new Label();
            groupBox3 = new GroupBox();
            txtRemainStock = new TextBox();
            cboPromotion = new ComboBox();
            btnRemove = new Button();
            lblStock = new Label();
            label16 = new Label();
            btnCheckOut = new Button();
            btnAddCart = new Button();
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
            groupBox1 = new GroupBox();
            dgvItems = new DataGridView();
            itemCode = new DataGridViewTextBoxColumn();
            itemName = new DataGridViewTextBoxColumn();
            catName = new DataGridViewTextBoxColumn();
            brandName = new DataGridViewTextBoxColumn();
            subCatName = new DataGridViewTextBoxColumn();
            subBrandName = new DataGridViewTextBoxColumn();
            itemPrice = new DataGridViewTextBoxColumn();
            itemWholeSalePrice = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            groupBox4 = new GroupBox();
            dgvCart = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            colItemName = new DataGridViewTextBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            colItemPrice = new DataGridViewTextBoxColumn();
            netAmount = new DataGridViewTextBoxColumn();
            itemRemainStock = new DataGridViewTextBoxColumn();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cboSubCategories);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(cboCategories);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(cboSubBrands);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(cboBrands);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtBarcode);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtByName);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(9, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(220, 530);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Search";
            // 
            // cboSubCategories
            // 
            cboSubCategories.FormattingEnabled = true;
            cboSubCategories.Location = new Point(6, 469);
            cboSubCategories.Name = "cboSubCategories";
            cboSubCategories.Size = new Size(208, 39);
            cboSubCategories.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(6, 434);
            label5.Name = "label5";
            label5.Size = new Size(194, 32);
            label5.TabIndex = 10;
            label5.Text = "by Sub-Category";
            // 
            // cboCategories
            // 
            cboCategories.FormattingEnabled = true;
            cboCategories.Location = new Point(6, 391);
            cboCategories.Name = "cboCategories";
            cboCategories.Size = new Size(208, 39);
            cboCategories.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Location = new Point(6, 355);
            label6.Name = "label6";
            label6.Size = new Size(143, 32);
            label6.TabIndex = 8;
            label6.Text = "by Category";
            // 
            // cboSubBrands
            // 
            cboSubBrands.FormattingEnabled = true;
            cboSubBrands.Location = new Point(6, 313);
            cboSubBrands.Name = "cboSubBrands";
            cboSubBrands.Size = new Size(208, 39);
            cboSubBrands.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(6, 277);
            label4.Name = "label4";
            label4.Size = new Size(160, 32);
            label4.TabIndex = 6;
            label4.Text = "by Sub-Brand";
            // 
            // cboBrands
            // 
            cboBrands.FormattingEnabled = true;
            cboBrands.Location = new Point(6, 235);
            cboBrands.Name = "cboBrands";
            cboBrands.Size = new Size(208, 39);
            cboBrands.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(3, 200);
            label3.Name = "label3";
            label3.Size = new Size(109, 32);
            label3.TabIndex = 4;
            label3.Text = "by Brand";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(6, 156);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(208, 38);
            txtBarcode.TabIndex = 3;
            txtBarcode.TextChanged += txtBarcode_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(3, 121);
            label2.Name = "label2";
            label2.Size = new Size(133, 32);
            label2.TabIndex = 2;
            label2.Text = "by Barcode";
            // 
            // txtByName
            // 
            txtByName.Location = new Point(6, 80);
            txtByName.Name = "txtByName";
            txtByName.Size = new Size(208, 38);
            txtByName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(6, 45);
            label1.Name = "label1";
            label1.Size = new Size(111, 32);
            label1.TabIndex = 0;
            label1.Text = "by Name";
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Bottom;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(15, 950);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(208, 50);
            btnExit.TabIndex = 5;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Location = new Point(15, 548);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(208, 50);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "&Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(15, 622);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(208, 50);
            btnShowAll.TabIndex = 0;
            btnShowAll.Text = "Show All &Items";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblUserName);
            panel1.Controls.Add(lblStaffCode);
            panel1.Controls.Add(lblStaffRole);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnShowAll);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(232, 1016);
            panel1.TabIndex = 0;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 9F);
            lblUserName.Location = new Point(15, 688);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(132, 30);
            lblUserName.TabIndex = 8;
            lblUserName.Text = "lblUserName";
            lblUserName.Visible = false;
            // 
            // lblStaffCode
            // 
            lblStaffCode.AutoSize = true;
            lblStaffCode.Font = new Font("Segoe UI", 9F);
            lblStaffCode.Location = new Point(15, 748);
            lblStaffCode.Name = "lblStaffCode";
            lblStaffCode.Size = new Size(125, 30);
            lblStaffCode.TabIndex = 9;
            lblStaffCode.Text = "lblStaffCode";
            lblStaffCode.Visible = false;
            // 
            // lblStaffRole
            // 
            lblStaffRole.AutoSize = true;
            lblStaffRole.Font = new Font("Segoe UI", 9F);
            lblStaffRole.Location = new Point(15, 718);
            lblStaffRole.Name = "lblStaffRole";
            lblStaffRole.Size = new Size(117, 30);
            lblStaffRole.TabIndex = 10;
            lblStaffRole.Text = "lblStaffRole";
            lblStaffRole.Visible = false;
            // 
            // txtItemName
            // 
            txtItemName.Location = new Point(6, 69);
            txtItemName.Name = "txtItemName";
            txtItemName.ReadOnly = true;
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
            groupBox3.Controls.Add(txtRemainStock);
            groupBox3.Controls.Add(cboPromotion);
            groupBox3.Controls.Add(btnRemove);
            groupBox3.Controls.Add(lblStock);
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(btnCheckOut);
            groupBox3.Controls.Add(btnAddCart);
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
            groupBox3.Location = new Point(238, 819);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1524, 197);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Details";
            // 
            // txtRemainStock
            // 
            txtRemainStock.Location = new Point(208, 145);
            txtRemainStock.Name = "txtRemainStock";
            txtRemainStock.ReadOnly = true;
            txtRemainStock.Size = new Size(196, 38);
            txtRemainStock.TabIndex = 45;
            // 
            // cboPromotion
            // 
            cboPromotion.FormattingEnabled = true;
            cboPromotion.Location = new Point(6, 144);
            cboPromotion.Name = "cboPromotion";
            cboPromotion.Size = new Size(196, 39);
            cboPromotion.TabIndex = 12;
            // 
            // btnRemove
            // 
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Location = new Point(1301, 37);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(208, 50);
            btnRemove.TabIndex = 44;
            btnRemove.Text = "&Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(208, 110);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(71, 32);
            lblStock.TabIndex = 36;
            lblStock.Text = "Stock";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 110);
            label16.Name = "label16";
            label16.Size = new Size(126, 32);
            label16.TabIndex = 34;
            label16.Text = "Promotion";
            // 
            // btnCheckOut
            // 
            btnCheckOut.BackColor = SystemColors.Control;
            btnCheckOut.FlatStyle = FlatStyle.Flat;
            btnCheckOut.ForeColor = Color.Black;
            btnCheckOut.Location = new Point(1301, 131);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(208, 50);
            btnCheckOut.TabIndex = 33;
            btnCheckOut.Text = "&Check Out";
            btnCheckOut.UseVisualStyleBackColor = false;
            btnCheckOut.Click += btnCheckOut_Click;
            // 
            // btnAddCart
            // 
            btnAddCart.BackColor = SystemColors.Control;
            btnAddCart.FlatStyle = FlatStyle.Flat;
            btnAddCart.ForeColor = Color.Black;
            btnAddCart.Location = new Point(1016, 131);
            btnAddCart.Name = "btnAddCart";
            btnAddCart.Size = new Size(196, 50);
            btnAddCart.TabIndex = 31;
            btnAddCart.Text = "&Add Cart";
            btnAddCart.UseVisualStyleBackColor = false;
            btnAddCart.Click += btnAddCart_Click;
            // 
            // chbWholeSale
            // 
            chbWholeSale.AutoSize = true;
            chbWholeSale.Location = new Point(814, 145);
            chbWholeSale.Name = "chbWholeSale";
            chbWholeSale.Size = new Size(74, 36);
            chbWholeSale.TabIndex = 30;
            chbWholeSale.Text = "Yes";
            chbWholeSale.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(1016, 34);
            label15.Name = "label15";
            label15.Size = new Size(158, 32);
            label15.TabIndex = 24;
            label15.Text = "Sub Category";
            // 
            // txtSubCategory
            // 
            txtSubCategory.Location = new Point(1016, 69);
            txtSubCategory.Name = "txtSubCategory";
            txtSubCategory.ReadOnly = true;
            txtSubCategory.Size = new Size(196, 38);
            txtSubCategory.TabIndex = 25;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(814, 34);
            label14.Name = "label14";
            label14.Size = new Size(110, 32);
            label14.TabIndex = 22;
            label14.Text = "Category";
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(814, 69);
            txtCategory.Name = "txtCategory";
            txtCategory.ReadOnly = true;
            txtCategory.Size = new Size(196, 38);
            txtCategory.TabIndex = 23;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(208, 69);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(196, 38);
            txtQuantity.TabIndex = 24;
            txtQuantity.Text = "1";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(612, 34);
            label13.Name = "label13";
            label13.Size = new Size(124, 32);
            label13.TabIndex = 20;
            label13.Text = "Sub Brand";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(208, 34);
            label8.Name = "label8";
            label8.Size = new Size(106, 32);
            label8.TabIndex = 23;
            label8.Text = "Quantity";
            // 
            // txtSubBrand
            // 
            txtSubBrand.Location = new Point(612, 69);
            txtSubBrand.Name = "txtSubBrand";
            txtSubBrand.ReadOnly = true;
            txtSubBrand.Size = new Size(196, 38);
            txtSubBrand.TabIndex = 21;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(410, 34);
            label12.Name = "label12";
            label12.Size = new Size(76, 32);
            label12.TabIndex = 18;
            label12.Text = "Brand";
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(410, 69);
            txtBrand.Name = "txtBrand";
            txtBrand.ReadOnly = true;
            txtBrand.Size = new Size(196, 38);
            txtBrand.TabIndex = 19;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(612, 110);
            label11.Name = "label11";
            label11.Size = new Size(192, 32);
            label11.TabIndex = 16;
            label11.Text = "Whole Sale Price";
            // 
            // txtWholeSalePrice
            // 
            txtWholeSalePrice.Location = new Point(612, 145);
            txtWholeSalePrice.Name = "txtWholeSalePrice";
            txtWholeSalePrice.ReadOnly = true;
            txtWholeSalePrice.Size = new Size(196, 38);
            txtWholeSalePrice.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(410, 110);
            label10.Name = "label10";
            label10.Size = new Size(65, 32);
            label10.TabIndex = 14;
            label10.Text = "Price";
            // 
            // txtItemPrice
            // 
            txtItemPrice.Location = new Point(410, 145);
            txtItemPrice.Name = "txtItemPrice";
            txtItemPrice.ReadOnly = true;
            txtItemPrice.Size = new Size(196, 38);
            txtItemPrice.TabIndex = 15;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvItems);
            groupBox1.Location = new Point(244, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(758, 781);
            groupBox1.TabIndex = 41;
            groupBox1.TabStop = false;
            groupBox1.Text = "Products";
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.BackgroundColor = SystemColors.Window;
            dgvItems.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { itemCode, itemName, catName, brandName, subCatName, subBrandName, itemPrice, itemWholeSalePrice, Column1, Column3, Column4 });
            dgvItems.Location = new Point(0, 37);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersWidth = 72;
            dgvItems.Size = new Size(752, 738);
            dgvItems.TabIndex = 42;
            dgvItems.CellContentClick += dgvItems_CellContentClick;
            // 
            // itemCode
            // 
            itemCode.DataPropertyName = "itemCode";
            itemCode.HeaderText = "Item Code";
            itemCode.MinimumWidth = 9;
            itemCode.Name = "itemCode";
            itemCode.ReadOnly = true;
            itemCode.Width = 175;
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
            // Column1
            // 
            Column1.DataPropertyName = "itemStock";
            Column1.HeaderText = "itemStock";
            Column1.MinimumWidth = 9;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 175;
            // 
            // Column3
            // 
            Column3.DataPropertyName = "itemRemainStock";
            Column3.HeaderText = "itemRemainStock";
            Column3.MinimumWidth = 9;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 175;
            // 
            // Column4
            // 
            Column4.DataPropertyName = "itemSoldStock";
            Column4.HeaderText = "itemSoldStock";
            Column4.MinimumWidth = 9;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 175;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dgvCart);
            groupBox4.Location = new Point(1011, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(742, 781);
            groupBox4.TabIndex = 43;
            groupBox4.TabStop = false;
            groupBox4.Text = "Shopping Cart";
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.BackgroundColor = SystemColors.Control;
            dgvCart.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dgvCart.ColumnHeadersHeight = 72;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCart.Columns.AddRange(new DataGridViewColumn[] { Column2, colItemName, quantity, colItemPrice, netAmount, itemRemainStock });
            dgvCart.Location = new Point(6, 37);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 72;
            dgvCart.Size = new Size(730, 738);
            dgvCart.TabIndex = 44;
            dgvCart.CellEndEdit += dgvCart_CellEndEdit;
            // 
            // Column2
            // 
            Column2.DataPropertyName = "itemCode";
            Column2.HeaderText = "Item Code";
            Column2.MinimumWidth = 9;
            Column2.Name = "Column2";
            Column2.Visible = false;
            Column2.Width = 175;
            // 
            // colItemName
            // 
            colItemName.DataPropertyName = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.MinimumWidth = 9;
            colItemName.Name = "colItemName";
            colItemName.Width = 180;
            // 
            // quantity
            // 
            quantity.DataPropertyName = "quantity";
            quantity.HeaderText = "Quantity";
            quantity.MinimumWidth = 9;
            quantity.Name = "quantity";
            quantity.Width = 155;
            // 
            // colItemPrice
            // 
            colItemPrice.DataPropertyName = "itemPrice";
            colItemPrice.HeaderText = "Item Price";
            colItemPrice.MinimumWidth = 9;
            colItemPrice.Name = "colItemPrice";
            colItemPrice.Width = 160;
            // 
            // netAmount
            // 
            netAmount.DataPropertyName = "netAmount";
            netAmount.HeaderText = "Amount";
            netAmount.MinimumWidth = 9;
            netAmount.Name = "netAmount";
            netAmount.Width = 160;
            // 
            // itemRemainStock
            // 
            itemRemainStock.DataPropertyName = "itemRemainStock";
            itemRemainStock.HeaderText = "Item Remain Stock";
            itemRemainStock.MinimumWidth = 9;
            itemRemainStock.Name = "itemRemainStock";
            itemRemainStock.Width = 175;
            // 
            // frmSale
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1765, 1016);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnShowAll;
        private Button btnExit;
        private Button btnSearch;
        private GroupBox groupBox2;
        private ComboBox cboSubCategories;
        private Label label5;
        private ComboBox cboCategories;
        private Label label6;
        private ComboBox cboSubBrands;
        private Label label4;
        private ComboBox cboBrands;
        private Label label3;
        private TextBox txtBarcode;
        private Label label2;
        private TextBox txtByName;
        private Label label1;
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
        private TextBox txtQuantity;
        private Label label8;
        private CheckBox chbWholeSale;
        private Button btnCheckOut;
        private Button btnAddCart;
        private Label lblStock;
        private Label label16;
        private GroupBox groupBox1;
        private DataGridView dgvItems;
        private GroupBox groupBox4;
        private DataGridView dgvCart;
        private Button btnRemove;
        private ComboBox cboPromotion;
        private DataGridViewTextBoxColumn itemCode;
        private DataGridViewTextBoxColumn itemName;
        private DataGridViewTextBoxColumn catName;
        private DataGridViewTextBoxColumn brandName;
        private DataGridViewTextBoxColumn subCatName;
        private DataGridViewTextBoxColumn subBrandName;
        private DataGridViewTextBoxColumn itemPrice;
        private DataGridViewTextBoxColumn itemWholeSalePrice;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private TextBox txtRemainStock;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn colItemName;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn colItemPrice;
        private DataGridViewTextBoxColumn netAmount;
        private DataGridViewTextBoxColumn itemRemainStock;
        private Label lblUserName;
        private Label lblStaffCode;
        private Label lblStaffRole;
    }
}
