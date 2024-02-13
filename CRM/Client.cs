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
    public partial class Client : Form
    {
        Point lastPoint;
        public Client()
        {
            InitializeComponent();
            this.button3.Image = (Image)(new Bitmap(CRM.Properties.Resources.user2, new Size(30, 30)));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Close f = new Close();
            f.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(58, 94, 202);
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.RoyalBlue;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            Requests acc = new Requests();
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.FromArgb(58, 94, 202);
            button3.BackColor = Color.RoyalBlue;
            int r = 1;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            CreateRequest acc = new CreateRequest();
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.r = r;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.FromArgb(58, 94, 202);
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            Account acc = new Account(this);
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Gray;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void Client_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(58, 94, 202);
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.RoyalBlue;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            Requests acc = new Requests();
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
