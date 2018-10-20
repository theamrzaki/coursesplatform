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

                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                    id = Convert.ToInt64(rdr[0]);
                }
                PhoneNumber_crud.add(PhonesTableType.Center,id,center.phoneno,PhonesTypes.Mobile);     
            }
            return id;
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

        #region GetByCenterID

        public static Center getCenterByID(long centerID)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Center", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", centerID);
                com.Parameters.AddWithValue("@Action", "GetCenterByID");

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();
                Center center = new Center();
                if (rdr.Read())
                {
                    center = parse_center(rdr);

                }

                center.branches = Branch_crud.getBranchesByCenterID(centerID);

                return center;
            }

        }

        #endregion

        #region Helper
        private static Center parse_center(SqlDataReader rdr)
        {
            Center center = new Center();
            center.id = Convert.ToInt64(rdr["id"]);
            center.name = rdr["name"].ToString();
            center.about = rdr["about"].ToString();
            center.email = rdr["email"].ToString();
            center.website = rdr["website"].ToString();
            center.fb_page = rdr["fb_page"].ToString();
            center.instagram_page = rdr["instagram_page"].ToString();
            center.twitter_page = rdr["twitter_page"].ToString();
            center.linked_in_page = rdr["linked_in_page"].ToString();

            try
            {
                center.date = Convert.ToDateTime(rdr["date"]);
            }catch(Exception ex)
            {
                center.date = DateTime.Now;
            }

            try
            {
                center.edit_date = Convert.ToDateTime(rdr["edit_date"]);
            }
            catch (Exception ex)
            {
                center.edit_date = DateTime.Now;
            }

            int is_blocked = Convert.ToInt32(rdr["is_blocked"]);
            center.is_blocked = (is_blocked == 1 ) ? true : false;

            center.step = Convert.ToInt32(rdr["step"]);
            return center;
        }
        #endregion
    }
}