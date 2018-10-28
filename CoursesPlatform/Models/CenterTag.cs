using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class CenterTag
    {
        public long     id          { get; set; }
        public long     center_id   { get; set; }
        public string   tag         { get; set; }
        public string   soundex     { get; set; }
        public string   source      { get; set; }
    }
}