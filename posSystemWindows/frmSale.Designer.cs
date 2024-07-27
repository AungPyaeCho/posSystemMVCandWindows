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
            panel1 = new Panel();
            btnExit = new Button();
            btnSearch = new Button();
            btnShowAll = new Button();
            dgvItems = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            btnRemove = new Button();
            btnCheckOut = new Button();
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox2 = new GroupBox();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            label4 = new Label();
            comboBox3 = new ComboBox();
            label5 = new Label();
            comboBox4 = new ComboBox();
            label6 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnShowAll);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 936);
            panel1.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(12, 854);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(435, 70);
            btnExit.TabIndex = 5;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Location = new Point(12, 88);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(435, 70);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(12, 12);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(435, 70);
            btnShowAll.TabIndex = 0;
            btnShowAll.Text = "Show All Items";
            btnShowAll.UseVisualStyleBackColor = true;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.BackgroundColor = SystemColors.Window;
            dgvItems.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7 });
            dgvItems.Location = new Point(456, 12);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersWidth = 72;
            dgvItems.Size = new Size(1330, 912);
            dgvItems.TabIndex = 1;
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
            // Column2
            // 
            Column2.DataPropertyName = "itemName";
            Column2.HeaderText = "Item Name";
            Column2.MinimumWidth = 9;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 175;
            // 
            // Column3
            // 
            Column3.DataPropertyName = "itemStock";
            Column3.HeaderText = "Stock";
            Column3.MinimumWidth = 9;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 175;
            // 
            // Column4
            // 
            Column4.DataPropertyName = "catName";
            Column4.HeaderText = "Category";
            Column4.MinimumWidth = 9;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 175;
            // 
            // Column5
            // 
            Column5.DataPropertyName = "subCatName";
            Column5.HeaderText = "Sub Category";
            Column5.MinimumWidth = 9;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 175;
            // 
            // Column6
            // 
            Column6.DataPropertyName = "brandName";
            Column6.HeaderText = "Brand";
            Column6.MinimumWidth = 9;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 175;
            // 
            // Column7
            // 
            Column7.DataPropertyName = "subBrandName";
            Column7.HeaderText = "Sub Brand";
            Column7.MinimumWidth = 9;
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 175;
            // 
            // btnRemove
            // 
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Location = new Point(1792, 12);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(330, 70);
            btnRemove.TabIndex = 6;
            btnRemove.Text = "Remove Item";
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnCheckOut
            // 
            btnCheckOut.FlatStyle = FlatStyle.Flat;
            btnCheckOut.Location = new Point(2234, 12);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(330, 70);
            btnCheckOut.TabIndex = 7;
            btnCheckOut.Text = "Check Out";
            btnCheckOut.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(1792, 88);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(772, 836);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Shopping Cart";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 72;
            dataGridView1.Size = new Size(760, 794);
            dataGridView1.TabIndex = 0;
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
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(12, 164);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(435, 684);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Search";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 38);
            label1.Name = "label1";
            label1.Size = new Size(124, 37);
            label1.TabIndex = 0;
            label1.Text = "by Name";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 78);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(423, 42);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(6, 163);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(423, 42);
            textBox2.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 123);
            label2.Name = "label2";
            label2.Size = new Size(149, 37);
            label2.TabIndex = 2;
            label2.Text = "by Barcode";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 208);
            label3.Name = "label3";
            label3.Size = new Size(122, 37);
            label3.TabIndex = 4;
            label3.Text = "by Brand";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 248);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(423, 44);
            comboBox1.TabIndex = 5;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(6, 335);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(423, 44);
            comboBox2.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 295);
            label4.Name = "label4";
            label4.Size = new Size(178, 37);
            label4.TabIndex = 6;
            label4.Text = "by Sub-Brand";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(6, 509);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(423, 44);
            comboBox3.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 469);
            label5.Name = "label5";
            label5.Size = new Size(217, 37);
            label5.TabIndex = 10;
            label5.Text = "by Sub-Category";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(6, 422);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(423, 44);
            comboBox4.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 382);
            label6.Name = "label6";
            label6.Size = new Size(161, 37);
            label6.TabIndex = 8;
            label6.Text = "by Category";
            // 
            // frmSale
            // 
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(2576, 936);
            Controls.Add(groupBox1);
            Controls.Add(btnCheckOut);
            Controls.Add(btnRemove);
            Controls.Add(dgvItems);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 11.1428576F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "frmSale";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sale";
            Load += frmSale_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgvItems;
        private Button btnShowAll;
        private Button btnExit;
        private Button btnSearch;
        private Button btnRemove;
        private Button btnCheckOut;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private GroupBox groupBox2;
        private ComboBox comboBox3;
        private Label label5;
        private ComboBox comboBox4;
        private Label label6;
        private ComboBox comboBox2;
        private Label label4;
        private ComboBox comboBox1;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox1;
        private Label label1;
    }
}
