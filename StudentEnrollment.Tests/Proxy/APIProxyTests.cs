﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class APIProxyTests
    {
        APIProxy proxy;
        public APIProxyTests()
        {
            this.proxy = new APIProxy();
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void APIProxyTest()
        {
            Assert.IsNotNull(this.proxy);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getAdminAPITest()
        {
            Admin admin = this.proxy.getAdmin(1);
            Assert.IsNotNull(admin);
            Assert.AreEqual(admin.Email, "admin@email.com");
            Assert.AreEqual(admin.ID, 1);
            Assert.AreEqual(admin.FirstName, "Addison");
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getCourseAPITest()
        {
            Course course = this.proxy.getCourse(1);
            Assert.IsNotNull(course);
            Assert.AreEqual(course.CourseCode, "SWEN-344");
            Assert.AreEqual(course.Name, "Web Engineering");
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getCourseListAPITest()
        {
            Course[] courseList = this.proxy.getCourseList();
            Assert.IsNotNull(courseList);
            Assert.IsNotNull(courseList[0]);
            Assert.AreEqual(courseList[0].CourseCode, "SWEN-344");
            Assert.AreEqual(courseList[1].CourseCode, "SWEN-444");
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getSectionAPITest()
        {
            Section section = this.proxy.getSection(1);
            Assert.IsNotNull(section);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getLocationAPITest()
        {
            Location loc = this.proxy.getLocation(1);
            Assert.IsNotNull(loc);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getCurrentTermAPITest()
        {
            Term term = this.proxy.getCurrentTerm();
            Assert.IsNotNull(term);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getTermAPITest()
        {
            Term term = this.proxy.getTerm("20161");
            Assert.IsNotNull(term);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getTermsAPITest()
        {
            Term[] terms = this.proxy.getTerms();
            Assert.IsNotNull(terms);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getStudentAPITest()
        {
            Student student = this.proxy.getStudent(1);
            Assert.IsNotNull(student);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getInstructorAPITest()
        {
            Student student = this.proxy.getStudent(1);
            Assert.IsNotNull(student);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getBookAPITest()
        {
            Book book = this.proxy.getBook(1);
            Assert.IsNotNull(book);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getCourseSectionsAPITest()
        {
            Course course = proxy.getCourse(1);
            Section[] sections = this.proxy.getCourseSections(course);
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getStudentSectionsAPITest()
        {
            Student student = proxy.getStudent(1);
            Section[] sections = this.proxy.getStudentSections(student);
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getInstructorSectionsAPITest()
        {
            Instructor instructor = proxy.getInstructor(1);
            Section[] sections = this.proxy.getInstructorSections(instructor);
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getSectionStudentsAPITest()
        {
            Section section = proxy.getSection(1);
            Student[] students = proxy.getSectionStudents(section);
            Assert.IsNotNull(students);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getCoursePrereqsAPITest()
        {
            Course course = proxy.getCourse(1);
            Course[] prereqs = this.proxy.getCoursePrereqs(course);
            Assert.IsNotNull(prereqs);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void getSectionBooksAPITest()
        {
            Section section = proxy.getSection(1);
            Book[] books = proxy.getSectionBooks(section);
            Assert.IsNotNull(books);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void createCourseAPITest()
        {
            Course course = new Course(11, "TEST", "Unit Test Course", 1, 1, false);
            this.proxy.createCourse(course);
            Assert.IsNotNull(this.proxy.getCourse(11));
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void createSectionAPITest()
        {
            Section section = new Section(11, 1, 1, 1, 1, 1, false);
            this.proxy.createSection(section);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void createStudentAPITest()
        {
            Student student = new Student(11, "username", "email", "firstName", "lastName", 1, 1.0f);
            this.proxy.createStudent(student);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void createTermAPITest()
        {
            Term term = new Term(11, "20203", new DateTime(2020, 1, 1), new DateTime(2020, 5, 1));
            this.proxy.createTerm(term);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void createBookAPITest()
        {
            Book book = new Book(11, 11111111, "Test Book");
            this.proxy.createBook(book);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("APIProxy")]
        public void enrollStudentAPITest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void toggleCourseAPITest()
        {
            this.proxy.toggleCourse(1);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void toggleSectionAPITest()
        {
            this.proxy.toggleSection(1);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void updateCourseAPITest()
        {
            Course course = this.proxy.getCourse(1);
            Assert.IsNotNull(course);
            String testCode = "TEST-1234";
            String testName = "Test Update";
            int testCredits = 666;
            int testGPA = 666;
            bool testAvail = false;
            Course updateCourse = new Course(course.ID, testCode, testName, testCredits, testGPA, testAvail);
            this.proxy.updateCourse(updateCourse);
            updateCourse = this.proxy.getCourse(1);
            Assert.AreEqual(updateCourse.ID, 1);
            Assert.AreEqual(updateCourse.CourseCode, testCode);
            Assert.AreEqual(updateCourse.Name, testName);
            Assert.AreEqual(updateCourse.Credits, testCredits);
            Assert.AreEqual(updateCourse.MinGPA, testGPA);
            Assert.AreEqual(updateCourse.Availability, testAvail);
            this.proxy.updateCourse(course);
            updateCourse = this.proxy.getCourse(1);
            Assert.AreEqual(course, updateCourse);
        }

        [TestMethod]
        [TestCategory("APIProxy")]
        public void updateSectionAPITest()
        {
            Section section = this.proxy.getSection(1);
            Assert.IsNotNull(section);
            int testMaxStudents = 666;
            int testTermID = 666;
            int testInstructorID = 666;
            int testCourseID = 666;
            int testLocationID = 666;
            bool testAvail = false;
            Section updateSection = new Section(section.ID, testMaxStudents, testTermID, testInstructorID, testCourseID, testLocationID, testAvail);
            this.proxy.updateSection(updateSection);
            updateSection = this.proxy.getSection(1);
            Assert.AreEqual(updateSection.ID, 1);
            Assert.AreEqual(updateSection.MaxStudents, testMaxStudents);
            Assert.AreEqual(updateSection.TermID, testTermID);
            Assert.AreEqual(updateSection.InstructorID, testInstructorID);
            Assert.AreEqual(updateSection.CourseID, testCourseID);
            Assert.AreEqual(updateSection.LocationID, testLocationID);
            Assert.AreEqual(updateSection.Availability, testAvail);
            this.proxy.updateSection(section);
            updateSection = this.proxy.getSection(1);
            Assert.AreEqual(section, updateSection);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("APIProxy")]
        public void waitlistStudentAPITest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("APIProxy")]
        public void withdrawStudentAPITest()
        {
            Assert.Fail();
        }
    }
}