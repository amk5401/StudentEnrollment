using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Student : User
    {
        public Student(int id, String username, String email, String firstName, String lastName, int yearLevel, float gpa) 
            : base(id, username, email, firstName, lastName){
            YearLevel = yearLevel;
            GPA = gpa;
            //EnrolledSections = enrolledSections;
        }
        public int YearLevel { get; set; }
        public float GPA { get; set; }
        public Section[] EnrolledSections { get; set; }
    }
}
