﻿using System;
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

            //createStudent(new Student(8, "b", "b", "bob", "johnson", 4, 2.3f, new List<Section>()));

            //Read in students.json
            dynamic studentsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/students.json"));
            foreach (var student in studentsJSON)
            {
                JObject modelJSON = student;
                this.students.Add((Student)ModelFactory.createModelFromJson("student", modelJSON.ToString()));
            }

            //Read in instructor.json
            dynamic instructorsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/instructors.json"));
            foreach (var instructor in instructorsJSON)
            {
                JObject modelJSON = instructor;
                this.instructors.Add((Instructor)ModelFactory.createModelFromJson("instructor", modelJSON.ToString()));
            }

            //Read in admins.json
            dynamic adminJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/admins.json"));
            foreach (var admin in adminJSON)
            {
                JObject modelJSON = admin;
                this.admins.Add((Admin)ModelFactory.createModelFromJson("admin", modelJSON.ToString()));
            }

            //Read in courses.json
            dynamic coursesJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/courses.json"));
            foreach (var course in coursesJSON.courses)
            {
                JObject modelJSON = course;
                this.courses.Add((Course)ModelFactory.createModelFromJson("course", modelJSON.ToString()));
            }

            //Read in sections.json
            dynamic sectionsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/sections.json"));
            foreach (var section in sectionsJSON)
            {
                JObject modelJSON = section;
                this.sections.Add((Section)ModelFactory.createModelFromJson("section", modelJSON.ToString()));
            }

            //Read in terms.json
            dynamic termsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/terms.json"));
            foreach (var term in termsJSON)
            {
                JObject modelJSON = term;
                this.terms.Add((Term)ModelFactory.createModelFromJson("term", modelJSON.ToString()));
            }

            //Read in locations.json
            dynamic locationsJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/locations.json"));
            foreach (var location in locationsJSON)
            {
                JObject modelJSON = location;
                this.locations.Add((Location)ModelFactory.createModelFromJson("location", modelJSON.ToString()));
            }

            //Read in books.json
            dynamic booksJSON = JsonConvert.DeserializeObject(File.ReadAllText(filePath + "/jsonData/books.json"));
            foreach (var book in booksJSON)
            {
                JObject modelJSON = book;
                this.books.Add((Book)ModelFactory.createModelFromJson("book", modelJSON.ToString()));
            }

            //Update references


        }
        #region Model Getters
        //Methods for Retreiving data from API
        public Admin getAdmin(int ID)
        {
            Admin admin = null;
            if (admins.Exists(x => x.ID == ID))
            {
                admin = admins.Find(x => x.ID.Equals(ID));
            }
            return admin;
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

        public Course[] getCourseList()
        {
            return courses.ToArray();
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

        public Term getTerm(String termCode)
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
        public Book getBook(int ID)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Shared Data Tables
        //Returning arrays of Model objects from tables of IDs
        public Section[] getCourseSections(Course course)
        {
            return sections.FindAll(x => x.Course.Equals(course)).ToArray();
        }

        public Section[] getStudentSections(Student student)
        {
            foreach (Section section in sections)
            {
                foreach (Student s in section.StudentsInSection)
                {
                    if (s.ID == student.ID)
                    {

                    }
                }
            }
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
        public Student[] getSectionStudents(Section section)
        {
            List<Student> students = new List<Student>();
            foreach (String studentID in section.StudentsInSectionIDs)
            {
                students.Add(getStudent(Convert.ToInt32(studentID)));
            }
            return students.ToArray();
        }
        public Course[] getCoursePrereqs(Course course)
        {
            List<Course> courses = new List<Course>();
            foreach (String courseID in course.PrerequsiteIDs)
            {
                courses.Add(getCourse(Convert.ToInt32(courseID)));
            }
            return courses.ToArray();
        }
        public Book[] getSectionBooks(Section section)
        {
            throw new NotImplementedException();
        }
        
        #endregion


        #region Creation Methods
        //Methods for adding data to the database
        public void createCourse(Course course)
        {
            this.courses.Add(course);
        }

        public void createSection(Section section)
        {
            JObject newStudent = new JObject(
                new JProperty("ID", section.ID),
                new JProperty("COURSE_ID", section.Course.ID),
                new JProperty("TERM_ID", section.Term.ID),
                new JProperty("PROFESSOR_ID", section.Instructor.ID),
                new JProperty("CLASSROOM_ID", section.Location.ID)
             //new JProperty("location", student.LastName)
             );

            dynamic studentsJSON = JsonConvert.DeserializeObject(File.ReadAllText(this.filePath + "/jsonData/students.json"));
            JArray newJSON = studentsJSON;
            newJSON.Add(newStudent);

            File.WriteAllText(this.filePath + "/jsonData/students.json", newJSON.ToString());
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
        public void createBook(Book book)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Reference Setting
        //Methods for setting references between models
        public void setStudentReferences(Student student)
        {
            throw new NotImplementedException();
        }

        public void setInstructorReferences(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        public void setCourseReferences(Course course)
        {
            course.Prerequisites = getCoursePrereqs(course);
        }

        public void setSectionReferences(Section section)
        {
            section.StudentsInSection = getSectionStudents(section);
            section.Instructor = getInstructor(section.InstructorID);
            section.Course = getCourse(section.CourseID);
            section.Location = getLocation(section.LocationID);
        }
        #endregion


        #region Interaction Methods
        //Methods for interactions between models
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
        public void toggleCourse(int ID)
        {
            Course course = this.getCourse(ID);
            // TODO: finish when course has the availability property added in the API's schema
        }
        public void waitlistStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }
        public void withdrawStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
