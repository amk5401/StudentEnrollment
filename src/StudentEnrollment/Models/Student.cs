using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Student : User
    {
        public int YearLevel { get; set; }
        public float GPA { get; set; } 
    }
}
