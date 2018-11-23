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
    public class Search_crud
    {
        public static List<int> search_center_tag(string query)
        {
            List<SearchToken> search_tokens = SearchToken.getTokens(query);

            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Search", con);
                com.CommandType = CommandType.StoredProcedure;
                int i = 0;
                List<string> search_soundex = new List<string>();
                foreach (var item in search_tokens)
                {
                    com.Parameters.AddWithValue("@tag"+i, item.soundex);
                    search_soundex.Add(item.soundex);
                    i++;
                }
                com.Parameters.AddWithValue("@Action", "SearchCenterTag");

                SQL_Utility.Stored_Procedure(ref com, search_soundex);

                SqlDataReader rdr = com.ExecuteReader();
                Center center = null;
                List<int> centers_id = new List<int>();
                while (rdr.Read())
                {
                    centers_id.Add( Convert.ToInt32(rdr[0]));
                }

                return centers_id;
            }

        }
        
    }
}