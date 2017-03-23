using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Section
    {
        public Course Course { get; set; }
        public Instructor Instructor { get; set;}
        public int ID { get; set; }
        public int MaxStudents { get; set; }
        //public Term Term { get; set; }
    }
}
