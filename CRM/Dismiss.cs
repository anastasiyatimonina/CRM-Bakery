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
    public partial class Dismiss : Form
    {
        public Panel mainpanel;
        public int value;
        DataBase database = new DataBase();
        public int r;
        public Dismiss()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string queryString2 = $"UPDATE Request SET State = 'Отмена' WHERE RequestId = {value}";
            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
            database.openConnection();
            command2.ExecuteNonQuery();
            if (text != "")
            {
                string queryString3 = $"UPDATE Request SET EmpMessage = '{text}' WHERE RequestId = {value}";
                SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
                command3.ExecuteNonQuery();
            }
            database.closeConnection();
            MessageBox.Show("Заявка отклонена!");
            NewRequestInfo acc = new NewRequestInfo();
            this.Close();
            acc.mainpanel = this.mainpanel;
            if (this.mainpanel.Controls.Count > 0)
                for (int i = this.mainpanel.Controls.Count - 1; i >= 0; --i)
                    this.mainpanel.Controls.RemoveAt(i);
            r = 2;
            acc.value = this.value;
            acc.TopLevel = false;
            acc.r = this.r;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
        }
    }
}
