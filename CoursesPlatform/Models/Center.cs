using CoursesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class Center
    {
        public long id { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string phoneno { get; set; }
        
        public string fb_page { get; set; }
        public string twitter_page { get; set; }
        public string linked_in_page { get; set; }
        public string instagram_page { get; set; }

        public DateTime date { get; set; }
        public DateTime edit_date { get; set; }
        public bool is_blocked { get; set; }
        public int step { get; set; }

        public List<Branch> branches { get; set; }

        public Center()
        {
            branches = new List<Branch>();
        }

    }
}