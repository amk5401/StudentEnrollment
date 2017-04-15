using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Course : Model
    {
        public Course() {}
        public Course(int id, String courseCode, String name, int credits, int minGPA, bool availability)
        {
            ID = id;
            CourseCode = courseCode;
            Name = name;
            Credits = credits;
            MinGPA = minGPA;
            Availability = availability;
        }

        public int ID { get; set; }
        public String CourseCode { get; set; }
        public String Name { get; set; }
        public int Credits { get; set; }

        // TODO: refactor GPA to double
        public int MinGPA { get; set; }
        public bool Availability { get; set; }
        public override string ToString()
        {
            string val = String.Format("Course #{0}: {1}, {2} \nCredits {3} \nMinGPA {4}",
                this.ID,
                this.CourseCode,
                this.Name,
                this.Credits,
                this.MinGPA);
            if (Availability)
            {
                return val + "\nCurrently available";
            }
            return val + "\nNot currently available";
        }


    }
}
