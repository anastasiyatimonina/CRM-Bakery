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
    public partial class Requests : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        public Requests()
        {
            InitializeComponent();
        }

        private void Requests_Load(object sender, EventArgs e)
        {

            string queryString1 = $"SELECT ClientId FROM Client WHERE UserId = {DataSend.UserId}";
            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
            database.openConnection();
            string ClientId = command1.ExecuteScalar().ToString();
            SqlDataReader reader1 = command1.ExecuteReader();
            reader1.Read();
            int clientid = Convert.ToInt32(reader1.GetValue(0));
            reader1.Close();

            string queryString = $"SELECT RequestId, OrderDate, DeliveryDate, State, Sum FROM Request WHERE State = 'Новая' AND ClientId = {clientid} ORDER BY OrderDate DESC";
            string queryString2 = $"SELECT RequestId, OrderDate, DeliveryDate, State, Sum FROM Request WHERE State = 'В работе' AND ClientId = {clientid} ORDER BY OrderDate DESC";
            string queryString3 = $"SELECT RequestId, OrderDate, DeliveryDate, State, Sum FROM Request WHERE State = 'Отмена' AND ClientId = {clientid} ORDER BY OrderDate DESC";
            string queryString4 = $"SELECT RequestId, OrderDate, DeliveryDate, State, Sum FROM Request WHERE State = 'Завершена' AND ClientId = {clientid} ORDER BY OrderDate DESC";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
            SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
            SqlCommand command4 = new SqlCommand(queryString4, database.getConnection());
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();
            List<string[]> data2 = new List<string[]>();
            List<string[]> data3 = new List<string[]>();
            List<string[]> data4 = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = Convert.ToDateTime(reader[1]).ToShortDateString().ToString();
                data[data.Count - 1][2] = Convert.ToDateTime(reader[2]).ToShortDateString().ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();

            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                data2.Add(new string[5]);

                data2[data2.Count - 1][0] = reader2[0].ToString();
                data2[data2.Count - 1][1] = Convert.ToDateTime(reader2[1]).ToShortDateString().ToString();
                data2[data2.Count - 1][2] = Convert.ToDateTime(reader2[2]).ToShortDateString().ToString();
                data2[data2.Count - 1][3] = reader2[3].ToString();
                data2[data2.Count - 1][4] = reader2[4].ToString();
            }
            reader2.Close();

            SqlDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                data3.Add(new string[5]);

                data3[data3.Count - 1][0] = reader3[0].ToString();
                data3[data3.Count - 1][1] = Convert.ToDateTime(reader3[1]).ToShortDateString().ToString();
                data3[data3.Count - 1][2] = Convert.ToDateTime(reader3[2]).ToShortDateString().ToString();
                data3[data3.Count - 1][3] = reader3[3].ToString();
                data3[data3.Count - 1][4] = reader3[4].ToString();
            }
            reader3.Close();

            SqlDataReader reader4 = command4.ExecuteReader();
            while (reader4.Read())
            {
                data4.Add(new string[5]);

                data4[data4.Count - 1][0] = reader4[0].ToString();
                data4[data4.Count - 1][1] = Convert.ToDateTime(reader4[1]).ToShortDateString().ToString();
                data4[data4.Count - 1][2] = Convert.ToDateTime(reader4[2]).ToShortDateString().ToString();
                data4[data4.Count - 1][3] = reader4[3].ToString();
                data4[data4.Count - 1][4] = reader4[4].ToString();
            }
            reader4.Close();

            database.closeConnection();

            foreach (string[] s in data)
                dataGridViewRequest.Rows.Add(s);
            foreach (string[] s in data2)
                dataGridViewRequest.Rows.Add(s);
            foreach (string[] s in data4)
                dataGridViewRequest.Rows.Add(s);
            foreach (string[] s in data3)
                dataGridViewRequest.Rows.Add(s);

            int j = dataGridViewRequest.Rows.Count;
            for (int i = 1; i < dataGridViewRequest.Rows.Count; i++)
            {
                if (dataGridViewRequest[3, i].Value.ToString() == "Новая")
                {
                    dataGridViewRequest[3, i].Style.ForeColor = Color.Green;
                }
                if (dataGridViewRequest[3, i].Value.ToString() == "Отмена")
                {
                    dataGridViewRequest[3, i].Style.ForeColor = Color.Gray;
                }
                if (dataGridViewRequest[3, i].Value.ToString() == "Завершена")
                {
                    dataGridViewRequest[3, i].Style.ForeColor = Color.Orange;
                }
                if (dataGridViewRequest[3, i].Value.ToString() == "В работе")
                {
                    dataGridViewRequest[3, i].Style.ForeColor = Color.Blue;
                }
            }
        }

        private void dataGridViewRequest_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            RequestInfo acc = new RequestInfo();
            acc.mainpanel = this.mainpanel;
            acc.dataGridView1 = this.dataGridViewRequest;
            acc.TopLevel = false;
            acc.r = 1;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }
    }
}
