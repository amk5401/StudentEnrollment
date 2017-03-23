using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class LocalProxy : Proxy
    {
        void Proxy.createBook(Book book)
        {
            throw new NotImplementedException();
        }

        void Proxy.createCourse(Course course)
        {
            throw new NotImplementedException();
        }

        void Proxy.createSection(Section section)
        {
            throw new NotImplementedException();
        }

        void Proxy.createStudent(Student student)
        {
            throw new NotImplementedException();
        }

        void Proxy.createTerm(Term term)
        {
            throw new NotImplementedException();
        }

        void Proxy.deleteSection(Section section)
        {
            throw new NotImplementedException();
        }

        void Proxy.enrollStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }

        Admin Proxy.getAdmin(int ID)
        {
            throw new NotImplementedException();
        }

        Book Proxy.getBook(int ID)
        {
            throw new NotImplementedException();
        }

        Course Proxy.getCourse(int ID)
        {
            throw new NotImplementedException();
        }

        Course[] Proxy.getCourseList()
        {
            throw new NotImplementedException();
        }

        Section[] Proxy.getCourseSections(Course course)
        {
            throw new NotImplementedException();
        }

        Term Proxy.getCurrentTerm()
        {
            throw new NotImplementedException();
        }

        Instructor Proxy.getInstructor(int ID)
        {
            throw new NotImplementedException();
        }

        Section[] Proxy.getInstructorSections(Instructor student)
        {
            throw new NotImplementedException();
        }

        Room Proxy.getRoom(int ID)
        {
            throw new NotImplementedException();
        }

        Section Proxy.getSection(int ID)
        {
            throw new NotImplementedException();
        }

        Book[] Proxy.getSectionBooks(Section section)
        {
            throw new NotImplementedException();
        }

        Student Proxy.getStudent(int ID)
        {
            throw new NotImplementedException();
        }

        Section[] Proxy.getStudentSections(Student student)
        {
            throw new NotImplementedException();
        }

        Term Proxy.getTerm(int ID)
        {
            throw new NotImplementedException();
        }

        void Proxy.toggleCourse(int ID)
        {
            throw new NotImplementedException();
        }

        void Proxy.waitlistStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }

        void Proxy.withdrawStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }
    }
}
