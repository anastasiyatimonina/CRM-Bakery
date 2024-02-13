using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class Account : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        string Role;
        Form ff;
        public Account(Form f)
        {
            InitializeComponent();
            SetRoundedShape(panel2, 30);
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
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            ChangePassword acc = new ChangePassword(ff);
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Account_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = (this.Width - pictureBox1.Width) / 2;
            button1.Left = (this.Width - button1.Width) / 2;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT Role, Login, Password FROM Account WHERE UserId = {DataSend.UserId}";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            database.openConnection();
            Role = command.ExecuteScalar().ToString();
            Role = Role.Replace(" ", "");
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string login = Convert.ToString(reader.GetValue(1));
            string password = Convert.ToString(reader.GetValue(2));
            reader.Close();
            if (Role == "Менеджер")
            {
                label4.Text = login;
                string queryString2 = $"SELECT Surname, Name, Patronymic, Position FROM Employee WHERE UserId = {DataSend.UserId}";
                SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                SqlDataReader reader2 = command2.ExecuteReader();
                reader2.Read();
                string surname = Convert.ToString(reader2.GetValue(0));
                string name = Convert.ToString(reader2.GetValue(1));
                string patronymic = Convert.ToString(reader2.GetValue(2));
                string position = Convert.ToString(reader2.GetValue(3));
                label1.Text = surname + " " + name + " " + patronymic;
                label1.Left = (this.Width - label1.Width) / 2;
                label6.Text = position;
                label6.Left = (this.Width - label6.Width) / 2;
                reader2.Close();
            }
            else
            {
                if (Role == "Клиент")
                {
                    label4.Text = login;
                    string queryString2 = $"SELECT Surname, Name, Patronymic, PhoneNumber, Email, Address FROM Client WHERE UserId = {DataSend.UserId}";
                    SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                    SqlDataReader reader2 = command2.ExecuteReader();
                    reader2.Read();
                    string surname = Convert.ToString(reader2.GetValue(0));
                    string name = Convert.ToString(reader2.GetValue(1));
                    string patronymic = Convert.ToString(reader2.GetValue(2));
                    string phonenumber = Convert.ToString(reader2.GetValue(3));
                    string email = Convert.ToString(reader2.GetValue(4));
                    string address = Convert.ToString(reader2.GetValue(5));
                    label1.Text = surname + " " + name + " " + patronymic;
                    label1.Left = (this.Width - label1.Width) / 2;
                    label6.Text = phonenumber+"\n"+email+"\n"+address;
                    label6.Left = (this.Width - label6.Width) / 2;
                    reader2.Close();

                }
                else if (Role == "Администратор")
                {
                    label4.Text = login;
                    string queryString2 = $"SELECT Surname, Name, Patronymic, Position FROM Employee WHERE UserId = {DataSend.UserId}";
                    SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                    SqlDataReader reader2 = command2.ExecuteReader();
                    reader2.Read();
                    string surname = Convert.ToString(reader2.GetValue(0));
                    string name = Convert.ToString(reader2.GetValue(1));
                    string patronymic = Convert.ToString(reader2.GetValue(2));
                    string position = Convert.ToString(reader2.GetValue(3));
                    label1.Text = surname + " " + name + " " + patronymic;
                    label1.Left = (this.Width - label1.Width) / 2;
                    label6.Text = position;
                    label6.Left = (this.Width - label6.Width) / 2;
                    reader2.Close();
                }
            }
            string s = label6.Text;
            int n = s.Count(c => (c == '\n'));
            panel2.Top = label6.Top + (n+1) * 20 + 20;
            label2.Top = panel2.Top + 5;
            button1.Top = panel2.Top + 70;
            button2.Top = button1.Top + 70;
            database.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ff.Close();
        }
    }
}
