using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using StudentEnrollment.Models;

namespace StudentEnrollmentTests
{
    [TestClass]
    public class ModelFactoryUnitTests
    {

        [TestMethod]
        public void createModelUnitTest() {

            String modelType = "student";
            String json = "{ 'firstName' : 'Test', 'lastName' : 'Test', 'id' : 1, 'username' : 'testtest', 'email' : 'email@test.com', 'yearLevel' : 2, 'gpa' : 3, 'EnrolledSections' : 'null' }";
            Model model = ModelFactory.createModelFromJson(modelType, json);

            Student student = (Student)model;
            Console.Write(model);
            Assert.IsNotNull(model);

            Assert.AreEqual(student.FirstName, "Test");
            Assert.AreEqual(student.LastName, "Test");
            Assert.AreEqual(student.Email, "email@test.com");
            Assert.AreNotEqual(student.Email, "example@test.com");
            //making sure that all properties are defiend
        }

        
    }
}
