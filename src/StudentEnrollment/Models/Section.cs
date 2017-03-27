using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Section : Model
    {
        public Section(int id, int maxStudents, Term term, Instructor instructor, Course course, Location location, List<Student> studentsInSection)
        {
            ID = id;
            MaxStudents = maxStudents;
            Term = term;
            Course = course;
            Instructor = instructor;
            Location = location;
            StudentsInSection = studentsInSection;
        }

        public Course Course { get; }
        public Instructor Instructor { get; }
        public int ID { get; }
        public int MaxStudents { get; }
        public Term Term { get; }
        public Location Location { get; }
        public List<Student> StudentsInSection { get; }
    }
}
