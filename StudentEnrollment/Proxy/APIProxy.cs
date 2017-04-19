﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using StudentEnrollment.Models;
using Newtonsoft.Json;

namespace StudentEnrollment.Proxy
{
    public class APIProxy : IProxy
    {
        static HttpClient client = new HttpClient();
        private static String API_URL = "http://vm344f.se.rit.edu/API/API.php";

        static async Task<String> GetFromAPI(string uri)
        {
            using (client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        static async Task<String> PostToAPI(string uri, Dictionary<String, String> postParemeters)
        {
            var postData = new List<KeyValuePair<string, string>>();
            foreach (String key in postParemeters.Keys)
            {
                postData.Add(new KeyValuePair<string, string>(key, postParemeters[key]));
            }
            HttpContent content = new FormUrlEncodedContent(postData);
            using (client = new HttpClient())
            {
                var response = client.PostAsync(uri, content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        #region Model Getters
        //Methods for Retreiving data from API
        public Admin getAdmin(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=general&function=getAdmin&adminID={1}", API_URL, ID)).Result;
            Admin admin = (Admin)ModelFactory.createModelFromJson("admin", json);
            return admin;
        }

        public Book getBook(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=book_store&function=getBook&bookID={1}", API_URL, ID)).Result;
            Book book = (Book)ModelFactory.createModelFromJson("book", json);
            return book;
        }

        public Course getCourse(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=general&function=getCourse&courseID={1}", API_URL, ID)).Result;
            Course course = (Course)ModelFactory.createModelFromJson("course", json);

            return course;
        }

        public Course[] getCourseList()
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getCourseList", API_URL)).Result;
            Course[] courses = (Course[])ModelFactory.createModelArrayFromJson("course", json);

            return courses;
        }

        public Term getCurrentTerm()
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getTerms", API_URL)).Result;
            List<Term> terms = ((Term[])ModelFactory.createModelArrayFromJson("term", json)).ToList();
            DateTime currentDate = DateTime.Now;
            Term currentTerm = null;
            if (terms.Exists(x => x.StartDate <= currentDate && x.EndDate >= currentDate))
            {
                currentTerm = terms.Find(x => x.StartDate <= currentDate && x.EndDate >= currentDate);
            }
            return currentTerm;
        }
        public Instructor getInstructor(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getProfessorUser&userID={1}", API_URL, ID)).Result;
            Instructor instructor = (Instructor)ModelFactory.createModelFromJson("instructor", json);
            return instructor;
        }
        public Location getLocation(int ID) // TODO: Wait for a getRoom location in the API
        {
            throw new NotImplementedException();
        }
        public Section getSection(int ID)
        {
            String json = GetFromAPI(API_URL + "?team=student_enrollment&function=getSection&sectionID=" + ID).Result;
            Section section = (Section)ModelFactory.createModelFromJson("section", json);

            return section;
        }
        public Student getStudent(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getStudentUser&userID={1}", API_URL, ID)).Result;
            Student student = (Student)ModelFactory.createModelFromJson("student", json);

            return student;
        }
        public Term getTerm(String termCode)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getTerm&termCode={1}", API_URL, termCode)).Result;
            Term term = (Term)ModelFactory.createModelFromJson("term", json);
            return term;
        }
        public Term[] getTerms()
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getTerms", API_URL)).Result;
            Term[] terms = (Term[])ModelFactory.createModelArrayFromJson("term", json);
            return terms;
        }
        #endregion

        #region Shared Data Tables
        //Returning arrays of Model objects from tables of IDs
        public Student[] getSectionStudents(Section section)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getSectionEnrolled&sectionID={1}", API_URL, section.ID)).Result;
            String[] studentIDs = ModelFactory.createIDListFromJson("student", json);
            List<Student> students = new List<Student>();
            foreach (String studentID in studentIDs)
            {
                students.Add(getStudent(Convert.ToInt32(studentID)));
            }
            return students.ToArray();
        }

        public Course[] getCoursePrereqs(Course course)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getPrereqs&courseID={1}", API_URL, course.ID)).Result;
            String[] courseIDs = ModelFactory.createIDListFromJson("course", json);
            List<Course> courses = new List<Course>();
            foreach (String courseID in courseIDs)
            {
                courses.Add(getCourse(Convert.ToInt32(courseID)));
            }
            return courses.ToArray();
        }

        public Section[] getCourseSections(Course course)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getCourseSections&courseID={1}", API_URL, course.ID)).Result;
            Section[] sections = (Section[])ModelFactory.createModelArrayFromJson("section", json);

            return sections;
        }

        public Section[] getInstructorSections(Instructor instructor)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getProfessorSections&professorID={1}", API_URL, instructor.ID)).Result;
            Section[] sections = (Section[])ModelFactory.createModelArrayFromJson("section", json);

            return sections;
        }
        public Section[] getStudentSections(Student student)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=getStudentSections&studentID={1}", API_URL, student.ID)).Result;
            String[] ids = ModelFactory.createIDListFromJson("section", json);
            List<Section> sections = new List<Section>();
            foreach (String str in ids)
            {
                sections.Add(this.getSection(Convert.ToInt32(str)));
            }
            return sections.ToArray();
        }
        public Book[] getSectionBooks(Section section) // TODO: Make sure the function doesn't change to getSectionBook(S). Making assumption this is singular
        {
            //String json = GetFromAPI(String.Format("{0}?team=book_store&function=getSectionBook&sectionID={1}", API_URL, section.ID)).Result;
            //Book book = (Book)ModelFactory.createModelFromJson("book", json);
            //return book;
            throw new NotImplementedException();
        }
        #endregion

        #region Creation Methods
        //Methods for adding data to the database
        public void createBook(Book book) // TODO: Waiting on the bookstore team for parameters
        {
            throw new NotImplementedException();
        }

        public void createCourse(Course course)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("courseCode", course.CourseCode);
            postData.Add("courseName", course.Name);
            postData.Add("credits", Convert.ToString(course.Credits));
            postData.Add("minGPA", Convert.ToString(course.MinGPA));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=general&function=postCourse", API_URL), postData).Result;
            // TODO: See what comes back from JSON
        }

        public void createSection(Section section)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("courseID", Convert.ToString(section.CourseID));
            postData.Add("professorID", Convert.ToString(section.InstructorID));
            postData.Add("maxStudents", Convert.ToString(section.MaxStudents));
            postData.Add("termID", Convert.ToString(section.TermID));
            postData.Add("classroomID", Convert.ToString(section.LocationID));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=postSection", API_URL), postData).Result;
            // TODO: See what comes back from JSON
        }

        /**
         * createStudent assumes that the User object attached to the Student object has already been registered within the database.
         * */
        public void createStudent(Student student)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("userID", Convert.ToString(student.ID));
            postData.Add("yearLevel", Convert.ToString(student.YearLevel));
            postData.Add("gpa", Convert.ToString(student.GPA));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=general&function=postStudent", API_URL), postData).Result;
            // TODO: See what comes back from JSON
        }

        public void createTerm(Term term)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("termCode", term.Code);
            postData.Add("startDate", (term.StartDate).ToString("YYYY-mm-dd"));
            postData.Add("endDate", (term.EndDate).ToString("YYYY-mm-dd"));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=postTerm", API_URL), postData).Result;
            // TODO: See what comes back from JSON
        }
        #endregion

        #region Update Methods
        //Methods for updating entities within the API

        /**
         * If goal is to update the availability, use the toggleCourse method.
         */
        public void updateCourse(Course course)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("courseCode", course.CourseCode);
            postData.Add("courseName", course.Name);
            postData.Add("credits", Convert.ToString(course.Credits));
            postData.Add("minGPA", Convert.ToString(course.MinGPA));
            postData.Add("id", Convert.ToString(course.ID));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=updateCourse", API_URL), postData).Result;
        }

        /**
         * If goal is to update the availability, use the toggleSection method.
         */
        public void updateSection(Section section)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("id", Convert.ToString(section.ID));
            postData.Add("courseID", Convert.ToString(section.CourseID));
            postData.Add("professorID", Convert.ToString(section.InstructorID));
            postData.Add("maxStudents", Convert.ToString(section.MaxStudents));
            postData.Add("termID", Convert.ToString(section.TermID));
            postData.Add("classroomID", Convert.ToString(section.LocationID));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=updateSection", API_URL), postData).Result;
            // TODO: See what comes back from JSON
        }

        #endregion

        #region Interaction Methods
        //Methods for interactions between models
        public void enrollStudent(Student student, Section section)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("studentID", Convert.ToString(student.ID));
            postData.Add("sectionID", Convert.ToString(section.ID));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=enrollStudent", API_URL), postData).Result;
            // TODO: See what comes back from JSON
        }
        public void toggleCourse(int ID) // Make sure this is what is supposed to be in the API
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=toggleCourse&courseID={1}", API_URL, ID)).Result;
        }

        public void toggleSection(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}?team=student_enrollment&function=toggleSection&sectionID={1}", API_URL, ID)).Result;
        }

        public void waitlistStudent(Student student, Section section)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("studentID", Convert.ToString(student.ID));
            postData.Add("sectionID", Convert.ToString(section.ID));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=waitlistStudent", API_URL), postData).Result;
            // See what comes back from the API
        }

        public void withdrawStudent(Student student, Section section)
        {
            Dictionary<String, String> postData = new Dictionary<string, string>();
            postData.Add("studentID", Convert.ToString(student.ID));
            postData.Add("sectionID", Convert.ToString(section.ID));
            String json = APIProxy.PostToAPI(String.Format("{0}?team=student_enrollment&function=withdrawStudent", API_URL), postData).Result;
            // See what comes back from the API
        }
        #endregion
    }
}
