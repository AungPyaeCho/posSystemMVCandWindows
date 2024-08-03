namespace posSystemWindows
{
    partial class frmMain
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
            lblUserName = new Label();
            lblStaffCode = new Label();
            lblStaffRole = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            btnSetting = new Button();
            btnAbout = new Button();
            btnExit = new Button();
            btnSale = new Button();
            groupBox1 = new GroupBox();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 9F);
            lblUserName.Location = new Point(6, 38);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(132, 30);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "lblUserName";
            // 
            // lblStaffCode
            // 
            lblStaffCode.AutoSize = true;
            lblStaffCode.Font = new Font("Segoe UI", 9F);
            lblStaffCode.Location = new Point(6, 98);
            lblStaffCode.Name = "lblStaffCode";
            lblStaffCode.Size = new Size(125, 30);
            lblStaffCode.TabIndex = 2;
            lblStaffCode.Text = "lblStaffCode";
            // 
            // lblStaffRole
            // 
            lblStaffRole.AutoSize = true;
            lblStaffRole.Font = new Font("Segoe UI", 9F);
            lblStaffRole.Location = new Point(6, 68);
            lblStaffRole.Name = "lblStaffRole";
            lblStaffRole.Size = new Size(117, 30);
            lblStaffRole.TabIndex = 3;
            lblStaffRole.Text = "lblStaffRole";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(876, 63);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(btnSetting);
            panel2.Controls.Add(btnAbout);
            panel2.Controls.Add(btnExit);
            panel2.Controls.Add(btnSale);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 63);
            panel2.Name = "panel2";
            panel2.Size = new Size(233, 473);
            panel2.TabIndex = 5;
            // 
            // btnSetting
            // 
            btnSetting.FlatStyle = FlatStyle.Flat;
            btnSetting.Location = new Point(10, 297);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(200, 50);
            btnSetting.TabIndex = 3;
            btnSetting.Text = "Setting";
            btnSetting.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            btnAbout.FlatStyle = FlatStyle.Flat;
            btnAbout.Location = new Point(10, 353);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(200, 50);
            btnAbout.TabIndex = 2;
            btnAbout.Text = "About ";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // btnExit
            // 
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(10, 409);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(200, 50);
            btnExit.TabIndex = 1;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnSale
            // 
            btnSale.FlatStyle = FlatStyle.Flat;
            btnSale.Location = new Point(10, 4);
            btnSale.Name = "btnSale";
            btnSale.Size = new Size(200, 50);
            btnSale.TabIndex = 0;
            btnSale.Text = "&Sale";
            btnSale.UseVisualStyleBackColor = true;
            btnSale.Click += btnSale_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblUserName);
            groupBox1.Controls.Add(lblStaffCode);
            groupBox1.Controls.Add(lblStaffRole);
            groupBox1.Dock = DockStyle.Right;
            groupBox1.Location = new Point(677, 63);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(199, 473);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Login User";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(876, 536);
            Controls.Add(groupBox1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 11.1428576F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMain";
            Load += frmMain_Load;
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblUserName;
        private Label lblStaffCode;
        private Label lblStaffRole;
        private Panel panel1;
        private Panel panel2;
        private Button btnSale;
        private Button btnExit;
        private Button btnAbout;
        private GroupBox groupBox1;
        private Button btnSetting;
    }
}