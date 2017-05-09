using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;

namespace StudentEnrollment.Proxy
{
    interface IProxy
    {
        //Model Getters
        //Methods for retreiving data from API
        Book getBook(int ID);
        Course[] getCourseList();
        Location getLocation(int ID);
        Term getCurrentTerm();
        Term getTerm(String ID);
        Term[] getTerms();
        Student getStudent(int ID);
        Instructor getInstructor(int ID);
        Admin getAdmin(int ID);
        Course getCourse(int ID);
        Section getSection(int ID);


        //Shared Data Tables
        //Returning arrays of Model objects from tables of IDs
        Section[] getStudentSections(Student student);
        Student[] getSectionStudents(Section section);
        Section[] getInstructorSections(Instructor student);
        Section[] getCourseSections(Course course);
        Book[] getSectionBooks(Section section);
        Course[] getCoursePrereqs(Course course);


        //Creation Methods
        //Methods for adding data to the database
        void createBook(Book book, String authorFirstName, String authorLastName, String publisher);
        void createTerm(Term term);
        int createSection(Section section);
        void createCourse(Course course);
        bool createStudent(Student student, string password);

        //Update Methods
        //Methods for updating existing entities within the database
        void updateCourse(Course course); // Not meant to update availability
        void updateSection(Section section); // Not meant to update availability

        //Interaction 
        //Methods for interactions between models
        void enrollStudent(Student student, Section section);
        void waitlistStudent(Student student, Section section);
        void withdrawStudent(Student student, Section section);
        void toggleCourse(int ID);
        void toggleSection(int ID);

        // Delete Methods
        bool deleteUser(int userID);
    }
}
