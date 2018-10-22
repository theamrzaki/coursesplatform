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
        #region Add Course
        public static long addCourse(Course course)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@instructor_id", course.instructor_id);
                com.Parameters.AddWithValue("@branch_id", course.branch_id);
                com.Parameters.AddWithValue("@course_type_id", course.course_type_id);
                com.Parameters.AddWithValue("@name", course.name);
                com.Parameters.AddWithValue("@description", course.description);
                com.Parameters.AddWithValue("@start_date", course.start_date);
                com.Parameters.AddWithValue("@end_date", course.end_date);
                com.Parameters.AddWithValue("@capacity", course.capacity);
                com.Parameters.AddWithValue("@capacity_is_visible", (int)course.capacity_is_visible);
                com.Parameters.AddWithValue("@old_price", course.old_price);
                com.Parameters.AddWithValue("@new_price", course.new_price);
                com.Parameters.AddWithValue("@expiration_register_date", course.expiration_register_date);
                com.Parameters.AddWithValue("@course_duration_in_hours", course.course_duration_in_hours);
                com.Parameters.AddWithValue("@no_of_days_per_week", course.no_of_days_per_week);
                com.Parameters.AddWithValue("@no_of_hours_per_day", course.no_of_hours_per_day);
                com.Parameters.AddWithValue("@center_or_instructor", (int)course.center_or_instructor);
                com.Parameters.AddWithValue("@is_visible", (int)course.is_visible);

                com.Parameters.AddWithValue("@Action", "InsertCourse");

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

        #region Add Course Day
        public static long addCourseDay(CourseDays courseDays)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@course_id", courseDays.course_id);
                com.Parameters.AddWithValue("@day", courseDays.day);
                com.Parameters.AddWithValue("@from_time", courseDays.from_time);
                com.Parameters.AddWithValue("@to_time", courseDays.to_time);
               

                com.Parameters.AddWithValue("@Action", "InsertCourseDay");

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

        #region Add Course Specialization
        public static long addCourseSpecialization(long course_id , long specialization_id)
        {
            long id = 0;
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@course_id", course_id);
                com.Parameters.AddWithValue("@specialization_id", specialization_id);

                com.Parameters.AddWithValue("@Action", "InsertSpecializationCourse");

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