using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class Autorization : Form
    {
        DataBase database = new DataBase();
        public Autorization()
        {
            InitializeComponent();
            SetRoundedShape(panel2, 30);
            SetRoundedShape(panel3, 30);
            //SetRoundedShape(panel4, 10);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Close f = new Close();
            f.ShowDialog();
            this.Show();
        }
        
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Gray;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }
        Point lastPoint;
        private void Autorization_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Autorization_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Role;
            var login = textBox1.Text;
            var pass = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT Role, UserId, Login, Password FROM Account WHERE Login = '{login}' and Password = '{pass}'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            database.openConnection();
            if (table.Rows.Count == 1)
            {
                Role = command.ExecuteScalar().ToString();
                Role = Role.Replace(" ", "");
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                DataSend.UserId = Convert.ToInt32(reader.GetValue(1));
                if (Role == "Менеджер")
                {
                    Manager f = new Manager(Role);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else if (Role == "Клиент")
                {
                    Client f = new Client();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else if (Role == "Администратор")
                {
                    Manager f = new Manager(Role);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
            else
            {
                panel4.Visible = true;
                label3.Visible = true;
            }
            database.closeConnection();
        }

        private void Autorization_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 25;
            textBox1.MaxLength = 16;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
