using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace HRsystem.Utility
{
    public class SQL_Utility
    {
        internal static void Stored_Procedure(ref SqlCommand com, List<string> query_list = null)
        {
           string Procedure = com.CommandText;
            string Action = "";
            try
            {
                Action = com.Parameters["@Action"].Value.ToString();
            }
            catch (Exception)
            {
                Action = Procedure;
            }

            if (query_list == null)
            {
                com.CommandText = Stored_Procedure(Procedure, Action, com.Parameters);
            }
            else
            {
                com.CommandText = Stored_Procedure(Procedure, Action, com.Parameters, query_list);
            }
            com.CommandType = System.Data.CommandType.Text;
        }

        private static string Stored_Procedure(string Procedure, string action, SqlParameterCollection parameters , List<string> query_list = null)
        {
            //string virtual_path = "StoredProcedure\\" + Procedure + ".xml";
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, virtual_path);

            string sqlStatement = string.Empty;
            
            string resourceName = "CoursesPlatform.StoredProcedure" + "." + Procedure+".xml";
            
            using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stm != null)
                {
                    sqlStatement = new StreamReader(stm).ReadToEnd();
                }
            }


            XmlDocument doc = new XmlDocument();
            try { doc.LoadXml(sqlStatement/*path*/); }
            catch (Exception ex) { }
            //XmlElement root = doc.DocumentElement;
            //String strOriginalString = "";

        

            XmlNode node = doc.SelectSingleNode("//action[@name='" + action + "']");
            string inner_text = node.InnerText;
            try
            {
                if (node.Attributes["type"].Value == "optional")
                {
                    XmlNode node_declares = doc.SelectSingleNode("//declares");
                    string node_declares_string = node_declares.InnerText;

                    List<string> lines = node_declares_string.Split('\n').ToList();
                    lines = lines.Where(s => !string.IsNullOrWhiteSpace(s)).Select(r => string.Concat("declare ", r)).ToList();

                    List<int> indexs = new List<int>();
                    foreach (var item in parameters)
                    {
                        int i = 0;
                        foreach (var li in lines)
                        {
                            var pattern = item.ToString() + @"\b";
                            var result = Regex.IsMatch(li, pattern); // returns false

                            if (result)
                            {
                                indexs.Add(i);
                            }
                            i++;
                        }
                    }
                    foreach (var item in indexs)
                    {
                        lines[item] = "--" + lines[item];
                    }
                    return string.Join("", lines.ToArray()) + inner_text;
                }
            }
            catch { }
            try { 
                if (node.Attributes["search"].Value == "multiple" && query_list!=null)
                {
                    //string commandText = "DELETE FROM Students WHERE name IN ({0})";
                    string[] paramNames = query_list.Select(
                        (s, i) => "@tag" + i.ToString()
                    ).ToArray();

                    string inClause = string.Join(",", paramNames);

                    return string.Format(inner_text, inClause);
                }
            }
            catch (Exception){ }

            return node.InnerText;
        }
        ///read from embedded resource 
        ///https://stackoverflow.com/questions/576571/where-do-you-put-sql-statements-in-your-c-sharp-projects
        ///
        ///embedded resource
        ///https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file
    }
}