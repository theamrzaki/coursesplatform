using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CoursesPlatform.Models.Enums;

namespace CoursesPlatform.Models
{
    public class Course
    {
        public long             id                             { get; set; }
        public long             instructor_id                  { get; set; }
        public long             branch_id                      { get; set; }
        public long             course_type_id                 { get; set; }
        public string           name                           { get; set; }
        public string           description                    { get; set; }
        public DateTime         start_date                     { get; set; }
        public DateTime         end_date                       { get; set; }
        public int              capacity                       { get; set; }
        public is_visible_enum capacity_is_visible             { get; set; }
        public Double           old_price                      { get; set; }
        public Double           new_price                      { get; set; }
        public DateTime         expiration_register_date       { get; set; }
        public int              course_duration_in_hours       { get; set; }
        public int              no_of_days_per_week            { get; set; }
        public int              no_of_hours_per_day            { get; set; }
        public DateTime         date_created                   { get; set; }
        public DateTime         edit_date                      { get; set; }
        public center_or_instructor_enum center_or_instructor  { get; set; }
        public is_visible_enum  is_visible                     { get; set; }

        public List<CourseDays> courseDays { get; set; }
        public List<Specialization> specializations { get; set; }

        public Course()
        {
            courseDays = new List<CourseDays>();
            specializations = new List<Specialization>();
        }
    }
}