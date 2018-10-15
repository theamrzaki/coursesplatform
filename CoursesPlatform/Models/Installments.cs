using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static HRsystem.Models.Enums;

namespace HRsystem.Models
{
    public class Installments
    {
        public int Id { get; set; }

        public int Emp_id { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public double Actual_Salary { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Installment_State_enum Installment_State { get; set; }
        public Installment_Begin_enum When_To_Begin { get; set; }

        public int Type { get; set; }
        public string Type_Name { get; set; }
        
        public Request.State_enum State { get; set; }
        public double Value { get; set; }
        public double Month_Value { get; set; }
        public int Num_Months { get; set; }
        public DateTime Assign_Date { get; set; }
        public DateTime Begin_Date { get; set; }
        public List<Installments_Months> Months { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        //------------Helpers--------------------//
        public Multiple_Type_enum Multiple_Type { get; set; }
        public int Comp_ID { get; set; }
        public int Dept_ID { get; set; }
        public int[] Emps_ID { get; set; }


    }
}