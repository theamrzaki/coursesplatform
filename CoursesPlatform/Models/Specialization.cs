using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class Specialization
    {
        public long id { get; set; }
        public String name { get; set; }

        public List<SpecializationTag> specializationTags { get; set; }

        public Specialization()
        {
            specializationTags = new List<SpecializationTag>();
        }
    }
}