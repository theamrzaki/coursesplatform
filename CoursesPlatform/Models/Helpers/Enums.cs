using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class Enums
    {
        #region Courses_Platform
        public enum PhonesTableType
        {
            Center = 0,
            Branch = 1
        }

        public enum PhonesTypes
        {
            Mobile = 0,
            Home = 1,
            Work = 2
        }

        public enum UsersTypes
        {
            CenterAdmin = 0,
            BranchAdmin = 1
        }

        public enum RegistirationsSteps
        {
            Step1 = 0,
            Step2 = 1,
            Step3 = 2,
            Step4 = 3,
            Completed = 4
        }
        
        public enum center_or_instructor_enum
        {
            instructor = 0,
            center = 1
        }

        public enum is_visible_enum
        {
            not_visible = 0,
            visible = 1
        }

        public enum days_of_week_enum
        {
            Sunday          = 1,
            Monday          = 2,
            Tuesday         = 3,
            Wednesday       = 4,
            Thursday        = 5,
            Friday          = 6,
            Saturday        = 7
        }

        #endregion

        #region Installment
        #region Installments Object
        public enum Installment_State_enum
        {
            Still_Paying = 0,
            Finished_Paying = 1
        }

        public enum Installment_Begin_enum
        {
            This_Month = 0,
            Coming_Month = 1
        }
        #endregion

        #region Helpers
        public enum Multiple_Type_enum
        {
            Company = 0 ,
            Department = 1 ,
            Multiple_Employees_in_company =2
        }
        public enum Override_Checks_enum
        {
            No = 0,
            Yes =1
        }
        #endregion

        #region Installment Type
        public enum Max_Fixed_enum
        {
            Max = 0,
            Fixed = 1
        }
        public enum Specific_General_enum
        {
            Specific = 0,
            General = 1
        }
        public enum Limit_Or_Not_enum
        {
            Limit = 0,
            No_Limit = 1
        }
        #endregion

        #region Installment Limit
        public enum User_Type_enum
        {
            Employee = 0,
            Manager = 1
        }
        public enum Open_Closed_enum
        {
            Open = 0,
            Closed = 1
        }
        public enum Bulk_Percentage_enum
        {
            Bulk = 0,
            Percentage = 1
        }
        #endregion
        #endregion
    }
}