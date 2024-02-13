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
    public partial class NewRequests : Form
    {
        public Panel mainpanel;
        DataBase database = new DataBase();
        public int r;
        public NewRequests()
        {
            InitializeComponent();
        }

        private void NewRequests_Load(object sender, EventArgs e)
        {
            string queryString = $"SELECT RequestId, OrderDate, DeliveryDate, State, Sum FROM Request WHERE State = 'Новая' ORDER BY OrderDate DESC";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();
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

            database.closeConnection();

            foreach (string[] s in data)
                dataGridViewRequest.Rows.Add(s);
        }

        private void dataGridViewRequest_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            NewRequestInfo acc = new NewRequestInfo();
            acc.mainpanel = this.mainpanel;
            acc.dataGridView1 = this.dataGridViewRequest;
            acc.TopLevel = false;
            r = 1;
            acc.r = this.r;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }
    }
}
