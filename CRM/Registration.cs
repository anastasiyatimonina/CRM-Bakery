using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class Registration : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        public Registration()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]|[ ]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                label13.Visible = false;
                textBox4.Visible = false;
                label8.Visible = true;
                textBox5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                panel6.Visible = true;
                panel7.Visible = true;
                textBox6.Visible = true;
                maskedTextBox1.Visible = true;
            }
            else if (index == 1)
            {
                panel6.Visible = false;
                panel7.Visible = false;
                textBox6.Visible = false;
                maskedTextBox1.Visible = false;
                label8.Visible = false;
                textBox5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label13.Visible = true;
                textBox4.Visible = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]|[ ]|[0-9]|[.]|[,]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            /*if (!Regex.Match(Symbol, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }*/
        }
        private void textbox6_LostFocus(object sender, EventArgs e)
        {
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            string email = textBox6.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                panel9.Visible = true;
                label14.Visible = true;
            }
            else
            {
                panel9.Visible = false;
                label14.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            bool b = true;
            if (index == 0)
            {
                string email = textBox6.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (!match.Success)
                {
                    panel9.Visible = true;
                    label14.Visible = true;
                    b = false;
                }
                else
                {
                    panel9.Visible = false;
                    label14.Visible = false;
                }
                if (!maskedTextBox1.MaskCompleted)
                {
                    b = false;
                    panel12.Visible = true;
                    label19.Visible = true;
                }
                else
                {
                    panel12.Visible = false;
                    label19.Visible = false;
                }
                if (textBox1.Text == "")
                {
                    panel13.Visible = true;
                    label20.Visible = true;
                    b = false;
                }
                else
                {
                    panel13.Visible = false;
                    label20.Visible = false;
                }
                if (textBox2.Text == "")
                {
                    panel14.Visible = true;
                    label21.Visible = true;
                    b = false;
                }
                else
                {
                    panel14.Visible = false;
                    label21.Visible = false;
                }
                if (textBox5.Text == "")
                {
                    panel16.Visible = true;
                    label23.Visible = true;
                    b = false;
                }
                else
                {
                    panel16.Visible = false;
                    label23.Visible = false;
                }
                if (textBox7.Text == "")
                {
                    panel17.Visible = true;
                    label24.Visible = true;
                    b = false;
                }
                else
                {
                    panel17.Visible = false;
                    label24.Visible = false;
                }
                if (textBox8.Text == "")
                {
                    panel18.Visible = true;
                    label25.Visible = true;
                    b = false;
                }
                else
                {
                    panel18.Visible = false;
                    label25.Visible = false;
                }
            }
            else if (index == 1)
            {
                if (textBox1.Text == "")
                {
                    panel13.Visible = true;
                    label20.Visible = true;
                    b = false;
                }
                else
                {
                    panel13.Visible = false;
                    label20.Visible = false;
                }
                if (textBox2.Text == "")
                {
                    panel14.Visible = true;
                    label21.Visible = true;
                    b = false;
                }
                else
                {
                    panel14.Visible = false;
                    label21.Visible = false;
                }
                if (textBox4.Text == "")
                {
                    panel16.Visible = true;
                    label23.Visible = true;
                    b = false;
                }
                else
                {
                    panel16.Visible = false;
                    label23.Visible = false;
                }
                if (textBox7.Text == "")
                {
                    panel17.Visible = true;
                    label24.Visible = true;
                    b = false;
                }
                else
                {
                    panel17.Visible = false;
                    label24.Visible = false;
                }
                if (textBox8.Text == "")
                {
                    panel18.Visible = true;
                    label25.Visible = true;
                    b = false;
                }
                else
                {
                    panel18.Visible = false;
                    label25.Visible = false;
                }
            }
            if (b)
            {
                string queryString2 = $"INSERT INTO Account (Login, Password, Role) VALUES ('{textBox7.Text}', '{textBox8.Text}','{comboBox1.Text}')";
                string queryString3 = $"SELECT SCOPE_IDENTITY()";
                SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
                database.openConnection();
                command2.ExecuteNonQuery();
                int userid = Convert.ToInt32(command3.ExecuteScalar());
                string queryString;
                if (comboBox1.SelectedIndex == 0)
                {
                    string phone = maskedTextBox1.Text;
                    phone = phone.Replace(" ", "");
                    phone = phone.Replace("-", "");
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace("+7", "8");
                    if (textBox3.Text == "")
                    {
                        queryString = $"INSERT INTO Client (UserId, Surname, Name, PhoneNumber, Email, Address) VALUES ({userid}, '{textBox1.Text}','{textBox2.Text}', '{phone}','{textBox6.Text}', '{textBox5.Text}')";
                    }
                    else
                    {
                        queryString = $"INSERT INTO Client (UserId, Surname, Name, Patronymic, PhoneNumber, Email, Address) VALUES ({userid}, '{textBox1.Text}','{textBox2.Text}', '{textBox3.Text}', '{phone}','{textBox6.Text}', '{textBox5.Text}')";
                    }
                    SqlCommand command = new SqlCommand(queryString, database.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    if (textBox3.Text == "")
                    {
                        queryString = $"INSERT INTO Employee (UserId, Surname, Name, Position) VALUES ({userid}, '{textBox1.Text}','{textBox2.Text}', '{textBox4.Text}')";
                    }
                    else
                    {
                        queryString = $"INSERT INTO Employee (UserId, Surname, Name, Patronymic, Position) VALUES ({userid}, '{textBox1.Text}','{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}')";
                    }
                    SqlCommand command = new SqlCommand(queryString, database.getConnection());
                    command.ExecuteNonQuery();
                }
                database.closeConnection();
                MessageBox.Show("Аккаунт зарегистрирован.");
                this.Close();
                string r = "Администратор";
                Clients f = new Clients(r);
                f.mainpanel = this.mainpanel;
                f.TopLevel = false;
                f.Dock = DockStyle.Fill;
                this.mainpanel.Controls.Add(f);
                this.mainpanel.Tag = f;
                f.Show();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[a-zA-Z]|[0-9]|[_]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[a-zA-Z]|[0-9]").Success)
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            AllRequests r = new AllRequests();
            r.mainpanel = this.mainpanel;
            r.TopLevel = false;
            r.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(r);
            this.mainpanel.Tag = r;
            r.Show();
        }
    }
}
