using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class User
    {
        public long id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public int type { get; set; }
    }
}