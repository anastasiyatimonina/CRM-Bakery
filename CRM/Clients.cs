using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class Clients : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        public string role;
        int value = 0;
        public Clients(string r)
        {
            InitializeComponent();
            role = r;
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                dataGridViewClient.Visible = true;
                if (role == "Администратор")
                {
                    button1.Visible = true;
                    dataGridViewClient.Height = dataGridViewClient.Height - 55;
                }
                dataGridViewProduct.Visible = false;
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Поиск по фамилии, имени или номеру телефона";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0 && textBox1.Text != "Поиск по фамилии, имени или номеру телефона")
            {
                dataGridViewClient.Rows.Clear();
                string queryString = $"SELECT ClientId, Surname, Name, Patronymic, PhoneNumber, Email, Address FROM Client WHERE Surname LIKE '%{textBox1.Text}%' OR Name LIKE '%{textBox1.Text}%' OR PhoneNumber LIKE '%{textBox1.Text}%' ORDER BY Surname";
                    SqlCommand command = new SqlCommand(queryString, database.getConnection());
                    database.openConnection();
                    SqlDataReader reader = command.ExecuteReader();

                    List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        data.Add(new string[5]);

                        data[data.Count - 1][0] = reader[0].ToString();
                        data[data.Count - 1][1] = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                        data[data.Count - 1][2] = reader[4].ToString();
                        data[data.Count - 1][3] = reader[5].ToString();
                        data[data.Count - 1][4] = reader[6].ToString();
                    }
                    reader.Close();

                    database.closeConnection();

                    foreach (string[] s in data)
                        dataGridViewClient.Rows.Add(s);
                }
            else if (index ==1 && textBox1.Text != "Поиск по названию продукции")
            {
                dataGridViewProduct.Rows.Clear();
                string queryString = $"SELECT ProductId, Name, Weight, ShelfLife, Description, Price FROM Product WHERE Hided = 0 AND Name LIKE '%{textBox1.Text}%' ORDER BY Name";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[6]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                    data[data.Count - 1][5] = reader[5].ToString();
                }
                reader.Close();

                database.closeConnection();

                foreach (string[] s in data)
                    dataGridViewProduct.Rows.Add(s);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                if (role == "Администратор")
                {
                    button1.Visible = true;
                }
                dataGridViewClient.Rows.Clear();
                dataGridViewClient.Visible = true;
                dataGridViewProduct.Visible = false;
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Поиск по фамилии, имени или номеру телефона";
                string queryString = $"SELECT ClientId, Surname, Name, Patronymic, PhoneNumber, Email, Address FROM Client ORDER BY Surname ";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[5]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                    data[data.Count - 1][2] = reader[4].ToString();
                    data[data.Count - 1][3] = reader[5].ToString();
                    data[data.Count - 1][4] = reader[6].ToString();
                }
                reader.Close();

                database.closeConnection();

                foreach (string[] s in data)
                    dataGridViewClient.Rows.Add(s);
            }
            else if (index == 1)
            {
                button1.Visible = false;
                dataGridViewProduct.Rows.Clear();
                dataGridViewClient.Visible = false;
                dataGridViewProduct.Visible = true;
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Поиск по названию продукции";

                string queryString = $"SELECT ProductId, Name, Weight, ShelfLife, Description, Price FROM Product WHERE Hided = 0 ORDER BY Name";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[6]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                    data[data.Count - 1][5] = reader[5].ToString();
                }
                reader.Close();

                database.closeConnection();

                foreach (string[] s in data)
                    dataGridViewProduct.Rows.Add(s);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Поиск по фамилии, имени или номеру телефона";
            }
            else if (index == 1)
            {
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Поиск по названию продукции";
            }
        }

        private void dataGridViewProduct_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewProduct.SelectedRows)
            {
                value = dataGridViewProduct.SelectedCells[6].RowIndex;
            }
            dataGridViewProduct.ClearSelection();
        }

        private void dataGridViewClient_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewClient.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.mainpanel.Controls.Count > 0)
            {
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            }
            Registration acc = new Registration();
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void dataGridViewProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProduct.CurrentCell.ColumnIndex == 6)
            {
                DataGridViewImageCell cell = null;

                string queryString = $"UPDATE Product SET Hided = 1 WHERE ProductId = {Convert.ToInt32(dataGridViewProduct[0,value].Value)}";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                command.ExecuteNonQuery();
                database.closeConnection();
                dataGridViewProduct.Rows.RemoveAt(value);
                int sum = 0;
                for (int i = 0; i < dataGridViewProduct.Rows.Count; i++)
                    sum += Convert.ToInt32(dataGridViewProduct[5, i].Value) * Convert.ToInt32(dataGridViewProduct[6, i].Value);
            }
        }
    }
    
}
