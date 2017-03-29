using System;
using System.Collections.Generic;
using StudentEnrollment.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;
using Newtonsoft.Json;

namespace StudentEnrollment.Proxy
{
    public class LocalProxy : Proxy
    {
        List<Student> students = new List<Student>();
        List<Admin> admins = new List<Admin>();
        List<Instructor> instructors = new List<Instructor>();

        List<Book> books = new List<Book>();
        List<Course> courses = new List<Course>();
        List<Section> sections = new List<Section>();
        List<Term> terms = new List<Term>();

        Dictionary<Student, List<Section>> studentClasses = new Dictionary<Student, List<Section>>();
        Dictionary<Section, List<Book>> sectionBooks = new Dictionary<Section, List<Book>>();

        public LocalProxy(String filePath)
        {
            dynamic initialJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/models.json"));
            foreach (var item in initialJSON)
            {
                foreach(var course in item.courses)
                {
                    //int[] prereqs = {0,0 };
                    //courses.Add(new Course(course.id, course.courseCode, course.name, course.credits, course.minGPA, prereqs));
                    Console.WriteLine("Creating course: " + course.name);
                }
                foreach (var student in item.students)
                {
                    //students.Add(new Student(student.id, student.username, student.email, student.firstName, student.lastName, student.yearLevel, student.gpa, student.enrolledSections));
                    Console.WriteLine("Creating student: " + student.name);
                }
                foreach(var loc in item.locations)
                {

                }
            }

        }

        //public void createBook(Book book)
        //{
        //    this.books.Add(book);
        //}

        public void createCourse(Course course)
        {
            this.courses.Add(course);
        }

        public void createSection(Section section)
        {
            this.sections.Add(section);
        }

        public void createStudent(Student student)
        {
            this.students.Add(student);
        }

        public void createTerm(Term term)
        {
            this.terms.Add(term);
        }

        public void enrollStudent(Student student, Section section)
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

        public Admin getAdmin(int ID)
        {
            Admin admin = null;
            if (admins.Exists(x => x.ID == ID))
            {
                admin = admins.Find(x => x.ID.Equals(ID));
            }
            return admin;
        }

        //public Book getBook(int ID)
        //{
        //    Book book = null;
        //    if (books.Exists(x => x.ID == ID))
        //    {
        //        book = books.Find(x => x.ID.Equals(ID));
        //    }
        //    return book;
        //}

        public Course getCourse(int ID)
        {
            Course course = null;
            if (courses.Exists(x => x.ID == ID))
            {
                course = courses.Find(x => x.ID.Equals(ID));
            }
            return course;
        }

        public Course[] getCourseList()
        {
            return courses.ToArray();
        }

        public void toggleCourse(int ID)
        {
            Course course = this.getCourse(ID);
            // TODO: finish when course has the availability property added in the API's schema
        }

        public Section getSection(int ID)
        {
            Section section = null;
            if (sections.Exists(x => x.ID == ID))
            {
                section = sections.Find(x => x.ID.Equals(ID));
            }
            return section;
        }

        public Section[] getCourseSections(Course course)
        {
            return sections.FindAll(x => x.Course.Equals(course)).ToArray();
        }

        public Section[] getStudentSections(Student student)
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

        public Section[] getInstructorSections(Instructor instructor)
        {
            return sections.FindAll(x => x.Instructor.Equals(instructor)).ToArray();
        }

        //public Book[] getSectionBooks(Section section)
        //{
        //    if (sectionBooks.ContainsKey(section))
        //    {
        //        return sectionBooks[section].ToArray();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public Location getLocation(int ID)
        {
            throw new NotImplementedException();
        }

        public Term getCurrentTerm()
        {
            if (terms.Exists(x => (x.StartDate <= DateTime.Now) && (DateTime.Now <= x.EndDate)))
            {
                return terms.Find(x => (x.StartDate <= DateTime.Now) && (DateTime.Now <= x.EndDate));
            }
            else
            {
                return null;
            }
        }

        public Term getTerm(String ID)
        {
            throw new NotImplementedException();
        }

        public void waitlistStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }

        public void withdrawStudent(Student student, Section section)
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

        public Book getBook(int ID)
        {
            throw new NotImplementedException();
        }

        public void createBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book[] getSectionBooks(Section section)
        {
            throw new NotImplementedException();
        }
    }
}
