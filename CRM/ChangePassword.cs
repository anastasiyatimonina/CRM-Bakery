using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class ChangePassword : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        Form ff;
        public ChangePassword(Form f)
        {
            InitializeComponent();
            SetRoundedShape(panel2, 30);
            SetRoundedShape(panel3, 30);
            SetRoundedShape(panel1, 30);
            ff = f;
        }
        static void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT Password FROM Account WHERE UserId = {DataSend.UserId}";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            database.openConnection();
            string pass = command.ExecuteScalar().ToString();
            pass = pass.Replace(" ", "");
            if (textBox1.Text != pass)
            {
                label5.Text = "Старый пароль введен неверно!";
                panel4.Visible = true;
                label5.Visible = true;
            }
            else
            {
                if (textBox2.Text != textBox3.Text)
                {
                    label5.Text = "Новый пароль повторно введен неверно!";
                    panel4.Visible = true;
                    label5.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                    label5.Visible = false;
                    string queryString2 = $"UPDATE Account SET Password = '{textBox2.Text}' WHERE UserId = {DataSend.UserId}";
                    SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                    command2.ExecuteNonQuery();
                    if (this.mainpanel.Controls.Count > 0)
                        for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                            this.mainpanel.Controls.RemoveAt(i);
                    Account acc = new Account(ff);
                    acc.mainpanel = this.mainpanel;
                    acc.TopLevel = false;
                    acc.Dock = DockStyle.Fill;
                    this.mainpanel.Controls.Add(acc);
                    this.mainpanel.Tag = acc;
                    acc.Show();
                }
            }
            database.closeConnection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Account acc = new Account(ff);
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
