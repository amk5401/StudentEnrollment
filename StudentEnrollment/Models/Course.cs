using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Course : Model
    {
        public Course(int id, String courseCode, String name, int credits, int minGPA, bool availability)
        {
            ID = id;
            CourseCode = courseCode;
            Name = name;
            Credits = credits;
            MinGPA = minGPA;
            Availability = availability;
        }

        public int ID { get; }
        public String CourseCode { get; }
        public String Name { get; }
        public int Credits { get; }

        // TODO: refactor GPA to double
        public int MinGPA { get; }
        public bool Availability { get;  }
        public override string ToString()
        {
            string val = String.Format("Course #{0}: {1}, {2} \nCredits {3} \nMinGPA {4}",
                                 this.ID, this.CourseCode, this.Name, this.Credits, this.MinGPA);
            if (Availability)
            {
                return val + "\nCurrently available";
            }
            return val + "\nNot currently available";
        }
        public override bool Equals(System.Object obj)
        {
            var course = obj as Course;
            if (course == null) return false;
            if (course.ID == this.ID && 
                course.CourseCode == this.CourseCode && 
                course.Name == this.Name &&
                course.Credits == this.Credits && 
                course.MinGPA == this.MinGPA && 
                course.Availability == this.Availability)
            {
                return true;
            }
            else return false;
        }

    }
}
