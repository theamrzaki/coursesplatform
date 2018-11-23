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

        public static List<Course> search_course(string query)
        {
            List<SearchToken> search_tokens = SearchToken.getTokens(query);

            List<Course> courses = new List<Course>();
            courses.AddRange(search_courses_by_course_tag(search_tokens));


            return courses;
        }

        private static List<Course> search_courses_by_course_tag(List<SearchToken> search_tokens)
        {

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
                com.Parameters.AddWithValue("@Action", "SearchCoursesByCourseTag");

                SQL_Utility.Stored_Procedure(ref com, search_soundex);

                SqlDataReader rdr = com.ExecuteReader();

                List<Course> courses = new List<Course>();
                while (rdr.Read())
                {
                    Course c = Course_crud.parse_course(rdr);
                    Branch_crud.getCourseCenteNamerAndBranchName(c);
                    c.specializations = Course_crud.getCourseSpecialization(c.course_type_id);
                    
                    courses.Add(c);
                }

                return courses;
            }

        }
        
    }
}