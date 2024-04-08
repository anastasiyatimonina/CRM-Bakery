using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class ReportProduction : Form
    {
        public Panel mainpanel;
        public ReportProduction()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var helper = new WordHelper("Заказ_на_производство_Шаблон.doc");

            string date = dateTimePicker1.Value.ToString("d");

            helper.Process(date);
        }

        private void ReportProduction_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			var helper = new WordHelper("Отчет_по_продажам.doc");

			string date1 = dateTimePicker2.Value.ToString("d");
			string date2 = dateTimePicker3.Value.ToString("d");

			helper.Process2(date1, date2);
		}

		private void button3_Click(object sender, EventArgs e)
		{
            Statistics st = new Statistics();
            st.Show();
		}
	}
}
