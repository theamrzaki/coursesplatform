using CoursesPlatform.Models;
using CoursesPlatform.Models.Helpers;
using HRsystem.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Crud
{
    public class Center_crud
    {
        #region Add
        public static long Add(Center center)
        {
            long i = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Center", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@name", center.name);
                com.Parameters.AddWithValue("@about", center.about);
                com.Parameters.AddWithValue("@email", center.email);
                com.Parameters.AddWithValue("@website", center.website);
                com.Parameters.AddWithValue("@Action", "InsertPrimary");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    i = Convert.ToInt64(rdr[0]);
                }
            }
            return i;
        }
        #endregion
    }
}