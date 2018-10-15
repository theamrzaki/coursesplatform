using HRsystem.Cruds;
using HRsystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HRsystem.Utility
{
    public class Permissions_Utility
    {
        //declare connection string  
        static string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        #region get( PageID_by_Name , Page_By_Id , Permissions_Of_Emp , Get_Remaining_Pages )
        
        public static int Get_PageID(string page_name)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Page_Name", page_name);
                com.Parameters.AddWithValue("@Action", "Get_PageID");
                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    return Convert.ToInt32(rdr[0]);
                }
                return 0;
            }
        }

        public static Pages Get_Page_By_Id(int Page_ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Page_ID", Page_ID);

                com.Parameters.AddWithValue("@Action", "Get_Page_By_Id");
                SQL_Utility.Stored_Procedure(ref com);
                //i = com.ExecuteNonQuery();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Pages p = new Pages
                    {
                        Page_Name = rdr["Page_Name"].ToString(),
                        Id = Convert.ToInt32(rdr["Id"]),
                        Group_Name = rdr["Group_Name"].ToString(),
                        Description = rdr["Description"].ToString(),
                    };
                    return p;
                }
            }
            return null;
        }

        internal static List<Pages> Get_Remaining_Pages(int Emp_Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                List<Pages> pers = new List<Pages>();

                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Emp_ID", Emp_Id);

                com.Parameters.AddWithValue("@Action", "Get_Remaining_Pages");
                SQL_Utility.Stored_Procedure(ref com);
                // i = com.ExecuteNonQuery();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    pers.Add(new Pages
                    {
                        Page_Name = rdr["Page_Name"].ToString(),
                        Id = Convert.ToInt32(rdr["Id"]),
                        Group_Name = rdr["Group_Name"].ToString(),
                        Description = rdr["Description"].ToString(),
                    });
                }
                return pers;
            }
            return null;
        }

        public static List<Permissions> Get_Permissions_Of_Emp(int Emp_Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                List<Permissions> pers = new List<Permissions>();

                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Emp_ID", Emp_Id);

                com.Parameters.AddWithValue("@Action", "Get_Permissions_Of_Emp");
                SQL_Utility.Stored_Procedure(ref com);

               // i = com.ExecuteNonQuery();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    pers.Add(new Permissions
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Emp_ID = Convert.ToInt32(rdr["Emp_ID"]),
                        Page_ID = Convert.ToInt32(rdr["Page_ID"]),
                        Emp_Name = Employeescrud.select_no_department(Convert.ToInt32(rdr["Emp_ID"])).Name,
                        Page_Name = Get_Page_By_Id(Convert.ToInt32(rdr["Page_ID"])).Page_Name,
                    });
                }
                return pers;
            }
            return null;
        }

        #endregion

        #region add roles
        //add role to emp
        public static int Add_Role(int Emp_Id , string Page_Name)
        {
            int Page_ID = Get_PageID(Page_Name);
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Emp_ID", Emp_Id);
                com.Parameters.AddWithValue("@Page_ID", Page_ID);

                com.Parameters.AddWithValue("@Action", "Add_Role");

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public static int Add_Role(int Emp_Id, int Page_ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Emp_ID", Emp_Id);
                com.Parameters.AddWithValue("@Page_ID", Page_ID);

                com.Parameters.AddWithValue("@Action", "Add_Role");
                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return i;
        }


        #region default_roles_definitions
        static string[] employee_roles = 
         {
            "Employees_edit_self_password",
            "Employees_details_self_profile",

            "Vacations_request",
            "Vacations_details_self",

            "Missions_complete_assigned",
            "Missions_details_self",

            "Roles_index_self"
        };

        static string[] manager_roles =
        {
            "Employees_index_department",
            "Employees_edit_department",
            "Employees_edit_self_password",
            "Employees_details_department",
            "Employees_export",


            "ArrivalTime_index_department",
            "ArrivalTime_delay_department",
            "ArrivalTime_export",


            "Vacations_index_department",
            "Vacations_reply_assigned",
            "Vacations_export",
            "Vacations_details_department",


            "Missions_index_department",
            "Missions_assign",
            "Missions_export",
            "Missions_details_department",

            "Payroll_index_department",

            "Roles_index_self"
        };

        static string[] hr_emp_roles =
        {
           "Employees_index_all",
           "Employees_edit",
           "Employees_edit_self_password",
           "Employees_details",
           "Employees_export",
           "Employees_Add",
           "Employees_import",
           "Employees_delete",
           
           "Companies_Index",
           "Companies_Add",

           "Departments_Index",
           "Departments_Add",
           "Departments_Edit",

           "Jobs_index",
           "Jobs_Add",

           "ArrivalTime_index_all",
           "ArrivalTime_delay_all",
           "ArrivalTime_import",
           "ArrivalTime_export",

           "Vacations_index_all",
           "Vacations_export",
           "Vacations_details_all",

           "Missions_index_all",
           "Missions_export",
           "Missions_details_all",

           "Payroll_index_all",

           "Settings_index",
           "Settings_edit",

           "Roles_index_self",
           "Roles_index_all"
        };

        static string[] hr_global_roles =
        {
           "Employees_index_all",
           "Employees_index_hr_employees",
           "Employees_edit",
           "Employees_edit_self_password",
           "Employees_details",
           "Employees_export",
           "Employees_Add",
           "Employees_import",
           "Employees_delete",

           "Companies_Index",
           "Companies_Add",

           "Departments_Index",
           "Departments_Add",
           "Departments_Edit",

           "Jobs_index",
           "Jobs_Add",

           "ArrivalTime_index_all",
           "ArrivalTime_delay_all",
           "ArrivalTime_import",
           "ArrivalTime_export",

           "Vacations_index_all",
           "Vacations_export",
           "Vacations_details_all",

           "Missions_index_all",
           "Missions_export",
           "Missions_details_all",

           "Payroll_index_all",

           "Settings_index",
           "Settings_edit",
           "Settings_hr_specific",

           "Roles_index_self",
           "Roles_index_all",
           "Roles_edit",

           "Logs_index"
        };
        #endregion

        #region initialize roles
        public static void initialize_emp (int Emp_Id)
        {
            delete_existing_roles(Emp_Id);

            foreach (var role in employee_roles)
            {
                Add_Role(Emp_Id, role);
            }
        }
        
        public static void initialize_manager(int Emp_Id)
        {
            delete_existing_roles(Emp_Id);

            foreach (var role in manager_roles)
            {
                Add_Role(Emp_Id, role);
            }
        }

        public static void initialize_hr_emp(int Emp_Id)
        {
            delete_existing_roles(Emp_Id);
            
            foreach (var role in hr_emp_roles)
            {
                Add_Role(Emp_Id, role);
            }
        }

        public static void initialize_hr_global(int Emp_Id)
        {
            delete_existing_roles(Emp_Id);
            
            foreach (var role in hr_global_roles)
            {
                Add_Role(Emp_Id, role);
            }
        }
        #endregion

        #endregion



        #region (delete_existing_roles --private--- , Remove_Role)
        private static int delete_existing_roles(int emp_Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Emp_ID", emp_Id);

                com.Parameters.AddWithValue("@Action", "delete_existing_roles");

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return 0;
        }
        
        internal static int Remove_Role(int permission_id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", permission_id);

                com.Parameters.AddWithValue("@Action", "Remove_Role");

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return 0;
        }
        #endregion


        #region See_If_Can_Access (by page --private--   , by list --public--)
        private static bool See_If_Can_Access(int Emp_Id, string Page_Name)
        {
            int Page_ID = Get_PageID(Page_Name);

            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Permissions_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Emp_ID", Emp_Id);
                com.Parameters.AddWithValue("@Page_ID", Page_ID);

                com.Parameters.AddWithValue("@Action", "See_If_Can_Access");
                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permissions is list of type Permissions
        /// </summary>
        public static bool See_If_Can_Access(object Permissions, string Page_Name)
        {
            if (Permissions != null)
            {
                List<Permissions> pers = (List<Permissions>)Permissions;

                foreach (var item in pers)
                {
                    if (item.Page_Name == Page_Name)
                    {
                        return true;
                    }
                }
                return false; 
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}