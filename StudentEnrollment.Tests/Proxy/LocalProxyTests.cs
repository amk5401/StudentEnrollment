using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Models;
using StudentEnrollment.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Proxy.Tests
{
    [TestClass()]
    public class LocalProxyTests
    {
        LocalProxy proxy;
        public LocalProxyTests()
        {
            this.proxy = new LocalProxy(new TestPathData());
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void LocalProxyTest()
        {
            Assert.IsNotNull(this.proxy);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getAdminLocalTest()
        {
            Admin admin = this.proxy.getAdmin(1);
            Assert.IsNotNull(admin);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getCourseLocalTest()
        {
            Course course = this.proxy.getCourse(1);
            Assert.IsNotNull(course);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getCourseListLocalTest()
        {
            Course[] courseList = this.proxy.getCourseList();
            Assert.IsNotNull(courseList);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getSectionLocalTest()
        {
            Section section = this.proxy.getSection(1);
            Assert.IsNotNull(section);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getLocationLocalTest()
        {
            Location loc = this.proxy.getLocation(1);
            Assert.IsNotNull(loc);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getCurrentTermLocalTest()
        {
            Term term = this.proxy.getCurrentTerm();
            Assert.IsNotNull(term);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getTermLocalTest()
        {
            Term term = this.proxy.getTerm("20161");
            Assert.IsNotNull(term);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getTermsLocalTest()
        {
            Term[] terms = this.proxy.getTerms();
            Assert.IsNotNull(terms);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getStudentLocalTest()
        {
            Student student = this.proxy.getStudent(1);
            Assert.IsNotNull(student);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getInstructorLocalTest()
        {
            Student student = this.proxy.getStudent(1);
            Assert.IsNotNull(student);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getBookLocalTest()
        {
            Book book = this.proxy.getBook(1);
            Assert.IsNotNull(book);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getCourseSectionsLocalTest()
        {
            Course course = proxy.getCourse(1);
            Section[] sections = this.proxy.getCourseSections(course);
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getStudentSectionsLocalTest()
        {
            Student student = proxy.getStudent(1);
            Section[] sections = this.proxy.getStudentSections(student);
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getInstructorSectionsLocalTest()
        {
            Instructor instructor = proxy.getInstructor(1);
            Section[] sections = this.proxy.getInstructorSections(instructor);
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getSectionStudentsLocalTest()
        {
            Section section = proxy.getSection(1);
            Student[] students = proxy.getSectionStudents(section);
            Assert.IsNotNull(students);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getCoursePrereqsLocalTest()
        {
            Course course = proxy.getCourse(1);
            Course[] prereqs = this.proxy.getCoursePrereqs(course);
            Assert.IsNotNull(prereqs);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void getSectionBooksLocalTest()
        {
            Section section = proxy.getSection(1);
            Book[] books = proxy.getSectionBooks(section);
            Assert.IsNotNull(books);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void createCourseLocalTest()
        {
            Course course = new Course(11, "TEST", "Unit Test Course", 1, 1, false);
            this.proxy.createCourse(course);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void createSectionLocalTest()
        {
            Section section = new Section(11, 1, 1, 1, 1, 1, false);
            this.proxy.createSection(section);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void createStudentLocalTest()
        {
            Student student = new Student(11, "username", "email", "firstName", "lastName", 1, 1.0f);
            this.proxy.createStudent(student, "testpassword");
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void createTermLocalTest()
        {
            Term term = new Term(11, "20203", new DateTime(2020, 1, 1), new DateTime(2020, 5, 1));
            this.proxy.createTerm(term);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void createBookLocalTest()
        {
            Book book = new Book(11, 11111111, "Test Book");
            this.proxy.createBook(book);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("LocalProxy")]
        public void enrollStudentLocalTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void toggleCourseLocalTest()
        {
            this.proxy.toggleCourse(1);
        }

        [TestMethod]
        [TestCategory("LocalProxy")]
        public void toggleSectionLocalTest()
        {
            this.proxy.toggleSection(1);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("LocalProxy")]
        public void waitlistStudentAPITest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("LocalProxy")]
        public void withdrawStudentLocalTest()
        {
            Assert.Fail();
        }
    }
}