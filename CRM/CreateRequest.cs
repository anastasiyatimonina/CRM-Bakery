using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRM
{
    public partial class CreateRequest : Form
    {
        DataBase database = new DataBase();
        public Panel mainpanel;
        int value = 0;
        string message = "";
        public int r;
        public int numreq;
        public CreateRequest()
        {
            InitializeComponent();
        }

        private void CreateRequest_Load(object sender, EventArgs e)
        {
            if (r == 1)
            {
                //label2.Text = DateTime.Now.ToLongDateString();
                label4.Text = "0 руб.";
                int sum = 0;
                if (this.dataGridViewRequest.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
                    {
                        sum += Convert.ToInt32(dataGridViewRequest[6, i].Value);
                    }
                }
                label4.Text = sum.ToString() + " руб.";
            }
            else if (r == 2)
            {
                pictureBox1.Visible = true;
                label1.Left = label1.Left + 30;
                dateTimePicker1.Left = dateTimePicker1.Left + 30;
                button2.Text = "Редактировать заявку";
                string queryString1 = $"SELECT OrderDate, DeliveryDate, Sum, ClMessage FROM Request WHERE RequestId = {numreq}";

                SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
                database.openConnection();
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    label2.Text = Convert.ToDateTime(reader1[0]).ToLongDateString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader1[1]);
                    label4.Text = reader1[2].ToString() + " руб.";
                    message = reader1[3].ToString();
                }
                reader1.Close();

                string queryString = $"SELECT Product.ProductId, Name, Weight, ShelfLife, Description, Price, Count FROM OrderedProduct INNER JOIN Product ON Product.ProductId = OrderedProduct.ProductId AND OrderedProduct.RequestId = {numreq}";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddingProduct acc = new AddingProduct();
            acc.mainpanel = this.mainpanel;
            acc.lab = this.label4;
            acc.dataGridViewRequest = this.dataGridViewRequest;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRequest.Rows.Count == 0) 
            {
                MessageBox.Show("Невозможно оформить пустую заявку! Пожалуйста, добавьте товары!");
            }
            else
            {
                if (Convert.ToInt32(label4.Text.Trim(new Char[] { ' ', 'р', 'у', 'б', '.' })) < 600)
                {
                    MessageBox.Show("Доставка осуществляется от 600 руб.");
                }
                else
                {
                    if (dateTimePicker1.Value.Date <= DateTime.Now.Date)
                    {
                        MessageBox.Show("Выбрана некорректная дата доставки! Доставку можно выбрать, начиная с завтрашнего дня!");
                    }
                    else
                    {
                        if (r == 1)
                        {
                            string queryString1 = $"SELECT ClientId FROM Client WHERE UserId = {DataSend.UserId}";
                            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
                            database.openConnection();
                            string ClientId = command1.ExecuteScalar().ToString();
                            SqlDataReader reader = command1.ExecuteReader();
                            reader.Read();
                            int day1 = dateTimePicker1.Value.Day;
                            int month1 = dateTimePicker1.Value.Month;
                            int year1 = dateTimePicker1.Value.Year;
                            string deliverydate = day1 + "-" + month1 + "-" + year1;
                            string orderdate = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                            int sum = Convert.ToInt32(label4.Text.Trim(new Char[] { ' ', 'р', 'у', 'б', '.' }));
                            int clientid = Convert.ToInt32(reader.GetValue(0));
                            reader.Close();
                            string queryString2;
                            if (message == "")
                                queryString2 = $"INSERT INTO Request (ClientId, OrderDate, DeliveryDate, State, Sum) VALUES ({clientid}, '{orderdate}','{deliverydate}', 'Новая', {sum})";
                            else
                                queryString2 = $"INSERT INTO Request (ClientId, OrderDate, DeliveryDate, State, Sum, ClMessage) VALUES ({clientid}, '{orderdate}','{deliverydate}', 'Новая', {sum}, '{message}')";
                            string queryString3 = $"SELECT SCOPE_IDENTITY()";
                            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                            SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
                            command2.ExecuteNonQuery();
                            int requestid = Convert.ToInt32(command3.ExecuteScalar());
                            int k = dataGridViewRequest.Rows.Count;
                            for (int i = 0; i < k; i++)
                            {
                                int p = Convert.ToInt32(dataGridViewRequest[0, i].Value);
                                int c = Convert.ToInt32(dataGridViewRequest[6, i].Value);
                                string query = $"INSERT INTO OrderedProduct (RequestId, ProductId, Count) VALUES ({requestid}, {Convert.ToInt32(dataGridViewRequest[0, i].Value)},{Convert.ToInt32(dataGridViewRequest[6, i].Value)})";
                                SqlCommand command4 = new SqlCommand(query, database.getConnection());
                                command4.ExecuteNonQuery();
                            }
                            database.closeConnection();
                            MessageBox.Show("Заявка оформлена!");
                        }
                        else if (r == 2)
                        {

                            string queryString1 = $"SELECT ClientId FROM Client WHERE UserId = {DataSend.UserId}";
                            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
                            database.openConnection();
                            string ClientId = command1.ExecuteScalar().ToString();
                            SqlDataReader reader = command1.ExecuteReader();
                            reader.Read();
                            int day1 = dateTimePicker1.Value.Day;
                            int month1 = dateTimePicker1.Value.Month;
                            int year1 = dateTimePicker1.Value.Year;
                            string deliverydate = day1 + "-" + month1 + "-" + year1;
                            string orderdate = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                            int sum = Convert.ToInt32(label4.Text.Trim(new Char[] { ' ', 'р', 'у', 'б', '.' }));
                            int clientid = Convert.ToInt32(reader.GetValue(0));
                            reader.Close();
                            string queryString2;
                            if (message == "")
                                queryString2 = $"UPDATE Request SET OrderDate = '{orderdate}', DeliveryDate = '{deliverydate}', Sum = {sum}, ClMessage = NULL WHERE RequestId = {numreq}";
                            else
                                queryString2 = $"UPDATE Request SET OrderDate = '{orderdate}', DeliveryDate = '{deliverydate}', Sum = {sum}, ClMessage = '{message}' WHERE RequestId = {numreq}";
                            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                            command2.ExecuteNonQuery();
                            string queryString3 = $"DELETE FROM OrderedProduct WHERE RequestId = {numreq}";
                            SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
                            command3.ExecuteNonQuery();
                            int k = dataGridViewRequest.Rows.Count;
                            for (int i = 0; i < k; i++)
                            {
                                int p = Convert.ToInt32(dataGridViewRequest[0, i].Value);
                                int c = Convert.ToInt32(dataGridViewRequest[6, i].Value);
                                string query = $"INSERT INTO OrderedProduct (RequestId, ProductId, Count) VALUES ({numreq}, {Convert.ToInt32(dataGridViewRequest[0, i].Value)},{Convert.ToInt32(dataGridViewRequest[6, i].Value)})";
                                SqlCommand command4 = new SqlCommand(query, database.getConnection());
                                command4.ExecuteNonQuery();
                            }
                            database.closeConnection();
                            this.Close();
                            RequestInfo f = new RequestInfo();
                            f.value = this.numreq;
                            f.r = 2;
                            f.mainpanel = this.mainpanel;
                            f.TopLevel = false;
                            f.Dock = DockStyle.Fill;
                            this.mainpanel.Controls.Add(f);
                            this.mainpanel.Tag = f;
                            f.Show();
                        }
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewRequest_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridViewRequest.CurrentCell.ColumnIndex == 6)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dataGridViewRequest_KeyPress);
            }
        }

        private void dataGridViewRequest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dataGridViewRequest_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
            {
                if (dataGridViewRequest[6, i].Value == null || dataGridViewRequest[6, i].Value.ToString() == "0")
                {
                        dataGridViewRequest[6, i].Value = "1";
                }
                else
                {
                    string s = dataGridViewRequest[6, i].Value.ToString();
                    while (s.Substring(0, 1) == "0")
                    {
                        s = s.Substring(1);
                    }
                    dataGridViewRequest[6, i].Value = s;
                    dataGridViewRequest[7, i].Value = (Convert.ToInt32(s)*Convert.ToInt32(dataGridViewRequest[5, i].Value)).ToString();
                }
            }
            int sum = 0;
            if (this.dataGridViewRequest.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dataGridViewRequest[5, i].Value)*Convert.ToInt32(dataGridViewRequest[6, i].Value);
                }
            }
            label4.Text = sum.ToString() + " руб.";
        }

        private void dataGridViewRequest_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewRequest_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRequest.SelectedRows)
            {
                value = dataGridViewRequest.SelectedCells[8].RowIndex;
            }
        }

        private void dataGridViewRequest_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientDismiss f = new ClientDismiss();
            f.value = this.message;
            f.mainpanel = this.mainpanel;
            f.ShowDialog();
            this.message = f.value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            RequestInfo f = new RequestInfo();
            f.value = this.numreq;
            f.r = 2;
            f.mainpanel = this.mainpanel;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void dataGridViewRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewRequest.CurrentCell.ColumnIndex == 8)
            {
                dataGridViewRequest.Rows.RemoveAt(value);
                int sum = 0;
                for (int i = 0; i < dataGridViewRequest.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dataGridViewRequest[5, i].Value) * Convert.ToInt32(dataGridViewRequest[6, i].Value);
                }
                label4.Text = sum.ToString() + " руб.";
            }
        }
    }
}
