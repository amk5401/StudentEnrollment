using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;
using Newtonsoft.Json;

namespace StudentEnrollment.Controllers
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
                case "course": model = createCourse(json); break;
                case "section": model = createSection(json); break;
                case "book": model = createBook(json); break;
            }
            return model;
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

        /* ID List Creation Methods */

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

        private static Course createCourse(String json)
        {
            dynamic contents = JsonConvert.DeserializeObject(json);
            int id = contents.id;
            string courseCode = contents.courseCode;
            string name = contents.name;
            int credits = contents.credits;
            int minGPA = contents.minGPA;
            int[] prereqs = contents.prereqs;
            return new Course(id, courseCode, name, credits, minGPA, prereqs);
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

    }
}

