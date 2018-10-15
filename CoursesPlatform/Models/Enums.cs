﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRsystem.Models
{
    public class Enums
    {
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