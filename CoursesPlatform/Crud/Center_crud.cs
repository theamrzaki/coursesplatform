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
using static CoursesPlatform.Models.Enums;

namespace CoursesPlatform.Crud
{
    public class Center_crud
    {
        #region Add
        public static long add(Center center)
        {
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
                if (rdr.Read())
                {
                    return Convert.ToInt64(rdr[0]);
                }              
            }
            return 0;
        }
        #endregion

        #region Update
        public static void updateSocialMedia(Center center)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Center", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fb_page", center.fb_page);
                com.Parameters.AddWithValue("@instagram_page", center.instagram_page);
                com.Parameters.AddWithValue("@twitter_page", center.twitter_page);
                com.Parameters.AddWithValue("@linked_in_page", center.linked_in_page);
                com.Parameters.AddWithValue("@id", center.id);
                com.Parameters.AddWithValue("@Action", "UpdateSocialMedia");

                SQL_Utility.Stored_Procedure(ref com);
                com.ExecuteNonQuery();

            }

        }

        public static void updateStep(long id, RegistirationsSteps step)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Center", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@step", (int)step);

                com.Parameters.AddWithValue("@Action", "UpdateStep");

                SQL_Utility.Stored_Procedure(ref com);
                com.ExecuteNonQuery();

            }

        }
        #endregion
    }
}