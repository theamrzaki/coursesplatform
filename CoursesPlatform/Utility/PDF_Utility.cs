using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HRsystem.Utility
{
    public class PDF_Utility
    {
        /// <param name="list">desired class </param>
        /// <param name="colum_names">columns you need in your report</param>
        /// <param name="destinationPath">path of saving</param>
        public static void ExportToPdf<T>(IEnumerable<T> list,string[] colum_names, string destinationPath)
        {
            DataTable dataTable = CreateDataTable(list);
            dataTable = dataTable.DefaultView.ToTable(false, colum_names);

            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationPath, FileMode.Create));
            document.Open();

            PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            table.WidthPercentage = 100;

            //Set columns names in the pdf file
            for (int k = 0; k < dataTable.Columns.Count; k++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dataTable.Columns[k].ColumnName));

                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 102, 102);

                table.AddCell(cell);
            }

            //Add values of DataTable in pdf file
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataTable.Rows[i][j].ToString()));

                    //Align the cell in the center
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }
            }

            document.Add(table);
            document.Close();
            //https://stackoverflow.com/questions/31933421/how-can-we-export-datatable-to-pdf-using-itextsharp
        }

        private static System.Data.DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            System.Data.DataTable dataTable = new System.Data.DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;

        }

    }
}