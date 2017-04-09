using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Models;

namespace StudentEnrollment.Tests
{
    [TestClass]
    public class ModelFactoryUnitTests
    {
        [TestMethod]
        public void createStudentUnitTest()


        {
            String modelType = "student";
            String json = "{'ID': 0, 'USERNAME': 'bob32', 'EMAIL' : 'bob@email.com', 'FIRSTNAME': 'Bob', 'LASTNAME': 'Johnson', 'YEAR_LEVEL': 2,'GPA': 3.5}";
            Model model = ModelFactory.createModelFromJson(modelType, json);

            Student student = (Student)model;

            Assert.IsNotNull(student);
            Assert.AreEqual(student.FirstName, "Bob");
            Assert.AreEqual(student.LastName, "Johnson");
            Assert.AreEqual(student.Email, "bob@email.com");
            Assert.AreNotEqual(student.Email, "bob@test.com");
        }
        [TestMethod]
        public void createInstructorUnitTest()
        {
            String modelType = "instructor";
            String json = "{'ID' : 0, 'USERNAME' : 'Prof1', 'EMAIL' : 'prof@test', 'FIRSTNAME': 'Bill', 'LASTNAME' : 'Jones'}";
            Model model = ModelFactory.createModelFromJson(modelType, json);

            Instructor instructor = (Instructor)model;

            Assert.IsNotNull(instructor);
            Assert.AreEqual(instructor.FirstName, "Bill");
        }



        [TestMethod]
        public void createAdminUnitTest()
        {
            String modelType = "admin";
            String json = "{'ID' : 0, 'USERNAME' : 'Admin1' , 'EMAIL' : 'admin@test.com', 'FIRSTNAME' : 'Ad' , 'LASTNAME' : 'MIN'}";

            Model model = ModelFactory.createModelFromJson(modelType, json);

            Admin admin = (Admin)model;

            Assert.IsNotNull(admin);
        }
        [TestMethod]
        public void createCourseUnitTest()
        {
            String modelType = "course";
            String json = "{'ID': 0, 'COURSE_CODE' : 'SWEN-101' , 'NAME' : 'Intro', 'CREDITS' : 3, 'MIN_GPA' : 1, 'AVAILABILITY' : true, 'prereqs' : 'null'}";

            Model model = ModelFactory.createModelFromJson(modelType, json);

            Course course = (Course)model;
            Assert.IsNotNull(course);
        }

        [TestMethod]
        public void createSectionUnitTest()
        {

        }
        [TestMethod]
        public void createBookUnitTest()
        {

        }

        [TestMethod]
        public void createTermUnitTest()
        {

        }

        [TestMethod]
        public void createLocationUnitTest()
        {

        }
        [TestMethod]
        public void createSectionIDListUnitTest()
        {

        }

        [TestMethod]
        public void createStudentIDListUnitTest()
        {

        }

        [TestMethod]
        public void createPrereqIDListUnitTest()
        {

        }
        public void createTermArrayFromJsonUnitTest()
        {

        }

        [TestMethod]
        public void createSectionArrayFromJsonUnitTest()
        {

        }

        [TestMethod]
        public void createCourseArrayFromJsonUnitTest()
        {

        }
        public void createIDListFromJsonUnitTest()
        {

        }

        [TestMethod]
        public void createModelArrayFromJsonUnitTest()
        {

        }



    }
}
