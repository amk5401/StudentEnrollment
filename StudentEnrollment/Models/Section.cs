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
        public int CourseID { get; }
        public int InstructorID { get; }
        public int ID { get; }
        public int MaxStudents { get; }
        public int TermID { get; }
        public int LocationID { get; }
        public bool Availability { get; }

        public override string ToString()
        {
            string val = String.Format("Section #{0}: \nMaximum Students: {1} \nCourse ID: {2} \nTerm ID: {3} \nInstructor ID: {4} \nLocation ID: {5}",
                this.ID, this.MaxStudents, this.CourseID, this.TermID, this.InstructorID, this.LocationID);
            if (Availability)
            {
                return val + "\nCurrently available";
            }
            return val + "\nNot currently available";
        }
        public override bool Equals(System.Object obj)
        {
            var section = obj as Section;
            if (section == null) return false;
            if (section.ID == this.ID &&
                section.MaxStudents == this.MaxStudents &&
                section.TermID == this.TermID &&
                section.InstructorID == this.InstructorID &&
                section.CourseID == this.CourseID &&
                section.LocationID == this.LocationID &&
                section.Availability == this.Availability)
            {
                return true;
            }
            else return false;
        }
    }
}
