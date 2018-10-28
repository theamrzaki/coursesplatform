using CoursesPlatform.Models;
using CoursesPlatform.Models.Helpers;
using CoursesPlatform.Utility.SearchUtility;
using HRsystem.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Crud
{
    public class CenterTag_crud
    {
        #region Add
        public static void add(long center_id , string input)
        {
            List<SearchToken> searchTokens = SearchToken.getTokens(input);
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                foreach (SearchToken st in searchTokens)
                { 
                    SqlCommand com = new SqlCommand("CenterTag", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@center_id", center_id);
                    com.Parameters.AddWithValue("@tag", st.tag);
                    com.Parameters.AddWithValue("@soundex", st.soundex);
                    com.Parameters.AddWithValue("@source", st.source);
                    com.Parameters.AddWithValue("@Action", "Insert");

                    SQL_Utility.Stored_Procedure(ref com);

                    com.ExecuteNonQuery();
                }
            }
            
        }
        #endregion
    }
}