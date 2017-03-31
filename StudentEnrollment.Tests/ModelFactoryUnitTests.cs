using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Models;

namespace StudentEnrollment.Tests
{
    [TestClass]
    public class ModelFactoryUnitTests
    {
        [TestMethod]
        public void createModelUnitTest()
        {
            String modelType = "student";
            String json = "{'ID': 0, 'USERNAME': 'bob32', 'EMAIL' : 'bob@email.com', 'FIRSTNAME': 'Bob', 'LASTNAME': 'Johnson', 'YEAR_LEVEL': 2,'GPA': 3.5}";
        Model model = ModelFactory.createModelFromJson(modelType, json);

            Student student = (Student)model;

            Assert.IsNotNull(student);

            Assert.AreEqual(student.FirstName, "Bob");
            Assert.AreEqual(student.LastName, "Johnson");
        }
        
    }
}
