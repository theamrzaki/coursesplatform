using CoursesPlatform.Models;
using CoursesPlatform.Models.Helpers;
using HRsystem.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Crud
{
    public class User_crud
    {
        #region Add
        public static long add(User user, int type)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("User", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@username", user.user_name);
                com.Parameters.AddWithValue("@password", user.password);
                com.Parameters.AddWithValue("@type", type);
                com.Parameters.AddWithValue("@Action", "Insert");

                SQL_Utility.Stored_Procedure(ref com);
                id = com.ExecuteNonQuery();

            }
            return id;
        }
        #endregion
    }
}