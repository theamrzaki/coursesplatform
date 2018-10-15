using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;

namespace HRsystem.Utility
{
    public class Logs_Utility
    {
        public static void Save_Object(object obj , string action)
        {
            string virtual_path = "Logs\\"+action+".xml";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, virtual_path);
            
            string json = " " + action + "  : ";
            json += JsonConvert.SerializeObject(obj);

            //write string to file
            System.IO.File.AppendAllText(path, json);
        }


    }
}