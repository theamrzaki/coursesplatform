using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CoursesPlatform.Models.Enums;

namespace CoursesPlatform.Models
{
    public class CourseDays
    {
       public long  id                  { get; set; }
       public long  course_id           { get; set; }
       public days_of_week_enum day     { get; set; }
       public DateTime  from_time       { get; set; }
       public DateTime to_time          { get; set; }
    }
}