using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class ClientDismiss : Form
    {
        public Panel mainpanel;
        public string value;
        DataBase database = new DataBase();
        public ClientDismiss()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            value = textBox1.Text;
            this.Close();
        }

        private void ClientDismiss_Load(object sender, EventArgs e)
        {
            textBox1.Text = value;
        }
    }
}
