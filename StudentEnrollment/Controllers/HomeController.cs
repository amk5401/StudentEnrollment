using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentEnrollment.Proxy;

namespace StudentEnrollment.Controllers
{
    public class HomeController : Controller
    {
        public LocalProxy localProxy;
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            localProxy = new LocalProxy();

            ViewData["CourseList"] = localProxy.getCourseList();



            //Creates a new section, sends data to View
            localProxy.createSection(new Models.Section(11, 30, 0, 0, 2, 1, true));
            Models.Section newSection = localProxy.getSection(11);

            ViewData["newSection"] = newSection;
            ViewData["newSectionRoom"] = localProxy.getLocation(newSection.LocationID);
            ViewData["newSectionCourse"] = localProxy.getCourse(newSection.CourseID);
            ViewData["newSectionInstructor"] = localProxy.getInstructor(newSection.InstructorID);

            //Adds student 0 to new section
            Models.Student student = localProxy.getStudent(0);
            localProxy.enrollStudent(student, newSection);

            //Sends student 0 data to view
            Models.Section[] sections = localProxy.getStudentSections(student);
            List<Models.Location> locations = new List<Models.Location>();
            List<Models.Course> courses = new List<Models.Course>();
            List<Models.Instructor> instructors = new List<Models.Instructor>();

            for (int i = 0; i < sections.Length; i++)
            {
                Models.Location loc = localProxy.getLocation(sections[i].LocationID);
                locations.Add(loc);
                Models.Course c = localProxy.getCourse(sections[i].CourseID);
                courses.Add(c);
                Models.Instructor instructor = localProxy.getInstructor(sections[i].InstructorID);
                instructors.Add(instructor);
            }

            ViewData["Student"] = student;
            ViewData["StudentSections"] = sections;
            ViewData["SectionRooms"] = locations.ToArray();
            ViewData["SectionCourses"] = courses.ToArray();
            ViewData["SectionInstructors"] = instructors.ToArray();

            return View();
        }
    }
}
