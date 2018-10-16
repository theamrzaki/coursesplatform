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
    public class Branch_crud
    {
       
        #region Add
        public static long add(Branch branch)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Branch", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@center_id", branch.center_id);
                com.Parameters.AddWithValue("@name", branch.name );
                com.Parameters.AddWithValue("@address", branch.address);

                com.Parameters.AddWithValue("@Action", "Insert");

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                    id = Convert.ToInt64(rdr[0]);
                }
                PhoneNumber_crud.add(PhonesTableType.Branch, id, branch.phoneno, PhonesTypes.Mobile);

            }
            return id;
        }
        #endregion

        #region UpdateLocation
        public static void updateLocation(long id, double lat,double lng)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Branch", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@lat", lat);
                com.Parameters.AddWithValue("@lng", lng);

                com.Parameters.AddWithValue("@Action", "UpdateLocation");

                SQL_Utility.Stored_Procedure(ref com);
                com.ExecuteNonQuery();

            }

        }
        #endregion


    }
}