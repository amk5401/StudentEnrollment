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
            ViewData["Title"] = "Sections of " + (proxy.getCourse(courseID)).Name;
            return PartialView(proxy.getCourseSections(proxy.getCourse(courseID)));
        }

        [HttpGet]
        public ActionResult Create(int courseID)
        {
            Course course = proxy.getCourse(courseID);
            if (course != null) return View(course);
            else return RedirectToAction("Home", "Index");
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Section" });
            IProxy p = new APIProxy();
            ViewData["Title"] = "Sections of " + (p.getCourse(courseID)).Name;
            return PartialView(p.getCourseSections(p.getCourse(courseID)));
        }

        [HttpGet]
        public ActionResult Detail(int sectionID)
        {

            ViewData["Title"] = (proxy.getSection(sectionID).CourseID + " - Section " + sectionID);
            ViewData["Role"] = Session["role"];
            Section section = proxy.getSection(sectionID);
            Course c = proxy.getCourse(section.CourseID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);
            Student[] students = proxy.getSectionStudents(section);
            int numStudents = students.Length;
            ViewData["Enrolled"] = numStudents;

            int waitlistStudents = proxy.getSectionWaitlist(section).Length;
            if (checkPermission("student") || checkPermission("Student")) {
                User user = (User)Session["user"];
                Student student = this.proxy.getStudent(user.ID);
                
                
                    if (numStudents >= section.MaxStudents)
                    {
                        ViewData["Enroll"] = "Waitlist";
                    }
                    else
                    {
                        ViewData["Enroll"] = "Enroll";
                    }
                
                foreach (Student s in students)
                {
                    if (s.ID == student.ID)
                    {
                        ViewData["Enroll"] = "Already Enrolled";
                    }
                }
            }
            
             
                
            
            ViewData["Instructor"] = instructor;
            ViewData["Course"] = c;
            ViewData["CourseCode"] = c.CourseCode;
            ViewData["CourseName"] = c.Name;
            ViewData["Waitlist"] = waitlistStudents;
            return View(section);
        }

        [HttpPost]
        public ActionResult EditSection(Section model)
        {
            if (ModelState.IsValid)
            {
                proxy.updateSection(model);//p.createCourse(model);
                return RedirectToAction("Detail", new { sectionID = model.ID });
            }

            else
            {
                return RedirectToAction("SectionList");
            }
        }

        [HttpPost]
        public ActionResult SubmitSection(Section section)
        {
            proxy.createSection(section);
            Course course = proxy.getCourse(section.CourseID);
            if (course != null) return View(course);
            else return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        public ActionResult Enroll(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "List", redirectController = "Section" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");
            Section section = this.proxy.getSection(sectionID);
            User user = (User)Session["user"];

            Student student = this.proxy.getStudent(user.ID); // TODO: Figure this out
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