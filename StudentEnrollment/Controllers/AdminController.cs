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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterStudent()
        {
            return View();
        }

        public ActionResult CreateTerm()
        {
            return View();
        }

        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStudentSubmit(string username, string password, string fname, string lname, string email, int yearLevel, float gpa)
        {
            Student student = new Student(0, username, email, fname, lname, yearLevel, gpa);
            bool success = proxy.createStudent(student, password);
            string message = success ? "Student was successfully created!" : "There was an error creating the Student.";
            return RedirectToAction("FormResponse", "Admin", new { message });
        }

        [HttpPost]
        public ActionResult CreateTermSubmit(string code, string startDate, string endDate)
        {
            return RedirectToAction("FormResponse", "Admin", new { message = "Term Creation Success" });
        }

        [HttpPost]
        public ActionResult CreateCourseSubmit(string courseCode, string name, string credits, string minGPA)
        {
            return RedirectToAction("FormResponse", "Admin", new { message = "Course Creation Success" });
        }

        public ActionResult FormResponse(string message)
        {
            ViewData["message"] = message;
            return View();
        }
    }
}
 
 