using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using HRsystem.Models;
using HRsystem.Cruds;

namespace HRsystem.Utility
{
    public class Excel_Utility
    {


        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName">Name of Your Report</param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        private static string ExcelTemplate(List<Columns_Excel> columns,  string worksheetName, string excelPath)
        {
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            
            //object misValue = System.Reflection.Missing.Value;
            int u = 50;//columns.Count +  40;
            try
            {
                // Start Excel and get Application object.
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                xlApp.Interactive = false;

                // for making Excel visible
                //excel.Visible = false;
                xlApp.DisplayAlerts = false;


                // Creation a new Workbook
                xlWorkBook = xlApp.Workbooks.Add(Type.Missing);

                // Workk sheet
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.ActiveSheet;
                xlWorkSheet.Name = worksheetName;

             //   //Previous code was referring to the wrong class, throwing an exception
             //   xlApp = new Application();
             ////   xlApp.DefaultSaveFormat = XlFileFormat.xlOpenXMLWorkbook;

             //   xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
             //   // xlWorkBook = xlApp.Workbooks.Add(misValue);

             //   xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);


             //   xlApp.Interactive = false;

             //   // for making Excel visible
             //  // xlApp.Visible = false;
             //   xlApp.DisplayAlerts = false;

                char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

                for (int i = 0; i <= columns.Count - 1; i++)
                {
                    xlWorkSheet.Cells[1, i + 1] = columns[i].Colum_name;

                    if (columns[i].DropDown_List != null )
                    {
                        if (columns[i].DropDown_List.Count != 0)
                        {
                            // Columns[y].excel_col_order_name = B ---->i need it in B:B format
                            string col_name_format = alpha[i] + ":" + alpha[i];
                            var drop_down_range = xlWorkSheet.Columns[col_name_format, Type.Missing];

                            // var my_range = xlWorkSheet.Range[xlWorkSheet.Cells[1, u], xlWorkSheet.Cells[columns[i].DropDown_List.Count, u]];
                            Microsoft.Office.Interop.Excel.Range my_range = xlWorkSheet.Range[xlWorkSheet.Cells[1, u], ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[columns[i].DropDown_List.Count, u])];

                            int y = 0;
                            foreach (var item in columns[i].DropDown_List)
                            {
                                xlWorkSheet.Cells[y + 1, u] = item;
                                y++;
                            }
                            xlWorkSheet.Names.Add("Range_" + u, my_range);

                            //var flatList = string.Join(",", columns[1].DropDown_List);

                            drop_down_range.Validation.Delete();
                            drop_down_range.Validation.Add(
                               XlDVType.xlValidateList,
                               XlDVAlertStyle.xlValidAlertInformation,
                               XlFormatConditionOperator.xlBetween,
                               "=Range_" + u,
                               Type.Missing);

                            drop_down_range.Validation.InCellDropdown = true;
                        }
                    }
                    FormattingExcelCells(xlWorkSheet.Cells[1, i + 1], columns[i].color, columns[i].Font_Color, true);
                    xlWorkSheet.Columns[i + 1].ColumnWidth = columns[i].Width;
                    u++;
                }

                // now we resize the columns
                Range excelCellrange = xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;
                border.Color = 0xFFFFFF;

                xlWorkBook.SaveAs(excelPath);
                xlWorkBook.Close(true);
                xlApp.Quit();

               // releaseObject(xlApp);
               // releaseObject(xlWorkBook);
               // releaseObject(xlWorkSheet);

                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName">Name of Your Report</param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        private static bool WriteDataTableToExcel<T>(IEnumerable<T> list, string worksheetName, string saveAsLocation, string[] colum_names = null)
        {
            string ReporType = worksheetName;

            System.Data.DataTable dataTable = CreateDataTable<T>(list);
            if (colum_names != null)
            {
                dataTable = dataTable.DefaultView.ToTable(false, colum_names);
            }

            Application excel;
            Workbook excelworkBook;
            Worksheet excelSheet;
            Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                excel.Interactive = false;

                // for making Excel visible
                //excel.Visible = false;
                excel.DisplayAlerts = false;


                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;


                excelSheet.Cells[1, 1] = ReporType;
                excelSheet.Cells[1, 2] = "Date : " + HRcrud.DateTime_Now().ToShortDateString();

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


                //now save the workbook and exit Excel


                excelworkBook.SaveAs(saveAsLocation); ;
                excelworkBook.Close();
                excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }

        }

        /// <summary>
        /// FUNCTION FOR FORMATTING EXCEL CELLS
        /// </summary>
        /// <param name="range"></param>
        /// <param name="HTMLcolorCode"></param>
        /// <param name="fontColor"></param>
        /// <param name="IsFontbool"></param>
        private static void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }

        public static System.Data.DataTable CreateDataTable<T>(IEnumerable<T> list)
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

////http://www.c-sharpcorner.com/UploadFile/deveshomar/exporting-datatable-to-excel-in-C-Sharp-using-interop/
//// https://stackoverflow.com/questions/18746064/using-reflection-to-create-a-datatable-from-a-class