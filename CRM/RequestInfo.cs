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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CRM
{
    public partial class RequestInfo : Form
    {
        public Panel mainpanel;
        public DataGridView dataGridView1;
        DataBase database = new DataBase();
        public int value;
        public int r;
        public RequestInfo()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Requests r = new Requests();
            r.mainpanel = this.mainpanel;
            r.TopLevel = false;
            r.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(r);
            this.mainpanel.Tag = r;
            r.Show();
        }

        private void RequestInfo_Load(object sender, EventArgs e)
        {
            if (r == 1)
                value = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            label5.Text = "Информация о заявке №" + value;
            label5.Left = (this.Width - label5.Width) / 2;

            string queryString1 = $"SELECT OrderDate, DeliveryDate, State, Sum, EmpMessage FROM Request WHERE RequestId = {value}";

            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
            database.openConnection();
            SqlDataReader reader1 = command1.ExecuteReader();
            string mess = "";
            while (reader1.Read())
            {
                label7.Text = Convert.ToDateTime(reader1[0]).ToShortDateString().ToString();
                label2.Text = Convert.ToDateTime(reader1[1]).ToShortDateString().ToString();
                label8.Text = reader1[2].ToString();
                label4.Text = reader1[3].ToString()+ " руб.";
                mess = reader1[4].ToString();
            }
            reader1.Close();

            database.closeConnection();

            if (label8.Text == "Новая")
            {
                button1.Visible = true;
                button2.Visible = true;
            }
            else if (label8.Text == "Отмена")
            {
                textBox1.Visible = true;
                label10.Visible = true;
                textBox1.Text = mess;
            }

            string queryString = $"SELECT Product.ProductId, Name, Weight, ShelfLife, Description, Price, Count FROM OrderedProduct INNER JOIN Product ON Product.ProductId = OrderedProduct.ProductId AND OrderedProduct.RequestId = {value}";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data1 = new List<string[]>();
            while (reader.Read())
            {
                data1.Add(new string[8]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][2] = reader[2].ToString();
                data1[data1.Count - 1][3] = reader[3].ToString();
                data1[data1.Count - 1][4] = reader[4].ToString();
                data1[data1.Count - 1][5] = reader[5].ToString();
                data1[data1.Count - 1][6] = reader[6].ToString();
                data1[data1.Count - 1][7] = (Convert.ToInt32(reader[5]) * Convert.ToInt32(reader[6])).ToString();
            }
            reader.Close();

            database.closeConnection();

            foreach (string[] s in data1)
                dataGridViewRequest.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteRequest f = new DeleteRequest();
            f.value = this.value;
            f.mainpanel = this.mainpanel;
            f.ShowDialog();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "Новая")
            {
                label8.ForeColor = Color.Green;
            }
            else if (label8.Text == "В работе")
            {
                label8.ForeColor = Color.Blue;
            }
            else if (label8.Text == "Завершена")
            {
                label8.ForeColor = Color.Orange;
            }
            else if (label8.Text == "Отмена")
            {
                label8.ForeColor = Color.Gray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            r = 2;
            this.Hide();
            CreateRequest acc = new CreateRequest();
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            acc.r = this.r;
            acc.numreq = this.value;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }
    }
}
