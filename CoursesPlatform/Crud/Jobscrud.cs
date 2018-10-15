using HRsystem.Models;
using HRsystem.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static HRsystem.Models.Jobs;

namespace HRsystem.Cruds
{
    public class Jobscrud
    {
        //declare connection string  
        static string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        #region List , SelectByName ,select
        public static List<Jobs> ListAll()
        {
            List<Jobs> lst = new List<Jobs>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", "");
                com.Parameters.AddWithValue("@Name", "");
                com.Parameters.AddWithValue("@Department_ID", "");
                com.Parameters.AddWithValue("@Action", "Select");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(parse_jobs(rdr));
                }
                return lst;
            }
        }

       

        internal static Jobs SelectByName(string job_name)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", "");
                com.Parameters.AddWithValue("@Name", job_name);
                com.Parameters.AddWithValue("@Department_ID", "");
                com.Parameters.AddWithValue("@Action", "SelectByName");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Jobs dpt = parse_jobs(rdr);
                    return dpt;
                }
                return null;
            }
        }
        internal static Jobs SelectById(int job_ID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", job_ID);
                com.Parameters.AddWithValue("@Action", "SelectById");

                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                    Jobs dpt = parse_jobs(rdr);
                    return dpt;
                }
                return null;
            }
        }



        private static Jobs parse_jobs(SqlDataReader rdr)
        {
            bool return_bool = false;
            if ((Jobs.return_insentive_type_enum)Convert.ToInt32(rdr["return_insentive_type"]) == return_insentive_type_enum.no)
            {
                return_bool = false;
            }
            else
            {
                return_bool = true;
            }
            Jobs dpt = new Jobs
            {
                Id = Convert.ToInt32(rdr["Id"]),
                Name = rdr["Name"].ToString(),
                return_insentive = return_bool,
                return_insentive_type =(Jobs.return_insentive_type_enum) Convert.ToInt32( rdr["return_insentive_type"]),
                return_insentive_value = Convert.ToDouble(rdr["return_insentive_value"]),
                on_which_salary = (Jobs.on_which_salary_enum)Convert.ToInt32(rdr["on_which_salary"])
            };
            return dpt;
        }

        #endregion

        public static int Count()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Count");
                SQL_Utility.Stored_Procedure(ref com);
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.Read())
                {
                    return Convert.ToInt32(rdr[0]);
                }
                return 0;
            }
        }

        public static int Add(Jobs job)
        {
            if (!job.return_insentive)
            {
                job.return_insentive_type = Jobs.return_insentive_type_enum.no;
                job.return_insentive_value = 0;
                job.on_which_salary = Jobs.on_which_salary_enum.no;
            }
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", "");
                com.Parameters.AddWithValue("@Name", job.Name);
                com.Parameters.AddWithValue("@Department_ID", job.Department_ID);//not used at all
                com.Parameters.AddWithValue("@return_insentive_type", job.return_insentive_type);
                com.Parameters.AddWithValue("@return_insentive_value", job.return_insentive_value);
                com.Parameters.AddWithValue("@on_which_salary", job.on_which_salary);
                com.Parameters.AddWithValue("@Action", "Insert");

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        internal static int Edit(Jobs job)
        {
            if (!job.return_insentive)
            {
                job.return_insentive_type = Jobs.return_insentive_type_enum.no;
                job.return_insentive_value = 0;
                job.on_which_salary = Jobs.on_which_salary_enum.no;
            }
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", job.Id);
                com.Parameters.AddWithValue("@Name", job.Name);
                com.Parameters.AddWithValue("@return_insentive_type", job.return_insentive_type);
                com.Parameters.AddWithValue("@return_insentive_value", job.return_insentive_value);
                com.Parameters.AddWithValue("@on_which_salary", job.on_which_salary);
                com.Parameters.AddWithValue("@Action", "Update");

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public static double caluclate_return_insneive(Employees emp)
        {
            double return_insentive_from_job = 0;
            Jobs j = Jobscrud.SelectById(emp.Job_ID);
            if (!(j.return_insentive_type == return_insentive_type_enum.no))
            {
                if (j.return_insentive_type == return_insentive_type_enum.Bulk)
                {
                    return_insentive_from_job = j.return_insentive_value ;
                }
                else if (j.return_insentive_type == return_insentive_type_enum.Percentage)
                {
                    if (j.on_which_salary == on_which_salary_enum.Actual_Salary)
                    {
                        return_insentive_from_job = j.return_insentive_value * emp.Actual_Salary;
                    }
                    else if (j.on_which_salary == on_which_salary_enum.Variable_Salary)
                    {
                        return_insentive_from_job = j.return_insentive_value * emp.Variable_Salary;
                    }
                    else if (j.on_which_salary == on_which_salary_enum.Other_Salary)
                    {
                        return_insentive_from_job = j.return_insentive_value * emp.Other_Salary;
                    }
                    else if (j.on_which_salary == on_which_salary_enum.Tax_Salary)
                    {
                        return_insentive_from_job = j.return_insentive_value * emp.Taxtable_Salary;
                    }
                }
            }
            return return_insentive_from_job;
        }


        //still
        //Method for Updating Jobs record  
        public int Update(Jobs emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.Id);
                com.Parameters.AddWithValue("@Name", emp.Name);

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an Jobs  
        public static int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Jobs_InsertUpdateDeleteSelect", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                com.Parameters.AddWithValue("@Name", "");
                com.Parameters.AddWithValue("@Department_ID", "");
                com.Parameters.AddWithValue("@Action", "Delete");

                SQL_Utility.Stored_Procedure(ref com);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}