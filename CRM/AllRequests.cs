using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class AllRequests : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        public AllRequests()
        {
            InitializeComponent();
        }

        private void AllRequests_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            int index = comboBox1.SelectedIndex;
            dataGridViewRequest.Rows.Clear();
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "Поиск по номеру заявки или фамилии клиента";
            string queryString ="";
            if (index == 0)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId ORDER BY OrderDate DESC";
            else if (index == 1)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Новая' ORDER BY OrderDate DESC";
            else if (index == 2)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'В работе' ORDER BY OrderDate DESC";
            else if (index == 3)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Завершена' ORDER BY OrderDate DESC";
            else if (index == 4)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Отмена' ORDER BY OrderDate DESC";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = Convert.ToDateTime(reader[2]).ToShortDateString().ToString();
                data[data.Count - 1][3] = Convert.ToDateTime(reader[3]).ToShortDateString().ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();

            database.closeConnection();

            foreach (string[] s in data)
                dataGridViewRequest.Rows.Add(s);

            for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
            {
                if (dataGridViewRequest[4, i].Value.ToString() == "Новая")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Green;
                }
                if (dataGridViewRequest[4, i].Value.ToString() == "Отмена")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Gray;
                }
                if (dataGridViewRequest[4, i].Value.ToString() == "Завершена")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Orange;
                }
                if (dataGridViewRequest[4, i].Value.ToString() == "В работе")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Blue;
                }
            }
        }

        private void dataGridViewRequest_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            RequestsInfo acc = new RequestsInfo();
            acc.mainpanel = this.mainpanel;
            acc.dataGridView1 = this.dataGridViewRequest;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "Поиск по номеру заявки или фамилии клиента";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (textBox1.Text != "Поиск по номеру заявки или фамилии клиента") { 
                dataGridViewRequest.Rows.Clear();
                string queryString = "";
                if (index == 0)
                    queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE Surname LIKE '%{textBox1.Text}%' OR CAST(RequestId AS VARCHAR(20)) LIKE '%{textBox1.Text}%' ORDER BY OrderDate DESC";
                else if (index == 1)
                    queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Новая' AND (Surname LIKE '%{textBox1.Text}%' OR CAST(RequestId AS VARCHAR(20)) LIKE '%{textBox1.Text}%') ORDER BY OrderDate DESC";
                else if (index == 2)
                    queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'В работе' AND (Surname LIKE '%{textBox1.Text}%' OR CAST(RequestId AS VARCHAR(20)) LIKE '%{textBox1.Text}%') ORDER BY OrderDate DESC";
                else if (index == 3)
                    queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Завершена' AND (Surname LIKE '%{textBox1.Text}%' OR CAST(RequestId AS VARCHAR(20)) LIKE '%{textBox1.Text}%') ORDER BY OrderDate DESC";
                else if (index == 4)
                    queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Отмена' AND (Surname LIKE '%{textBox1.Text}%' OR CAST(RequestId AS VARCHAR(20)) LIKE '%{textBox1.Text}%') ORDER BY OrderDate DESC";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[6]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = Convert.ToDateTime(reader[2]).ToShortDateString().ToString();
                data[data.Count - 1][3] = Convert.ToDateTime(reader[3]).ToShortDateString().ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();

            database.closeConnection();

            foreach (string[] s in data)
                dataGridViewRequest.Rows.Add(s);

                for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
                {
                    if (dataGridViewRequest[4, i].Value.ToString() == "Новая")
                    {
                        dataGridViewRequest[4, i].Style.ForeColor = Color.Green;
                    }
                    if (dataGridViewRequest[4, i].Value.ToString() == "Отмена")
                    {
                        dataGridViewRequest[4, i].Style.ForeColor = Color.Gray;
                    }
                    if (dataGridViewRequest[4, i].Value.ToString() == "Завершена")
                    {
                        dataGridViewRequest[4, i].Style.ForeColor = Color.Orange;
                    }
                    if (dataGridViewRequest[4, i].Value.ToString() == "В работе")
                    {
                        dataGridViewRequest[4, i].Style.ForeColor = Color.Blue;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            dataGridViewRequest.Rows.Clear();
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "Поиск по номеру заявки или фамилии клиента";
            string queryString = "";
            if (index == 0)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId ORDER BY OrderDate DESC";
            else if (index == 1)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Новая' ORDER BY OrderDate DESC";
            else if (index == 2)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'В работе' ORDER BY OrderDate DESC";
            else if (index == 3)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Завершена' ORDER BY OrderDate DESC";
            else if (index == 4)
                queryString = $"SELECT RequestId, Surname, OrderDate, DeliveryDate, State, Sum FROM Request INNER JOIN Client ON Client.ClientId = Request.ClientId WHERE State = 'Отмена' ORDER BY OrderDate DESC";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = Convert.ToDateTime(reader[2]).ToShortDateString().ToString();
                data[data.Count - 1][3] = Convert.ToDateTime(reader[3]).ToShortDateString().ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();

            database.closeConnection();

            foreach (string[] s in data)
                dataGridViewRequest.Rows.Add(s);

            for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
            {
                if (dataGridViewRequest[4, i].Value.ToString() == "Новая")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Green;
                }
                if (dataGridViewRequest[4, i].Value.ToString() == "Отмена")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Gray;
                }
                if (dataGridViewRequest[4, i].Value.ToString() == "Завершена")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Orange;
                }
                if (dataGridViewRequest[4, i].Value.ToString() == "В работе")
                {
                    dataGridViewRequest[4, i].Style.ForeColor = Color.Blue;
                }
            }

        }
    }
}
