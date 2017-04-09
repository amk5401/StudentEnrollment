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
        }
        public int YearLevel { get; set; }
        public float GPA { get; set; }

        public override string ToString()
        {
            return String.Format("Student #{0}: {1} {2} \nUsername: {3} \nEmail: {4} \nYear: {5} \nGPA: {6} ", 
                this.ID, 
                this.FirstName, 
                this.LastName, 
                this.Username, 
                this.Email, 
                this.YearLevel, 
                this.GPA);
        }
    }
    
}
