using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Models;
using StudentEnrollment.Controllers;
using System.Collections.Generic;

namespace StudentEnrollmentTests
{
    [TestClass]
    public class LocalProxyUnitTests
    {

        LocalProxy proxy = new LocalProxy();
        [TestMethod]
        public void createBookUnitTest()

        {
            Book book1 = new Book(1, 12345, "Web Engineering");
            Book book2 = new Book(2, 123456, "Beers of the World");
            proxy.createBook(book1);
            Assert.AreEqual(book1, proxy.books[0]);
            Assert.AreNotEqual(book2, proxy.books[0]);

            //create object, post to database, try getting it back
            

        }

        public void createCourseUnitTest()
        {

        }

        public void createSectionUnitTest()
        {

        }

        public void createStudentUnitTest()
        {

        }

        public void createTermUnitTest()
        {

        }

        public void deleteSectionUnitTest()
        {

        }

        public void enrollStudentUnitTest()
        {

        }

        public void getAdminUnitTest()
        {

        }

        public void getBookUnitTest()
        {

        }

        public void getCourseUnitTest()
        {

        }

        public void getCourseListUnitTest()
        {

        }

        public void toggleCourseUnitTest()
        {

        }

        public void getSectionUnitTest()
        {

        }

        public void getCourseSectionsUnitTest()
        {

        }

        public void getStudentSectionsUnitTest()
        {

        }

        public void getInstructorSectionsUnitTest()
        {

        }

        public void getSectionBooksUnitTest()
        {

        }

        public void getLocationUnitTest()
        {

        }

        public void getTermUnitTest()
        {

        }

        public void getCurrentTermUnitTest()
        {

        }

        public void waitlistStudentUnitTest()
        {

        }

        public void withdrawStudentUnitTest()
        {

        }


    }
}