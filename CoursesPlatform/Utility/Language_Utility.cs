using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace HRsystem.Utility
{
    public class Language_Utility
    {
        public static Dictionary<string, string> Load_Action(string Controller, string Action,string lang)
        {
            Dictionary<string, string> lang_dict = new Dictionary<string, string>();
            //string virtual_path = "Resources\\Lang.xml";
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, virtual_path);


            string sqlStatement = string.Empty;

            string resourceName = "HRsystem.Resources.Lang.xml";

            using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stm != null)
                {
                    sqlStatement = new StreamReader(stm).ReadToEnd();
                }
            }

            XmlDocument doc = new XmlDocument();
            try { doc.LoadXml(sqlStatement); }
            catch (Exception ex) { }

            XmlElement root = doc.DocumentElement;

            XmlNodeList nodes = root.SelectNodes("//controller[@name='" + Controller + "']/action[@name='" + Action + "']/element");
            foreach (XmlNode n in nodes)
            {
                if (lang=="a")
                {
                    lang_dict.Add(n.Attributes["id"].Value, n.Attributes["arabic"].Value);
                }
                else
                {
                    lang_dict.Add(n.Attributes["id"].Value, n.Attributes["english"].Value);
                }
            }
            
            return lang_dict;
        }
    }
}