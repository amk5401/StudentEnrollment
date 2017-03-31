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
                case "location": model = createLocation(json); break;
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
                case "section": idList = createSectionIDList(json); break;
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

        private static String[] createPrereqIDList(String json) // TODO: This is broke
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            return contents.prereqs;
        }

        private static String[] createSectionIDList(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            List<String> ids = new List<string>();
            foreach (var entry in contents)
            {
                ids.Add(entry.SECTION_ID.value);
            }
            return ids.ToArray();
        }

        /* Model Creation Methods */

        private static Student createStudent(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            string firstName = contents.FIRSTNAME;
            string lastName = contents.LASTNAME;
            String username = contents.USERNAME;
            String email = contents.EMAIL;
            int yearLevel = contents.YEAR_LEVEL;
            float gpa = contents.GPA;
            return new Student(id, username, email, firstName, lastName, yearLevel, gpa);
        }

        private static Instructor createInstructor(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            string firstName = contents.FIRSTNAME;
            string lastName = contents.LASTNAME;
            String username = contents.USERNAME;
            String email = contents.EMAIL;
            return new Instructor(id, username, email, firstName, lastName);
        }

        private static Admin createAdmin(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            string firstName = contents.FIRSTNAME;
            string lastName = contents.LASTNAME;
            String username = contents.USERNAME;
            String email = contents.EMAIL;
            return new Admin(id, username, email, firstName, lastName);
        }

        private static Course createCourse(String json)
        {
            dynamic contents = null;
            try
            {
                contents = JsonConvert.DeserializeObject(json);
            }
            catch(Exception e)
            {
                int i = 1;
            }
            

            int id = contents.ID;
            string courseCode = contents.COURSE_CODE;
            string name = contents.NAME;
            int credits = contents.CREDITS;
            int minGPA = contents.MIN_GPA;
            bool availability = contents.AVAILABILITY;
            return new Course(id, courseCode, name, credits, minGPA, availability);
        }

        private static Section createSection(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            int maxStudents = contents.MAX_STUDENTS;
            int termID = contents.TERM_ID;
            int instructorID = contents.PROFESSOR_ID;
            int classroomID = contents.CLASSROOM_ID;
            int courseID = contents.COURSE_ID;
            bool availability = contents.AVAILABILITY;

            return new Section(id, maxStudents, termID, instructorID, courseID, classroomID, availability);
        }

        private static Location createLocation(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            int capacity = contents.CAPACITY;
            int roomNumber = contents.ROOM_NUM;
            int buildingNumber = contents.BUILDING_NUM;

            return new Location(id, capacity, roomNumber, buildingNumber);
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
            int id = contents.ID;
            String termCode = contents.CODE;
            String start = contents.START_DATE;
            String end = contents.END_DATE;
            // SQL Date Format: YYYY-MM-DD
            DateTime startDate = Convert.ToDateTime(String.Format("{0}/{1}/{2} 00:00:00.00", start.Substring(5, 2), start.Substring(8, 2), start.Substring(0, 4)));
            DateTime endDate = Convert.ToDateTime(String.Format("{0}/{1}/{2} 00:00:00.00", end.Substring(5, 2), end.Substring(8, 2), end.Substring(0, 4)));

           // DateTime startDate = new DateTime(2017, 1, 1);
            //DateTime endDate = new DateTime(2017, 5, 20);
            return new Term(id, termCode, startDate, endDate);
        }

    }
}

