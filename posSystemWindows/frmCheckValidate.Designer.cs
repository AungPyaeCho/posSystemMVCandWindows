namespace posSystemWindows
{
    partial class frmCheckValidate
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
            btnExit = new Button();
            btnLogin = new Button();
            txtPassword = new TextBox();
            label2 = new Label();
            txtUserName = new TextBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnExit);
            groupBox1.Controls.Add(btnLogin);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtUserName);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(404, 247);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(168, 189);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(217, 52);
            btnExit.TabIndex = 5;
            btnExit.Text = "&Cancle";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(169, 131);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(217, 52);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "&Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(169, 78);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(216, 38);
            txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 81);
            label2.Name = "label2";
            label2.Size = new Size(123, 32);
            label2.TabIndex = 2;
            label2.Text = "Password :";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(169, 32);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(216, 38);
            txtUserName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 35);
            label1.Name = "label1";
            label1.Size = new Size(167, 32);
            label1.TabIndex = 0;
            label1.Text = "Admin Name :";
            // 
            // frmCheckValidate
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 272);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmCheckValidate";
            StartPosition = FormStartPosition.CenterScreen;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnExit;
        private Button btnLogin;
        private TextBox txtPassword;
        private Label label2;
        private TextBox txtUserName;
        private Label label1;
    }
}