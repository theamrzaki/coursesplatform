using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class SpecializationCourse
    {
        public long id                  { get; set; }
        public long course_id           { get; set; }
        public long specialization_id   { get; set; }
    }
}