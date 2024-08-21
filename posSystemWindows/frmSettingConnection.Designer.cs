namespace posSystemWindows
{
    partial class frmSettingConnection
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
            label7 = new Label();
            btnCancle = new Button();
            btnChange = new Button();
            txtAdminPassword = new TextBox();
            label6 = new Label();
            txtAdminName = new TextBox();
            label5 = new Label();
            txtServerPassword = new TextBox();
            label4 = new Label();
            txtUserName = new TextBox();
            label3 = new Label();
            ttxtDatabaseName = new TextBox();
            label2 = new Label();
            txtServerName = new TextBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnCancle);
            groupBox1.Controls.Add(btnChange);
            groupBox1.Controls.Add(txtAdminPassword);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtAdminName);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtServerPassword);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtUserName);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(ttxtDatabaseName);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtServerName);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(597, 473);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // label7
            // 
            label7.Location = new Point(6, 237);
            label7.Name = "label7";
            label7.Size = new Size(577, 68);
            label7.TabIndex = 14;
            label7.Text = "Enter Admin Name && Password to make change";
            // 
            // btnCancle
            // 
            btnCancle.FlatStyle = FlatStyle.Flat;
            btnCancle.Location = new Point(6, 400);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new Size(221, 56);
            btnCancle.TabIndex = 13;
            btnCancle.Text = "Cancle";
            btnCancle.UseVisualStyleBackColor = true;
            btnCancle.Click += btnCancle_Click;
            // 
            // btnChange
            // 
            btnChange.FlatStyle = FlatStyle.Flat;
            btnChange.Location = new Point(362, 400);
            btnChange.Name = "btnChange";
            btnChange.Size = new Size(221, 56);
            btnChange.TabIndex = 12;
            btnChange.Text = "Change";
            btnChange.UseVisualStyleBackColor = true;
            btnChange.Click += btnChange_Click;
            // 
            // txtAdminPassword
            // 
            txtAdminPassword.Location = new Point(233, 356);
            txtAdminPassword.Name = "txtAdminPassword";
            txtAdminPassword.PasswordChar = '*';
            txtAdminPassword.Size = new Size(350, 38);
            txtAdminPassword.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 359);
            label6.Name = "label6";
            label6.Size = new Size(200, 32);
            label6.TabIndex = 10;
            label6.Text = "Admin Password :";
            // 
            // txtAdminName
            // 
            txtAdminName.Location = new Point(233, 308);
            txtAdminName.Name = "txtAdminName";
            txtAdminName.Size = new Size(350, 38);
            txtAdminName.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 311);
            label5.Name = "label5";
            label5.Size = new Size(167, 32);
            label5.TabIndex = 8;
            label5.Text = "Admin Name :";
            // 
            // txtServerPassword
            // 
            txtServerPassword.Location = new Point(233, 185);
            txtServerPassword.Name = "txtServerPassword";
            txtServerPassword.PasswordChar = '*';
            txtServerPassword.Size = new Size(350, 38);
            txtServerPassword.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 188);
            label4.Name = "label4";
            label4.Size = new Size(197, 32);
            label4.TabIndex = 6;
            label4.Text = "Server Password :";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(233, 137);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(350, 38);
            txtUserName.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 140);
            label3.Name = "label3";
            label3.Size = new Size(144, 32);
            label3.TabIndex = 4;
            label3.Text = "User Name :";
            // 
            // ttxtDatabaseName
            // 
            ttxtDatabaseName.Location = new Point(233, 89);
            ttxtDatabaseName.Name = "ttxtDatabaseName";
            ttxtDatabaseName.Size = new Size(350, 38);
            ttxtDatabaseName.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 92);
            label2.Name = "label2";
            label2.Size = new Size(124, 32);
            label2.TabIndex = 2;
            label2.Text = "Database :";
            // 
            // txtServerName
            // 
            txtServerName.Location = new Point(233, 44);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(350, 38);
            txtServerName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 44);
            label1.Name = "label1";
            label1.Size = new Size(164, 32);
            label1.TabIndex = 0;
            label1.Text = "Server Name :";
            // 
            // frmSettingConnection
            // 
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 499);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 11.1428576F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "frmSettingConnection";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connection String";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtServerPassword;
        private Label label4;
        private TextBox txtUserName;
        private Label label3;
        private TextBox ttxtDatabaseName;
        private Label label2;
        private TextBox txtServerName;
        private Label label1;
        private TextBox txtAdminPassword;
        private Label label6;
        private TextBox txtAdminName;
        private Label label5;
        private Button btnChange;
        private Label label7;
        private Button btnCancle;
    }
}