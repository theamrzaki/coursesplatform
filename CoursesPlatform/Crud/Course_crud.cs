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
    public class Course_crud
    {
        #region Add Course
        public static long addCourse(Course_Insert_Helper helper)
        {
            Course course = new Course
            {
                instructor_id = 0,
                branch_id = helper.branch_id,
                course_type_id = helper.CourseType,

                name = helper.name,
                description = helper.description,

                start_date = Convert.ToDateTime(helper.start_date),
                end_date = Convert.ToDateTime(helper.end_date),
                capacity = helper.capacity,

                capacity_is_visible = is_visible_enum.visible,

                old_price = helper.old_price,
                new_price = helper.new_price,

                expiration_register_date = Convert.ToDateTime(helper.expiration_register_date),
                course_duration_in_hours = helper.course_duration_in_hours,
                no_of_days_per_week = helper.no_of_days_per_week,
                no_of_hours_per_day = helper.no_of_hours_per_day,

                center_or_instructor = center_or_instructor_enum.center,
                is_visible = is_visible_enum.visible
            };
            long course_id = addCourse(course);
            int i = 0;
            foreach (int item in helper.Days)
            {
                CourseDays day = new CourseDays();
                day.course_id = course_id;
                day.day = (days_of_week_enum)item;
                day.to_time = Convert.ToDateTime( helper.Tos[i]);
                day.from_time = Convert.ToDateTime(helper.froms[i]);
                addCourseDay(day);
                i++;
            }

            foreach (int item in helper.Specializations)
            {
                addCourseSpecialization(course_id, item);
                 i++;
            }
            return course_id;
        }


        private static long addCourse(Course course)
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

        private static long addCourseDay(CourseDays courseDays)
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

        private static long addCourseSpecialization(long course_id, long specialization_id)
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





        #region Get Course Data
        public static Course getCourseByID(long id)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);

                com.Parameters.AddWithValue("@Action", "GetCourseByID");

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();

                Course course = null;
                if(rdr.Read())
                {
                    course = parse_course(rdr);
                    Branch_crud.getCourseCenteNamerAndBranchName(course);
                }
                
                return course;
            }

        }
        #endregion


        #region Helper

        #region Get Course Days
        private static List<CourseDays> getCourseDaysByCourseID(long course_id)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@course_id", course_id);

                com.Parameters.AddWithValue("@Action", "GetCourseDays");

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();

                return parse_courseDays(rdr);
            }

        }
        #endregion

        #region Get Course Specialization
        private static List<Specialization> getCourseSpecialization(long course_id)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Course", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@course_id", course_id);

                com.Parameters.AddWithValue("@Action", "GetSpecializationForCourse");

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();

                return Specialization_CRUD.parse_specializations(rdr);
            }

        }
        #endregion


        private static Course parse_course(SqlDataReader rdr)
        {
            Course course = new Course();
            course.id = Convert.ToInt64(rdr["course_id"]);
            try
            {
                course.instructor_id = Convert.ToInt64(rdr["instructor_id"]);
            }
            catch (Exception)
            {
                course.instructor_id = 0;
            }
            try
            {
                course.branch_id = Convert.ToInt64(rdr["branch_id"]);
            }
            catch (Exception)
            {
                course.branch_id = 0;
            }
            try
            {
                course.course_type_id = Convert.ToInt64(rdr["course_type_id"]);

            }
            catch (Exception)
            {
                course.course_type_id = 0;
            }


            course.name = rdr["course_name"].ToString();
            try
            {
                course.description = Convert.ToString( rdr["description"]);
            }
            catch (Exception)
            {
                course.description = "";
            }
            
            try
            {
                course.start_date = Convert.ToDateTime(rdr["start_date"]);
            }
            catch (Exception)
            {
                course.start_date = DateTime.Now;
            }
            try
            {
                course.end_date = Convert.ToDateTime(rdr["end_date"]);
            }
            catch (Exception)
            {
                course.end_date = DateTime.Now;
            }

            try
            {
                course.capacity = Convert.ToInt32(rdr["capacity"]);
            }
            catch (Exception)
            {
                course.capacity = 0;
            }

            try
            {
                course.capacity_is_visible = (is_visible_enum)Convert.ToInt32(rdr["capacity_is_visible"]);
            }
            catch (Exception)
            {

                course.capacity_is_visible = is_visible_enum.not_visible;
            }

            try
            {
                course.old_price = Convert.ToDouble(rdr["old_price"]);
            }
            catch (Exception)
            {
                course.old_price = 0;
            }

            try
            {
                course.new_price = Convert.ToDouble(rdr["new_price"]);
            }
            catch (Exception)
            {
                course.new_price = 0;
            }

            try
            {
                course.expiration_register_date = Convert.ToDateTime(rdr["expiration_register_date"]);
            }
            catch (Exception)
            {
                course.expiration_register_date = DateTime.Now;
            }

            try
            {
                course.course_duration_in_hours = Convert.ToInt32(rdr["course_duration_in_hours"]);
            }
            catch (Exception)
            {
                course.course_duration_in_hours = 0;
            }

            try
            {
                course.no_of_days_per_week = Convert.ToInt32(rdr["no_of_days_per_week"]);
            }
            catch (Exception)
            {
                course.no_of_days_per_week = 0;
            }

            try
            {
                course.no_of_hours_per_day = Convert.ToInt32(rdr["no_of_hours_per_day"]);
            }
            catch (Exception)
            {
                course.no_of_hours_per_day = 0;
            }

            try
            {
                course.date_created = Convert.ToDateTime(rdr["date_created"]);
            }
            catch (Exception)
            {
                course.date_created = DateTime.Now;
            }

            try
            {
                course.edit_date = Convert.ToDateTime(rdr["edit_date"]);
            }
            catch (Exception)
            {
                course.edit_date = DateTime.Now;
            }

            try
            {
                course.center_or_instructor = (center_or_instructor_enum) Convert.ToInt32(rdr["center_or_instructor"]);
            }
            catch (Exception)
            {
                course.center_or_instructor = center_or_instructor_enum.center;
            }

            try
            {
                course.is_visible = (is_visible_enum)Convert.ToInt32(rdr["is_visible"]);
            }
            catch (Exception)
            {
                course.is_visible = is_visible_enum.visible;
            }

            try
            {
                course.courseTypeName = Convert.ToString(rdr["course_type_name"]);
            }catch(Exception ex)
            {
                course.courseTypeName = "";
            }
            

            course.courseDays       = getCourseDaysByCourseID(course.id);
            course.specializations  = getCourseSpecialization(course.id);
           
            return course;
        }

        private static List<CourseDays> parse_courseDays(SqlDataReader rdr)
        {
            List<CourseDays> courseDaysList = new List<CourseDays>();

            while (rdr.Read())
            {
                CourseDays courseDays = new CourseDays();
                courseDays.id = Convert.ToInt64(rdr["id"]);
                courseDays.course_id = Convert.ToInt64(rdr["course_id"]);
                courseDays.day = (days_of_week_enum)Convert.ToInt64(rdr["day"]);
                try
                {
                    courseDays.from_time = Convert.ToDateTime(rdr["from_time"]);
                }
                catch (Exception)
                {
                    courseDays.from_time = DateTime.Now;
                }

                try
                {
                    courseDays.to_time = Convert.ToDateTime(rdr["to_time"]);
                }
                catch (Exception)
                {
                    courseDays.to_time = DateTime.Now;
                }

                courseDaysList.Add(courseDays);
            }

            return courseDaysList;
        }


        #endregion
    }

}