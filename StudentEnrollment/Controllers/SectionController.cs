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
       
        public ActionResult Create(int courseID, string message = null)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Section" });
            if (!checkPermission("professor") && !checkPermission("admin")) return RedirectToAction("AccessDenied", "Home");

            Course course = proxy.getCourse(courseID);
            if (course != null)
            {
                ViewData["role"] = Session["role"];
                ViewData["title"] = "Create a Section of " + course.Name;
                ViewData["courseID"] = courseID;
                if (checkPermission("professor"))
                {
                    ViewData["instructorID"] = ((Instructor)Session["user"]).ID;
                }
                if (message != null && message != "") ViewData["message"] = message;
                return View();
            }
            else return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        public ActionResult Create(int instructorID, int maxStudents, int courseID, string termCode, int classroomID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Section" });
            if (!checkPermission("professor") && !checkPermission("admin")) return RedirectToAction("AccessDenied", "Home");

            Course course = proxy.getCourse(courseID);
            Instructor instructor = proxy.getInstructor(instructorID);
            Term term = proxy.getTerm(termCode);
            Location location = proxy.getLocation(classroomID);

            bool success = true;
            if (course != null && instructor != null && term != null && location != null)
            {
                Section section = new Section(0, maxStudents, term.ID, instructorID, courseID, classroomID, true);
                int id = proxy.createSection(section);
                if (id == -1 || proxy.getSection(id) == null)
                {
                    success = false;
                }
            }
            else success = false;

            if (success) return RedirectToAction("SectionList", "Section", new { courseID = courseID });
            else return RedirectToAction("Create", new { courseID = courseID, message = "There was an error creating the section" });
        }

        [HttpGet]
        public ActionResult Detail(int SectionID)
        {

ViewData["Title"] = (proxy.getSection(SectionID).CourseID + " - Section " + SectionID);
            ViewData["Role"] = Session["role"];
            Section section = proxy.getSection(SectionID);
            Course c = proxy.getCourse(section.CourseID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);
            Student[] students = proxy.getSectionStudents(section);
            Student[] waitlist = proxy.getSectionWaitlist(section);
            int numStudents = students.Length;
            ViewData["Enrolled"] = numStudents;
            ViewData["Student"] = this.proxy.getStudent(1);
            User user = (User)Session["user"];

            int waitlistStudents = proxy.getSectionWaitlist(section).Length;
            if (checkPermission("student") || checkPermission("Student"))
            {
                
                Student student = this.proxy.getStudent(user.ID);
                ViewData["Student"] = student;

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
            ViewData["SectionID"] = section.ID;
            return View(section);
        }

        [HttpPost]
        public ActionResult EditSection(Section sectionModel)
        {
            if (ModelState.IsValid)
            {

                proxy.updateSection(sectionModel);//p.createCourse(model);
                return RedirectToAction("Detail", new { sectionID = sectionModel.ID });
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
        public ActionResult Enroll(int sectionID, int studentID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "List", redirectController = "Section" });
            if (checkPermission("student"))
            {
                Section section = this.proxy.getSection(sectionID);
                User user = (User)Session["user"];

                Student student = this.proxy.getStudent(user.ID);
                this.proxy.enrollStudent(student, section);
                return RedirectToAction("Detail", "Section", new { sectionID = section.ID });
            }
            if (checkPermission("admin"))
            {
                Section section = this.proxy.getSection(sectionID);

                Student student = this.proxy.getStudent(studentID);
                this.proxy.enrollStudent(student, section);
                return RedirectToAction("Detail", "Section", new { sectionID = section.ID });


            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }

        }

        [HttpPost]
        public ActionResult Waitlist(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "List", redirectController = "Section" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");
            Section section = this.proxy.getSection(sectionID);
            User user = (User)Session["user"];

            Student student = this.proxy.getStudent(user.ID);
           
            this.proxy.waitlistStudent(student, section);
            return RedirectToAction("SectionList", "Section", new { courseID = section.CourseID });
        }
        public ActionResult Withdraw(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "List", redirectController = "Section" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");
            Section section = this.proxy.getSection(sectionID);
            User user = (User)Session["user"];

            Student student = this.proxy.getStudent(user.ID);
            this.proxy.withdrawStudent(student, section);
            return RedirectToAction("SectionList", "Section", new { courseID = section.CourseID });
        }
    }
}
