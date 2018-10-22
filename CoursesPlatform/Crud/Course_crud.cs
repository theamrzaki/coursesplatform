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
    public class Course_crud
    {
        #region Add
        public static long add(Course course)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
              /*  com.Parameters.AddWithValue("@name", center.name);
                com.Parameters.AddWithValue("@about", center.about);
                com.Parameters.AddWithValue("@email", center.email);
                com.Parameters.AddWithValue("@website", center.website);
                com.Parameters.AddWithValue("@Action", "Insert");*/

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                    id = Convert.ToInt64(rdr[0]);
                }
               // PhoneNumber_crud.add(PhonesTableType.Center, id, center.phoneno, PhonesTypes.Mobile);
            }
            return id;
        }
        #endregion
    }
}