using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace posSystemWindows
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            // Replace with your image resource name
            //pictureBox1.BackgroundImage = Properties.Resources.Venom_Hub;
            //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch; // Adjust layout as needed

            // Set the about text
            lblAbout.Text = "POS System is a comprehensive Point of Sale (POS) application designed to streamline your sales, inventory, and staff management processes. Our software is built to support a wide range of business operations, ensuring efficiency and ease of use for your daily tasks.\n\n" +
                            "Key Features:\n" +
                            "- Sales Management: Efficiently handle transactions, track sales, and generate reports.\n" +
                            "- Inventory Management: Keep track of stock levels, manage product information, and automate reorder processes.\n" +
                            "- Staff Management: Manage staff details, roles, and monitor login sessions.\n" +
                            "- Secure Transactions: Ensure the security of your transactions with robust encryption and secure login features.\n\n" +
                            "Our mission is to provide a reliable and user-friendly POS solution that meets the needs of businesses of all sizes.\n\n" +
                            "Version: 1.0\n\n" +
                            "For support and inquiries, please contact:\n" +
                            "Email: support@venomhub.com\n" +
                            "Phone: +1 (555) 123-4567\n" +
                            "Website: www.venomhub.com\n\n" +
                            "© 2024 Venom Hub. All rights reserved.\n\n" +
                            "Developed by Venom Hub\n\n" +
                            "Venom Hub is a leading software development company dedicated to delivering high-quality software solutions. Our team of experienced developers is committed to creating innovative and efficient applications that help businesses thrive in the digital age.\n\n" +
                            "Thank you for choosing our POS System. We appreciate your support and trust in our product.";
        }
    }
}
