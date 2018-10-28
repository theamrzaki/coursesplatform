using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class SpecializationCourseType
    {
        public long id                  { get; set; }
        public long course_type_id      { get; set; }
        public long specialization_id   { get; set; }
    }
}