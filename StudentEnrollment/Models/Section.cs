﻿using System;
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
        public Course Course { get; set; }
        public int InstructorID { get; }
        public int ID { get; }
        public int MaxStudents { get; }
        public int TermID { get; }
        public int LocationID { get; }
        public bool Availability { get; }
    }
}