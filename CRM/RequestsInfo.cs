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
    public partial class RequestsInfo : Form
    {
        public Panel mainpanel;
        public DataGridView dataGridView1;
        DataBase database = new DataBase();
        public int value;
        public RequestsInfo()
        {
            InitializeComponent();
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

        private void RequestsInfo_Load(object sender, EventArgs e)
        {
            value = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            label5.Text = "Информация о заявке №" + value;
            label5.Left = (this.Width - label5.Width) / 2;

            string queryString1 = $"SELECT OrderDate, DeliveryDate, State, Sum, ClMessage FROM Request WHERE RequestId = {value}";

            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
            database.openConnection();
            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                label7.Text = Convert.ToDateTime(reader1[0]).ToShortDateString().ToString();
                label2.Text = Convert.ToDateTime(reader1[1]).ToShortDateString().ToString();
                label8.Text = reader1[2].ToString();
                label4.Text = reader1[3].ToString() + " руб.";
                textBox1.Text = reader1[4].ToString();
            }
            reader1.Close();

            if (label8.Text == "В работе")
                button1.Visible = true;

            database.closeConnection();

            string queryString = $"SELECT Product.ProductId, Name, Weight, ShelfLife, Description, Price, Count FROM OrderedProduct INNER JOIN Product ON Product.ProductId = OrderedProduct.ProductId AND OrderedProduct.RequestId = {value}";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data1 = new List<string[]>();
            while (reader.Read())
            {
                data1.Add(new string[7]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][2] = reader[2].ToString();
                data1[data1.Count - 1][3] = reader[3].ToString();
                data1[data1.Count - 1][4] = reader[4].ToString();
                data1[data1.Count - 1][5] = reader[5].ToString();
                data1[data1.Count - 1][6] = reader[6].ToString();
            }
            reader.Close();

            string queryString2 = $"SELECT ClientId FROM Request WHERE RequestId = {value}";
            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
            string ClientId = command2.ExecuteScalar().ToString();
            SqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            int clientid = Convert.ToInt32(reader2.GetValue(0));
            reader2.Close();
            string queryString3 = $"SELECT Surname, Name, Patronymic, PhoneNumber, Email, Address FROM Client WHERE ClientId = {clientid}";
            SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
            SqlDataReader reader3 = command3.ExecuteReader();
            reader3.Read();
            string surname = Convert.ToString(reader3.GetValue(0));
            string name = Convert.ToString(reader3.GetValue(1));
            string patronymic = Convert.ToString(reader3.GetValue(2));
            string phonenumber = Convert.ToString(reader3.GetValue(3));
            string email = Convert.ToString(reader3.GetValue(4));
            string address = Convert.ToString(reader3.GetValue(5));
            label14.Text = surname + " " + name + " " + patronymic;
            label15.Text = phonenumber;
            label16.Text = email;
            label17.Text = address;
            reader3.Close();

            database.closeConnection();

            foreach (string[] s in data1)
                dataGridViewRequest.Rows.Add(s);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string queryString2 = $"UPDATE Request SET State = 'Завершена' WHERE RequestId = {value}";
            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
            database.openConnection();
            command2.ExecuteNonQuery();
            database.closeConnection();
            label8.Text = "Завершена";
			button1.Visible = false;
			MessageBox.Show("Заявка завершена!");
        }
    }
}
