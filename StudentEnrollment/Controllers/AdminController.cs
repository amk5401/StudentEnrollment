using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class AdminController : Controller
    {

        private APIProxy proxy = new APIProxy();


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
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        public ActionResult RegisterStudent()
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        public ActionResult CreateTerm()
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        public ActionResult CreateCourse()
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStudentSubmit(string username, string password, string fname, string lname, string email, int yearLevel, float gpa)
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            Student student = new Student(0, username, email, fname, lname, yearLevel, gpa);
            bool success = proxy.createStudent(student, password);
            string message = success ? "Student was successfully created!" : "There was an error creating the Student.";
            return RedirectToAction("FormResponse", "Admin", new { message });
        }

        [HttpPost]
        public ActionResult CreateTermSubmit(string code, string startDate, string endDate)
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return RedirectToAction("FormResponse", "Admin", new { message = "Term Creation Success" });
        }

        [HttpPost]
        public ActionResult CreateCourseSubmit(string courseCode, string name, string credits, string minGPA)
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            return RedirectToAction("FormResponse", "Admin", new { message = "Course Creation Success" });
        }

        public ActionResult FormResponse(string message)
        {
            if (!checkPermission()) return RedirectToAction("AccessDenied", "Home");
            ViewData["message"] = message;
            return View();
        }
    }
}
 
 