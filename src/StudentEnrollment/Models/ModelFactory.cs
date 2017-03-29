using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;
using Newtonsoft.Json;

namespace StudentEnrollment.Models
{
    public static class ModelFactory
    {
        public static Model createModelFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals("")) return null;

            Model model = null;
            switch (modelType)
            {
                case "student": model = createStudent(json); break;
                case "instructor": model = createInstructor(json); break;
                case "admin": model = createAdmin(json); break;
                case "course": model = createCourse(json); break;
                case "section": model = createSection(json); break;
                case "book": model = createBook(json); break;
                case "term": model = createTerm(json); break;
            }
            return model;
        }

        public static Model[] createModelArrayFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals("")) return null;

            Model[] models = null;
            switch (modelType)
            {
                case "course": models = createCourseArrayFromJson(json); break;
                case "section": models = createSectionArrayFromJson(json); break;
                case "term": models = createSectionArrayFromJson(json); break;
            }
            return models;
        }

        public static String[] createIDListFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals("")) return null;

            String[] idList = null;
            switch (modelType)
            {
                case "course": idList = createPrereqIDList(json); break;
            }
            return idList;
        }

        /* Model Array Creation Methods */

        private static Course[] createCourseArrayFromJson(String json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            List<Course> courseList = new List<Course>(); 
            foreach(var jsonCourse in jsonObject)
            {
                Course course = createCourse(jsonCourse);
                courseList.Add(course);
            }
            return courseList.ToArray();
        }

        private static Section[] createSectionArrayFromJson(String json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            List<Section> sectionList = new List<Section>();
            foreach (var jsonCourse in jsonObject)
            {
                Section section = createSection(jsonCourse);
                sectionList.Add(section);
            }
            return sectionList.ToArray();
        }

        private static Term[] createTermArrayFromJson(String json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            List<Term> termList = new List<Term>();
            foreach (var jsonCourse in jsonObject)
            {
                Term section = createTerm(jsonCourse);
                termList.Add(section);
            }
            return termList.ToArray();
        }

        /* ID Array Creation Methods */

        private static String[] createPrereqIDList(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            return contents.prereqs;
        }

        /* Model Creation Methods */

        private static Student createStudent(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            string firstName = contents.firstName;
            string lastName = contents.lastName;
            int id = contents.id;
            String username = contents.username;
            String email = contents.email;
            int yearLevel = contents.yearLevel;
            float gpa = contents.gpa;
            List<Section> enrolledSections = contents.enrolledSections;
            return new Student(id, username, email, firstName, lastName, yearLevel, gpa, enrolledSections);
        }

        private static Instructor createInstructor(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            string firstName = contents.firstName;
            string lastName = contents.lastName;
            int id = contents.id;
            String username = contents.username;
            String email = contents.email;
            return new Instructor(id, username, email, firstName, lastName);
        }

        private static Admin createAdmin(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            string firstName = contents.firstName;
            string lastName = contents.lastName;
            int id = contents.id;
            String username = contents.username;
            String email = contents.email;
            return new Admin(id, username, email, firstName, lastName);
        }

        private static Course createCourse(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);

            int id = contents.id;
            string courseCode = contents.courseCode;
            string name = contents.name;
            int credits = contents.credits;
            int minGPA = contents.minGPA;

            List<int> prereqs = new List<int>();
            foreach (var pre in contents.prereqs)
            {
                int test = pre;
                prereqs.Add(test);
            }
            return new Course(id, courseCode, name, credits, minGPA, prereqs.ToArray());
        }

        private static Section createSection(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            int maxStudent = contents.MAX_STUDENTS;
            string term = contents.TERM_ID;
            int instructor = contents.PROFESSOR_ID;
            int classroomID = contents.CLASSROOM_ID;
            int courseID = contents.COURSE_ID;
            int availability = contents.AVAILABILITY;
            // TODO: replace null with object instances once all methods are created
            return new Section(id, maxStudent, null, null, null, null, null);
        }

        private static Book createBook(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            int isbn = contents.ISBN;
            String title = contents.TITLE;
            return new Book(id, isbn, title);
        }

        private static Term createTerm(String json) // TODO: Verify that shady date logic
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = Convert.ToInt32(contents.ID);
            String termCode = contents.termCode;
            String start = contents.startDate;
            String end = contents.endDate;
            // SQL Date Format: YYYY-MM-DD
            DateTime startDate = Convert.ToDateTime(String.Format("{0}/{1}/{2} 00:00:00.00", start.Substring(5, 2), start.Substring(8, 2), start.Substring(0, 4)));
            DateTime endDate = Convert.ToDateTime(String.Format("{0}/{1}/{2} 00:00:00.00", end.Substring(5, 2), end.Substring(8, 2), end.Substring(0, 4)));
            return new Term(id, termCode, startDate, endDate);
        }

    }
}

