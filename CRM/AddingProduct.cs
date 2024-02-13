using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CRM
{
    public partial class AddingProduct : Form
    {
        DataBase database = new DataBase();
        int value = 0;
        public Panel mainpanel;
        public DataGridView dataGridViewRequest;
        public Label lab;
        public AddingProduct()
        {
            InitializeComponent();
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "Поиск по названию продукции";

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
                dataGridView1.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*try
            {
                int count = Convert.ToInt32(textBox1.Text);
                if (count <= 0)
                    throw new Exception();
                else
                {
                    int v = value;
                    string queryString1 = $"SELECT ProductId, Name, Weight, ShelfLife, Description, Price FROM Product WHERE ProductId = {v}";

                    SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
                    database.openConnection();
                    SqlDataReader reader1 = command1.ExecuteReader();

                    List<string[]> data1 = new List<string[]>();
                    int r5 = 0;
                    while (reader1.Read())
                    {
                        data1.Add(new string[7]);

                        data1[data1.Count - 1][0] = reader1[0].ToString();
                        data1[data1.Count - 1][1] = reader1[1].ToString();
                        data1[data1.Count - 1][2] = reader1[2].ToString();
                        data1[data1.Count - 1][3] = reader1[3].ToString();
                        data1[data1.Count - 1][4] = reader1[4].ToString();
                        data1[data1.Count - 1][5] = reader1[5].ToString();
                        r5 = Convert.ToInt32(reader1[5]);
                    }
                    reader1.Close();

                    database.closeConnection();
                    data1[data1.Count - 1][6] = count.ToString();
                    bool b = false;
                    int k = dataGridViewRequest.Rows.Count;
                    for (int i = 0; i < k; i++)
                    {
                        if (Convert.ToInt32(dataGridViewRequest[0, i].Value) == v)
                        {
                            dataGridViewRequest[6, i].Value = (Convert.ToInt32(dataGridViewRequest[6, i].Value) + count).ToString();
                            b = true;
                        }
                    }

                    if (!b)
                    {
                        foreach (string[] s in data1)
                            dataGridViewRequest.Rows.Add(s);
                    }
                    
                    k = dataGridViewRequest.Rows.Count;
                    int sum = 0;
                    for (int i = 0; i < k; i++)
                    {
                        sum += Convert.ToInt32(dataGridViewRequest[5, i].Value) * Convert.ToInt32(dataGridViewRequest[6, i].Value);
                    }
                    lab.Text = sum.ToString() + " руб.";

                    this.Close();
                    this.mainpanel.Controls[mainpanel.Controls.Count - 1].Visible = true;
                }
            }
            catch(Exception)
            {
                panel4.Visible = true;
                label3.Visible = true;
            }
            */
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                value = Convert.ToInt32(row.Cells[0].Value);
            }   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.mainpanel.Controls[mainpanel.Controls.Count-1].Visible = true;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int v = value;
            string queryString1 = $"SELECT ProductId, Name, Weight, ShelfLife, Description, Price FROM Product WHERE ProductId = {v}";

            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
            database.openConnection();
            SqlDataReader reader1 = command1.ExecuteReader();

            List<string[]> data1 = new List<string[]>();
            int r5 = 0;
            while (reader1.Read())
            {
                data1.Add(new string[8]);

                data1[data1.Count - 1][0] = reader1[0].ToString();
                data1[data1.Count - 1][1] = reader1[1].ToString();
                data1[data1.Count - 1][2] = reader1[2].ToString();
                data1[data1.Count - 1][3] = reader1[3].ToString();
                data1[data1.Count - 1][4] = reader1[4].ToString();
                data1[data1.Count - 1][5] = reader1[5].ToString();
                data1[data1.Count - 1][7] = reader1[5].ToString();
                r5 = Convert.ToInt32(reader1[5]);
            }
            reader1.Close();

            database.closeConnection();
            data1[data1.Count - 1][6] = "1";
            bool b = false;

            for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
            {
                if (Convert.ToInt32(dataGridViewRequest[0, i].Value) == v) 
                    b = true;
            }

            if (!b)
            {
                foreach (string[] s in data1)
                    dataGridViewRequest.Rows.Add(s);
            }

            int sum = 0;
            for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridViewRequest[5, i].Value) * Convert.ToInt32(dataGridViewRequest[6, i].Value);
            }
            lab.Text = sum.ToString() + " руб.";

            this.Close();
            this.mainpanel.Controls[mainpanel.Controls.Count - 1].Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "Поиск по названию продукции")
            {
                dataGridView1.Rows.Clear();
                string queryString = $"SELECT ProductId, Name, Weight, ShelfLife, Description, Price FROM Product WHERE Hided = 0 AND Name LIKE '%{textBox2.Text}%' ORDER BY Name";

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
                    dataGridView1.Rows.Add(s);
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = null;
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "Поиск по названию продукции";
        }
    }
}
