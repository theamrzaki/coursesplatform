using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class CourseTypeTag
    {
        public long     id              { get; set; }
        public string   tag             { get; set; }
        public long     course_type_id  { get; set; }
        public long     soundex         { get; set; }
        public long     source          { get; set; }
    }
}