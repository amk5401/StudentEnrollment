using System;
using System.Collections.Generic;
using StudentEnrollment.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        List<Location> locations = new List<Location>();

        Dictionary<Student, List<Section>> studentClasses = new Dictionary<Student, List<Section>>();
        Dictionary<Section, List<Book>> sectionBooks = new Dictionary<Section, List<Book>>();

        String filePath;

        public LocalProxy(String filePath)
        {
            this.filePath = filePath;

            createStudent(new Student(8, "b", "b", "bob", "johnson", 4, 2.3f, new List<Section>()));

            //Read in students.json
            dynamic studentsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/students.json"));
            foreach (var student in studentsJSON)
            {
                this.students.Add((Student)ModelFactory.createModelFromJson("student", student.ToString()));
            }

            //Read in instructor.json
            dynamic instructorsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/instructors.json"));
            foreach (var instructor in instructorsJSON)
            {
                this.students.Add((Student)ModelFactory.createModelFromJson("instructor", instructor.ToString()));
            }

            //Read in admins.json
            dynamic adminJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/admins.json"));
            foreach (var admin in adminJSON)
            {
                this.admins.Add((Admin)ModelFactory.createModelFromJson("admin", admin.ToString()));
            }

            //Read in courses.json
            dynamic coursesJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/courses.json"));
            foreach (var course in coursesJSON.courses)
            {
                this.courses.Add((Course)ModelFactory.createModelFromJson("course", course.ToString()));
            }

            //Read in sections.json
            dynamic sectionsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/sections.json"));
            foreach (var section in sectionsJSON)
            {
                this.sections.Add((Section)ModelFactory.createModelFromJson("section", section.ToString()));
            }

            //Read in terms.json
            dynamic termsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/terms.json"));
            foreach (var term in termsJSON)
            {
                this.terms.Add((Term)ModelFactory.createModelFromJson("term", term.ToString()));
            }

            //Read in locations.json
            dynamic locationsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/locations.json"));
            foreach (var location in locationsJSON)
            {
                this.locations.Add((Location)ModelFactory.createModelFromJson("location", location.ToString()));
            }

            //Read in books.json
            dynamic booksJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/books.json"));
            foreach (var book in booksJSON)
            {
                this.books.Add((Book)ModelFactory.createModelFromJson("book", book.ToString()));
            }

            //Update references


        }

        /*public void createBook(Book book)
        {
            this.books.Add(book);
        }*/

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
            JObject newStudent = new JObject(
                new JProperty("id", student.ID),
                new JProperty("username", student.Username),
                new JProperty("email", student.Email),
                new JProperty("firstName", student.FirstName),
                new JProperty("lastName", student.LastName),
                new JProperty("yearLevel", student.YearLevel),
                new JProperty("gpa", student.GPA),
                new JProperty("enrolledSections", student.EnrolledSections)
             );

            dynamic studentsJSON = JsonConvert.DeserializeObject(File.ReadAllText(this.filePath + "/jsonData/students.json"));
            JArray newJSON = studentsJSON;
            newJSON.Add(newStudent);

            File.WriteAllText(this.filePath + "/jsonData/students.json", newJSON.ToString());

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

        public Term getTerm(int ID)
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
