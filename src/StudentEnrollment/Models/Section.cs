using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Section : Model
    {
        public Section(int id, int maxStudents, int termID, int instructorID, int courseID, int locationID, bool availability)
        {
            ID = id;
            MaxStudents = maxStudents;
            TermID = termID;
            CourseID = courseID;
            InstructorID = instructorID;
            LocationID = locationID;
            Availability = availability;
        }
        //LocalProxy Constructor
        public Section(int id, int maxStudents, int termID, int instructorID, int courseID, int locationID, bool availability, String[] studentsInSectionIDs)
        {
            ID = id;
            MaxStudents = maxStudents;
            TermID = termID;
            CourseID = courseID;
            InstructorID = instructorID;
            LocationID = locationID;
            Availability = availability;
            StudentsInSectionIDs = studentsInSectionIDs;
        }
        public int CourseID { get; }
        public Course Course { get; set; }
        public int InstructorID { get; }
        public Instructor Instructor { get; set; }
        public int ID { get; }
        public int MaxStudents { get; }
        public int TermID { get; }
        public Term Term { get; set; }
        public int LocationID { get; }
        public Location Location { get; set; }
        public String[] StudentsInSectionIDs { get; }
        public Student[] StudentsInSection { get; set; }
        public bool Availability { get; }
        public Book[] Books { get; set; }
    }
}
