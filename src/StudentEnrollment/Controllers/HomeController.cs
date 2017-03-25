using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Models.Instructor i1 = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            Models.Instructor i2 = new StudentEnrollment.Models.Instructor(4, "gj888", "gj888@rit.edu", "Bob", "Smith");

            List<StudentEnrollment.Models.Instructor> list = new List<Models.Instructor> { i1, i2};
            ViewData["Instructor"] = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            ViewData["InstructorList"] = list;

            Stream file = System.IO.File.OpenRead(@"wwwroot\proxyFunctions.json");
            Dictionary<string, string> functions = new Dictionary<string, string>();
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                functions = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            ViewData["Functions"] = functions;
            return View();
        }

        public ActionResult TestApi(string function, string parameters)
        {
            Models.Instructor i1 = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            Models.Instructor i2 = new StudentEnrollment.Models.Instructor(4, "gj888", "gj888@rit.edu", "Bob", "Smith");

            ViewData["Function"] = function;
            ViewData["Parameters"] = parameters;

            Proxy p = new LocalProxy();
            dynamic result = "The function returned void.";
            if (function != null && parameters != null)
            {
                try
                {
                    switch (function)
                    {
                        case "getStudent": result = p.getStudent(Int32.Parse(parameters)); break;
                        case "getInstructor": result = p.getInstructor(Int32.Parse(parameters)); break;
                        case "getAdmin": result = p.getAdmin(Int32.Parse(parameters)); break;
                        case "getCourse": result = p.getStudent(Int32.Parse(parameters)); break;
                        case "getCourseList": result = p.getCourseList(); break;
                        case "getSection": result = p.getSection(Int32.Parse(parameters)); break;
                        case "getCourseSections": result = p.getCourseSections((Course)Convert.ChangeType(parameters, typeof(Course))); break;
                        case "deleteSection": p.deleteSection((Section)Convert.ChangeType(parameters, typeof(Section))); break;
                        case "getStudentSections": result = p.getStudentSections((Student)Convert.ChangeType(parameters, typeof(Student))); break;
                        case "getSectionStudents": result = p.getSectionStudents((Section)Convert.ChangeType(parameters, typeof(Section))); break;
                        case "getInstructorSections": result = p.getInstructorSections((Instructor)Convert.ChangeType(parameters, typeof(Instructor))); break;
                        case "getBook": result = p.getBook(Int32.Parse(parameters)); break;
                        case "getSectionBooks": result = p.getSectionBooks((Section)Convert.ChangeType(parameters, typeof(Section))); break;
                        case "getLocation": result = p.getLocation(Int32.Parse(parameters)); break;
                        case "getCurrentTerm": result = p.getCurrentTerm(); break;
                        case "getTerm": result = p.getTerm(Int32.Parse(parameters)); break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Function: " + function + " Parameter: " + parameters + " " + ex);
                }
            }
            else
            {
                result = "One or more of the inputs was not defined.";
            }
            ViewData["Result"] = result;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Student Enrollment System for SWEN-344";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
