using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;
using System.Globalization;

namespace StudentEnrollment.Controllers
{
    public class AdminController : Controller
    {

        private APIProxy proxy = new APIProxy();

        private bool loggedIn()
        {
            if (Session["user"] == null) return false; else return true;
        }

        private bool checkPermission()
        {
            if (Session["user"] != null && Session["role"] != null && String.Equals("admin", (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        // GET: Admin
        public ActionResult Index()
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        public ActionResult RegisterStudent()
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        public ActionResult CreateTerm()
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        public ActionResult CreateCourse()
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStudentSubmit(string username, string password, string fname, string lname, string email, int yearLevel, float gpa)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            Student student = new Student(0, username, email, fname, lname, yearLevel, gpa);
            bool success = proxy.createStudent(student, password);
            string message = success ? "Student was successfully created!" : "There was an error creating the Student.";
            return RedirectToAction("FormResponse", "Admin", new { message });
        }

        [HttpPost]
        public ActionResult CreateTermSubmit(string code, string startDate, string endDate)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            DateTime start = DateTime.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); 
            DateTime end = DateTime.ParseExact(endDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            Term term = new Term(0, code, start, end);
            proxy.createTerm(term);
            return RedirectToAction("FormResponse", "Admin", new { message = "Term Creation Success" });
        }

        [HttpPost]
        public ActionResult CreateCourseSubmit(string courseCode, string name, string credits, string minGPA)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            Course course = new Course(0, courseCode, name, Convert.ToInt32(credits), Convert.ToInt32(minGPA), true);
            proxy.createCourse(course);
            return RedirectToAction("FormResponse", "Admin", new { message = "Course Creation Success" });
        }

        public ActionResult FormResponse(string message)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            ViewData["message"] = message;
            return View();
        }

        [HttpGet] // this action result returns the partial containing the modal
        public ActionResult CreateBook()
        {
            var book = new Book();
            return PartialView("_CreateBook", book);
        }

        [HttpPost]
        public ActionResult CreateBook(Book model)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Admin" });
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            //proxy.createBook(model);
            return RedirectToAction("FormResponse", "Admin", new { message = "Book Creation Success" });
        }
    }
}
 
 