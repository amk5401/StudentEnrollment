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
        public int MinGPA { get; }
        public bool Availability { get;  }
        // TODO: Add in availability property ASAP


    }
}
