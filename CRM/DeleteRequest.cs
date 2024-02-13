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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRM
{
    public partial class DeleteRequest : Form
    {
        DataBase database = new DataBase();
        public int value;
        public Panel mainpanel;
        public DeleteRequest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            string queryString2 = $"DELETE FROM OrderedProduct WHERE RequestId = {value}";
            string queryString = $"DELETE FROM Request WHERE RequestId = {value}";
            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            command2.ExecuteNonQuery();
            command.ExecuteNonQuery();
            database.closeConnection();
            Requests acc = new Requests();
            this.mainpanel.Controls[mainpanel.Controls.Count - 1].Visible = false;
            acc.mainpanel = this.mainpanel;
            acc.TopLevel = false;
            acc.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(acc);
            this.mainpanel.Tag = acc;
            acc.Show();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.mainpanel.Controls[mainpanel.Controls.Count - 1].Visible = true;
        }
    }
}
