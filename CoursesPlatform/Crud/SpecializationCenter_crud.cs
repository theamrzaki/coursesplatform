﻿using CoursesPlatform.Models.Helpers;
using HRsystem.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Crud
{
    public class SpecializationCenter_crud
    {
        #region Add
        public static long add(long center_id, long specialization_id)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SpecializationCenter", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@center_id", center_id);
                com.Parameters.AddWithValue("@specialization_id", specialization_id);
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
    }
}