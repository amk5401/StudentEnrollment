using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Course
    {
        public int ID { get; set; }
        public String CourseCode { get; set; }
        public String Name { get; set; }
        public int Credits { get; set; } 
        public int MinGPA { get; set; }
    }
}
