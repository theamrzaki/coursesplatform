using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class CourseType
    {
        public long id           { get; set; }
        public string name       { get; set; }
        public List<CourseTypeTag> courseTypeTags { get; set; }

        public CourseType()
        {
            courseTypeTags = new List<CourseTypeTag>();
        }
    }
}