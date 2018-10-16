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
    public class PhoneNumber_crud
    {
        #region Add
        public static long add(int table_type, int fk_id,string phone_number,int phone_type)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("PhoneNumber", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@table_type", table_type);
                com.Parameters.AddWithValue("@fk_id", fk_id);
                com.Parameters.AddWithValue("@phone_number", phone_number);
                com.Parameters.AddWithValue("@phone_type", phone_type);
                com.Parameters.AddWithValue("@Action", "Insert");

                SQL_Utility.Stored_Procedure(ref com);
                id = com.ExecuteNonQuery();

            }
            return id;
        }
        #endregion
    }
}