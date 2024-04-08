using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System.Data;

namespace CRM
{
    class WordHelper
    {
        private FileInfo _fileInfo;
        DataBase database = new DataBase();
        public WordHelper(string filename) 
        {
            if (File.Exists(filename))
            {
                _fileInfo = new FileInfo(filename);
            }
            else
            {
                throw new ArgumentException("File not found");
            }
        }

        internal bool Process(string date)
        {
            /*Type wordType = Type.GetTypeFromProgID("Word.Application");
            dynamic app = Activator.CreateInstance(wordType);
            app = null;*/
            string queryString = $"SELECT Product.Name AS Наименование, SUM(OrderedProduct.Count) AS Количество FROM OrderedProduct INNER JOIN Product ON OrderedProduct.ProductId = Product.ProductId INNER JOIN Request ON Request.RequestId = OrderedProduct.RequestId AND Request.DeliveryDate='{date}' AND Request.State='В работе' GROUP BY Product.Name";
            database.openConnection();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, database.getConnection());
            adapter.SelectCommand.ExecuteNonQuery();
            adapter.Fill(dataTable);
            database.closeConnection();
            try
            {
                Type wordType = Type.GetTypeFromProgID("Word.Application");
                Type wordType2 = Type.GetTypeFromProgID("Word.Document");
                dynamic app = Activator.CreateInstance(wordType);
                dynamic doc = Activator.CreateInstance(wordType2);
                app = new Microsoft.Office.Interop.Word.Application();
                doc = app.Application.Documents.Add(Type.Missing);
                //var paragraph1 = doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Size = 16;
                doc.Content.Font.Name = "Times New Roman";
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Заказ на производство на " + date;
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                /* Object file = _fileInfo.FullName;

                 Object missing = Type.Missing;

                 app.Documents.Open(file);

                 Word.Find find = app.Selection.Find;
                 find.Text = "<DATE>";
                 find.Replacement.Text = date;

                 Object wrap = Word.WdFindWrap.wdFindContinue;
                 Object replace = Word.WdReplace.wdReplaceAll;

                 find.Execute(FindText: Type.Missing,
                     MatchCase: false,
                     MatchWholeWord: false,
                     MatchWildcards: false,
                     MatchSoundsLike: missing,
                     MatchAllWordForms: false,
                     Forward: true,
                     Wrap: wrap,
                     Format: false,
                     ReplaceWith: missing, Replace: replace);*/
                var paragraph = doc.Paragraphs.Add();
                var tableRange = doc.Paragraphs[doc.Paragraphs.Count].Range;
                doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Size = 14;
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(tableRange, dataTable.Rows.Count+1, dataTable.Columns.Count, Type.Missing);
                table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dataTable.Columns[i].ColumnName;
                }
                for (int i = 0; i<dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dataTable.Rows[i][j].ToString();
                    }
                }

                app.Visible = true;

                /*Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss ")+_fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();*/
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            /*finally
            {
                if (app != null)
                    app.Quit();
            }*/
            return false;
        }
		internal bool Process2(string date1, string date2)
		{
			/*Type wordType = Type.GetTypeFromProgID("Word.Application");
            dynamic app = Activator.CreateInstance(wordType);
            app = null;*/
			string queryString = $"SELECT Product.Name AS Наименование, SUM(OrderedProduct.Count) AS Количество FROM OrderedProduct INNER JOIN Product ON OrderedProduct.ProductId = Product.ProductId INNER JOIN Request ON Request.RequestId = OrderedProduct.RequestId AND Request.DeliveryDate>='{date1}' AND Request.DeliveryDate<='{date2}' AND Request.State='Завершена' GROUP BY Product.Name";
			database.openConnection();
			System.Data.DataTable dataTable = new System.Data.DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter(queryString, database.getConnection());
			adapter.SelectCommand.ExecuteNonQuery();
			adapter.Fill(dataTable);
			database.closeConnection();
			try
			{
				Type wordType = Type.GetTypeFromProgID("Word.Application");
				Type wordType2 = Type.GetTypeFromProgID("Word.Document");
				dynamic app = Activator.CreateInstance(wordType);
				dynamic doc = Activator.CreateInstance(wordType2);
				app = new Microsoft.Office.Interop.Word.Application();
				doc = app.Application.Documents.Add(Type.Missing);
				//var paragraph1 = doc.Paragraphs.Add();
				doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Size = 16;
				doc.Content.Font.Name = "Times New Roman";
				doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Отчет по продажам в период с " + date1 + " по "+date2;
				doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
				/* Object file = _fileInfo.FullName;

                 Object missing = Type.Missing;

                 app.Documents.Open(file);

                 Word.Find find = app.Selection.Find;
                 find.Text = "<DATE>";
                 find.Replacement.Text = date;

                 Object wrap = Word.WdFindWrap.wdFindContinue;
                 Object replace = Word.WdReplace.wdReplaceAll;

                 find.Execute(FindText: Type.Missing,
                     MatchCase: false,
                     MatchWholeWord: false,
                     MatchWildcards: false,
                     MatchSoundsLike: missing,
                     MatchAllWordForms: false,
                     Forward: true,
                     Wrap: wrap,
                     Format: false,
                     ReplaceWith: missing, Replace: replace);*/
				var paragraph = doc.Paragraphs.Add();
				var tableRange = doc.Paragraphs[doc.Paragraphs.Count].Range;
				doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Size = 14;
				doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
				Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(tableRange, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing);
				table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
				table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					table.Cell(1, i + 1).Range.Text = dataTable.Columns[i].ColumnName;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					for (int j = 0; j < dataTable.Columns.Count; j++)
					{
						table.Cell(i + 2, j + 1).Range.Text = dataTable.Rows[i][j].ToString();
					}
				}

				app.Visible = true;

				/*Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss ")+_fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();*/
				return true;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			/*finally
            {
                if (app != null)
                    app.Quit();
            }*/
			return false;
		}
	}
}
