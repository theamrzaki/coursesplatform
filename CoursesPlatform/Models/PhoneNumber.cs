using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class PhoneNumber
    {
        public long id { get; set; }
        public long fk_id { get; set; }
        public int table_type { get; set; }
        public string phone_number { get; set; }
        public int phone_type { get; set; }
    }
}