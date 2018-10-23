using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models.Helpers
{
    public class Course_Insert_Helper
    {
        public int CourseType                         { get; set; }
        public int[] Days                             { get; set; }
        public int[] Specializations                  { get; set; }
        public string[] Tos                           { get; set; }
        public int capacity                        { get; set; }
        public int course_duration_in_hours        { get; set; }
        public string description                     { get; set; }
        public string end_date                        { get; set; }
        public string expiration_register_date        { get; set; }
        public string[] froms                         { get; set; }
        public string name                            { get; set; }
        public double new_price                       { get; set; }
        public int no_of_days_per_week             { get; set; }
        public int no_of_hours_per_day             { get; set; }
        public double old_price                       { get; set; }
        public string start_date                      { get; set; }
    }
}