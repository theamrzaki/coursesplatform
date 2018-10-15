using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using HRsystem.Models;
using OpenXMLExcel.SLExcelUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HRsystem.Utility
{
    public class ExcelOpenXml_Utility
    {
        /// <summary>
        /// usage 
        /// byte[] file = DownloadFile(List_of_any_obj)
        /// 
        /// Response.AddHeader("Content-Disposition", "attachment; filename=ExcelFile.xlsx");
        /// return new FileContentResult(file,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List of any object</param>
        /// <returns>byte array of the excel file that is created</returns>
        public static byte[] CreateExcel<T>(IEnumerable<T> list, string[] colum_names = null)
        {
            SLExcelData Data = Convert_To_ExcelData(list, colum_names);
            var file = (new SLExcelWriter()).GenerateExcel(Data);
            return file;

          //  how it would be used
           /* Response.AddHeader("Content-Disposition",
                "attachment; filename=ExcelFile.xlsx");
            return new FileContentResult(file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");*/
        }


       
        

        public static byte[] ExcelTemplate(List<Columns_Excel> columns)
        {
            /// excel range
            /// https://forums.asp.net/t/2137906.aspx?Openxml+insert+data+from+dataset+into+Excel+Range
            /// 
            /// name range
            /// https://stackoverflow.com/questions/7279383/named-ranges-in-excel-openxml
            /// 
            /// data validation
            /// https://stackoverflow.com/questions/33886205/openxml-datavalidation-set-predefined-list-for-columns
            /// 
            
            var file = (new SLExcelWriter()).ExcelTemplate(columns);
            return file;
        }




        private static SLExcelData Convert_To_ExcelData<T>(IEnumerable<T> list , string[] colum_names=null)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            SLExcelData Data = new SLExcelData();

            if (colum_names !=null)//if specifc rows are selected , add them to headers
            {
                Data.Headers.AddRange(colum_names);
            }
            else//else ==> loop through all object's variables
            {
                foreach (PropertyInfo info in properties)
                {
                    Data.Headers.Add(info.Name);
                }
            }

            foreach (T entity in list)
            {
                //object[] values = new object[properties.Length];
                List<string> values = new List<string>();
                for (int i = 0; i < Data.Headers.Count; i++)
                {
                    try
                    {
                        values.Add(entity.GetType().GetProperty(Data.Headers[i]).GetValue(entity, null).ToString());
                       
                        //values.Add(properties[i].GetValue(entity).ToString());
                    }
                    catch (Exception)
                    {
                        values.Add("");
                    }
                }
                Data.DataRows.Add(values);
            }

            return Data;

        }


    }
}