using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;
using System.Web.Mvc;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentEnrollment.Controllers
{
    public class SectionController : Controller
    {
        private APIProxy proxy = new APIProxy();

        private bool loggedIn()
        {
            if (Session["user"] == null) return false; else return true;
        }

        private bool checkPermission(string role)
        {
            if (Session["user"] != null && Session["role"] != null && String.Equals(role, (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult SectionList(int courseID)
        {
            IProxy p = new APIProxy();
            ViewData["Title"] = "Sections of " + (p.getCourse(courseID)).Name;
            return PartialView(p.getCourseSections(p.getCourse(courseID)));
        }

        [HttpGet]
        public ActionResult Detail(int sectionID)
        {

            ViewData["Title"] = (proxy.getSection(sectionID).CourseID + " - Section " + sectionID);

            Section section = proxy.getSection(sectionID);
            Course c = proxy.getCourse(section.CourseID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);

            int numStudents = proxy.getSectionStudents(section).Length;
            int waitlistStudents = proxy.getSectionWaitlist(section).Length;

            ViewData["Enrolled"] = numStudents;

            if (numStudents >= section.MaxStudents)
            {
                ViewData["Enroll"] = "Waitlist";
            }
            else
            {
                ViewData["Enroll"] = "Enroll";
            }
            ViewData["Instructor"] = instructor;
            ViewData["Course"] = c;
            ViewData["CourseCode"] = c.CourseCode;
            ViewData["CourseName"] = c.Name;
            ViewData["Waitlist"] = waitlistStudents;
            return View(section);
        }

        [HttpPost]
        public ActionResult Enroll(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "List", redirectController = "Section" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");
            Section section = this.proxy.getSection(sectionID);
            Student student = this.proxy.getStudent(4); // TODO: Figure this out
            this.proxy.enrollStudent(student, section);
            return RedirectToAction("SectionList", "Section", new { courseID = section.CourseID });
        }

        [HttpPost]
        public ActionResult Waitlist(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "List", redirectController = "Section" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");
            Section section = this.proxy.getSection(sectionID);
            Student student = this.proxy.getStudent(4); // TODO: Figure this out
            this.proxy.waitlistStudent(student, section);
            return RedirectToAction("SectionList", "Section", new { courseID = section.CourseID });
        }
    }
}