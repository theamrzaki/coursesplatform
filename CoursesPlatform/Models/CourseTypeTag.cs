using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class CourseTypeTag
    {
        public long id                  { get; set; }
        public string name              { get; set; }
        public long course_type_id      { get; set; }
    }
}