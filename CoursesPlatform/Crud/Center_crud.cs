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
        public static long add(Center center)
        {
            long id = 0;
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
                id = com.ExecuteNonQuery();
              
            }
            return id;
        }
        #endregion

        #region UpdateSocialMedia
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
        #endregion

        #region UpdateStep
        public static void updateStep(long id , int step)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Center", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@step", step);
            
                com.Parameters.AddWithValue("@Action", "UpdateStep");

                SQL_Utility.Stored_Procedure(ref com);
                com.ExecuteNonQuery();

            }

        }
        #endregion
    }
}