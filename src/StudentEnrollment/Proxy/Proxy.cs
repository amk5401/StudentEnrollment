﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;

namespace StudentEnrollment.Proxy
{
    interface Proxy
    {
        Student getStudent(int ID);
        void createStudent(Student student);
        Instructor getInstructor(int ID);
        Admin getAdmin(int ID);
        Course getCourse(int ID);
        void createCourse(Course course);
        Course[] getCourseList();
        void toggleCourse(int ID);
        Section getSection(int ID);
        Section[] getCourseSections(Course course);
        void createSection(Section section);
        //HashSet<Section, Grade> getStudentGrades(Student student);
        Section[] getStudentSections(Student student);
        Student[] getSectionStudents(Section section);
        Section[] getInstructorSections(Instructor student);
        //Book getBook(int ID); R2
        //void createBook(Book book); R2
        //Book[] getSectionBooks(Section section); R2
        Location getLocation(int ID);
        Term getCurrentTerm();
        Term getTerm(int ID);
        void createTerm(Term term);
        void enrollStudent(Student student, Section section);
        void waitlistStudent(Student student, Section section);
        void withdrawStudent(Student student, Section section);
    }
}
