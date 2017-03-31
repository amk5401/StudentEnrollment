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



        public void createAdminUnitTest()
        {

        }
        public void createCourseUnitTest()
        {

        }

        public void createSectionUnitTest()
        {

        }

        public void createBookUnitTest()
        {

        }

        public void createTermUnitTest()
        {

        }

        public void createLocationUnitTest()
        {

        }

        public void createSectionIDListUnitTest()
        {

        }
        public void createStudentIDListUnitTest()
        {

        }

        public void createPrereqIDListUnitTest()
        {

        }
        public void createTermArrayFromJsonUnitTest()
        {

        }

        public void createSectionArrayFromJsonUnitTest()
        {

        }

        public void createCourseArrayFromJsonUnitTest()
        {

        }
        public void createIDListFromJsonUnitTest()
        {

        }

        public void createModelArrayFromJsonUnitTest()
        {

        }



    }
}
