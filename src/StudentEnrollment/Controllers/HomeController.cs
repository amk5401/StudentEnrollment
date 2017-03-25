using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

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
