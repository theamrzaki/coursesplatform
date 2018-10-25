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
    public class Specialization_CRUD
    {
        #region Get Specialization Tags

        public static List<SpecializationTag> getSpecializationTags(long specialization_id)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Specialization", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@specialization_id", specialization_id);
                com.Parameters.AddWithValue("@Action", "GetSpecializationTags");

                SQL_Utility.Stored_Procedure(ref com);

                SqlDataReader rdr = com.ExecuteReader();
              
                return parse_specialization_tags(rdr);
            }

        }

        #endregion

        #region Helpers

        public static List<Specialization> parse_specializations(SqlDataReader rdr)
        {
            List<Specialization> specializationsList = new List<Specialization>();

            while (rdr.Read())
            {
                Specialization specialization = new Specialization();
                specialization.id = Convert.ToInt64(rdr["id"]);
                specialization.name = rdr["name"].ToString();
                specialization.specializationTags = getSpecializationTags(specialization.id);

                specializationsList.Add(specialization);
            }

            return specializationsList;
        }

        private static List<SpecializationTag> parse_specialization_tags(SqlDataReader rdr)
        {
            List<SpecializationTag> specializationTagList = new List<SpecializationTag>();

            while (rdr.Read())
            {
                SpecializationTag specializationTag = new SpecializationTag();
                specializationTag.id = Convert.ToInt64("id");
                specializationTag.specialization_id = Convert.ToInt64("specialization_id");
                specializationTag.name = rdr["name"].ToString();


                specializationTagList.Add(specializationTag);
            }

            return specializationTagList;
        }

        #endregion

    }
}