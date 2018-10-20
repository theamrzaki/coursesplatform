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

        #region getBranchesByCenterID
        public static List<Branch> getBranchesByCenterID(long centerID)
        {
            using (SqlConnection con = new SqlConnection(Database.connection_string))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Branch", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@center_id", centerID);

                com.Parameters.AddWithValue("@Action", "GetBranchByCenterID");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();

                return parse_Branches(rdr);

            }

        }

        #endregion

        #region Helper
        private static List<Branch> parse_Branches(SqlDataReader rdr)
        {
            List<Branch> branches = new List<Branch>();

            while (rdr.Read())
            {
                Branch branch = new Branch();
                branch.id = Convert.ToInt64(rdr["id"]);
                branch.name = rdr["name"].ToString();
                branch.center_id = Convert.ToInt64(rdr["center_id"]);
                try
                {
                    branch.lat = Convert.ToDouble("lat");
                }
                catch (Exception)
                {
                    branch.lat = 0;
                }

                try
                {
                    branch.lng = Convert.ToDouble("lng");
                }
                catch (Exception)
                {
                    branch.lng = 0;
                }
                branch.address = rdr["address"].ToString();
                try
                {
                    branch.date = Convert.ToDateTime("date") ;
                }
                catch (Exception)
                {

                }

                try
                {
                    branch.edit_date = Convert.ToDateTime("edit_date");
                }
                catch (Exception)
                {

                }

                int is_blocked = Convert.ToInt32(rdr["is_blocked"]);
                branch.is_blocked = (is_blocked == 1) ? true : false;

                branches.Add(branch);
            }
            return branches;
        }
        #endregion


    }
}