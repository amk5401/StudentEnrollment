using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class LocalProxy : Proxy
    {
        List<Student> students = new List<Student>();
        List<Admin> admins = new List<Admin>();
        List<Instructor> instructors = new List<Instructor>();

        public List<Book> books = new List<Book>();
        List<Course> courses = new List<Course>();
        List<Section> sections = new List<Section>();

        public void createBook(Book book)
        {
            this.books.Add(book);
        }

        List<Term> terms = new List<Term>();

        Dictionary<Student, List<Section>> studentClasses = new Dictionary<Student, List<Section>>();
        Dictionary<Section, List<Book>> sectionBooks = new Dictionary<Section, List<Book>>();

        void Proxy.createCourse(Course course)
        {
            this.courses.Add(course);
        }

        void Proxy.createSection(Section section)
        {
            this.sections.Add(section);
        }

        void Proxy.createStudent(Student student)
        {
            this.students.Add(student);
        }

        void Proxy.createTerm(Term term)
        {
            this.terms.Add(term);
        }

        void Proxy.deleteSection(Section section)
        {
            this.sections.Remove(section);
        }

        void Proxy.enrollStudent(Student student, Section section)
        {
            if (studentClasses.ContainsKey(student))
            {
                studentClasses[student].Add(section);
            }
            else
            {
                studentClasses.Add(student, new List<Section> { section });
            }
        }

        Admin Proxy.getAdmin(int ID)
        {
            Admin admin = null;
            if (admins.Exists(x => x.ID == ID))
            {
                admin = admins.Find(x => x.ID.Equals(ID));
            }
            return admin;
        }

        Book Proxy.getBook(int ID)
        {
            Book book = null;
            if (books.Exists(x => x.ID == ID))
            {
                book = books.Find(x => x.ID.Equals(ID));
            }
            return book;
        }

        public Course getCourse(int ID)
        {
            Course course = null;
            if (courses.Exists(x => x.ID == ID))
            {
                course = courses.Find(x => x.ID.Equals(ID));
            }
            return course;
        }

        Course[] Proxy.getCourseList()
        {
            return courses.ToArray();
        }

        void Proxy.toggleCourse(int ID)
        {
            Course course = this.getCourse(ID);
            // TODO: finish when course has the availability property added in the API's schema
        }

        Section Proxy.getSection(int ID)
        {
            Section section = null;
            if (sections.Exists(x => x.ID == ID))
            {
                section = sections.Find(x => x.ID.Equals(ID));
            }
            return section;
        }

        Section[] Proxy.getCourseSections(Course course)
        {
            return sections.FindAll(x => x.Course.Equals(course)).ToArray();
        }

        Section[] Proxy.getStudentSections(Student student)
        {
            if (studentClasses.ContainsKey(student))
            {
                return studentClasses[student].ToArray();
            }
            else
            {
                return null;
            }
        }

        Section[] Proxy.getInstructorSections(Instructor instructor)
        {
            return sections.FindAll(x => x.Instructor.Equals(instructor)).ToArray();
        }

        Book[] Proxy.getSectionBooks(Section section)
        {
            if (sectionBooks.ContainsKey(section))
            {
                return sectionBooks[section].ToArray();
            }
            else
            {
                return null;
            }
        }

        Location Proxy.getLocation(int ID)
        {
            throw new NotImplementedException();
        }

        Term Proxy.getCurrentTerm()
        {
            throw new NotImplementedException();
        }

        Term Proxy.getTerm(int ID)
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

        public Student getStudent(int ID)
        {
            throw new NotImplementedException();
        }

        public Instructor getInstructor(int ID)
        {
            throw new NotImplementedException();
        }

        public Student[] getSectionStudents(Section section)
        {
            throw new NotImplementedException();
        }
    }
}
