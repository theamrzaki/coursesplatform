using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class Branch
    {
        public long id { get; set; }
        public long center_id { get; set; }
        public string name { get; set; }
        public string abbrev { get; set; }
        public string address { get; set; }
        public string phoneno { get; set; }

        public double lat { get; set; }
        public double lng { get; set; }

        public DateTime date { get; set; }
        public DateTime edit_date { get; set; }
        public bool is_blocked { get; set; }

        public int courses { get; set; }
        public int users { get; set; }


        public string color { get; set; }

    }
}