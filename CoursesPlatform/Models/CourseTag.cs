using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class CourseTag
    {
        public  long        id              { get; set; }
        public  long        course_id       { get; set; }
        public  String      tag             { get; set; }
        public  String      soundex         { get; set; }
        public  String      source          { get; set; }

    }
}