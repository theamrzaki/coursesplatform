using CoursesPlatform.Models;
using CoursesPlatform.Models.Helpers;
using HRsystem.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static CoursesPlatform.Models.Enums;

namespace CoursesPlatform.Crud
{
    public class User_crud
    {
        #region Add
        public static long add(User user)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("User", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@center_id", user.center_id);
                com.Parameters.AddWithValue("@username", user.user_name);
                com.Parameters.AddWithValue("@password", MD5_create("Mostafa_We" + user.password + "Alaa_Amr_we_zaki_we_Mina"));
                com.Parameters.AddWithValue("@type", user.type);
                com.Parameters.AddWithValue("@Action", "Insert");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                    id = Convert.ToInt64(rdr[0]);
                }
            }
            return id;
        }
        #endregion

        #region Login
        public static User Login(User user)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("User", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@username", user.user_name);
                com.Parameters.AddWithValue("@password", MD5_create("Mostafa_We" + user.password + "Alaa_Amr_we_zaki_we_Mina"));
                com.Parameters.AddWithValue("@Action", "Login");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                  return  parse_user(rdr);
                }
            }
            return null;
        }
        #endregion

        #region Helpers
        private static User parse_user(SqlDataReader rdr)
        {
            User usr = new User();
            usr.id = Convert.ToInt64(rdr["id"]);
            usr.user_name = rdr["username"].ToString();
            usr.type = Convert.ToInt32(rdr["type"]);
            usr.center_id = Convert.ToInt64(rdr["center_id"]);

            return usr;
        }

        public static string MD5_create(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.MD5.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        } 
        #endregion
    }
}