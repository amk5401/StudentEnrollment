﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StudentEnrollment.Models
{
    public static class ModelFactory
    {
        public static Model createModelFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals("") || json == null || json.Equals("") || json.Equals("null")) return null;

            Model model = null;
            switch (modelType)
            {
                case "student": model = createStudent(json); break;
                case "instructor": model = createInstructor(json); break;
                case "admin": model = createAdmin(json); break;
                case "user": model = createUser(json); break;
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
            if (modelType == null || modelType.Equals("") || json == null || json.Equals("") || json.Equals("null")) return null;

            Model[] models = null;
            switch (modelType)
            {
                case "course": models = createCourseArrayFromJson(json); break;
                case "section": models = createSectionArrayFromJson(json); break;
                case "term": models = createTermArrayFromJson(json); break;
            }
            return models;
        }

        public static String[] createIDListFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals("") || json == null || json.Equals("") || json.Equals("null")) return null;

            String[] idList = null;
            switch (modelType)
            {
                case "course": idList = createPrereqIDList(json); break;
                case "section": idList = createSectionIDList(json); break;
                case "sectionInstructor": idList = createSectionInstructorIDList(json); break;
                case "student": idList = createStudentIDList(json); break;
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
                JObject jsonObj = jsonCourse;
                Course course = createCourse(jsonObj.ToString());
                courseList.Add(course);
            }
            return courseList.ToArray();
        }

        private static Section[] createSectionArrayFromJson(String json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            List<Section> sectionList = new List<Section>();
            foreach (var jsonSection in jsonObject)
            {
                JObject jsonObj = jsonSection;
                Section section = createSection(jsonObj.ToString());
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
                Term term = createTerm(jsonCourse.ToString());
                termList.Add(term);
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
                ids.Add(Convert.ToString(entry.SECTION_ID.Value));
            }
            return ids.ToArray();
        }

        private static String[] createSectionInstructorIDList(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            List<String> ids = new List<string>();
            foreach (var entry in contents)
            {
                ids.Add(Convert.ToString(entry.ID.Value));
            }
            return ids.ToArray();
        }

        private static String[] createStudentIDList(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            List<String> ids = new List<string>();
            foreach (var entry in contents)
            {
                ids.Add(Convert.ToString(entry.STUDENT_ID.Value));
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

        private static User createUser(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            string firstName = contents.FIRSTNAME;
            string lastName = contents.LASTNAME;
            String username = contents.USERNAME;
            String email = contents.EMAIL;
            String role = contents.ROLE;
            User user = new User(id, username, email, firstName, lastName);
            user.Role = role;
            return user;
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
            int buildingID = contents.BUILDING_ID;
            return new Location(id, capacity, roomNumber, buildingID);
        }

        private static Book createBook(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int isbn = Convert.ToInt32(contents.ISBN);
            String title = contents.TITLE;
            int publisherID = Convert.ToInt32(contents.PUBLISHER_ID);
            double price = Convert.ToInt32(contents.PRICE);
            String thumbnail = contents.THUMBNAIL_URL;
            bool avail = contents.AVAILABLE == "true" ? true : false;
            int count = Convert.ToInt32(contents.COUNT);
            return new Book(isbn, title, publisherID, thumbnail, price, avail, count);
    }

        private static Term createTerm(String json) // TODO: Verify that shady date logic
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.ID;
            String termCode = contents.CODE;
            String start = contents.START_DATE;
            String end = contents.END_DATE;
            // SQL Date Format: YYYY-MM-DD
            DateTime startDate = DateTime.Now; // TODO: I don't think this should be like this but it works for now
            DateTime endDate =  DateTime.Now.AddDays(1.0);
            try
            {
                startDate = Convert.ToDateTime(String.Format("{0}-{1}-{2} 00:00:00.000", start.Substring(5, 2), start.Substring(8, 2), start.Substring(0, 4)));
                endDate = Convert.ToDateTime(String.Format("{0}-{1}-{2} 00:00:00.000", end.Substring(5, 2), end.Substring(8, 2), end.Substring(0, 4)));
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error: Invalid Date Fields Parsed From Json. Date 1: " + start + ", Date 2: " + end);
            }
            return new Term(id, termCode, startDate, endDate);
        }

    }
}

