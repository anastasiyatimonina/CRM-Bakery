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
    public partial class Manager : Form
    {
        public string role;
        Point lastPoint;
        public Manager(string r)
        {
            InitializeComponent();
            role = r;
            this.button3.Image = (Image)(new Bitmap(CRM.Properties.Resources.user2, new Size(30, 30)));
        }
        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
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
            button4.BackColor = Color.RoyalBlue;
            button5.BackColor = Color.RoyalBlue;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            NewRequests acc = new NewRequests();
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
            button4.BackColor = Color.RoyalBlue;
            button5.BackColor = Color.RoyalBlue;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            AllRequests acc = new AllRequests();
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.RoyalBlue;
            button4.BackColor = Color.FromArgb(58, 94, 202);
            button5.BackColor = Color.RoyalBlue;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            Clients acc = new Clients(role);
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

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.FromArgb(58, 94, 202);
            button4.BackColor = Color.RoyalBlue;
            button5.BackColor = Color.RoyalBlue;
            if (this.mainpanel.Controls.Count > 0) 
            { 
                for (int i = this.mainpanel.Controls.Count-1;i>=0;--i)
                    this.mainpanel.Controls.RemoveAt(i);
            }
            Account acc = new Account(this);
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            if (role == "Менеджер")
            {
                button1.BackColor = Color.FromArgb(58, 94, 202);
                button2.BackColor = Color.RoyalBlue;
                button3.BackColor = Color.RoyalBlue;
                button4.BackColor = Color.RoyalBlue;
                //button5.Visible = false;
                if (this.mainpanel.Controls.Count > 0)
                    for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                        this.mainpanel.Controls.RemoveAt(i);
                NewRequests acc = new NewRequests();
                acc.mainpanel = this.mainpanel;
                acc.TopLevel = false;
                acc.Dock = DockStyle.Fill;
                this.mainpanel.Controls.Add(acc);
                this.mainpanel.Tag = acc;
                acc.Show();
            }
            else if (role == "Администратор")
            {
                button1.BackColor = Color.FromArgb(58, 94, 202);
                button2.BackColor = Color.RoyalBlue;
                button3.BackColor = Color.RoyalBlue;
                button4.BackColor = Color.RoyalBlue;
                button5.BackColor = Color.RoyalBlue;
                if (this.mainpanel.Controls.Count > 0)
                    for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                        this.mainpanel.Controls.RemoveAt(i);
                NewRequests acc = new NewRequests();
                acc.mainpanel = this.mainpanel;
                acc.TopLevel = false;
                acc.Dock = DockStyle.Fill;
                this.mainpanel.Controls.Add(acc);
                this.mainpanel.Tag = acc;
                acc.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.RoyalBlue;
            button4.BackColor = Color.RoyalBlue;
            button5.BackColor = Color.FromArgb(58, 94, 202);
            if (this.mainpanel.Controls.Count > 0)
            {
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            }
            ReportProduction acc = new ReportProduction();
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
