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
	public partial class Statistics : Form
	{
		Point lastPoint;
		DataBase database = new DataBase();
		public Statistics()
		{
			InitializeComponent();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void panel4_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}
		}

		private void panel4_MouseDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}

		private void Statistics_Load(object sender, EventArgs e)
		{
			comboBox1.SelectedIndex = 0;
			chart1.ChartAreas[0].AxisY.Title = "Сумма (руб.)";
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SalesChart();
		}
		private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
		{
			SalesChart();
			TopProducts();
			AntiTopProducts();
		}

		private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
		{
			SalesChart();
			TopProducts();
			AntiTopProducts();
		}
		public void SalesChart()
		{
			int index = comboBox1.SelectedIndex;
			DateTime date1 = dateTimePicker5.Value;
			DateTime date2 = dateTimePicker4.Value;
			chart1.Series[0].Points.Clear();
			if (date1 <= date2)
			{
				if (index == 0)
				{
					chart1.Series[0].Points.Clear();
					string datei;
					database.openConnection();
					for (DateTime i = date1; i <= date2; i = i.AddDays(1))
					{
						datei = i.Day + "-" + i.Month + "-" + i.Year;
						string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate = '{datei}' AND State = 'Завершена'";

						SqlCommand command = new SqlCommand(queryString, database.getConnection());
						int sum = 0;
						if (command.ExecuteScalar().ToString() != "")
							//s = command.ExecuteScalar().ToString();
							sum = Convert.ToInt32(command.ExecuteScalar().ToString());

						chart1.Series[0].Points.AddXY(i, sum);
					}
					database.closeConnection();
				}
				else if (index == 1)
				{
					chart1.Series[0].Points.Clear(); ;
					if (date1.Month != date2.Month || date1.Year != date2.Year)
					{
						if (date1.Day != 1)
						{
							database.openConnection();
							string newdate = DateTime.DaysInMonth(date1.Year, date1.Month) + "-" + date1.Month + "-" + date1.Year;
							string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{date1}' AND DeliveryDate <= '{newdate}' AND State = 'Завершена'";
							SqlCommand command = new SqlCommand(queryString, database.getConnection());
							int sum = 0;
							if (command.ExecuteScalar().ToString() != "")
								sum = Convert.ToInt32(command.ExecuteScalar().ToString());

							database.closeConnection();
							chart1.Series[0].Points.AddXY(date1.ToString("Y"), sum);


							DateTime xdate;
							if (date1.Month != 12)
							{
								xdate = new DateTime(date1.Year, date1.Month + 1, 1);
							}
							else
							{
								xdate = new DateTime(date1.Year + 1, 1, 1);
							}
							while (xdate <= date2)
							{
								if (xdate.Month == date2.Month && xdate.Year == date2.Year)
								{
									database.openConnection();
									newdate = 1 + "-" + date2.Month + "-" + date2.Year;
									queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{newdate}' AND DeliveryDate <= '{date2}' AND State = 'Завершена'";
									command = new SqlCommand(queryString, database.getConnection());
									sum = 0;
									if (command.ExecuteScalar().ToString() != "")
										sum = Convert.ToInt32(command.ExecuteScalar().ToString());

									database.closeConnection();
									chart1.Series[0].Points.AddXY(date2.ToString("Y"), sum);
									break;
								}
								else
								{
									database.openConnection();
									newdate = DateTime.DaysInMonth(xdate.Year, xdate.Month) + "-" + xdate.Month + "-" + xdate.Year;
									queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{xdate}' AND DeliveryDate <= '{newdate}' AND State = 'Завершена'";
									command = new SqlCommand(queryString, database.getConnection());
									sum = 0;
									if (command.ExecuteScalar().ToString() != "")
										sum = Convert.ToInt32(command.ExecuteScalar().ToString());

									database.closeConnection();
									chart1.Series[0].Points.AddXY(xdate.ToString("Y"), sum);
									xdate = xdate.AddMonths(1);
								}
							}
						}
						else
						{
							DateTime xdate = date1;
							while (xdate <= date2)
							{
								if (xdate.Month == date2.Month && xdate.Year == date2.Year)
								{
									database.openConnection();
									string newdate = 1 + "-" + date2.Month + "-" + date2.Year;
									string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{newdate}' AND DeliveryDate <= '{date2}' AND State = 'Завершена'";
									SqlCommand command = new SqlCommand(queryString, database.getConnection());
									int sum = 0;
									if (command.ExecuteScalar().ToString() != "")
										sum = Convert.ToInt32(command.ExecuteScalar().ToString());

									database.closeConnection();
									chart1.Series[0].Points.AddXY(date2.ToString("Y"), sum);
									break;
								}
								else
								{
									database.openConnection();
									string newdate = DateTime.DaysInMonth(xdate.Year, xdate.Month) + "-" + xdate.Month + "-" + xdate.Year;
									string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{xdate}' AND DeliveryDate <= '{newdate}' AND State = 'Завершена'";
									SqlCommand command = new SqlCommand(queryString, database.getConnection());
									int sum = 0;
									if (command.ExecuteScalar().ToString() != "")
										sum = Convert.ToInt32(command.ExecuteScalar().ToString());

									database.closeConnection();
									chart1.Series[0].Points.AddXY(xdate.ToString("Y"), sum);
									xdate = xdate.AddMonths(1);
								}
							}
						}

					}

					else
					{
						database.openConnection();
						string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{date1}' AND DeliveryDate <= '{date2}' AND State = 'Завершена'";

						SqlCommand command = new SqlCommand(queryString, database.getConnection());
						int sum = 0;
						if (command.ExecuteScalar().ToString() != "")
							sum = Convert.ToInt32(command.ExecuteScalar().ToString());

						database.closeConnection();

						chart1.Series[0].Points.AddXY(date1.ToString("Y"), sum);
					}
				}
				else if (index == 2)
				{
					chart1.Series[0].Points.Clear();
					if (date1.Year != date2.Year)
					{
						database.openConnection();
						string newdate = 31 + "-" + 12 + "-" + date1.Year;
						string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{date1}' AND DeliveryDate <= '{newdate}' AND State = 'Завершена'";
						SqlCommand command = new SqlCommand(queryString, database.getConnection());
						int sum = 0;
						if (command.ExecuteScalar().ToString() != "")
							sum = Convert.ToInt32(command.ExecuteScalar().ToString());

						database.closeConnection();
						chart1.Series[0].Points.AddXY(date1.ToString("yyyy"), sum);
						DateTime xdate = new DateTime(date1.Year + 1, 1, 1);
						while (xdate <= date2)
						{
							if (xdate.Year == date2.Year)
							{
								database.openConnection();
								string queryString2 = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{xdate}' AND DeliveryDate <= '{date2}' AND State = 'Завершена'";

								SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
								int sum2 = 0;
								if (command2.ExecuteScalar().ToString() != "")
									sum2 = Convert.ToInt32(command2.ExecuteScalar().ToString());

								database.closeConnection();

								chart1.Series[0].Points.AddXY(date2.ToString("yyyy"), sum2);
								break;
							}
							else
							{
								DateTime newdate2 = new DateTime(xdate.Year, 12, 31);
								database.openConnection();
								string queryString2 = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{xdate}' AND DeliveryDate <= '{newdate2}' AND State = 'Завершена'";

								SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
								int sum2 = 0;
								if (command2.ExecuteScalar().ToString() != "")
									sum2 = Convert.ToInt32(command2.ExecuteScalar().ToString());

								database.closeConnection();
								chart1.Series[0].Points.AddXY(xdate.ToString("yyyy"), sum2);

								xdate = xdate.AddYears(1);
							}
						}
					}
					else
					{
						database.openConnection();
						string queryString = $"SELECT SUM(Sum) FROM Request WHERE DeliveryDate >= '{date1}' AND DeliveryDate <= '{date2}' AND State = 'Завершена'";

						SqlCommand command = new SqlCommand(queryString, database.getConnection());
						int sum = 0;
						if (command.ExecuteScalar().ToString() != "")
							sum = Convert.ToInt32(command.ExecuteScalar().ToString());

						database.closeConnection();

						chart1.Series[0].Points.AddXY(date1.ToString("yyyy"), sum);
					}
				}
			}
		}
		public void TopProducts()
		{
			int index = comboBox1.SelectedIndex;
			DateTime date1 = dateTimePicker5.Value;
			DateTime date2 = dateTimePicker4.Value;
			string sdate1 = date1.Day+"-"+date1.Month+"-"+date1.Year;
			string sdate2 = date2.Day + "-" + date2.Month + "-" + date2.Year;
			chart2.Series[0].Points.Clear();
			if (date1 <= date2)
			{
				database.openConnection();
				string queryString = $"SELECT Product.Name, SUM(COUNT) " +
					$"FROM OrderedProduct " +
					$"INNER JOIN Product ON Product.ProductId = OrderedProduct.ProductId " +
					$"INNER JOIN Request ON Request.RequestId = OrderedProduct.RequestId " +
					$"WHERE Request.DeliveryDate >= '{sdate1}' AND Request.DeliveryDate <= '{sdate2}' AND Request.State = 'Завершена' " +
					$"GROUP BY Product.Name " +
					$"ORDER BY SUM(COUNT) DESC";

				SqlCommand command = new SqlCommand(queryString, database.getConnection());
				SqlDataReader reader = command.ExecuteReader();

				List<string[]> data = new List<string[]>();
				while (reader.Read())
				{
					data.Add(new string[2]);

					data[data.Count - 1][0] = reader[0].ToString();
					data[data.Count - 1][1] = reader[1].ToString();
				}
				reader.Close();

				database.closeConnection();

				if (data.Count > 0)
				{
					if (data.Count>=5)
					for (int i = 0; i < 5; i++)
					{
						chart2.Series[0].Points.AddXY(data[4-i][0],Convert.ToInt32(data[4-i][1]));
					}
					else
						for (int i = 0; i < data.Count; i++)
						{
							chart2.Series[0].Points.AddXY(data[data.Count-1-i][0],Convert.ToInt32(data[data.Count - 1 - i][1]));
						}
				}
			}
			}
		public void AntiTopProducts()
		{
			int index = comboBox1.SelectedIndex;
			DateTime date1 = dateTimePicker5.Value;
			DateTime date2 = dateTimePicker4.Value;
			string sdate1 = date1.Day + "-" + date1.Month + "-" + date1.Year;
			string sdate2 = date2.Day + "-" + date2.Month + "-" + date2.Year;
			chart3.Series[0].Points.Clear();
			if (date1 <= date2)
			{
				database.openConnection();
				string queryString = $"SELECT Product.Name, SUM(COUNT) " +
					$"FROM OrderedProduct " +
					$"INNER JOIN Product ON Product.ProductId = OrderedProduct.ProductId " +
					$"INNER JOIN Request ON Request.RequestId = OrderedProduct.RequestId " +
					$"WHERE Request.DeliveryDate >= '{sdate1}' AND Request.DeliveryDate <= '{sdate2}' AND Request.State = 'Завершена' " +
					$"GROUP BY Product.Name " +
					$"ORDER BY SUM(COUNT)";

				SqlCommand command = new SqlCommand(queryString, database.getConnection());
				SqlDataReader reader = command.ExecuteReader();

				List<string[]> data = new List<string[]>();
				while (reader.Read())
				{
					data.Add(new string[2]);

					data[data.Count - 1][0] = reader[0].ToString();
					data[data.Count - 1][1] = reader[1].ToString();
				}
				reader.Close();

				database.closeConnection();

				if (data.Count > 0)
				{
					if (data.Count >= 5)
						for (int i = 0; i < 5; i++)
						{
							chart3.Series[0].Points.AddXY(data[4 - i][0], Convert.ToInt32(data[4 - i][1]));
						}
					else
						for (int i = 0; i < data.Count; i++)
						{
							chart3.Series[0].Points.AddXY(data[data.Count - 1 - i][0], Convert.ToInt32(data[data.Count - 1 - i][1]));
						}
				}
			}
		}
	}
}
